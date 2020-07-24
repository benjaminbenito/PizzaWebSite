using PizzaWebSite.Models;
using PizzaWebSite.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PizzaWebSite.Controllers
{
    public class PizzaController : Controller
    {
        // GET: Pizza
        public ActionResult Index()
        {
            return View(FakeDbPizza.Instance.Pizza);
        }

        // GET: Pizza/Details/5
        public ActionResult Details(int id)
        {
            PizzaCreateViewModel vm = new PizzaCreateViewModel();
            vm.Pizza = FakeDbPizza.Instance.Pizza.FirstOrDefault(x => x.Id == id);

            if (vm.Pizza != null)
            {
                return View(vm);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Pizza/Create
        public ActionResult Create()
        {
            PizzaCreateViewModel vm = new PizzaCreateViewModel();
            vm.Ingredients = FakeDbPizza.Instance.IngredientsDisponibles.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Nom }).ToList();
            vm.Pates = FakeDbPizza.Instance.PatesDisponibles.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Nom }).ToList();

            return View(vm);
        }

        // POST: Pizza/Create
        [HttpPost]
        public ActionResult Create(PizzaCreateViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (FakeDbPizza.Instance.Pizza.Any(p => p.Nom.ToLower() == vm.Pizza.Nom.ToLower()))
                    {
                        ModelState.AddModelError("", "Pizza with this name alrealy exist.");
                        vm.Pates = FakeDbPizza.Instance.PatesDisponibles.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Nom }).ToList();
                        vm.Ingredients = FakeDbPizza.Instance.IngredientsDisponibles.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Nom }).ToList();
                        return View(vm);
                    }
                    Pizza pizza = vm.Pizza;
                    pizza.Pate = FakeDbPizza.Instance.PatesDisponibles.FirstOrDefault(x => x.Id == vm.IdSelectedPate);
                    pizza.Ingredients = FakeDbPizza.Instance.IngredientsDisponibles.Where(x => vm.IdSelectedIngredients.Contains(x.Id)).ToList();
                    pizza.Id = FakeDbPizza.Instance.Pizza.Count == 0 ? 1 : FakeDbPizza.Instance.Pizza.Max(x => x.Id) + 1;
                    FakeDbPizza.Instance.Pizza.Add(pizza);

                    return RedirectToAction("Index");
                }
                return View();

            }
            catch
            {
                vm.Pates = FakeDbPizza.Instance.PatesDisponibles.Select(x => new SelectListItem { Text = x.Nom, Value = x.Id.ToString() }).ToList();
                vm.Ingredients = FakeDbPizza.Instance.IngredientsDisponibles.Select(x => new SelectListItem { Text = x.Nom, Value = x.Id.ToString() }).ToList();

                return View(vm);
            }
        }

        // GET: Pizza/Edit/5
        public ActionResult Edit(int id)
        {   
            PizzaCreateViewModel vm = new PizzaCreateViewModel();
            vm.Pizza = FakeDbPizza.Instance.Pizza.FirstOrDefault(x => x.Id == id);
            vm.Ingredients = FakeDbPizza.Instance.IngredientsDisponibles.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Nom }).ToList();
            vm.IdSelectedIngredients = vm.Pizza.Ingredients.Select(x => x.Id).ToList();
            vm.Pates = FakeDbPizza.Instance.PatesDisponibles.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Nom }).ToList();
            vm.IdSelectedPate = vm.Pizza.Pate.Id;

            if (vm.Pizza != null)
            {
                return View(vm);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Pizza/Edit/5
        [HttpPost]
        public ActionResult Edit(PizzaCreateViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (FakeDbPizza.Instance.Pizza.Any(p => p.Nom.ToLower() == vm.Pizza.Nom.ToLower() && p.Id != vm.Pizza.Id ))
                    {
                        ModelState.AddModelError("", "Pizza with this name alrealy exist.");
                        vm.Pates = FakeDbPizza.Instance.PatesDisponibles.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Nom }).ToList();
                        vm.Ingredients = FakeDbPizza.Instance.IngredientsDisponibles.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Nom }).ToList();
                        return View(vm);
                    }
                    Pizza pizza = FakeDbPizza.Instance.Pizza.FirstOrDefault(x => x.Id == vm.Pizza.Id);
                    pizza.Nom = vm.Pizza.Nom;
                    pizza.Pate = FakeDbPizza.Instance.PatesDisponibles.FirstOrDefault(x => x.Id == vm.IdSelectedPate);
                    pizza.Ingredients = FakeDbPizza.Instance.IngredientsDisponibles.Where(x => vm.IdSelectedIngredients.Contains(x.Id)).ToList();

                    return RedirectToAction("Index");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Pizza/Delete/5
        public ActionResult Delete(int id)
        {
            PizzaCreateViewModel vm = new PizzaCreateViewModel();
            vm.Pizza = FakeDbPizza.Instance.Pizza.FirstOrDefault(x => x.Id == id);
            return View(vm);
        }

        // POST: Pizza/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Pizza pizza = FakeDbPizza.Instance.Pizza.FirstOrDefault(x => x.Id == id);
                FakeDbPizza.Instance.Pizza.Remove(pizza);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}