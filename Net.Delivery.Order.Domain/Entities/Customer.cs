using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Net.Delivery.Order.Domain.Enums;

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

        /// <summary>
        /// Customer's type
        /// </summary>
        [Column("TIPO_USUARIO")]
        public EUserType Type { get; set; }


        // Relacionamentos
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
