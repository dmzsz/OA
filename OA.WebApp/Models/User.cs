using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OA.WebApp.Models
{
    [Table("st_user")]
    public class User : BaseEntity
    {
        [Key]
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        [Required, StringLength(100), Display(Name = "姓名")]
        public string UserName { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        //导航属性
        //[Display(Name = "角色")]
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

        public virtual ICollection<UserCompany> UserCompanies { get; set; } = new List<UserCompany>();

        public virtual ICollection<UserDepartment> UserDepartments { get; set; } = new List<UserDepartment>();

        [Display(Name = "权限")]
        public virtual ICollection<UserPrivilege> UserPrivileges { get; set; } = new List<UserPrivilege>();

        [Display(Name = "职工")]
        public virtual Employee Employee { get; set; }

    }
}
