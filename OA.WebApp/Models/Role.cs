using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OA.WebApp.Models
{
    [Table("st_role")]
    public class Role : BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Display(Name = "角色名")]
        public string Name { get; set; }

        [Display(Name = "描述")]
        public string Description { get; set; }

        //导航属性
        public ICollection<UserRole> UserRoles { get; set; }

        public ICollection<RolePrivilege> RolePrivileges { get; set; }

        //[Display(Name = "用户")]
        //public virtual ICollection<UserRole> Users { get; set; }

        //[Display(Name = "权限")]
        //public virtual ICollection<Privilege> Privileges { get; set; }
    }
}