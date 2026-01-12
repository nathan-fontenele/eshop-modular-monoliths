namespace Catalog.Data.Seed;

public static class InitialData
{
    public static IEnumerable<Product> Products =>
        new List<Product>
        {
            Product.Create ("Iphone X", ["category1"], "Description", "", 1000)
        };
}