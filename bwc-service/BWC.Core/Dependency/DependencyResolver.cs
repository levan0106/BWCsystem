using BWC.Core.Interfaces;
using BWC.Core.Repositories.MySql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BWC.Core.Dependency
{
    [T.Core.DenpendencyResolver.ExportEx(typeof(T.Core.DenpendencyResolver.IComponent))]
    public class DependencyResolver : T.Core.DenpendencyResolver.IComponent
    {
        public void SetUp(T.Core.DenpendencyResolver.IRegisterComponent registerComponent)
        {
            registerComponent.RegisterType<IUser, UserRepository>();
            registerComponent.RegisterType<IMaterial, MaterialRepository>();
            registerComponent.RegisterType<IComponent, ComponentRepository>();
            registerComponent.RegisterType<ISupplier, SupplierRepository>();
            registerComponent.RegisterType<IProduct, ProductRepository>();
            registerComponent.RegisterType<IColor, ColorRepository>();
            registerComponent.RegisterType<IProductComponent, ProductComponentRepository>();
            registerComponent.RegisterType<IProductMaterial, ProductMaterialRepository>();
            registerComponent.RegisterType<IProductPrice, ProductPriceRepository>();
            registerComponent.RegisterType<IOrder, OrderRepository>();
            registerComponent.RegisterType<IOrderComponent, OrderComponentRepository>();
            registerComponent.RegisterType<IOrderProduct, OrderProductRepository>();
            registerComponent.RegisterType<IOrderInvoice, OrderInvoiceRepository>();
            registerComponent.RegisterType<IOrderPayment, OrderPaymentRepository>();
            registerComponent.RegisterType<IOrderItem, OrderItemRepository>();
            registerComponent.RegisterType<ICustomer, CustomerRepository>();
            registerComponent.RegisterType<IEmployee, EmployeeRepository>();
            registerComponent.RegisterType<ITimeSheet, TimeSheetRepository>();
            registerComponent.RegisterType<IDiscount, DiscountRepository>();
            registerComponent.RegisterType<ICategory, CategoryRepository>();
            registerComponent.RegisterType<ISetting, SettingRepository>();
            registerComponent.RegisterType<IMakerSheet, MakerSheetRepository>();
            registerComponent.RegisterType<IUserToken, UserTokenRepository>();

        }
    }
    public static class Resolver
    {
        public static void Initialize(string dll)
        {
            T.Core.DenpendencyResolver.Bootstrapper.WebAPIInitialize(dll);
        }
    }
}
