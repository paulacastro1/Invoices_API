using AutoMapper;
using Billing_API_Net8.Controllers.Resources;
using Billing_API_NET8.Helpers;
using Billing_API_Net8.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
//using System.Data.Entity;

namespace Billing_API_NET8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        public CurrencyController(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        //[AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetCurrency()
        {
            var list = await context.Currency.OrderBy(c => c.Code).ToListAsync();
            return Ok(mapper.Map<List<Currency>, List<CurrencyResource>>(list));
        }

        [HttpPost]
        public async Task<IActionResult> PostCurrency([FromBody] Currency payload)
        {
            Currency newRegister = new Currency();
            newRegister.Id = Guid.NewGuid();
            newRegister.Code = payload.Code;
            newRegister.Description = payload.Description;

            context.Currency.Add(newRegister);
            context.SaveChanges();

            return Ok(newRegister);
        }


        [HttpPut("{id}")]

        public async Task<IActionResult> PutCurrency(Guid id, [FromBody] CurrencyResource resource)

        {

            var register = await context.Currency.Where(c => c.Id == id).FirstOrDefaultAsync(); //Para que devuelva lo primero que encuentre

            mapper.Map<CurrencyResource, Currency>(resource, register); //Actualiza

            await context.SaveChangesAsync();

            var result = mapper.Map<CurrencyResource, Currency>(resource);

            return Ok(result);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurrency(Guid id)
        {
            var register = await context.Currency.Where(c => c.Id == id).ExecuteDeleteAsync();
            return Ok();

        }

    }
}