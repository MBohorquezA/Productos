namespace Productos.WebApi.Models.ViewModels
{
    public class VMProduct
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public double Price { get; set; }

        public int Stock { get; set; }
    }
}
