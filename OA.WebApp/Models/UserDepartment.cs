using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OA.WebApp.Models
{
    [Table("st_user_department")]
    public class UserDepartment : BaseEntity
    {
        [Key, Column(Order = 0)]
        public int UserID { get; set; }

        [Key, Column(Order = 1)]
        public int DepartmentID { get; set; }

        //导航属性
        public virtual User User { get; set; }
        
        public virtual Department Department { get; set; }
    }
}
