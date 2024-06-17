using FatecSisMed.Web.Models;

namespace FatecSisMed.Web.Services.Interfaces
{
    public interface IMarcaService
    {
        Task<IEnumerable<MarcaViewModel>>GetAllMarcas(string token);
        Task<MarcaViewModel> FindMarcaById(int id, string token);
        Task<MarcaViewModel>
            CreateMarca(MarcaViewModel marcaViewModel, string token);
        Task<MarcaViewModel>
            UpdateMarca(MarcaViewModel marcaViewModel, string token);
        Task<bool> DeleteMarcaById(int id, string token);
    }
}
