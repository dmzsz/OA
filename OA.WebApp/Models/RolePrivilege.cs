using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OA.WebApp.Models
{
    // 角色和权限的多对多对应表。
    // 中间有一个字段用于标识，
    // 该角色对于某个资源权限是公司级别还是部门级别（个人，超级管理员（all））
    // RoleID、PrivilegeID、Scope三者为联合主键
    [Table("st_role_privilege")]
    public class RolePrivilege : BaseEntity
    {
        [Key, Column(Order = 0)]
        public int ID { get; set; }

        [Column(Order = 1)]
        public int RoleID { get; set; }

        [Column(Order = 2)]
        public int PrivilegeID { get; set; }

        // 权限唯一标识，md5
        public string UniquePriv { get; set; }

        // 公司（C） 部门(D)  个人（U） 所有（A） 默认值 （Null）空
        // 这里的公司 部门 个人 使用一个范围， 不特指某个公司、部门、个人
        // 某一个权限设置为公司，就某某资源的index页面来讲，
        // 当前用户可以使用这个权限查看，他所在所有公司下的某某资源的所有信息（一次只显示一个公司的）
        // 设置为所有(A)可以将Privilege指定的Property和Action所有的权限全部打开
        // 设置为空时，没有任何的权限
        [Display(Name = "使用范围")]
        public string Scope { get; set; }

        //导航属性
        public virtual Role Role { get; set; }

        public virtual Privilege Privilege { get; set; }
    }
}