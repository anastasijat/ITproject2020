﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ITproject2020.Startup))]
namespace ITproject2020
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
