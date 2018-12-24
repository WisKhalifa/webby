using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Mvc5;
using webby.Controllers;
using webby.Interfaces;
using webby.Models;

namespace webby
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            container.RegisterType<IPostRepository, PostRepository>();
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<HomeController>(new InjectionConstructor());
            container.RegisterType<AdminController>(new InjectionConstructor());
            container.RegisterType<ManageController>(new InjectionConstructor());
        }
    }
}