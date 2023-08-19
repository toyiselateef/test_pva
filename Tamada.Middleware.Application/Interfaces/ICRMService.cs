 
namespace Tamada.Middleware.Application.Interfaces
{
    public interface ICRMService
    {
        Task<bool> LogCaseOnCrm(CrmCaseRequest request);
    }
}
