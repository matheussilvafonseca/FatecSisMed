using FatecSisMed.Web.Models;

namespace FatecSisMed.Web.Services.Interfaces
{
    public interface IRemedioService
    {
        Task<IEnumerable<RemedioViewModel>> GetAllRemedios(string token);
        Task<RemedioViewModel> FindRemedioById(int id, string token);
        Task<RemedioViewModel>
            CreateRemedio(RemedioViewModel remedioViewModel, string token);
        Task<RemedioViewModel>
            UpdateRemedio(RemedioViewModel remedioViewModel, string token);
        Task<bool> DeleteRemedioById(int id, string token);
    }
}
