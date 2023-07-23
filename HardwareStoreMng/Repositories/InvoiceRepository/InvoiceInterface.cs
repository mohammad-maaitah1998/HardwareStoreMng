using HardwareStoreMng.DTO;

namespace HardwareStoreMng.Repositories.InvoiceRepository
{
    public interface InvoiceInterface
    {
        IEnumerable<InvoiceDTO> GetAllInvoices();
        InvoiceDTO GetInvoiceById(int invoiceId);
        void AddNewInvoice(InvoiceDTO invoiceDto);
        void UpdateInvoice(InvoiceDTO invoiceDto);
        void DeleteInvoice(int invoiceId);
    }
}
