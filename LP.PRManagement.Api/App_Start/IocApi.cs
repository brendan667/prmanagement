using Autofac;
using Autofac.Integration.WebApi;
using LP.PRManagement.Api.Controllers;
using LP.PRManagement.Core.Startup;
using LP.PRManagement.Dal.Persistance;
using LP.PRManagement.Dal.Sql;
using System.Web.Http.Dependencies;

namespace LP.PRManagement.Api.App_Start
{
    /// <summary>
    /// 
    /// </summary>
    public class IocApi : IocCoreBase
    {
        private static bool _isInitialized;
        private static readonly object Locker = new object();
        private static IocApi _instance;
        private readonly IContainer _container;

        /// <summary>
        /// 
        /// </summary>
        public IocApi()
        {
            var builder = new ContainerBuilder();
            SetupCore(builder);
            WebApi(builder);
            _container = builder.Build();

        }
        

        #region Instance

        /// <summary>
        /// 
        /// </summary>
        public static IocApi Instance
        {
            get
            {
                if (_isInitialized) return _instance;
                lock (Locker)
                {
                    if (!_isInitialized)
                    {
                        _instance = new IocApi();
                        _isInitialized = true;
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public IContainer Container
        {
            get { return _container; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        #endregion

        #region Overrides of IocCoreBase

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        protected override IGeneralUnitOfWorkFactory GetInstanceOfIGeneralUnitOfWorkFactory(IComponentContext arg)
        {
            return new SqlConnectionFactory("PRManagement");
        }

        #endregion

        #region Private Methods

        private void WebApi(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(typeof(UserController).Assembly);
            builder.Register(t => new AutofacWebApiDependencyResolver(_container)).As<IDependencyResolver>();
        }

        #endregion
    }
}