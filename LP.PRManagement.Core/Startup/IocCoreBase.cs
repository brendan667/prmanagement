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
            builder.RegisterType<UserManager>().As<IUserManager>();
            builder.RegisterType<ArtistManager>().As<IArtistManager>();
            builder.RegisterType<ContactInfoGroupManager>().As<IContactInfoGroupManager>();
            builder.RegisterType<FestivalManager>().As<IFestivalManager>();
            builder.RegisterType<VenueManager>().As<IVenueManager>();
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
