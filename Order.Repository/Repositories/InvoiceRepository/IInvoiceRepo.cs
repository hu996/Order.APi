using Order.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Repository.Repositories.InvoiceRepository
{
    public interface IInvoiceRepo
    {
        Task<Invoice>GetInvoidByIdAsync(int id);
        Task<Invoice>CreateInvoiceAsync(Invoice Invc );
        Task<List<Invoice>>GetIAllnvoidAsync();
        
    }
}
