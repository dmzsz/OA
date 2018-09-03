using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OA.WebApp.Models
{
    [Table("st_company")]
    public class Company : BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; internal set; }
        public string Name { get; set; }

        public int ParentCompanyID { get; set; }

        //导航属性
        public virtual ICollection<UserCompany> UserCompanies { get; set; }

        //[Display(Name = "父公司")]
        //public virtual Company ParentCompany { get; set; }

        //[Display(Name = "子公司")]
        ////[InverseProperty("ParentCompany")]
        //public virtual ICollection<Company> ChildrenCompanies { get; set; }
    }
}