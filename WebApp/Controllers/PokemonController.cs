using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class PokemonController : Controller
    {
        private PokemonDbContext db = new PokemonDbContext();

        // GET: Pokemon
        public ActionResult Index()
        {
            return View(db.Pokemons.ToList());
        }

        // GET: Pokemon/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pokemon pokemon = db.Pokemons.Find(id);
            if (pokemon == null)
            {
                return HttpNotFound();
            }
            return View(pokemon);
        }
    }
}
