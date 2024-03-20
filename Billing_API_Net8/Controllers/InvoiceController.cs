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
    public class InvoiceController : ControllerBase
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        public InvoiceController(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        //[AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetInvoice()
        {
            var list = await context.Invoice.OrderBy(c => c.Date).ToListAsync();
            return Ok(mapper.Map<List<Invoice>, List<ClientResource>>(list));
        }

        [HttpPost]
        public async Task<IActionResult> PostInvoice([FromBody] Invoice payload)
        {
            var existingCurrency = await context.Currency.FindAsync(payload.CurrencyId);

            if (existingCurrency == null)
            {
                // Handle case where currency doesn't exist (e.g., throw error)
                return BadRequest("Invalid currency ID");
            }
            var existingClient = await context.Client.FindAsync(payload.ClientId);

            if (existingClient == null)
            {
                // Handle case where client doesn't exist (e.g., throw error)
                return BadRequest("Invalid client ID");
            }

            Invoice newRegister = new Invoice();
            newRegister.Id = Guid.NewGuid();
            newRegister.Date = payload.Date;
            newRegister.Amount = payload.Amount;
            newRegister.Description = payload.Description;
            newRegister.CurrencyId = existingCurrency.Id;
            newRegister.ClientId = existingClient.Id;

            context.Invoice.Add(newRegister);
            context.SaveChanges();

            return Ok(newRegister);
        }


        [HttpPut("{id}")]

        public async Task<IActionResult> PutInvoice(Guid id, [FromBody] InvoiceResource resource)

        {

            var register = await context.Invoice.Where(c => c.Id == id).FirstOrDefaultAsync(); //Para que devuelva lo primero que encuentre

            mapper.Map<InvoiceResource, Invoice>(resource, register); //Actualiza

            await context.SaveChangesAsync();

            var result = mapper.Map<InvoiceResource, Invoice>(resource);

            return Ok(result);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(Guid id)
        {
            var register = await context.Client.Where(c => c.Id == id).ExecuteDeleteAsync();
            return Ok();

        }

    }
}