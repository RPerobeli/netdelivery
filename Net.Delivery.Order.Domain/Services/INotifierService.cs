using Net.Delivery.Order.Domain.Entities;
using System.Threading.Tasks;

namespace Net.Delivery.Order.Domain.Services
{
    /// <summary>
    /// Notifier contract service
    /// </summary>
    public interface INotifierService
    {

        /// <summary>
        /// Notifies the customer about some order update
        /// Notifies the customer with email
        /// </summary>
        /// <param name="Customer">customer data</param>
        void Notify(Customer customer);

        /// <summary>
        /// Notifies customer with SMS
        /// </summary>
        /// <param name="Customer">customer data</param>
        void NotifySMS(Customer customer);
    }
}
