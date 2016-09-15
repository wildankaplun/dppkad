﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Dppkad.Common
{
    public class CustomPrincipal : IPrincipal
    {
        public CustomPrincipal(CustomIdentity identity)
        {
            this.Identity = identity;
        }

        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            return true;
        }
    }
}