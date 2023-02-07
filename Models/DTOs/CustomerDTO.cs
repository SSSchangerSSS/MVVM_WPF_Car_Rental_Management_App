using Models.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Models.CustomerModels;

namespace Models.DTOs
{
    /// <summary>
    /// <see cref="CustomerDTO"/> class is a data transfer object for <see cref="Customer"/> model,
    /// the purpose of this class is to seperate logic from <see cref="CustomerDTO"/> which is a class that communicates with data base throw dbset
    /// </summary>
    public class CustomerDTO : IIdentifier
    {
        [Key]
        [Column("CustomerId")]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        public byte Age { get; set; }
        [Required]
        [MaxLength(11)]
        public string Mobile { get; set; }
    }
}
