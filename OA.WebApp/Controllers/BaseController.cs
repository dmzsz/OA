using Microsoft.AspNetCore.Mvc;
using OA.WebApp.Filter;

namespace OA.WebApp.Controllers
{
    [ServiceFilter(typeof(SidebarActionFilter))]
    public class BaseController : Controller
    {
        
    }
}
