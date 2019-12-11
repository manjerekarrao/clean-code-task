using System.Collections.Generic;
using ErrorHandling.Task1.ThirdParty;

namespace ErrorHandling.Task1
{
    public class UserReportBuilder
    {

        private IUserDao _userDao;

        public double? GetUserTotalOrderAmount(string userId)
        {

            if (_userDao == null)
                return null;

            IUser user = _userDao.GetUser(userId);
            if (user == null)
                return -1.0;

            IList<IOrder> orders = user.GetAllOrders();

            if (orders.Count == 0)
                return -2.0;

            double sum = 0.0;
            foreach (IOrder order in orders) {

                if (order.IsSubmitted()) {
                    double total = order.Total();
                    if (total < 0)
                        return -3.0;
                    sum += total;
                }
            }

            return sum;
        }


        public IUserDao GetUserDao()
        {
            return _userDao;
        }

        public void SetUserDao(IUserDao userDao)
        {
            this._userDao = userDao;
        }
    }
}
