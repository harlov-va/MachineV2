using Ninject.Modules;
using Machine.DAL.Interfaces;
using Machine.DAL.Repositories;

namespace Machine.BLL.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        //private string connectionString;
        //public ServiceModule(string connection)
        //{
        //    connectionString = connection;
        //}
        public override void Load()
        {
            Bind<IProductRepository>().To<EFProductRepository>();
        }
    }
}