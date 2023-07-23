using HardwareStoreMng.Context;
using HardwareStoreMng.DTO;
using HardwareStoreMng.Models;
using Microsoft.EntityFrameworkCore;

namespace HardwareStoreMng.Repositories.InvoiceRepository
{
    public class InvoiceImplemintation : InvoiceInterface
    {
        public readonly HardwareStoreMngContext _context;

        public InvoiceImplemintation(HardwareStoreMngContext context)
        {
            _context = context;
        }

        public void AddNewInvoice(InvoiceDTO invoiceDto)
        {
            var newInovoice = new Invoice
            {
                InvoiceId = invoiceDto.InvoiceId,
                InvoiceNumber = invoiceDto.InvoiceNumber,
                TotalPrice = invoiceDto.TotalPrice,
                Date = invoiceDto.Date,
                CustomerId=invoiceDto.CustomerId,
                EmployeeId=invoiceDto.EmployeeId,

            };


            _context.Invoice.Add(newInovoice);
            _context.SaveChanges();
        }

        public void DeleteInvoice(int invoiceId)
        {
            var existingInvoice = _context.Invoice.Find(invoiceId);

            if (existingInvoice == null)
            {

                return;
            }

            _context.Invoice.Remove(existingInvoice);
            _context.SaveChanges();
        }

        public IEnumerable<InvoiceDTO> GetAllInvoices()
        {
            var invoices = _context.Invoice.ToList();
            var invoiceDtos = invoices.Select(p => new InvoiceDTO
            {
                InvoiceId = p.InvoiceId,
                InvoiceNumber = p.InvoiceNumber,
                TotalPrice = p.TotalPrice,
                Date = p.Date,
                CustomerId= p.CustomerId,
                EmployeeId= p.EmployeeId

            });
            return invoiceDtos;
        }

        public InvoiceDTO GetInvoiceById(int invoiceId)
        {
            var invoice = _context.Invoice.FirstOrDefault(p => p.InvoiceId == invoiceId);

            if (invoice == null)
            {
                return null;
            }
            var invoiceDto = new InvoiceDTO
            {
                InvoiceId = invoice.InvoiceId,
                InvoiceNumber = invoice.InvoiceNumber,
                TotalPrice = invoice.TotalPrice,
                Date = invoice.Date,
                CustomerId= invoice.CustomerId,
                EmployeeId= invoice.EmployeeId
            };

            return invoiceDto;
        }

        public void UpdateInvoice(InvoiceDTO invoiceDto)
        {
            var existingInoice = _context.Invoice.Find(invoiceDto.InvoiceId);

            if (existingInoice == null)
            {
                return;
            }
            existingInoice.TotalPrice = invoiceDto.TotalPrice;
            _context.SaveChanges();
        }
    }
}
