using RhodeIT.Databases;
using RhodeIT.Models;
using System.Collections.Generic;
using System.ComponentModel;

namespace RhodeIT.ViewModels
{
    public class TransactionHistoryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private List<TransactionReceipt> receipts;
        public List<TransactionReceipt> Receipts
        {
            get => receipts;
            private set
            {
                if (value != receipts)
                {
                    receipts = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(receipts)));
                }
            }
        }

        public TransactionHistoryViewModel()
        {
            Setup();
        }

        private void Setup()
        {
            RhodeITDB db = new RhodeITDB();
            // Receipts = db.GetTransactionReceipts();
            Receipts = new List<TransactionReceipt>();
        }

        /// <summary>
        /// Invoked when a property is assigned a new value
        /// </summary>
        /// <param name="e"> property thats changed</param>
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

    }
}
