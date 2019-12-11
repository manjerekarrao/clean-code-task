using Naming.Task1.ThirdParty;

namespace Naming.Task1
{
    public class CollectOrderService : IOrderService
    {
        private readonly ICollectionService _collectionService;
        private readonly INotificationManager _notificationService;

        public CollectOrderService(ICollectionService collectionService, INotificationManager notificationManager)
        {
            _collectionService = collectionService;
            _notificationService = notificationManager;
        }

        public void SubmitOrder(IOrder order)
        {
            if (_collectionService.IsEligibleForCollect(order))
            {
                _notificationService.NotifyCustomer(Message.ReadyForCollect, 4); // 4 - info notification level
            }
            else
            {
                _notificationService.NotifyCustomer(Message.ImpossibleToCollect, 1); // 1 - critical notification level
            }
        }
    }
}
