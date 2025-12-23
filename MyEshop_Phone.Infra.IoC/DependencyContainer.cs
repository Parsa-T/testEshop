using Microsoft.Extensions.DependencyInjection;
using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Application.Services;
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
        public static void RegisterServices(IServiceCollection services)
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
            //Application
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IProductsServices, ProductsServices>();
            services.AddScoped<IProductsCommentServices, ProductsCommentServices>();
            services.AddScoped<IProductsGroupServices, ProductsGroupServices>();
            services.AddScoped<IProductsFeaturseServices, ProductsFeaturseServices>();
            services.AddScoped<IFeaturesServices, FeaturesServices>();
            services.AddScoped<ITagsServices, TagsServices>();
            services.AddScoped<IProductsGalleriseServices, ProductsGalleriseServices>();
            services.AddScoped<IProductColorServices, ProductColorServices>();
            services.AddScoped<ISubGroupsServices, SubGroupsServices>();
            //Application and Data
            services.AddScoped<IPColorServices, PColorRepository>();
        }
    }
}
