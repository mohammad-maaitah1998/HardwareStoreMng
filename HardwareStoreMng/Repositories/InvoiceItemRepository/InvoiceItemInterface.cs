using HardwareStoreMng.DTO;

namespace HardwareStoreMng.Repositories.InvoiceItemRepository
{
    public interface InvoiceItemInterface
    {
        IEnumerable<InvoiceItemDTO> GetAllInvoiceItem();
        InvoiceItemDTO GetInvoiceItemtById(int invoiceId);
        public List<InvoiceItemDTO> GetInvoiceItemsByInvoiceId(int invoiceId);
        void AddInvoiceItem(InvoiceItemDTO invoiceitemDto);
        void UpdateInvoiceItem(InvoiceItemDTO invoiceitemDto);
        void DeleteInvoiceItem(int InvoiceId,int ProductId);

    }
}
