namespace DataMapperApp.DataMappers
{
    using DataMapperApp.Configuration;
    using DataMapperApp.Helpers;
    using DataMapperApp.Models;
    using Microsoft.Extensions.Options;
    using System.Data.SqlClient;

    public class AdoNetDataMapper : DataMapper, IDataMapper
    {
        public AdoNetDataMapper(IOptions<AppSettings> options) : base(options)
        {
            DataConnectionString = options.Value.DataConnectionString;
        }

        public Entity? GetEntity(int id)
        {
            Entity? entity = null;
            var cmdText = SqlCommandHelper.BuildCommand(
                SqlCommandHelper.CommandType.SELECT,
                id.ToString()
            );

            using (var conn = new SqlConnection(DataConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(cmdText, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            entity = new Entity
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Created = reader.GetDateTime(reader.GetOrdinal("Created")),
                                Updated = reader.GetDateTime(reader.GetOrdinal("Updated")),
                            };
                        }
                    }
                }
            }

            return entity;
        }

        public IEnumerable<Entity> GetEntities()
        {
            List<Entity> entities = new List<Entity>();
            var cmdText = SqlCommandHelper.BuildCommand(SqlCommandHelper.CommandType.SELECT_ALL);

            using (var conn = new SqlConnection(DataConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(cmdText, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var entity = new Entity
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    Name = reader.GetString(reader.GetOrdinal("Name")),
                                    Created = reader.GetDateTime(reader.GetOrdinal("Created")),
                                    Updated = reader.GetDateTime(reader.GetOrdinal("Updated")),
                                };
                                entities.Add(entity);
                            }
                        }
                    }
                }
            }

            return entities;
        }

        public int CreateEntity(Entity entity)
        {
            int rowsAffected = 0;

            var dateTimeUtcNow = DateTime.UtcNow.ToString("s");
            var cmdText = SqlCommandHelper.BuildCommand(
                SqlCommandHelper.CommandType.INSERT,
                new string[] { entity.Name, dateTimeUtcNow, dateTimeUtcNow }
            );

            using (var conn = new SqlConnection(DataConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(cmdText, conn))
                {
                    rowsAffected = cmd.ExecuteNonQuery();
                }

            }
            return rowsAffected;
        }

        public int UpdateEntity(Entity entity)
        {
            int rowsAffected = 0;

            var dateTimeUtcNow = DateTime.UtcNow.ToString("s");
            var cmdText = SqlCommandHelper.BuildCommand(
                SqlCommandHelper.CommandType.UPDATE,
                new string[] { entity.Id.ToString(), entity.Name, dateTimeUtcNow, dateTimeUtcNow }
            );

            using (var conn = new SqlConnection(DataConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(cmdText, conn))
                {
                    rowsAffected = cmd.ExecuteNonQuery();
                }

            }
            return rowsAffected;
        }

        public int DeleteEntity(int id)
        {
            int rowsAffected = 0;

            var dateTimeUtcNow = DateTime.UtcNow.ToString("s");
            var cmdText = SqlCommandHelper.BuildCommand(
                SqlCommandHelper.CommandType.DELETE,
                new string[] { id.ToString() }
            );

            using (var conn = new SqlConnection(DataConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(cmdText, conn))
                {
                    rowsAffected = cmd.ExecuteNonQuery();
                }

            }
            return rowsAffected;
        }
    }
}
