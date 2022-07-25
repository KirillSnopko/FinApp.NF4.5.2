using FinApp.Entities.Identity.Account;
using System;
using System.ComponentModel.DataAnnotations;

namespace FinApp.Entities.Finance
{
    public class FinanceOperation
    {
        public int id { get; set; }
        public string idUser { get; set; }
        public int idDepository { get; set; }
        public double amountOfMoney { get; set; }
        public string comment { get; set; }
        public DateTime created { get; set; }
        public bool isSpending { get; set; }
    }
}