// Program to implement the Builder Design Pattern for complex objects
using System;

class Product
{
    public string PartA { get; set; }
    public string PartB { get; set; }
    public string PartC { get; set; }

    public void Show()
    {
        Console.WriteLine($"Product Parts: PartA={PartA}, PartB={PartB}, PartC={PartC}");
    }
}

abstract class Builder
{
    protected Product product = new Product();
    public abstract void BuildPartA();
    public abstract void BuildPartB();
    public abstract void BuildPartC();
    public Product GetProduct() => product;
}

class ConcreteBuilder : Builder
{
    public override void BuildPartA() => product.PartA = "Engine";
    public override void BuildPartB() => product.PartB = "Wheels";
    public override void BuildPartC() => product.PartC = "Body";
}

class Director
{
    private Builder builder;
    public Director(Builder builder) => this.builder = builder;
    public void Construct()
    {
        builder.BuildPartA();
        builder.BuildPartB();
        builder.BuildPartC();
    }
}

class Program
{
    static void Main()
    {
        Builder builder = new ConcreteBuilder();
        Director director = new Director(builder);
        director.Construct();

        Product product = builder.GetProduct();
        product.Show();
    }
}