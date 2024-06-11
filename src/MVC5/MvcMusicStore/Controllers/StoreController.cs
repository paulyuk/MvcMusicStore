using MvcMusicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcMusicStore.Controllers
{
    public class StoreController : Controller
    {
        MusicStoreEntities storeDB = new MusicStoreEntities();
        //
        // GET: /Store/

        public ActionResult Index()
        {
            var genres = storeDB.Genres.ToList();

            return View(genres);
        }


        //
        // GET: /Store/Browse?genre=Disco

        private Genre GetGenre(string genre)
        {
            var query = storeDB.Genres.Include("Albums").ToList();
            var genreModel = query.Single(g => g.Name == genre);
            return genreModel;
        }

        public ActionResult Details(int id) 
        {
            var album = storeDB.Albums.Find(id);

            return View(album);
        }

        [ChildActionOnly]
        public ActionResult GenreMenu()
        {
            var genres = storeDB.Genres
                .OrderByDescending(
                    g => g.Albums.Sum(
                    a => a.OrderDetails.Sum(
                    od => od.Quantity)))
                .Take(9)
                .ToList();

            return PartialView(genres);
        }


        public ActionResult Browse(string genre)
        {
            // For consistent Code Optimizations recommendations,introduces load to the DB call
            // ...so there is no need to run additional Load Tests, as the App Insights Availability
            // tests are sufficient.
            // This is just for demo purposes

            for (int i = 0; i < 200; i++)
            {
                var genreModelLoop = GetGenre(genre);
            }

            var genreModel = storeDB.Genres.Include("Albums")
                .Single(g => g.Name == genre);
            return View(genreModel);
        }
    }
}