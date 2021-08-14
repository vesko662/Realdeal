using System.Collections.Generic;
using Realdeal.Models.Message;

namespace Realdeal.Service.Message
{
    public interface IMessageService
    {
        public IEnumerable<InboxMessageViewModel> GetInboxMessages();
    }
}
