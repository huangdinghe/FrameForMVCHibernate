using System.Linq;
using System.Web.Mvc;

namespace MvcApp.Controllers
{
    public class UcController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string controllerName = filterContext.Controller.ToString();//控制器
            string actionName = filterContext.ActionDescriptor.ActionName;//Action

            if (controllerName.Equals("MvcApp.Controllers.SysUserController") && (actionName == "Login"))
            {
                //如果是用户请求登录页面则不验证权限
                base.OnActionExecuting(filterContext);
            }
            else
            {
                if (AppHelper.LoginedUser == null)//如果登录信息丢失
                {
                    RedirectToUnAuthurizePage(filterContext);
                    Response.End();//结束响应
                }
                else
                {
                    if (!IsAuthenticated(controllerName, actionName))//判断登录用户是否有权限访问当前页面，如果没有访问权限
                    {
                        //强行跳转到“未授权页面”
                        RedirectToUnAuthurizePage(filterContext);
                    }
                    else
                    {
                        base.OnActionExecuting(filterContext);
                    }
                }
            }
        }
        private void RedirectToUnAuthurizePage(ActionExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Redirect("~/SysUser/Login");
        }
        /// <summary>
        /// 判断当前用户是否有模块访问权限
        /// </summary>
        private bool IsAuthenticated(string controllerClassName, string actionName)
        {
            if (AppHelper.Privileges == null)
            {
                return false;
            }
            if (controllerClassName.Equals("MvcApp.Controllers.HomeController"))
            {
                return true;
            }
            if (AppHelper.Privileges.Where(o => o.LoadClassName == controllerClassName).Count() > 0)
            {
                return true;
            }
            return false;
        }
    }
}