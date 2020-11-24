using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dal.Domain.Contracts;
using Dal.Domain.Entities;
using Dal.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Dal.MyWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("GetTest")]
        public async Task<IActionResult> GetTest()
        {
            var products = await _unitOfWork.Product.GetAll();

            var productModels = await _unitOfWork.Product.GetAllAsModel<ProductModel>();

            var productCount = await _unitOfWork.Product.Count(x => x.Id > 2);

            var productById = await _unitOfWork.Product.GetById(1);


            Product newProduct = new Product() { Name = "New Product" };

            await _unitOfWork.Product.Add(newProduct);
            await _unitOfWork.CommitAsync();

            var productByName = await _unitOfWork.Product.Get(x => x.Name == "New Product");
            productByName.Name = "New Product v2";

            _unitOfWork.Product.Update(productByName);
            await _unitOfWork.CommitAsync();

            _unitOfWork.Product.Delete(productByName);
            await _unitOfWork.CommitAsync();

            return Ok(products);
        }
    }
}
