using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? Image { get; set; }

        public string? Description { get; set; }

        public float Price { get; set; }

        public int Category_Id { get; set; }
        public int Brand_Id { get; set; }

    }
}
