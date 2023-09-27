namespace DataMapperApp.Controllers
{
    using DataMapperApp.DataMappers;
    using DataMapperApp.Models;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class EntityController : ControllerBase
    {
        private readonly IDataMapper _dataMapper;

        public EntityController(IDataMapper dataMapper)
        {
            _dataMapper = dataMapper;
        }

        [HttpGet("{id}", Name = "GetEntity")]
        public Entity? Get(int id)
        {
            return _dataMapper.GetEntity(id);
        }

        [HttpGet(Name = "GetEntities")]
        public IEnumerable<Entity> GetAll()
        {
            return _dataMapper.GetEntities();
        }

        [HttpPost(Name = "CreateEntity")]
        public int Create(string name)
        {
            return _dataMapper.CreateEntity(
                new Entity
                {
                    Name = name
                }
            );
        }

        [HttpPut(Name = "UpdateEntity")]
        public int UpdateEntity(int id, string name)
        {
            return _dataMapper.UpdateEntity(
                new Entity
                {
                    Id = id,
                    Name = name
                }
            );
        }

        [HttpDelete(Name = "DeleteEntity")]
        public int DeleteEntity(int id)
        {
            return _dataMapper.DeleteEntity(id);
        }


    }
}