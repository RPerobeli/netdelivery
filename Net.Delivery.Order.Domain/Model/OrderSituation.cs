namespace Net.Delivery.Order.Domain.Model
{
    /// <summary>
    /// Possible order situations
    /// </summary>
    public enum OrderSituation
    {
        /// <summary>
        /// Order created
        /// </summary>
        CREATED,
        /// <summary>
        /// Order delivered
        /// </summary>
        DELIVERED,

        /// <summary>
        /// Order canceled
        /// </summary>
        CANCELED
    }
}
