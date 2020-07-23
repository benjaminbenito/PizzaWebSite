using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzaWebSite.Models
{
    public class Pizza
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public Pate Pate { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    }
}
