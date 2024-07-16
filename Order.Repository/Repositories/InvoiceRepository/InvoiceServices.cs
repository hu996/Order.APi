using Microsoft.EntityFrameworkCore;
using Order.Core.Entities;
using Order.Repository.Data;

namespace Order.Repository.Repositories.InvoiceRepository
{
    public class InvoiceServices : IInvoiceRepo
    {
        private readonly ApplicationDbContext? _context;

        public async Task<Invoice> CreateInvoiceAsync(Invoice Invc)
        {
            await _context.invoices.AddAsync(Invc);
            await _context.SaveChangesAsync();
            return Invc;
        }

        public async Task<List<Invoice>> GetIAllnvoidAsync()
        {
            try
            {
                return await _context.invoices.AsNoTracking().ToListAsync();    
            }
            catch(Exception ex)
            {

                throw new Exception("ther No innvoces",ex);
            }
          
        }

        public async Task<Invoice> GetInvoidByIdAsync(int id)
        {
            try
            {
                return await _context.invoices.FindAsync(id);
            }

            catch (Exception ex)
            {
                throw new Exception($"No Invoices With This {id}");
            }
        }
    }
}
