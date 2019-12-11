using Naming.Task4.ThirdParty;

namespace Naming.Task4.Service
{
    public class CustomerContactService : ICustomerContactService
    {
        private readonly ICustomerContactRepository _customerContactRepository;

        public CustomerContactService(ICustomerContactRepository customerContactRepository)
        {
            this._customerContactRepository = customerContactRepository;
        }

        public CustomerContact FindCustomerContactDetailsByCustomerId(float customerId)
        {
            return _customerContactRepository.FindById(customerId);
        }

        public void UpdateCustomerContactDetails(CustomerContact customerContact)
        {
            _customerContactRepository.Update(customerContact);
        }
    }
}
