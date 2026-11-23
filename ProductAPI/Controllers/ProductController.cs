using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Data;
using ProductAPI.Models;


namespace ProductAPI.Controllers
{
    [Route("api/[controller]")] // Route Attribut


    [ApiController] // ApiController Attribut
    public class ProductController : ControllerBase // Webcontroller Klasse für PRefix Controller Product
    {
        // CRUD (Create, Read, Update, Delete)
        private readonly ApplicationDbContext _context; // Wert nur bei Deklaration oder in Konstruktor setzen

        public ProductController(ApplicationDbContext dbContext)
        {
            _context = dbContext; // Datenbank
        }


        [HttpPost] //Methode auf Webserver Ressource erzeugen für POST Endpunkt in Swagger
        public IActionResult CreateProduct([FromBody] Product product)
        {
            _context.Products.Add(product);

            _context.SaveChanges(); // wird gespeichert!

            // return Ok("hat geklappt"); // Antwort zurücksenden mit Statuscode 200 an Client

            return CreatedAtAction(nameof(CreateProduct), new { id = product.Id }, product); // Antwort zurücksenden mit Statuscode 201 an Client mit location: https://localhost:7093/api/Product?id=5 
        }


        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(_context.Products.ToList());

            // return Ok("API läuft!"); // Antwort zurücksenden mit Statuscode 200 an Client
        }

        [HttpDelete]
        public IActionResult DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);

            if (product == null)
            {
                return NotFound("keine Produkte gefunden"); // Statuscode 404 Not Found
            }

            _context.Products.Remove(product);
            _context.SaveChanges();

            return NoContent();
        }


        [HttpPut]
        public IActionResult UpdateProduct(int id, [FromBody] Product product)
        {
            var productInDb = _context.Products.Find(id);


            if (product == null)
            {
                return NotFound("keine Produkte gefunden"); // Statuscode 404 Not Found
            }

            productInDb.Name = product.Name;
            productInDb.Price = product.Price;

            _context.SaveChanges();

            return Ok();

        }


    }
}
