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

    public HomeController(ILogger<HomeController> logger, IProductsServices productsServices, IQueriProductsServices queri)
    {
        _logger = logger;
        _productsServices = productsServices;
        _services = queri;
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
        var products = await _productsServices.GetAllProduct();
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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
