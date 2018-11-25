using System;

namespace ApiModels
{
    public class Account
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Number { get; set; }

        public float Amount { get; set; }

        public Account()
        {
        }

        public Account(Guid id)
        {
            Id = id;
        }
    }
}