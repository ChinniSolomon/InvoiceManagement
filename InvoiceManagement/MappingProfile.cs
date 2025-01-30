using AutoMapper;
using InvoiceManagement.Models;


namespace InvoiceManagement
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Map from CreateInvoiceDto to Invoice
            CreateMap<CreateInvoiceDto, Invoice>()
                .ForMember(dest => dest.Items, opt => opt.Ignore());  // We will handle the items separately

            // Map from CreateInvoiceItemDto to InvoiceItem
            CreateMap<CreateInvoiceItemDto, InvoiceItem>();
        }
    }
}
