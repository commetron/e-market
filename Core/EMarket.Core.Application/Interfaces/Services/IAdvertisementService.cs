using System.Collections.Generic;
using System.Threading.Tasks;
using EMarket.Core.Application.ViewModels.Advertisements;

namespace EMarket.Core.Application.Interfaces.Services
{
    public interface IAdvertisementService : IGenericService<SaveAdvertisementViewModel, AdvertisementViewModel>
    {
        Task<List<AdvertisementViewModel>> GetAllViewModelHome();
        Task<List<AdvertisementViewModel>> GetAllViewModelWithFilters(FilterAdvertisementViewModel filters);
    }
}
