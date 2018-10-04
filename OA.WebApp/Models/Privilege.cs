using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OA.WebApp.Models
{
    // 将所有的资源和控制器组合，便于将角色和使用范围进行绑定
    [Table("st_privilege")]
    public class Privilege : BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Display(Name = "权限名")]
        public string Name { get; set; }

        [Display(Name = "描述")]
        public string Description { get; set; }

        [Display(Name = "控制器分组")]
        public string Namespace { get; set; }

        [Display(Name = "控制器名")]
        public string ControllerName { get; set; }

        [Display(Name = "动作名")]
        public string ActionName { get; set; }

        [Display(Name = "是否已分配")]
        public bool IsUsed { get; set; }

        // 开启前端的scope下拉选项，
        // 为true的时候还需要将RolePrivilege的Scope设置为U（User）
        public bool ScopeEnable { get; set; }
        

        public ICollection<RolePrivilege> RolePrivileges { get; set; }

        //导航属性
        //[Display(Name = "角色")]
        //public virtual ICollection<Role> Roles { get; set; }

    }
}