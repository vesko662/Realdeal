using Realdeal.Models.Advert;
using System.Collections.Generic;

namespace Realdeal.Service.Observe
{
    public interface IObserveService
    {
        public bool StartObservingAdvert(string advertId,bool emailNothification);
        public bool StopObservingAdvert(string advertId);
        public IEnumerable<AdvertShowingViewModel> GetAllObservingAdverts();
        public void RemoveAllObservingUsers(string advertId);
        public bool IsAdvertObserved(string advertId);
        public void SendEmailOUpdate(string advertId, string title, string content);
    }
}
