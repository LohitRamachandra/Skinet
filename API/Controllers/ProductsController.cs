using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
         private readonly ILogger<ProductsController> _logger;
        // private readonly IProductRespository _repo;
        private readonly IGenericRepository<Product> _produtsRepo;
        private readonly IGenericRepository<ProductBrand> _prodtuctBrandRepo;
        private readonly IGenericRepository<ProductType> _prodtuctTypeRepo;

        public ProductsController(ILogger<ProductsController> logger,  IGenericRepository<Product> productsRepo,
        IGenericRepository<ProductBrand> productBrandRepo,
        IGenericRepository<ProductType> productTypeRepo)  // IProductRespository repo
        {
            _logger = logger;
            // _repo = repo;
            _prodtuctTypeRepo = productTypeRepo;
            _prodtuctBrandRepo = productBrandRepo;
            _produtsRepo = productsRepo;

        }

        // public IActionResult Index()
        // {
        //     return View();
        // }

        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View("Error!");
        // }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();

            var products = await _produtsRepo.ListAsync(spec);
            return Ok(products);

            // var products = await _produtsRepo.ListAllAsync();
            // return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            return await _produtsRepo.GetEntityWithSpec(spec);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            var productBrands = await _prodtuctBrandRepo.ListAllAsync();
            return Ok(productBrands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            var productTypes = await _prodtuctTypeRepo.ListAllAsync();
            return Ok(productTypes);
        }
    }
}