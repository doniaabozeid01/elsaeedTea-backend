﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace elsaeedTea.data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        //public string? PhoneNumber { get; set; }

    }
}
