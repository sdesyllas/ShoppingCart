using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using ShoppingCart.Abstractions;
using ShoppingCart.Domain;

namespace ShoppingCart.Common
{
    public class CsvDataSource : IDataSource
    {
        public IEnumerable<Product> LoadProducts(string path)
        {
            var csv = new CsvReader(File.OpenText(path));
            var products = csv.GetRecords<Product>();
            
            return products.ToList();
        }
    }
}
