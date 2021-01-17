using BaseLibrary.Attributes;
using Microsoft.AspNetCore.Mvc;
using Sample.Mongo.Entities;
using Sample.Mongo.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiResponse]
    public class MongoController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public MongoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task Test()
        {
            var product1 = await _unitOfWork.Products.FindByIdAsync("5ff7aebd223ed7b226b4d3a6");

            await _unitOfWork.Products.InsertManyAsync(new List<Product>
            {
                new Product
                {
                    Name = "Test 1",
                    IsActive = true,
                    IsCertificate = true
                },
                new Product
                {
                    Name = "Test 2",
                    IsActive = true,
                    IsCertificate = true
                }
            });

            _unitOfWork.Products.InsertOne(new Product()
            {
                Name = "Test 311111111111111111111111",
                IsActive = true,
                IsCertificate = true
            });

            _unitOfWork.Products.InsertMany(new List<Product>
            {
                new Product
                {
                    Name = "Test 4",
                    IsActive = true,
                    IsCertificate = true
                },
                new Product
                {
                    Name = "Test 52222222222222222222222222222",
                    IsActive = true,
                    IsCertificate = true
                }
            });

            _unitOfWork.SaveChanges();
        }
    }
}
