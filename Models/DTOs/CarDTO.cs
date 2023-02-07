using Models.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Models.CarModels;

namespace Models.DTOs
{
    /// <summary>
    /// <see cref="CarDTO"/> class is a data transfer object for <see cref="Car"/> model,
    /// the purpose of this class is to seperate logic from <see cref="CarDTO"/> which is a class that communicates with data base throw dbset
    /// </summary>
    public class CarDTO : IIdentifier
    {
        [Key]
        [Column("CarId")]
        public Guid Id { get; set; }
        [Required]
        [Column(TypeName = "date")]
        public DateTime CreationDate { get; set; }
        [Required] 
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string Color { get; set; }
    }
}
