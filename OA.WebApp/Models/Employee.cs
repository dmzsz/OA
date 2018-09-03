using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OA.WebApp.Models
{
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

        //导航属性
        [Display(Name = "系统用户")]
        public virtual ICollection<User> Users { get; set; }
    }
}
