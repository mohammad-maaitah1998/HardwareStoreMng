using HardwareStoreMng.Context;
using HardwareStoreMng.DTO;
using HardwareStoreMng.Models;
using StackExchange.Redis;

namespace HardwareStoreMng.Repositories.BarCodesRepository
{
    public class BarCodesImplemintation : BarCodesInterface
    {
        public readonly HardwareStoreMngContext _context;
        

        public BarCodesImplemintation(HardwareStoreMngContext context)
        {
            _context = context;
        }

        public void AddBarCode(BarCodesDTO barcodDto)
        {
            var newBarcode = new BarCodes
            {
                BarCodeId = barcodDto.BarCodeId,
                ProductId = barcodDto.ProductId,
                BarCodeName = barcodDto.BarCodeName
                

            };


            _context.BarCodes.Add(newBarcode);
            _context.SaveChanges();
        }

        public void DeleteBarCode(int barcodeId)
        {
            var existingBarcodeId = _context.BarCodes.Find(barcodeId);

            if (existingBarcodeId == null)
            {

                return;
            }

            _context.BarCodes.Remove(existingBarcodeId);
            _context.SaveChanges();
        }

        public IEnumerable<BarCodesDTO> GetAllBarCodes()
        {
            var barcodes = _context.BarCodes.ToList();
            var barcodesDtos = barcodes.Select(p => new BarCodesDTO
            {
                BarCodeId = p.BarCodeId,
                ProductId = p.ProductId,
                BarCodeName = p.BarCodeName
                

            });
            return barcodesDtos;
        }

        public List<BarCodesDTO> GetBarcodeById(int productId)
        {
            return _context.BarCodes
            .Where(item => item.ProductId == productId)
            .Select(item => new BarCodesDTO
            {

                BarCodeId = item.BarCodeId,
                ProductId = item.ProductId,
                BarCodeName = item.BarCodeName

            }).ToList();
        }

        public void UpdateBarcode(BarCodesDTO barcodDto)
        {

            var existingBarcode = _context.BarCodes.Find(barcodDto.BarCodeId);

            if (existingBarcode == null)
            {

                return;
            }

            existingBarcode.BarCodeName= barcodDto.BarCodeName;


            _context.SaveChanges();
        }
    }
}
