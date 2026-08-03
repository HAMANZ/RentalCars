using System.Collections.Generic;
using System.Threading.Tasks;
using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;

public interface ILoggerError
{
    Task<DynamicResponse<List<LoggerErrorDTO>>> GetAllAsync();
    Task<DynamicResponse<LoggerErrorDTO>> GetAsync(long id);
    Task<DynamicResponse<bool>> AddAsync(string methodName, string actionType, string parameters, string result);
    Task<DynamicResponse<bool>> DeleteAsync(long id);
    Task<DynamicResponse<bool>> UpdateAsync(LoggerErrorDTO toUpdate);
}
