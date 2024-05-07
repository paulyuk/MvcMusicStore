using MvcMusicStore.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Http;

namespace MvcMusicStore.Controllers
{
    public class ApiController : System.Web.Http.ApiController
    {
        private MusicStoreEntities db = new MusicStoreEntities();

        [HttpGet]
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

        [HttpGet]
        [Route("api/genres")]
        public IQueryable<GenreServiceModel> GetGenres()
        {
            return db.Genres.OrderBy(x => x.Name)
                            .Select(x => new GenreServiceModel
                            {
                                Name = x.Name,
                                Description = x.Description
                            });
        }

        [HttpGet]
        [Route("api/cart/{shoppingCartId}")]
        public ShoppingCartServiceModel GetCart(string shoppingCartId)
            => ShoppingCartServiceModel.MapFromCart(ShoppingCart.GetCart(db, shoppingCartId));

        [HttpPost]
        [Route("api/cart/{shoppingCartId}")]
        public ShoppingCartServiceModel AddItemToCart(string shoppingCartId, ShoppingCartItemServiceModel item)
        {
            var shoppingCart = ShoppingCart.GetCart(db, shoppingCartId);
            shoppingCart.AddToCart(item.AlbumId, item.Quantity);
            db.SaveChanges();

            shoppingCart = ShoppingCart.GetCart(db, shoppingCartId);
            return ShoppingCartServiceModel.MapFromCart(shoppingCart);
        }

        [HttpPost]
        [Route("api/cart/{shoppingCartId}/order")]
        public OrderSubmittedServiceModel SubmitOrder(string shoppingCartId, OrderServiceModel order)
        {
            var shoppingCart = ShoppingCart.GetCart(db, shoppingCartId);

            var newOrder = new Order
            {
                Address = order.Address,
                City = order.City,
                Country = order.Country,
                Email = order.Email,
                FirstName = order.FirstName,
                LastName = order.LastName,
                OrderDate = DateTime.Now,
                Phone = order.Phone,
                PostalCode = order.PostalCode,
                State = order.State,
                Username = shoppingCartId // this presumes the shopping cart id is the user id for the api scenario
            };

            db.Orders.Add(newOrder);

            shoppingCart.CreateOrder(newOrder);

            db.SaveChanges();

            return new OrderSubmittedServiceModel 
            { 
                OrderId = newOrder.OrderId,
                TotalCharged = newOrder.Total
            };
        }
    }

    public class AlbumServiceModel
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Genre { get; set; }
        public int AlbumId { get; set; }
    }

    public class GenreServiceModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class ShoppingCartItemServiceModel
    {
        public int AlbumId { get; set; }
        public int Quantity { get; set; }
    }
    public class ShoppingCartServiceModel
    {
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItemServiceModel> CartItems { get; set; }

        internal static ShoppingCartServiceModel MapFromCart(ShoppingCart shoppingCart) =>
            new ShoppingCartServiceModel
            {
                ShoppingCartId = shoppingCart.ShoppingCartId,
                CartItems = shoppingCart.GetCartItems().Select(x => new ShoppingCartItemServiceModel
                {
                    AlbumId = x.AlbumId,
                    Quantity = x.Count
                }).ToList()
            };
    }

    public class OrderServiceModel
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
    }

    public class OrderSubmittedServiceModel
    {
        public int OrderId { get; set; }
        public decimal TotalCharged { get; set; }
    }
}