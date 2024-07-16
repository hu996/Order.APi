using Microsoft.AspNetCore.SignalR;
using Order.Core.Entities;
using Order.Repository.Data;
using Order.Repository.Repositories.InvoiceRepository;

namespace Order.APi.Hubs
{
    public class OrderHub:Hub
    {
        private readonly IInvoiceRepo _invoiceRepo;
        private readonly ApplicationDbContext _context;
        public OrderHub(IInvoiceRepo invoiceRepo, ApplicationDbContext context)
        {
            _invoiceRepo = invoiceRepo;
        }
        public async Task UpdateOrder(int orderId, string status)
        {
            if(status == "Paid")
            {
                var x = _context.orders.Find(orderId);
                if (x is not null)
                {
                    var invc = new Invoice
                    {
                        InvoiceDate = DateTime.Now,
                        TotalAmount = x.TotalAmount,
                        OrderId = orderId,
                    };

                    var creatinvc = await _invoiceRepo.CreateInvoiceAsync(invc);
                }
            }
        

            await Clients.All.SendAsync("UpdateStatus", orderId, status);
        }
    }
}
