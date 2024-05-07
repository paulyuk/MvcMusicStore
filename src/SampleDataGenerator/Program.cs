using Bogus;
using SampleDataGenerator;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Web;

Console.WriteLine("Getting genres");

var httpClient = new HttpClient();
httpClient.BaseAddress = new Uri("https://mvcmusicstoredev001.azurewebsites.net");
var musicStoreApiClient = new MusicStoreApiClient(httpClient);

var options = new JsonSerializerOptions
{
    WriteIndented = true,
};

// get the genres
var genres = await musicStoreApiClient.GetGenresAsync();

// get the albums
var albums = await musicStoreApiClient.GetAlbumsAsync();

// generate a random user
var CreateNewCustomer = new Func<OrderServiceModel>(() =>
{
    var fakes = new Faker<OrderServiceModel>()
        .StrictMode(true)
        .RuleFor(x => x.Address, f => f.Address.StreetAddress())
        .RuleFor(x => x.Email, f => f.Internet.ExampleEmail())
        .RuleFor(x => x.City, f => f.Address.City())
        .RuleFor(x => x.Country, "USA")
        .RuleFor(x => x.FirstName, f => f.Person.FirstName)
        .RuleFor(x => x.LastName, f => f.Person.LastName)
        .RuleFor(x => x.Phone, f => f.Phone.PhoneNumber())
        .RuleFor(x => x.State, f => f.Address.StateAbbr())
        .RuleFor(x => x.PostalCode, f => f.Address.ZipCode())
        .RuleFor(x => x.Username, f => Regex.Replace(f.Internet.UserName(), @"[^\w\-]", "", RegexOptions.None));

    var user = fakes.Generate();
    return user;
});

// sets up a list of "favorites" so we have some obvious sales trends
var favoriteArtists = new[] { "AC/DC", 
    "Audioslave", 
    "Cake", 
    "Creedence Clearwater Revival", 
    "Daft Punk", 
    "David Bowie", 
    "Tool", 
    "Thievery Corporation", 
    "The Stone Roses", 
    "Stevie Ray Vaughan", 
    "Run DMC", 
    "R.E.M.", 
    "Radiohead"
};

// pick a few random albums
var GetRandomAlbumsForOrder = new Func<List<AlbumServiceModel>>(() =>
{
    var result = new List<AlbumServiceModel>();
    var randomCount = Random.Shared.Next(2, 10);
    for (int i = 1; i < randomCount; i++)
    {
        var randomAlbumId = Random.Shared.Next(1, albums.Count);
        var album = albums.First(x => x.AlbumId == randomAlbumId);
        result.Add(album);
    }
    return result;
});

// pick a random album by one of the favorite artists
var GetRandomFavorite = new Func<AlbumServiceModel>(() =>
{
    var randomFavorite = favoriteArtists[Random.Shared.Next(0, favoriteArtists.Length)];
    return albums.First(x => x.Artist == randomFavorite);
});

// submit the new order
var SubmitNewOrder = new Func<OrderServiceModel, List<AlbumServiceModel>, Task<OrderSubmittedServiceModel>>(async (order, albums) =>
{
    await musicStoreApiClient.GetCartAsync(order.Username);

    foreach (var album in albums)
    {
        await musicStoreApiClient.AddItemToCartAsync(order.Username, new ShoppingCartItemServiceModel
        {
            AlbumId = album.AlbumId,
            Quantity = 1
        });
    }

    return await musicStoreApiClient.SubmitOrderAsync(order.Username, order);
});

// create and submit a random order
var CreateRandomOrder = new Func<Task>(async () =>
{
    // create a new cart for a random user
    var randomOrder = CreateNewCustomer();
    Console.WriteLine(JsonSerializer.Serialize<OrderServiceModel>(randomOrder, options));

    // create some random albums
    var randomAlbums = GetRandomAlbumsForOrder();
    randomAlbums.Add(GetRandomFavorite());
    Console.WriteLine(JsonSerializer.Serialize<List<AlbumServiceModel>>(randomAlbums, options));

    // submit the order
    var result = await SubmitNewOrder(randomOrder, randomAlbums);
    Console.WriteLine(JsonSerializer.Serialize<OrderSubmittedServiceModel>(result, options));
});

for (var i = 0; i < 10; i++)
{
    await CreateRandomOrder();
}

Console.WriteLine("Finished");
Console.ReadLine();