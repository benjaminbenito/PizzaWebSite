using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PizzaWebSite.Models;

namespace PizzaWebSite.Utils
{
    public class FakeDbPizza
    {
        private static FakeDbPizza _instance;
        static readonly object instanceLock = new object();

        private FakeDbPizza()
        {
            pizza = this.GetPizza();
        }

        public static FakeDbPizza Instance
        {
            get
            {
                if (_instance == null) //Les locks prennent du temps, il est préférable de vérifier d'abord la nullité de l'instance.
                {
                    lock (instanceLock)
                    {
                        if (_instance == null) //on vérifie encore, au cas où l'instance aurait été créée entretemps.
                            _instance = new FakeDbPizza();
                    }
                }
                return _instance;
            }
        }

        private List<Pizza> pizza;

        public List<Pizza> Pizza
        {
            get { return pizza; }
        }

        private List<Pizza> GetPizza()
        {
            var i = 1;
            return new List<Pizza>
            {
                new Pizza{Id=i++,Nom = "test",Pate = this.PatesDisponibles.FirstOrDefault(x => x.Id == 1),Ingredients= new List<Ingredient> {} },
                new Pizza{Id=i++,Nom = "test2",Pate = this.PatesDisponibles.FirstOrDefault(x => x.Id == 3),Ingredients = new List<Ingredient>()},
            };
        }

        public List<Ingredient> IngredientsDisponibles { get; } = new List<Ingredient>
        {
            new Ingredient(){Id=1,Nom="Mozzarella"},
            new Ingredient(){Id=2,Nom="Jambon"},
            new Ingredient(){Id=3,Nom="Tomate"},
            new Ingredient(){Id=4,Nom="Oignon"},
            new Ingredient(){Id=5,Nom="Cheddar"},
            new Ingredient(){Id=6,Nom="Saumon"},
            new Ingredient(){Id=7,Nom="Champignon"},
            new Ingredient(){Id=8,Nom="Poulet"}
        };

        public List<Pate> PatesDisponibles { get; } = new List<Pate>
        {
            new Pate(){ Id=1,Nom="Pate fine, base crême"},
            new Pate(){ Id=2,Nom="Pate fine, base tomate"},
            new Pate(){ Id=3,Nom="Pate épaisse, base crême"},
            new Pate(){ Id=4,Nom="Pate épaisse, base tomate"}
        };
    }
}