using HardwareStoreMng.DTO;

namespace HardwareStoreMng.CashServices
{
    public interface IredisCashServices
    {
        T GetData<T>(string key);


        bool SetData<T>(string key, T value, DateTimeOffset expirationTime);


        object RemoveData(string key);
        
    }
}
