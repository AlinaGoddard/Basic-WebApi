using System;

namespace ApiModels
{
    public class Transaction
    {
        public float Amount { get; set; }

        public DateTime Date { get; set; }

        public Transaction(float amount, DateTime date)
        {
            Amount = amount;
            Date = date;
        }
    }
}