﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;
using Web.MainApplication.Controllers;
using Web.MainApplication.Resources;

namespace Web.MainApplication.WebUtility
{
    public class AplicationAuthorizationFilter : ActionFilterAttribute, IAuthenticationFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user = ((BaseController)filterContext.Controller).User;

           // List<string> roles = new List<string>();
            //AddDefaultRoleToUser(out roles);
            //roles = roles.Concat(user.Identity.Roles())
            var roles  = ((ClaimsIdentity)user.Identity).FindAll(ClaimTypes.Role).ToList().Select(x => x.Value.ToLower()).ToList();
           // roles = roles.Concat(roless.ToList());
            string actionName = filterContext.Controller.ControllerContext.RouteData.Values["action"].ToString().ToLower();
            string controllerName = filterContext.Controller.ControllerContext.RouteData.Values["controller"].ToString().ToLower();

            if (!user.Identity.IsAuthenticated && ((SystemResources.LoginPath.ToLower() != ("/" + controllerName + "/" + actionName))))
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
            if (user.Identity.IsAuthenticated && !roles.Contains(controllerName + "/" + actionName))
            {
               // if (user.Identity.Name != "abigail" && user.Identity.Name != "indv01")
               // {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "UnAuthorized" }));
               // }
            }
            var controller = (BaseController)filterContext.Controller;

            controller.ViewData[SystemResources.ViewDataPathKey] = controllerName + "/" + actionName;
        }
        private void AddDefaultRoleToUser(out List<string> roles)
        {
            roles = new List<string>();
            roles = SystemResources.DefaultRole.ToLower().Split(';').ToList();
        }
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            //filterContext.Result = new RedirectResult("~/Content/RangeError.html");
            //var temp = ((HomeController)filterContext.Controller).User.Identity;
        }
        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            //Your Code
        }

        //public override void OnResultExecuting(ResultExecutingContext filterContext)
        //{
        //    base.OnResultExecuting(filterContext);
        //    var controller = (BaseController)filterContext.Controller;

        //    controller.TempData["MessageToViewError"] = controller.MessageToViewError;
        //    controller.TempData["MessageToViewInfo"] = controller.MessageToViewInfo;
        //    controller.TempData["MessageToViewSuccess"] = controller.MessageToViewSuccess;
        //    controller.TempData["MessageToViewWarning"] = controller.MessageToViewWarning;
        //    controller.ClearMessage();
        //}
    }
}