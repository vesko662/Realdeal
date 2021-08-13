using Realdeal.Models.Advert;
using System.Collections.Generic;

namespace Realdeal.Service.Observe
{
    public interface IObserveService
    {
        public bool StartObservingAdvert(string advertId);
        public bool StopObservingAdvert(string advertId);
        public IEnumerable<AdvertShowingViewModel> GetAllObservingAdverts();
        public void RemoveAllObservingUsers(string advertId);
        public bool IsAdvertObserved(string advertId);
    }
}
