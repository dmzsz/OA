using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OA.Core.Tools;

namespace OA.WebApp.Models
{
    // 将RolePrivilege和Privilege的记录在save之后，整合在这张表中。
    // 这是类存在的目的是为了加快用户资源认证的需求。
    // 当用户访问某个Action的时候，查询这个表确认权限（返回大于1条的记录，即有权限访问）。
    [Table("st_user_privilege")]
    public class UserPrivilege : BaseEntity
    {
        [Key, Column(Order = 0)]
        public int UserID { get; set; }

        [Display(Name = "模型名")]
        public string ModelName { get; set; }

        // 数据库字段名字 或者叫 modal属性名
        [Display(Name = "属性名")]
        public string PropertyName { get; set; }

        [Display(Name = "控制器名")]
        public string ControllerName { get; set; }

        [Display(Name = "动作名")]
        public string ActionName { get; set; }

        // 开启前端的scope下拉选项
        public string Scope { get; set; }

        //导航属性
        public virtual User User { get; set; }
    }
}