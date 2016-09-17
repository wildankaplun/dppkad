using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dppkad.Common
{
    public class SimpleSessionPersister
    {
        static string usernameSessionVar = "username";
        static string roleSessionVar = "role";

        public static string Username
        {
            get
            {
                if (HttpContext.Current == null) return string.Empty;
                var sessionVar = HttpContext.Current.Session[usernameSessionVar];
                if (sessionVar != null)
                    return sessionVar as string;
                return null;
            }
            set
            { HttpContext.Current.Session[usernameSessionVar] = value; }
        }

        public static string Role
        {
            get
            {
                if (HttpContext.Current == null) return string.Empty;
                var sessionVar = HttpContext.Current.Session[roleSessionVar];
                if (sessionVar != null)
                    return sessionVar as string;
                return null;
            }
            set
            { HttpContext.Current.Session[roleSessionVar] = value; }
        }
    }
}