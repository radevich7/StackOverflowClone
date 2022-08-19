using System.Web.Mvc;
using Unity;
using Unity.WebApi;
using Unity.Mvc5;
using StackOverflowClone.ServiceLayer;
using System.Web.Mvc;
using System.Web.Http;

namespace StackOverflowClone
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<IQuestionsService, QuestionsService>();
            container.RegisterType<IUsersService, UsersService>();

            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
    }
}