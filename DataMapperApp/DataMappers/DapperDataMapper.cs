namespace DataMapperApp.DataMappers
{
    using Dapper;
    using DataMapperApp.Configuration;
    using DataMapperApp.Helpers;
    using DataMapperApp.Models;
    using Microsoft.Extensions.Options;
    using System.Data.SqlClient;

    public class DapperDataMapper : DataMapper, IDataMapper
    {
        public DapperDataMapper(IOptions<AppSettings> options) : base(options)
        {
            DataConnectionString = options.Value.DataConnectionString;
        }

        public Entity? GetEntity(int id)
        {
            Entity? entity;
            var cmd = SqlCommandHelper.BuildCommand(
                SqlCommandHelper.CommandType.SELECT,
                new string[] { id.ToString() }
            );

            using (var conn = new SqlConnection(DataConnectionString))
            {
                entity = conn.QuerySingleOrDefault<Entity>(cmd);
            }

            return entity;
        }

        public IEnumerable<Entity> GetEntities()
        {
            IEnumerable<Entity> entities;
            var cmd = SqlCommandHelper.BuildCommand(SqlCommandHelper.CommandType.SELECT_ALL);

            using (var conn = new SqlConnection(DataConnectionString))
            {
                entities = conn.Query<Entity>(cmd);
            }

            return entities;
        }

        public int CreateEntity(Entity entity)
        {
            int rowsAffected = 0;

            var dateTimeUtcNow = DateTime.UtcNow.ToString("s");
            var cmd = SqlCommandHelper.BuildCommand(
                SqlCommandHelper.CommandType.INSERT,
                new string[] { entity.Name, dateTimeUtcNow, dateTimeUtcNow }
            );

            using (var conn = new SqlConnection(DataConnectionString))
            {
                rowsAffected = conn.Execute(cmd);
            }

            return rowsAffected;
        }

        public int UpdateEntity(Entity entity)
        {
            int rowsAffected = 0;

            var dateTimeUtcNow = DateTime.UtcNow.ToString("s");
            var cmd = SqlCommandHelper.BuildCommand(
                SqlCommandHelper.CommandType.UPDATE,
                new string[] { entity.Id.ToString(), entity.Name, dateTimeUtcNow, dateTimeUtcNow }
            );

            using (var conn = new SqlConnection(DataConnectionString))
            {
                rowsAffected = conn.Execute(cmd);
            }

            return rowsAffected;
        }

        public int DeleteEntity(int id)
        {
            int rowsAffected = 0;

            var dateTimeUtcNow = DateTime.UtcNow.ToString("s");
            var cmd = SqlCommandHelper.BuildCommand(
                SqlCommandHelper.CommandType.DELETE,
                new string[] { id.ToString() }
            );

            using (var conn = new SqlConnection(DataConnectionString))
            {
                rowsAffected = conn.Execute(cmd);
            }

            return rowsAffected;
        }
    }
}