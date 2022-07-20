using FinApp.Entities.Identity.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinApp.Entities.Finance
{
    public class Depository
    {
        public int id { get; set; }

        [Required]
        public string name { get; set; }

        public UserApp user { get; set; }

        [Required]
        [EnumDataType(typeof(TypeDep))]
        public TypeDep typeDep { get; set; }

        [Required]
        [EnumDataType(typeof(TypeMoney))]
        public TypeMoney typeMoney { get; set; }

        public int amount { get; set; }

    }

    public enum TypeDep
    {
        DEPOSIT, CARD, CASH
    }

    public enum TypeMoney
    {
        USD, RUB, EUR, BYN
    }

}



