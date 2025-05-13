using Net.Delivery.Order.Domain.Entities;
using System;

namespace Net.Delivery.Order.Domain.Services
{
    /// <summary>
    /// Notifier service
    /// </summary>
    public class NotifierService : INotifierService
    {
        /// <summary>
        /// Notifies the customer about some order update
        /// </summary>
        /// <param name="customer">customer data</param>
        public void Notify(Customer customer)
        {
            SendEmail(customer.Email);

        }

        public void NotifySMS(Customer customer)
        {
            SendSMS(customer.PhoneNumber);
        }

        /// <summary>
        /// Sends email about order update to customer
        /// </summary>
        /// <param name="customer">Customer data</param>
        private void SendEmail(string email)
        {
            Console.WriteLine("Email sent to recipient: " + email);
        }

        /// <summary>
        /// Sends SMS about order update to customer
        /// </summary>
        /// <param name="customer">Customer data</param>
        private void SendSMS(string phoneNumber)
        {
            Console.WriteLine("SMS sent to recipient: " + phoneNumber);
        }
    }
}
