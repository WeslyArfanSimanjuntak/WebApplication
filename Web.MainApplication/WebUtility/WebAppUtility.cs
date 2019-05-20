using Interface.Application;
using Repository.Application.DataModel;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Mvc.Html;
using System.Web.Routing;
using Web.MainApplication.Controllers;
using Web.MainApplication.WebUtility;

namespace System.Web.Mvc
{
    public static class WebAppUtility
    {
        public static void CopyPropertiesAuditLogTo<T, TU>(T source, TU dest)
        {
            var sourceProps = typeof(T).GetProperties().Where(x => x.CanRead).ToList();
            var destProps = typeof(TU).GetProperties()
                    .Where(x => x.CanWrite)
                    .ToList();

            foreach (var sourceProp in sourceProps)
            {
                if (destProps.Any(x => x.Name == sourceProp.Name))
                {
                    var p = destProps.First(x => x.Name == sourceProp.Name);
                    if (p.CanWrite)
                    { // check if the property can be set or no.
                        var originalValue = p.GetValue(dest, null);
                        p.SetValue(dest, sourceProp.GetValue(source, null) != null ? sourceProp.GetValue(source, null) : originalValue, null);
                    }
                }

            }

        }
        public static string LoginUserFullName
        {
            get
            {

                return ((ClaimsIdentity)HttpContext.Current.User.Identity).FindAll(ClaimTypes.GivenName).Select(x => x.Value).FirstOrDefault();
            }
        }

        public static string LoginUserName
        {
            get
            {
                return ((ClaimsIdentity)HttpContext.Current.User.Identity).FindAll(ClaimTypes.Name).Select(x => x.Value).FirstOrDefault();
            }
        }

        public static string Username(this IIdentity identity)
        {

            var claim = ((ClaimsIdentity)identity).FindFirst(ClaimTypes.Name);
            return claim != null ? claim.Value : "";
        }
        public static string FullName(this IIdentity identity)
        {

            var claim = ((ClaimsIdentity)identity).FindFirst(WebClaimIdentity.FullNameType);
            return claim != null ? claim.Value : "";
        }
        public static string MenuString(this IIdentity identity)
        {

            var claim = ((ClaimsIdentity)identity).FindFirst(WebClaimIdentity.MenuString);
            return claim != null ? claim.Value : "";
        }
        public static string Email(this IIdentity identity)
        {

            var claim = ((ClaimsIdentity)identity).FindFirst(ClaimTypes.Email);
            return claim != null ? claim.Value : "";
        }
        public static List<string> Roles(this IIdentity identity)
        {

            var claim = ((ClaimsIdentity)identity).FindAll(ClaimTypes.Role).Select(x => x.Value).ToList();
            return claim;
        }


        public static SelectList CreateSelectList(int max, int min = 1, bool giveEmptyList = true, bool orderByAsc = true, string concatText = "")

        {
            List<SelectListItem> returnValue = new List<SelectListItem>();

            if (giveEmptyList)
            {
                returnValue.Add(new SelectListItem
                {
                    Text = "",
                    Value = ""
                });
                for (int counter = min; counter <= max; counter++)
                {
                    returnValue.Add(new SelectListItem
                    {
                        Text = counter.ToString() + concatText,
                        Value = counter.ToString()
                    });
                }
            }
            else
            {
                for (int counter = min; counter <= max; counter++)
                {
                    returnValue.Add(new SelectListItem
                    {
                        Text = counter.ToString() + concatText,
                        Value = counter.ToString()
                    });
                }
            }
            if (orderByAsc == false)
            {
                returnValue.OrderByDescending(x => x.Value);
                return new SelectList(returnValue, "Value", "Text");

            }
            return new SelectList(returnValue, "Value", "Text");
        }
        public static SelectList CreateSelectList(int max, object selectedValue, int min = 1, bool giveEmptyList = true, bool orderByAsc = true, string concatText = "")

        {
            List<SelectListItem> returnValue = new List<SelectListItem>();

            if (giveEmptyList)
            {
                returnValue.Add(new SelectListItem
                {
                    Text = "",
                    Value = ""
                });
                for (int counter = min; counter <= max; counter++)
                {
                    returnValue.Add(new SelectListItem
                    {
                        Text = counter.ToString() + concatText,
                        Value = counter.ToString()
                    });
                }
            }
            else
            {
                for (int counter = min; counter <= max; counter++)
                {
                    returnValue.Add(new SelectListItem
                    {
                        Text = counter.ToString() + concatText,
                        Value = counter.ToString()
                    });
                }
            }
            if (orderByAsc == false)
            {
                returnValue.OrderByDescending(x => x.Value);
                return new SelectList(returnValue, "Value", "Text");

            }
            return new SelectList(returnValue, "Value", "Text", selectedValue);
        }
        public static void AddBlank(this List<SelectListItem> x)
        {
            x.Reverse();
            x.Add(new SelectListItem()
            {
                Value = "",
                Text = "---"
            });
            x.Reverse();

        }
        public static void AddBlank(this SelectList x)
        {
            x.Reverse();
            var temp = x;
            var tempToList = temp.ToList();

            tempToList.Add(new SelectListItem()
            {
                Value = "",
                Text = "---"
            });
            tempToList.Reverse();
            x = tempToList.ToSelectList();

        }
        public static SelectList CreateSelectList(this SelectList x, object selectedValue)
        {

            return x.ToList().ToSelectList(selectedValue);

        }
        public static SelectList CreateSelectList(this SelectList x, object selectedValue, IEnumerable disabledValue)
        {
            x.Reverse();
            var temp = x;
            var tempToList = temp.ToList();

            tempToList.Add(new SelectListItem()
            {
                Value = "",
                Text = "---"
            });
            tempToList.Reverse();
            x = tempToList.ToSelectList(selectedValue, disabledValue);
            return tempToList.ToSelectList(selectedValue, disabledValue);

        }
        public static void AddItemValText(this List<SelectListItem> x, string val, string text, bool selected = false, bool disabled = false)
        {
            x.Add(new SelectListItem()
            {
                Value = val,
                Text = text,
                Disabled = disabled,
                Selected = selected
            });
        }
        public static SelectList ToSelectList(this List<SelectListItem> x)
        {
            SelectList selectList = new SelectList(x, "Value", "Text");
            return selectList;
        }
        public static SelectList ToSelectList(this List<SelectListItem> x, object selectedValue)
        {
            SelectList selectList = new SelectList(x, "Value", "Text", selectedValue);
            return selectList;
        }
        public static SelectList ToSelectList(this List<SelectListItem> x, object selectedValue, IEnumerable disabledValues)
        {
            SelectList selectList = new SelectList(x, "Value", "Text", selectedValue, disabledValues);
            return selectList;
        }
        public static SelectList SelectListIsActive(object selectedValue = null)
        {
            List<SelectListItem> sli = new List<SelectListItem>();
            sli.AddBlank();
            sli.AddItemValText("1", "Active");
            sli.AddItemValText("0", "Non-Active");
            if (selectedValue != null)
            {
                return sli.ToSelectList(selectedValue);
            }
            return sli.ToSelectList();
        }
        public static string RitaseNumberGenerator(double ritaseNumber)
        {
            return "RTS-" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "-" + ritaseNumber.ToString().PadLeft(7, '0');
        }
        public static string TransactionProductAdjustmentNumberGenerator(double counter)
        {
            return "AJT-" + DateTime.Now.ToShortDateString() + "-" + counter.ToString().PadLeft(7, '0');
        }
        public static string TransactionProductConvertionNumberGenerator(double counter)
        {
            return "CVT-" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "-" + counter.ToString().PadLeft(7, '0');
        }
        public static string TransactionProductMixingNumberGenerator(double counter)
        {
            return "MXG-" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "-" + counter.ToString().PadLeft(7, '0');
        }
        public static string TransactionProductNumberGenerator(double transactionIdMax)
        {
            return "TRS-" + DateTime.Now.ToShortDateString() + "-" + transactionIdMax.ToString().PadLeft(7, '0');
        }
        public static string DeliveryRequestNumberGenerator(double drMax)
        {
            return "DLR-" + DateTime.Now.ToShortDateString() + "-" + drMax.ToString().PadLeft(7, '0');
        }
        public static string ContractIdGenerator(double contractNumber)
        {
            return "CTR-" + DateTime.Now.ToShortDateString() + "-" + contractNumber.ToString().PadLeft(7, '0');
        }
        public static string FinanceTransactionNumberGenerator(double counter)
        {
            return "FTR-" + DateTime.Now.ToShortDateString() + "-" + counter.ToString().PadLeft(7, '0');
        }

        public static string DeliveryOrderNumberGenerator(double counter)
        {
            return "DVO-" + DateTime.Now.ToShortDateString() + "-" + counter.ToString().PadLeft(7, '0');
        }
        public static string DeliveryOrderInvoiceNumberGenerator(double counter)
        {
            return "DVOI-" + DateTime.Now.ToShortDateString() + "-" + counter.ToString().PadLeft(7, '0');
        }
        public static void SetPropertyUpdate(this IAuditableObject entity)
        {
            entity.UpdatedBy = LoginUserName;
            entity.UpdatedDate = DateTime.Now;
        }
        public static void SetPropertyCreate(this IAuditableObject entity, short isActive = 1)
        {
            entity.CreatedBy = LoginUserName;
            entity.CreatedDate = DateTime.Now;
            entity.IsActive = isActive;
        }

        // Action Link Extention

        public static MvcHtmlString ActionLinkAuthorized(this HtmlHelper htmlHelper, string linkText, string actionName)
        {
            var user = ((BaseController)htmlHelper.ViewContext.Controller).User;
            string currentController = htmlHelper.ViewContext.RouteData.Values["controller"].ToString().ToLower();
            if (user.IsInRole(currentController.ToLower() + "/" + actionName.ToLower()))
            {
                return htmlHelper.ActionLink(linkText, actionName);
            }
            return null;
        }
        public static MvcHtmlString ActionLinkAuthorized(this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues)
        {
            var user = ((BaseController)htmlHelper.ViewContext.Controller).User;
            string currentController = htmlHelper.ViewContext.RouteData.Values["controller"].ToString().ToLower();
            if (user.IsInRole(currentController.ToLower() + "/" + actionName.ToLower()))
            {
                return htmlHelper.ActionLink(linkText, actionName, routeValues);
            }
            return null;
        }
        public static MvcHtmlString ActionLinkAuthorized(this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues, object htmlAttributes)
        {
            var user = ((BaseController)htmlHelper.ViewContext.Controller).User;
            string currentController = htmlHelper.ViewContext.RouteData.Values["controller"].ToString().ToLower();
            if (user.IsInRole(currentController.ToLower() + "/" + actionName.ToLower()))
            {
                return htmlHelper.ActionLink(linkText, actionName, routeValues, htmlAttributes);
            }
            return null;
        }
        public static MvcHtmlString ActionLinkAuthorized(this HtmlHelper htmlHelper, string linkText, string actionName, RouteValueDictionary routeValues)
        {
            var user = ((BaseController)htmlHelper.ViewContext.Controller).User;
            string currentController = htmlHelper.ViewContext.RouteData.Values["controller"].ToString().ToLower();
            if (user.IsInRole(currentController.ToLower() + "/" + actionName.ToLower()))
            {
                return htmlHelper.ActionLink(linkText, linkText, actionName, routeValues);
            }
            return null;
        }
        public static MvcHtmlString ActionLinkAuthorized(this HtmlHelper htmlHelper, string linkText, string actionName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            var user = ((BaseController)htmlHelper.ViewContext.Controller).User;
            string currentController = htmlHelper.ViewContext.RouteData.Values["controller"].ToString().ToLower();
            if (user.IsInRole(currentController.ToLower() + "/" + actionName.ToLower()))
            {
                return htmlHelper.ActionLink(linkText, actionName, routeValues, htmlAttributes);
            }
            return null;
        }
        public static MvcHtmlString ActionLinkAuthorized(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName)
        {
            var user = ((BaseController)htmlHelper.ViewContext.Controller).User;
            if (user.IsInRole(controllerName.ToLower() + "/" + actionName.ToLower()))
            {
                return htmlHelper.ActionLink(linkText, actionName, controllerName);
            }
            return null;
        }
        public static MvcHtmlString ActionLinkAuthorized(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues, object htmlAttributes)
        {
            var user = ((BaseController)htmlHelper.ViewContext.Controller).User;
            if (user.IsInRole(controllerName.ToLower() + "/" + actionName.ToLower()))
            {
                return htmlHelper.ActionLink(linkText, actionName, controllerName, routeValues, htmlAttributes);
            }
            return null;
        }
        public static MvcHtmlString ActionLinkAuthorized(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            var user = ((BaseController)htmlHelper.ViewContext.Controller).User;
            if (user.IsInRole(controllerName.ToLower() + "/" + actionName.ToLower()))
            {
                return htmlHelper.ActionLink(linkText, actionName, controllerName, routeValues, htmlAttributes);
            }
            return null;
        }
        public static MvcHtmlString ActionLinkAuthorized(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, string protocol, string hostName, string fragment, object routeValues, object htmlAttributes)
        {
            var user = ((BaseController)htmlHelper.ViewContext.Controller).User;
            if (user.IsInRole(controllerName.ToLower() + "/" + actionName.ToLower()))
            {
                return htmlHelper.ActionLink(linkText, actionName, controllerName, protocol, hostName, fragment, routeValues, htmlAttributes);
            }
            return null;
        }
        public static MvcHtmlString ActionLinkAuthorized(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            var user = ((BaseController)htmlHelper.ViewContext.Controller).User;
            if (user.IsInRole(controllerName.ToLower() + "/" + actionName.ToLower()))
            {
                return htmlHelper.ActionLink(linkText, actionName, controllerName, protocol, hostName, fragment, routeValues, htmlAttributes);
            }
            return null;
        }


        // dbcontex extention
        public static long DeliveryOrderSequence(this DB_TritsurEntities db)
        {
            var sequence = db.TableSequence.Where(x => x.Year == DateTime.Now.Year).FirstOrDefault().DeliveryOrder;
            return sequence.HasValue ? sequence.Value : 0;

        }
        public static float ParamSetupTonToM3(this DB_TritsurEntities db)
        {
            float retval;
            float.TryParse(db.ParameterSetup.Where(x => x.Name == "Convertion(ton to m3)").FirstOrDefault().Value, out retval);
            return retval;
        }
        public static long DeliveryOrderInvoiceSequence(this DB_TritsurEntities db)
        {
            var sequence = db.TableSequence.Where(x => x.Year == DateTime.Now.Year).FirstOrDefault().DeliveryOrderInvoice;
            return sequence.HasValue ? sequence.Value : 0;

        }
        public static long ProductMixing(this DB_TritsurEntities db)
        {
            var sequence = db.TableSequence.Where(x => x.Year == DateTime.Now.Year).FirstOrDefault().ProductMixing;
            return sequence.HasValue ? sequence.Value : 0;

        }

    }

    public class DecimalModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            ValueProviderResult valueResult = bindingContext.ValueProvider
                .GetValue(bindingContext.ModelName);

            ModelState modelState = new ModelState { Value = valueResult };

            object actualValue = null;

            if (valueResult.AttemptedValue != string.Empty)
            {
                try
                {
                    actualValue = Convert.ToDecimal(valueResult.AttemptedValue, CultureInfo.CurrentCulture);
                }
                catch (FormatException e)
                {
                    modelState.Errors.Add(e);
                }
            }

            bindingContext.ModelState.Add(bindingContext.ModelName, modelState);

            return actualValue;
        }
    }
    public class EntityEnum
    {

        public const string CONTRACT = "CONTRACT";
        public const string ContractProduct = "ContractProduct";
        public const string DeliveryOrder = "DeliveryOrder";
        public const string DeliveryOrderInvoice = "DeliveryOrderInvoice";
        public const string DeliveryRequest = "DeliveryRequest";

    }

}