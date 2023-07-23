using HardwareStoreMng.DTO;

namespace HardwareStoreMng.Repositories.BarCodesRepository
{
    public interface BarCodesInterface
    {
        IEnumerable<BarCodesDTO> GetAllBarCodes();
        public List<BarCodesDTO> GetBarcodeById(int productId);
        void AddBarCode(BarCodesDTO barcodDto);
        void UpdateBarcode(BarCodesDTO barcodDto);
        void DeleteBarCode(int barcodeId);
    }
}
