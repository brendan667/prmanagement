using Autofac;
using LP.PRManagement.Core.Managers;
using LP.PRManagement.Core.Managers.Interfaces;
using LP.PRManagement.Dal.Persistance;

namespace LP.PRManagement.Core.Startup
{
    public abstract class IocCoreBase
    {
        protected void SetupCore(ContainerBuilder builder)
        {
            SetupDb(builder);
            SetupManagers(builder);
        }

        protected virtual void SetupDb(ContainerBuilder builder)
        {
            builder.Register(GetInstanceOfIGeneralUnitOfWorkFactory).SingleInstance();
            builder.Register(x => x.Resolve<IGeneralUnitOfWorkFactory>().GetConnection());
        }

        private static void SetupManagers(ContainerBuilder builder)
        {
            //builder.RegisterType<BaseManagerArguments>();
            builder.RegisterType<UserManager>().As<IUserManager>();
            //builder.RegisterType<UserManager>().As<IUserManager>();
            //builder.RegisterType<CompanyManager>().As<ICompanyManager>();
            //builder.RegisterType<FunctionalGroupManager>().As<IFunctionalGroupManager>();
            //builder.RegisterType<MandateAuditLogManager>().As<IMandateAuditLogManager>();
            //builder.RegisterType<MandateManager>().As<IMandateManager>();
            //builder.RegisterType<MandateStatusManager>().As<IMandateStatusManager>();
            //builder.RegisterType<TriggerAuditLogManager>().As<ITriggerAuditLogManager>();
            //builder.RegisterType<TriggerManager>().As<ITriggerManager>();
            //builder.RegisterType<TriggerStateManager>().As<ITriggerStateManager>();
        }

        //private static void SetupValidation(ContainerBuilder builder)
        //{
        //    builder.RegisterType<ValidatorFactory>().As<IValidatorFactory>();
        //    builder.RegisterType<UserValidator>().As<IValidator<User>>();
        //    builder.RegisterType<CompanyValidator>().As<IValidator<Company>>();
        //}

        protected abstract IGeneralUnitOfWorkFactory GetInstanceOfIGeneralUnitOfWorkFactory(IComponentContext arg);
    }
}
