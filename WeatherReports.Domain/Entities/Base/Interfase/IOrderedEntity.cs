namespace WeatherReports.Domain.Entities.Base.Interfase
{
    public interface IOrderedEntity : IEntity
    {
        int Order { get; set; }
    }
}
