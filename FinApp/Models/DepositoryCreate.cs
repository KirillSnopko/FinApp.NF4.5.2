using FinApp.Entities.Finance;
using System.ComponentModel.DataAnnotations;


namespace FinApp.Models
{
    public class DepositoryCreate
    {
        [Display(Name = "Name")]
        [Required]
        public string name { get; set; }

        [Display(Name = "Type")]
        [Required]
        [EnumDataType(typeof(TypeDep))]
        public TypeDep typeDep { get; set; }

        [Display(Name = "Currency")]
        [Required]
        [EnumDataType(typeof(TypeMoney))]
        public TypeMoney typeMoney { get; set; }

        [Display(Name = "Amount of money")]
        public int? amount { get; set; }
    }
}