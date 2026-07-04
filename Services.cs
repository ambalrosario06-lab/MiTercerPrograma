using MiTercerPrograma.Models;

namespace MiTercerPrograma.Services
{
    class ProductService
    {
        private readonly List<Product> _products =
        [
            new Product
            {
                Id = 1,
                Name = "Inspiron 15",
                Category = "Computadora",
                Price = 650
            },
            new Product
            {
                Id = 2,
                Name = "G203",
                Category = "Mouse",
                Price = 35
            },
            new Product
            {
                Id = 3,
                Name = "K552",
                Category = "Teclado",
                Price = 75
            },
            new Product
            {
                Id = 4,
                Name = "MacBook Air",
                Category = "Computadora",
                Price = 999
            },
            new Product
            {
                Id = 5,
                Name = "Odyssey G3",
                Category = "Monitor",
                Price = 220
            }
        ];

        public List<Product> FindAll()
        {
            return _products;
        }

        public bool Delete(int id)
        {
            Product? product = _products.FirstOrDefault(c => c.Id == id);

            if (product is null) return false;

            _products.Remove(product);
            return true;
        }

        public bool Create(Product product)
        {
            _products.Add(product);
            return true;
        }
    }
}