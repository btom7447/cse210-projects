using System;

class Program
{
    static void Main(string[] args)
    {
        // Create addresses
        Address address1 = new Address("123 Main St", "New York", "NY", "USA");
        Address address2 = new Address("456 Elm St", "Toronto", "ON", "Canada");

        // Create customers
        Customer customer1 = new Customer("John Doe", address1);
        Customer customer2 = new Customer("Alice Smith", address2);

        // Create products
        Product product1 = new Product("Laptop", "P1234", 999.99, 1);
        Product product2 = new Product("Wireless Mouse", "P5678", 49.99, 2);
        Product product3 = new Product("Headphones", "P9101", 79.99, 1);
        Product product4 = new Product("Keyboard", "P1112", 89.99, 1);

        // Create first order
        Order order1 = new Order(customer1);
        order1.AddProduct(product1);
        order1.AddProduct(product2);

        // Create second order
        Order order2 = new Order(customer2);
        order2.AddProduct(product3);
        order2.AddProduct(product4);

        // Display details for Order 1
        Console.WriteLine("\n==============================");
        Console.WriteLine("ORDER 1 DETAILS:");
        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order1.GetTotalPrice():F2}");
        Console.WriteLine("==============================\n");

        // Display details for Order 2
        Console.WriteLine("==============================");
        Console.WriteLine("ORDER 2 DETAILS:");
        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order2.GetTotalPrice():F2}");
        Console.WriteLine("==============================\n");
    }
}

// Class representing an Address
class Address
{
    private string street;
    private string city;
    private string state;
    private string country;

    public Address(string street, string city, string state, string country)
    {
        this.street = street;
        this.city = city;
        this.state = state;
        this.country = country;
    }

    public bool IsInUSA()
    {
        return country.ToLower() == "usa";
    }

    public string GetFullAddress()
    {
        return $"{street}\n{city}, {state}\n{country}";
    }
}

// Class representing a Customer
class Customer
{
    private string name;
    private Address address;

    public Customer(string name, Address address)
    {
        this.name = name;
        this.address = address;
    }

    public bool LivesInUSA()
    {
        return address.IsInUSA();
    }

    public string GetName()
    {
        return name;
    }

    public string GetAddress()
    {
        return address.GetFullAddress();
    }
}

// Class representing a Product
class Product
{
    private string name;
    private string productId;
    private double pricePerUnit;
    private int quantity;

    public Product(string name, string productId, double pricePerUnit, int quantity)
    {
        this.name = name;
        this.productId = productId;
        this.pricePerUnit = pricePerUnit;
        this.quantity = quantity;
    }

    public double GetTotalCost()
    {
        return pricePerUnit * quantity;
    }

    public string GetProductInfo()
    {
        return $"{name} (ID: {productId})";
    }
}

// Class representing an Order
class Order
{
    private List<Product> products;
    private Customer customer;
    private const double USA_SHIPPING_COST = 5.00;
    private const double INTERNATIONAL_SHIPPING_COST = 35.00;

    public Order(Customer customer)
    {
        this.customer = customer;
        this.products = new List<Product>();
    }

    public void AddProduct(Product product)
    {
        products.Add(product);
    }

    public double GetTotalPrice()
    {
        double totalCost = 0;
        foreach (var product in products)
        {
            totalCost += product.GetTotalCost();
        }

        double shippingCost = customer.LivesInUSA() ? USA_SHIPPING_COST : INTERNATIONAL_SHIPPING_COST;
        return totalCost + shippingCost;
    }

    public string GetPackingLabel()
    {
        string label = "Packing Label:\n";
        foreach (var product in products)
        {
            label += $"- {product.GetProductInfo()}\n";
        }
        return label;
    }

    public string GetShippingLabel()
    {
        return $"Shipping Label:\n{customer.GetName()}\n{customer.GetAddress()}";
    }
}