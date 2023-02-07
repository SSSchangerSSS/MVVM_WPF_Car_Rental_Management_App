using Models.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Models.Models.RentalModels;

namespace Models.DTOs
{
    /// <summary>
    /// <see cref="RentalDTO"/> class is a data transfer object for <see cref="Rental"/> model,
    /// the purpose of this class is to seperate logic from <see cref="RentalDTO"/> which is a class that communicates with data base throw dbset
    /// </summary>
    public class RentalDTO : IIdentifier
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid CarId { get; set; }
        [Required]
        public Guid CustomerId { get; set; }
        [Required]
        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }
        [Required]
        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }
        [ForeignKey("CarId")]
        public  CarDTO CarDTOs { get; set; }
        [ForeignKey("CustomerId")]
        public  CustomerDTO CustomerDTOs { get; set; }
    }
}
