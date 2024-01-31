using ApiCatalogo.Context;
using Microsoft.AspNetCore.Mvc;

namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context; // instância 

        public ProductsController(AppDbContext context)
        {
            _context = context;
        } // instância do context injetada no controlador
    }
}
