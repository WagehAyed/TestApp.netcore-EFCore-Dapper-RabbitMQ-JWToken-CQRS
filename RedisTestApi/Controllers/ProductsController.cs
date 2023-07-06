using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DBContextClass _dbContext;
        private readonly ICacheService _cacheService;
        public ProductsController(DBContextClass dbContext, ICacheService cacheService)
        {
            _dbContext = dbContext;
            _cacheService = cacheService;
        }

        [HttpPost("AddProduct")]
        public async Task<Product> Post([FromBody]Product value)
        {
            var obj=await _dbContext.Products.AddAsync(value);
           var isRemove= await _cacheService.RemoveAsync("Product");
            _dbContext.SaveChanges();
            return obj.Entity;
        }
        [HttpGet("Products")]
        public async Task<IEnumerable<Product>> Get()
        {
            var cachData =await _cacheService.GetAsync<IEnumerable<Product>>("Product");
            if (cachData != null)
            {
                return cachData;
            }

            var expirationTime = DateTimeOffset.Now.AddMinutes(5);
            cachData = await _dbContext.Products.ToListAsync();
            await _cacheService.SetAsync<IEnumerable<Product>>("Product", cachData, expirationTime);
            return cachData;
                
        }
    }
}
