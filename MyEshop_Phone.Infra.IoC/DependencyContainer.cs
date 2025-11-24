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
            services.AddScoped<IUserRepository,UserRepository>();
            services.AddScoped<IProductsRepository, ProductsRepository>();
            services.AddScoped<IProductsCommentRepository, ProductCommentRepository>();
            //Application
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IProductsServices, ProductsServices>();
            services.AddScoped<IProductsCommentServices, ProductsCommentServices>();
        }
    }
}
