﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinApp.Entities.Identity.Account
{
    public class Role : IdentityRole
    {
        public Role() : base() { }

        public Role(string name)
            : base(name)
        { }
    }
}