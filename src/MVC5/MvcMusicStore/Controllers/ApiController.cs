using MvcMusicStore.Models;
using System.Data;
using System.Linq;
using System.Web.Http;

namespace MvcMusicStore.Controllers
{
    public class ApiController : System.Web.Http.ApiController
    {
        private MusicStoreEntities db = new MusicStoreEntities();

        [Route("api/albums")]
        public IQueryable<AlbumServiceModel> GetAlbums()
        {
            return db.Albums.Select(x => new AlbumServiceModel
            {
                Title = x.Title,
                Artist = x.Artist.Name,
                Genre = x.Genre.Name,
                AlbumId = x.AlbumId
            });
        }
    }

    public class AlbumServiceModel
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Genre { get; set; }
        public int AlbumId { get; set; }
    }
}