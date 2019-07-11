using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;
using Repository.Application;
using System.Collections.Generic;
using Repository.Application.DataModel;
using Web.MainApplication.WebUtility;
using Web.MainApplication.Models;
using Microsoft.Owin.Security;
using Web.MainApplication.Resources;
using Web.MainApplication.Service.System;
using System.Web;
using System.Text;
using System;

namespace Web.MainApplication.Controllers
{
    public class AccountController : BaseController
    {
        //wesly simanjuntak
        private UnitOfWork unitOfWork;
        private DB_TritsurEntities db = new DB_TritsurEntities();

        public AccountController()
        {
            this.unitOfWork = new UnitOfWork();
        }
        public AccountController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ActionResult Login(string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public ActionResult Login(LoginViewModelWebApp model)
        {
            try
            {

                string returnUrl = Request.Params["returnUrl"] != null ? Request.Params["returnUrl"] : null;
                string userName = model.UserName;
                string password = model.Password;

                string passwordHash = Encriptor.SHA1(password);
                var aspNetUser = this.db.AspNetUsers.Where(x => x.Username == userName).ToList().Where(x => x.Username == userName).FirstOrDefault();
                if (aspNetUser == null)
                {
                    WarningMessagesAdd("Username is not Exist");
                }
                else
                {
                    if (aspNetUser.Password == passwordHash)
                    {
                        SignIn(this.GetClaims(aspNetUser));
                    }
                    else
                    {
                        WarningMessagesAdd("Password is not Match");
                    }
                }
                if (WarningMessages().Count > 0)
                {
                    return View();
                }
                if (returnUrl != null)
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                ErrorMessagesAdd(e.MessageToList());
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private void SignIn(List<Claim> claims)//Mind!!! This is System.Security.Claims not WIF claims
        {

            var claimsIdentity = new WebClaimIdentity(claims, SystemResources.DefaultAuthenticationTypes_ApplicationCookie);

            //This uses OWIN authentication

            AuthenticationManager.SignOut(SystemResources.DefaultAuthenticationTypes_ExternalCookie);



            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = true }, claimsIdentity);


            HttpContext.User = new WebAppClaimPrincipal(AuthenticationManager.AuthenticationResponseGrant.Principal);


        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private List<Claim> GetClaims(AspNetUsers aspNetUser)
        {
            var claims = new List<Claim>();


            List<string> roles = new List<string>();
            List<int> userRolesId = new List<int>();
            foreach (var groupUser in aspNetUser.AspNetGroupUser)
            {
                foreach (var item in groupUser.AspNetGroups.AspNetRoleGroup)
                {
                    userRolesId.Add(item.RolesId);
                    if (item.AspNetRoles.Type.ToLower() == "function")
                    {
                        roles.Add(item.AspNetRoles.AspNetRoles2.Name + "/" + item.AspNetRoles.Name);

                    }
                    else if (item.AspNetRoles.Type.ToLower() == "controller")
                    {
                        roles.Add(item.AspNetRoles.Name + "/index");
                    }
                }

            }
            userRolesId = userRolesId.Distinct().ToList();
            SystemResources.DefaultRole.ToLower().Split(';').ToList().ForEach(x =>
            {
                roles.Add(x);
            });

            roles = roles.Distinct().ToList();
            roles = roles.OrderBy(x => x).ToList();
            if (string.IsNullOrEmpty(aspNetUser.Email) == false)
            {
                claims.Add(new Claim(ClaimTypes.Email, aspNetUser.Email));
            }
            if (string.IsNullOrEmpty(aspNetUser.FullName) == false)
            {
                claims.Add(new Claim(WebClaimIdentity.FullNameType, aspNetUser.FullName));
            }

            if (string.IsNullOrEmpty(aspNetUser.Username) == false)
            {
                claims.Add(new Claim(ClaimTypes.Name, aspNetUser.Username));
                claims.Add(new Claim(WebClaimIdentity.Username, aspNetUser.Username));
            }

            foreach (var item in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item.ToLower()));
            }

            var userMenu = "";

            db.Menu.Where(x => x.MenuLevel == 1 && x.IsActive == 1).OrderBy(x => x.Sequence).ToList().ForEach(x =>
              {
                  userMenu += GenerateUL(x, userRolesId);
              });
            claims.Add(new Claim(WebClaimIdentity.MenuString, userMenu));
            return claims;
        }
        private string GenerateUL(Menu menu, List<int> userRolesId)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("");

            if (menu.Menu1.Count > 0 && ContainsChildMenuOwnedByUser(menu.Menu1.ToList(), userRolesId))
            {
                sb.AppendLine("<li class=\"treeview\" id=\"li_" + menu.MenuName + "\">");
                sb.AppendLine("<a href=\"#\">");
                sb.AppendLine("<i class=\"" + menu.MenuIClass + "\"></i>");
                sb.AppendLine("<span>" + menu.MenuName + "</span>");
                sb.AppendLine("<span class=\"pull-right-container\"></span>");
                sb.AppendLine("<i class=\"fa fa-angle-left pull-right\"></i>");
                //sb.AppendLine("</span>");
                sb.AppendLine("</a>");
                sb.AppendLine("<ul class=\"treeview-menu\" id=\"ul_" + menu.MenuName + "\">");
                foreach (var menuItem in menu.Menu1.OrderBy(x => x.Sequence))
                {
                    sb.Append(GenerateUL(menuItem, userRolesId));
                }
                sb.AppendLine("</ul>");
                sb.AppendLine("</li>");
            }
            else
            {
                if (menu.AspNetRoles1 != null && userRolesId.Exists(x => x == menu.AspNetRoles1.Id))
                {
                    string handler = menu.AspNetRoles1.ParentId != null ? menu.AspNetRoles1.AspNetRoles2.Name.ToLower() + "/" + menu.AspNetRoles1.Name.ToLower() : menu.AspNetRoles1.Name.ToLower();
                    string menuText = menu.MenuName;
                    string classVal = menu.MenuIClass;
                    string urlBase = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

                    string id = "";

                    if (menu.AspNetRoles1.Type == "Controller")
                    {
                        id = menu.AspNetRoles1.Name + "_Index";
                    }
                    else
                    {
                        id = menu.AspNetRoles1.ParentId != null ? menu.AspNetRoles1.AspNetRoles2.Name.ToLower() + "_" + menu.AspNetRoles1.Name.ToLower() : menu.AspNetRoles1.Name.ToLower();
                    }

                    string line = String.Format(@"<li id=""{4}""><a href=""{3}{0}""><i class=""{2}""></i><span>{1}</span></a></li>", handler, menuText, classVal, urlBase, id.ToLower());
                    sb.Append(line);
                }

            }

            sb.Append("");
            return sb.ToString();

        }
        private bool ContainsChildMenuOwnedByUser(List<Menu> listMenu, List<int> role)
        {
            foreach (var item in listMenu)
            {
                if (item.AspNetRoles1 != null && role.Contains(Convert.ToInt32(item.AspNetRoles)))
                {
                    return true;
                }
            }
            return false;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}