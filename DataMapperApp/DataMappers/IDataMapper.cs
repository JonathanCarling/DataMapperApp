namespace DataMapperApp.DataMappers
{
    using DataMapperApp.Models;

    public interface IDataMapper
    {
        Entity? GetEntity(int id);

        IEnumerable<Entity> GetEntities();

        int CreateEntity(Entity entity);

        int UpdateEntity(Entity entity);

        int DeleteEntity(int id);
    }
}