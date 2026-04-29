// Program to map an Entity to a DTO manually using Reflection
using System;
using System.Reflection;

class UserEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}

class UserDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
}

class Mapper
{
    public static TDestination Map<TSource, TDestination>(TSource source) where TDestination : new()
    {
        TDestination destination = new TDestination();
        PropertyInfo[] sourceProps = typeof(TSource).GetProperties();
        PropertyInfo[] destProps = typeof(TDestination).GetProperties();

        foreach (var destProp in destProps)
        {
            foreach (var sourceProp in sourceProps)
            {
                if (destProp.Name == sourceProp.Name && destProp.PropertyType == sourceProp.PropertyType)
                {
                    destProp.SetValue(destination, sourceProp.GetValue(source));
                    break;
                }
            }
        }

        return destination;
    }
}

class Program
{
    static void Main()
    {
        UserEntity user = new UserEntity { Id = 1, Name = "Alice", Email = "alice@example.com" };
        UserDTO userDto = Mapper.Map<UserEntity, UserDTO>(user);

        Console.WriteLine($"DTO Id: {userDto.Id}, Name: {userDto.Name}");
    }
}