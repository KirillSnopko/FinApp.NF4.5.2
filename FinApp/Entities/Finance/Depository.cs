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
        [Display(Name = "id")]
        public int id { get; set; }

        [Display(Name = "Name")]
        [Required]
        public string name { get; set; }
        public string UserId { get; set; }
        public UserApp user { get; set; }

        [Display(Name = "Type")]
        [Required]
        [EnumDataType(typeof(TypeDep))]
        public TypeDep typeDep { get; set; }

        [Display(Name = "Currency")]
        [Required]
        [EnumDataType(typeof(TypeMoney))]
        public TypeMoney typeMoney { get; set; }

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



