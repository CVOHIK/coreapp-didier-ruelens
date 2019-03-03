using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStore.Data;

namespace MusicStore.Controllers
{
    public class StoreController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StoreController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListGenres()
        {
            var listgenres = _context.Genre.OrderBy(g => g.Name);
            return View(listgenres.ToList());
        }

        //public IActionResult ListAlbums(int id)
        //{
        //    //var GetAlbumById = _context.Album
        //    //    .Where(a => a.GenreID == id);
        //    //return View(GetAlbumById);

        //    var GetAlbumById = _context.Genre
        //        .Include(g => g.Albums)
        //        .Where(g => g.GenreID == id)
        //        //.FirstOrDefaultAsync();
        //    return View(GetAlbumById);
        //}

        public IActionResult ListAlbums()
        {
            var listalbums = _context.Album.OrderBy(a => a.Title);
            return View(listalbums.ToList());
        }

        //public IActionResult Details(int id)
        //{
        //    var album = _context.Album
        //        .SingleOrDefault(a => a.AlbumID == id);
        //    return View(album);
        //}

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Album
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .FirstOrDefaultAsync(m => m.AlbumID == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }
    }
}