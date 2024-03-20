using AutoMapper;
using Billing_API_Net8.Controllers.Resources;
using Billing_API_Net8.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Billing_API_NET8.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Domain to API Resource
            CreateMap<Client, ClientResource>();
            CreateMap<Invoice, InvoiceResource>();
            CreateMap<Currency, CurrencyResource>();

            //API Resource to Domain
            CreateMap<ClientResource, Client>();
            CreateMap<InvoiceResource, Invoice>();
            CreateMap<CurrencyResource, Currency>();
            //CreateMap<EcommerceConfigurationResource, EcommerceConfiguration>();

            //CreateMap<WebScrapLinkResource, WebScrapLink>()
            //    .ForMember(v => v.WebSearch, opt => opt.Ignore());

        }
    }
}
