using System;
using FatecSisMed.Web.Models;

namespace FatecSisMed.Web.Services.Interfaces
{
	public interface IConvenioService
	{
        Task<IEnumerable<ConvenioViewModel>>
            GetAllConvenios(string token);
        Task<ConvenioViewModel> FindConvenioById(int id, string token);
        Task<ConvenioViewModel>
            CreateConvenio(ConvenioViewModel convenioViewModel, string token);
        Task<ConvenioViewModel>
            UpdateConvenio(ConvenioViewModel convenioViewModel, string token);
        Task<bool> DeleteConvenioById(int id, string token);
    }
}

