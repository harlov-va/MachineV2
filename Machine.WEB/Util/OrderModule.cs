using Ninject.Modules;
using Machine.BLL.Services;
using Machine.BLL.Interfaces;

namespace Machine.WEB.Util
{
    public class OrderModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IOrderService>().To<OrderService>();
        }
    }
}