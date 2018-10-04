using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OA.WebApp.Models
{
    [Table("st_user_role")]
    public class UserRole : BaseEntity
    {
        [Key, Column(Order = 0)]
        public int UserID { get; set; }

        [Key, Column(Order = 1)]
        public int RoleID { get; set; }

        
        //导航属性
        public virtual User User { get; set; }

        public virtual Role Role { get; set; }
    }
}
