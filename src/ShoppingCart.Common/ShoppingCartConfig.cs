using System.Configuration;
using ShoppingCart.Abstractions;

namespace ShoppingCart.Common
{
    public class ShoppingCartConfig : IConfig
    {
        public string GetDataSourcePath()
        {
            return ConfigurationManager.AppSettings["ShoppingCart.CsvFilePath"];
        }
    }
}
