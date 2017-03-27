using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ShoppingCart.Abstractions;
using ShoppingCart.Business;
using ShoppingCart.Common;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector.Integration.WebApi;

namespace ShoppingCart.Service
{
    public class IocConfig
    {
        public static void ResolveDependencies()
        {
            // Create the container as usual.
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

            // Register your types, for instance using the scoped lifestyle:
            container.Register<IDataSource, CsvDataSource>(Lifestyle.Scoped);
            container.Register<IConfig, ShoppingCartConfig>(Lifestyle.Scoped);
            container.Register<IProductContext, ProductContext>(Lifestyle.Scoped);
            container.Register<IShoppingBasketContext, ShoppingBasketContext>(Lifestyle.Scoped);

            // This is an extension method from the integration package.
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}