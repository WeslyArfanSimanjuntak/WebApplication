using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Web.Application.Service.System
{
    public class IndosuryaUtility
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
        public static string BPRSubsidiaryId
        {
            get
            {
                return ((ClaimsIdentity)HttpContext.Current.User.Identity).FindAll(BancassuranceClaimIdentity.BPRSubsidiaryId).Select(x => x.Value).FirstOrDefault();
            }
        }
        public static string BPRSubsidiaryName
        {
            get
            {
                return ((ClaimsIdentity)HttpContext.Current.User.Identity).FindAll(BancassuranceClaimIdentity.BPRSubsidiaryName).Select(x => x.Value).FirstOrDefault();
            }
        }
        public static string BPRPolicyNumber
        {
            get
            {
                return ((ClaimsIdentity)HttpContext.Current.User.Identity).FindAll(BancassuranceClaimIdentity.BPRPolicyId).Select(x => x.Value).FirstOrDefault();
            }
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
    }
}