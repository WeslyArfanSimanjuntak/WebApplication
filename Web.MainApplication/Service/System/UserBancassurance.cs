using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace Web.Application.Service.System
{
    public class UserBancassurance
    {

        public static string UserName
        {
            get
            {
                return ((ClaimsIdentity)HttpContext.Current.User.Identity).FindAll(ClaimTypes.Name).Select(x => x.Value).FirstOrDefault();
            }
        }
        public static string FullName
        {
            get
            {
                return ((ClaimsIdentity)HttpContext.Current.User.Identity).FindAll(ClaimTypes.GivenName).Select(x => x.Value).FirstOrDefault();
            }
        }
        public static bool IsAuthenticated
        {
            get
            {
                return HttpContext.Current.User.Identity.IsAuthenticated;
            }
        }
        public static List<string> Groups
        {
            get
            {
                return ((ClaimsIdentity)HttpContext.Current.User.Identity).FindAll(BancassuranceClaimIdentity.GroupClaimType).Select(x => x.Value).ToList();
            }
        }
        public static List<string> Roles
        {
            get
            {
                return ((ClaimsIdentity)HttpContext.Current.User.Identity).FindAll(ClaimTypes.Role).Select(x => x.Value).ToList();
            }
        }
        public static bool IsBprUser
        {
            get
            {
                return Convert.ToBoolean(((ClaimsIdentity)HttpContext.Current.User.Identity).FindAll(BancassuranceClaimIdentity.IsBPRUser).Select(x => x.Value).FirstOrDefault());
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
        public static string BPRPolicyId
        {
            get
            {
                return ((ClaimsIdentity)HttpContext.Current.User.Identity).FindAll(BancassuranceClaimIdentity.BPRPolicyId).Select(x => x.Value).FirstOrDefault();
            }
        }
    }
}