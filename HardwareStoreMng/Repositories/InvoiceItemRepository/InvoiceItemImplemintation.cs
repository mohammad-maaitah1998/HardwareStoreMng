using HardwareStoreMng.Context;
using HardwareStoreMng.DTO;
using HardwareStoreMng.Models;

namespace HardwareStoreMng.Repositories.InvoiceItemRepository
{
    public class InvoiceItemImplemintation:InvoiceItemInterface
    {
        public readonly HardwareStoreMngContext _context;

        public InvoiceItemImplemintation(HardwareStoreMngContext context)
        {
            _context = context;
        }

        public void AddInvoiceItem(InvoiceItemDTO invoiceitemDto)
        {
            var newIvoiceItem = new InvoiceItem
            {
                InvoiceItemId = invoiceitemDto.InvoiceItemId,
                InvoiceId = invoiceitemDto.InvoiceId,
                ProductId = invoiceitemDto.ProductId,
                Quantity = invoiceitemDto.Quantity,

            };


            _context.InvoiceItem.Add(newIvoiceItem);
            _context.SaveChanges();
        }

        public void DeleteInvoiceItem(int invoiceid,int productId)
        {
            var invoiceItemToDelete = _context.InvoiceItem
             .FirstOrDefault(item => item.InvoiceId == invoiceid && item.ProductId == productId);

            if (invoiceItemToDelete != null)
            {
                _context.InvoiceItem.Remove(invoiceItemToDelete);
                _context.SaveChanges();
            }
        }

        public IEnumerable<InvoiceItemDTO> GetAllInvoiceItem()
        {
            var InvoiceItem = _context.InvoiceItem.ToList();
            var invoiceItemDtos = InvoiceItem.Select(p => new InvoiceItemDTO
            {
                InvoiceItemId = p.InvoiceItemId,
                InvoiceId = p.InvoiceId,
                ProductId = p.ProductId,
                Quantity = p.Quantity

            });
            return invoiceItemDtos;
        }

        public List<InvoiceItemDTO> GetInvoiceItemsByInvoiceId(int invoiceId)
        {
            return _context.InvoiceItem
            .Where(item => item.InvoiceId == invoiceId)
            .Select(item => new InvoiceItemDTO
            {

                InvoiceItemId = item.InvoiceItemId,
                InvoiceId = item.InvoiceId,
                ProductId = item.ProductId,
                Quantity = item.Quantity

            }).ToList();
            
        }

        public InvoiceItemDTO GetInvoiceItemtById(int invoiceId)
        {

            var InvoiceItem = _context.InvoiceItem.FirstOrDefault(p => p.InvoiceId == invoiceId);

            if (InvoiceItem == null)
            {
                return null;
            }


            var InvoiceitemDto = new InvoiceItemDTO
            {
                InvoiceItemId = InvoiceItem.InvoiceItemId,
                InvoiceId = InvoiceItem.InvoiceId,
                ProductId = InvoiceItem.ProductId,
                Quantity = InvoiceItem.Quantity
            };

            return InvoiceitemDto;
        }

        public void UpdateInvoiceItem(InvoiceItemDTO invoiceitemDto)
        {
            var updateinvoceitem = _context.InvoiceItem
            .Where(p => p.InvoiceId == invoiceitemDto.InvoiceId && p.ProductId == invoiceitemDto.ProductId);

            foreach (var invoiceItem in updateinvoceitem)
            {
                invoiceItem.Quantity = invoiceitemDto.Quantity;
            }

            _context.SaveChanges();
        }
    }
}
