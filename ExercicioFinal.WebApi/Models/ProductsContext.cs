using System.Data.Entity;

namespace ExercicioFinal.WebApi.Models
{
    public class ProductsContext : DbContext
    {
        public ProductsContext() : base("name=ProductsContext")
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}