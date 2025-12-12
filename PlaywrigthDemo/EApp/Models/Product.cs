namespace PlaywrigthDemo.EApp.Models
{
    public class Product
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ProductType ProductType { get; set; }
    }

    public enum ProductType
    {
        CPU,
        MONITOR,
        PERIPHARALS,
        EXTERNAL
    }
}