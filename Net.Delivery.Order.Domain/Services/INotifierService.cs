using Net.Delivery.Order.Domain.Entities;

namespace Net.Delivery.Order.Domain.Services
{
    /// <summary>
    /// Notifier contract service
    /// </summary>
    public interface INotifierService
    {

        /// <summary>
        /// Notifies the customer about some order update
        /// </summary>
        /// <param name="order">Order data</param>
        void Notify(Customer customer);
    }
}
