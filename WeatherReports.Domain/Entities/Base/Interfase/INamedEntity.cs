namespace WeatherReports.Domain.Entities.Base.Interfase
{
    public interface INamedEntity : IEntity
    {
        string Name { get; set; }
    }
}
