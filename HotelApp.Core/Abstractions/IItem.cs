namespace HotelApp.Core.Abstractions
{
    public interface IItem
    {
        int Id { get; }
        string Name { get; }
        decimal Price { get; }
        decimal GetFinalPrice(DateTime now);
    }
}
