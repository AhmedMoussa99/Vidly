using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private VidlyDbContext _context;

        public MoviesController()
        {
            _context = new VidlyDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ActionResult New()
        {
            var genres = _context.Genres.ToList();
            var viewmodel = new NewMovieViewModel
            {
                genres = genres
            };
            return View(viewmodel);
        }
        public ActionResult Edit(int Id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == Id);
            if (movie == null)
                return HttpNotFound();
            var viewModel = new NewMovieViewModel
            {
                Movie = movie,
                genres = _context.Genres.ToList()
            };
            return View("New", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewmodel = new NewMovieViewModel
                {
                   Movie = movie , 
                   genres = _context.Genres.ToList()
                  
                };
                return View("New", viewmodel);
            }
            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.ReleaseDate = movie.ReleaseDate;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Movies");

        }
        // GET: Movies
        public ActionResult Random()
        {
            var movie = new Movie() { Name = "sherk!" };
            var customers = new List<Customer>
            {
                new Customer{Name="customer 1"},
                 new Customer{Name="customer 2"}
            };
            var viewmodel = new RandomMovieViewModel
            {
                movie = movie,
                customers = customers

            };
            return View(viewmodel);

        }
        public ActionResult Index(int? pageindex, string sortby)
        {
            var movies = _context.Movies.Include(m => m.genre).ToList();
            return View(movies);
        }
        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.genre).SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return HttpNotFound();
            return View(movie);
        }
    }
}