using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using RhodeIT.Databases;
using RhodeIT.Models;

namespace RhodeIT.Droid.Helpers
{
    public class StudentDataPreloader
    {
        private Context context;

        public StudentDataPreloader(Context ctx)
        {
            context = ctx;
            Proload();
        }

        private void Proload()
        {
            RhodesDataBase db = new RhodesDataBase();
            List<Student> students = new List<Student>();
            using (var input = context.Assets.Open("Students.json"))
            using (StreamReader sr = new StreamReader(input))
            {
               dynamic stuff = JsonConvert.DeserializeObject(sr.ReadLine());
                students.Add(new Student { StudentNo = stuff.StudentNumber, Password = stuff.Password });
            }
            db.StoreStudents(students);

        }
    }
}