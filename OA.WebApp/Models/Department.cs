using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OA.WebApp.Models
{
    [Table("st_department")]
    public class Department : BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; internal set; }
        public string Name { get; set; }

        public int ParentID { get; set; }

        //导航属性
        [Display(Name = "用户")]
        public virtual ICollection<User> Users { get; set; }

        [Display(Name = "父部门")]
        public virtual Department ParentDepartment { get; set; }

        [Display(Name = "子部门")]
        public virtual ICollection<Department> ChildrenDepartments { get; set; }
    }
}