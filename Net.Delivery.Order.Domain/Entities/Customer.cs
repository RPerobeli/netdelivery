using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Net.Delivery.Order.Domain.Entities
{
    /// <summary>
    /// Customer entity
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Customer's Id - AutoIncrement PK
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public long Id { get; set; }


        /// <summary>
        /// Customer's name
        /// </summary>
        [Column("NAME")]
        public string Name { get; set; }

        /// <summary>
        /// Customer's phone number
        /// </summary>
        [Column("PHONE_NUMBER")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Customer's e-mail
        /// </summary>
        [Column("EMAIL")]
        public string Email { get; set; }


        // Relacionamentos
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
