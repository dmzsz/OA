namespace OA.WebApp.ViewModels
{
    public class UserDto
    {
        public int ID { get; set; }

        // 用户名
        public string UserName { get; set; }
        
        // 密码
        public string Password { get; set; }

        // 确认密码
        public string RePassword { get; set; }
        
        // 邮箱
        public string Email { get; set; }

        // 图片验证码
        public string ImageCode { get; set; }
        
        // 图片验证的图片位置
        public string ImagePosition { get; set; }

        // 短信验证码
        public string SmsCode { get; set; }

        // email验证码
        public string EmailCode { get; set; }
        public string ReturnUrl { get; set; }
    }
}
