﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;
using LoginBCryptMVCDemo.Models;

namespace LoginBCryptMVCDemo.Mappings
{
    public class UserMap:ClassMap<User>
    {
        public UserMap() {
            Table("Users");
            Id(u=>u.Id).GeneratedBy.Identity();
            Map(u => u.UserName);
            Map(u => u.Password);
        }
    }
}