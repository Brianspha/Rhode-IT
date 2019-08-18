using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using Newtonsoft.Json;
using RhodeIT.Databases;
using RhodeIT.Models;
using UIKit;

namespace RhodeIT.iOS.Helpers
{
    public class StudentDataPreloader
    {
        public void StoreStudents()
        {
            List<Student> students = new List<Student>();
            RhodesDataBase db = new RhodesDataBase();
            var text = System.IO.File.OpenText("Files/RhodesMap.geojson");
            string line = text.ReadLine();
            while (true)
            {
                if (string.IsNullOrEmpty(line)) break;
                dynamic stuff = JsonConvert.DeserializeObject(line);
                students.Add(new Student { StudentNo = stuff.StudentNumber, Password = stuff.Password });
                line = text.ReadLine();
            }
            db.StoreStudents(students); 
        }
    }
}