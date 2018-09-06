using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace OA.WebApp.Authorization
{
    public interface IDocument
    {
        string Creator { get; set; }
    }
    
    public class DocumentAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, IDocument>
    {
        private IHttpContextAccessor _httpContextAccessor = null;

        public DocumentAuthorizationHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
            OperationAuthorizationRequirement requirement, 
            IDocument resource)
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;

            // 如果是Admin角色就直接授权成功
            if (context.User.IsInRole("admin"))
            {
                context.Succeed(requirement);
            }
            else
            {
                // 允许任何人创建或读取资源
                if (requirement == Operations.Create || requirement == Operations.Read)
                {
                    context.Succeed(requirement);
                }
                else
                {
                    // 只有资源的创建者才可以修改和删除
                    if (context.User.Identity.Name == resource.Creator)
                    {
                        context.Succeed(requirement);
                    }
                    else
                    {
                        context.Fail();
                    }
                }
            }
            return Task.CompletedTask;
        }
    }
}
