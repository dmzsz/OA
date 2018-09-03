using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OA.Core.Tools;

namespace OA.WebApp.Models
{
    // 将所有的资源和控制器组合，便于将角色和使用范围进行绑定
    [Table("st_privilege")]
    public class Privilege : BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; internal set; }

        [Display(Name = "权限名")]
        public string Name { get; set; }

        [Display(Name = "描述")]
        public string Description { get; set; }

        [Display(Name = "模型名")]
        public string ModelName { get; set; }

        // 数据库字段名字 或者叫 modal属性名
        [Display(Name = "属性名")]
        public string PropertyName { get; set; }

        [Display(Name = "控制器名")]
        public string ControllerName { get; set; }

        [Display(Name = "动作名")]
        public string ActionName { get; set; }

        // 开启前端的scope下拉选项，
        // 为true的时候还需要将RolePrivilege的Scope设置为U（User）
        public bool ScopeEnable { get; set; }
        
        // 由于方便用户访问资源时，它和用户访问的资源进行对比，成功后即可访问。
        // 然后再使用scope带入进数据查询语句where中。
        [Display(Name = "唯一标识")]
        public string Identity
        {
            get { return Identity; }
            set {
                Identity = MD5Generate.Md5String(
                    ControllerName + ActionName + ModelName + PropertyName
                    );
            }
        }

        public ICollection<RolePrivilege> RolePrivileges { get; internal set; }

        //导航属性
        //[Display(Name = "角色")]
        //public virtual ICollection<Role> Roles { get; set; }

    }
}