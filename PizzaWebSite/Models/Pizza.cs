using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PizzaWebSite.Models
{
    public class Pizza
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title Name is required.")]
        [StringLength(maximumLength: 50, MinimumLength = 2)]
        public string Nom { get; set; }
        public Pate Pate { get; set; }
        [Required(ErrorMessage = "Ingredients is required.")]
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    }
}
