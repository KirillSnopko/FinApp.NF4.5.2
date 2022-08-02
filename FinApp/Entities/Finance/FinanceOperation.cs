using System;

namespace FinApp.Entities.Finance
{
    public class FinanceOperation
    {
        public int id { get; set; }
        public string idUser { get; set; }
        public int idDepository { get; set; }
        public double amountOfMoney { get; set; }
        public Category category { get; set; }
        public string comment { get; set; }
        public DateTime created { get; set; }
        public bool isSpending { get; set; }
    }

    public enum Category
    {
        Home, Repair, Supermarkets, Pharmacy, Entertainment, Transport, Clothing, Electronics, Others, Addition
    }
}