using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyEshop_Phone.Application.DTO;
using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Models;

namespace MyEshop_Phone.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    IProductsServices _productsServices;
    IQueriProductsServices _services;
    ISubGroupsServices _subGroupsServices;

    public HomeController(ILogger<HomeController> logger, IProductsServices productsServices, IQueriProductsServices queri, ISubGroupsServices subGroupsServices)
    {
        _logger = logger;
        _productsServices = productsServices;
        _services = queri;
        _subGroupsServices = subGroupsServices;
    }

    public IActionResult Index()
    {
        return View();
    }
    [Route("AboutUs")]
    public IActionResult AboutUs()
    {
        return View();
    }
    [Route("Products")]
    public async Task<IActionResult> AllProducts()
    {
        //var products = await _productsServices.GetAllProduct();
        var products =await _services.GetProductsFullData();
        return View(products);
    }
    [Route("Product/{id}")]
    public async Task<IActionResult> Products(int id)
    {
        var result = await _services.ShowSingleProducts(id);
        if(result==null)
            return NotFound();
        return View(result);
    }
    [Route("ProductsGroups/{id}")]
    public async Task<IActionResult> PageGroupsProducts(int id)
    {
        if(id==null)
            return NotFound();
        var products = await _productsServices.ShowGroupsById(id);
        return View(products);
    }
    [Route("SubMenu/{id}")]
    public async Task<IActionResult> SubMenuProducts(int id)
    {
        if(id==null)
            return NotFound();
        return View(await _subGroupsServices.ShowSubMenuById(id));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
