using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyEshop_Phone.Application.Common.Interfaces;
using MyEshop_Phone.Application.Common.services;
using MyEshop_Phone.Application.Common.setting;
using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Application.Services;
using MyEshop_Phone.Application.Utilitise;
using MyEshop_Phone.Domain.Interface;
using MyEshop_Phone.Infra.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Infra.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            //Data & Domain
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductsRepository, ProductsRepository>();
            services.AddScoped<IProductsCommentRepository, ProductCommentRepository>();
            services.AddScoped<IProductsGroupeRepository, ProductsGroupsRepository>();
            services.AddScoped<IProductsFeaturseRepository, ProductsFeaturseRepository>();
            services.AddScoped<IFeaturseRepository, FeaturseRepository>();
            services.AddScoped<IProductsTagsRepository, ProductsTagsRepository>();
            services.AddScoped<IGalleriseRepository, GalleriseRepository>();
            services.AddScoped<IColorRepository, ColorRepository>();
            services.AddScoped<IProductColorRepository, ProductColorRepository>();
            services.AddScoped<ISubMenuGroupsRepository, SubMenuGroupsRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderDitelsRepository, OrderDitelsRepository>();
            services.AddScoped<ICodePostalRepository, PostalCodeRepository>();
            //Application
            services.AddScoped<IOrderDitelsServices, OrderDitelsServices>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IOrderServices, OrderServices>();
            services.AddScoped<IProductsServices, ProductsServices>();
            services.AddScoped<IProductsCommentServices, ProductsCommentServices>();
            services.AddScoped<IProductsGroupServices, ProductsGroupServices>();
            services.AddScoped<IProductsFeaturseServices, ProductsFeaturseServices>();
            services.AddScoped<IFeaturesServices, FeaturesServices>();
            services.AddScoped<ITagsServices, TagsServices>();
            services.AddScoped<IProductsGalleriseServices, ProductsGalleriseServices>();
            services.AddScoped<IProductColorServices, ProductColorServices>();
            services.AddScoped<ISubGroupsServices, SubGroupsServices>();
            services.AddScoped<IPostalCodeServices, PostalCodeServices>();
            //Zarinpal
            services.Configure<ZarinpalSettings>(configuration.GetSection("Zarinpal"));
            services.AddHttpClient<IPaymentGateway, ZarinpalPaymentGateway>();
            //Application and Data
            services.AddScoped<IPColorServices, PColorRepository>();
            services.AddScoped<IGroupsSubMenuServices, GroupsSubMenuRepository>();
            services.AddScoped<IQueriProductsServices, QueriProductsRepository>();
            services.AddScoped<IOrderQuery, OrderQuery>();
            services.AddScoped<IAdminOrdersServices, AdminOrdersRepository>();
        }
    }
}
