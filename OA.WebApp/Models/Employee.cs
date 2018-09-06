using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OA.WebApp.Models
{
    [Table("st_employee")]
    public class Employee
    {
        [Key]
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Display(Name = "年龄")]
        public int Age { get; set; }

        public string Sex { get; internal set; }

        public int UserID { get; set; }

        //导航属性
        [Display(Name = "系统用户")]
        public virtual User User { get; set; }
    }
}
