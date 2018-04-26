using ExercicioFinal.WebApi.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;

namespace ExercicioFinal.WebApi.Controllers
{
    public class ProductsController : ODataController
    {
        ProductsContext db = new ProductsContext();

        private bool ProductExists(int id)
        {
            return db.Products.Any(t => t.Id == id);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        [EnableQuery]
        public IQueryable<Product> Get()
        {
            return db.Products;
        }

        public async Task<IHttpActionResult> Post(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Products.Add(product);
            await db.SaveChangesAsync();
            return Created(product);
        }
    }
}
