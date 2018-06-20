using AutoMapper;
using InstratructureLayer.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstratructureLayer.Repositories
{
    public class EventRepository : Repository<EventEntity, Guid>, IEventRepository
    {
        private IConfiguration _configuration;

        private string posgresConnectionString;
        
        public EventRepository(DbContext context, IMapper mapper, IConfiguration configuration) : base(context, mapper)
        {
            _configuration = configuration;
            posgresConnectionString = _configuration.GetConnectionString("PosgreConnection");
        }

        protected override IQueryable<EventEntity> Entities => GetAll();

        public override Guid Add(EventEntity Item)
        {
            if (Item.Id == new Guid())
                Item.Id = Guid.NewGuid();
            using (var conn = new NpgsqlConnection(posgresConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    //default only use by not include the column in the insert statement. different from specify that column and insert NULL value into it
                    cmd.CommandText = @"INSERT INTO dbo.eventstore (id, aggid, isdeleted, jsondata, objtype, type) VALUES (@p1 , @p2 , @p3 , @p4 , @p5,@p6)";
                    cmd.Parameters.AddWithValue("p1", Item.Id);
                    cmd.Parameters.AddWithValue("p2", Item.AggId);
                    cmd.Parameters.AddWithValue("p3", Item.IsDeleted);//bit not boolean
                    cmd.Parameters.AddWithValue("p4", Item.JsonData);
                    cmd.Parameters.AddWithValue("p5", Item.ObjType);
                    cmd.Parameters.AddWithValue("p6", (int) Item.Type);
                    cmd.ExecuteNonQuery();
                }
            }
            return Item.Id;
        }

        public override void Delete(Guid Id)
        {
            //base.Delete(Id);
            throw new NotImplementedException();
        }

        public override EventEntity Get(Guid Id)
        {
            var result = new List<EventEntity>();

            using (var conn = new NpgsqlConnection(posgresConnectionString))
            {
                try
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand($"SELECT id, createdDate, updateddate , type, jsondata , objType, isdeleted, aggid " +
                        $"FROM dbo.eventstore " +
                        $"ORDER BY createdDate DESC " +
                        $"WHERE id = '{Id}' ", conn))
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            result.Add(new EventEntity()
                            {
                                Id = Guid.Parse(reader.GetString(0)),
                                CreatedDate = reader.GetDateTime(1),//NpgsqlDateTime
                                UpdatedDate = reader.GetDateTime(2),
                                Type = (EventType)reader.GetInt32(3),
                                JsonData = !reader.IsDBNull(4) ? reader.GetString(4) : string.Empty,
                                ObjType = reader.GetString(5),
                                IsDeleted = reader.GetBoolean(6),
                                AggId = !reader.IsDBNull(7) ? reader.GetString(7) : string.Empty
                            });
                        }
                }
                catch (Exception e)
                {
                    var k = e;
                }
            }
            return result.Single();

        }

        public override IQueryable<EventEntity> GetAll()
        {
            var result = new List<EventEntity>();

            using (var conn = new NpgsqlConnection(posgresConnectionString))
            {
                try {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand($"SELECT id, createdDate, updateddate , type, jsondata , objType, isdeleted, aggid " +
                        $"FROM dbo.eventstore " +
                        $"ORDER BY createdDate DESC ", conn))
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            result.Add(new EventEntity()
                            {
                                Id = Guid.Parse(reader.GetString(0)),
                                CreatedDate = reader.GetDateTime(1),//NpgsqlDateTime
                                UpdatedDate = reader.GetDateTime(2),
                                Type = (EventType)reader.GetInt32(3),
                                JsonData = !reader.IsDBNull(4) ? reader.GetString(4) : string.Empty,
                                ObjType = reader.GetString(5),
                                IsDeleted = reader.GetBoolean(6),
                                AggId = !reader.IsDBNull(7) ? reader.GetString(7) : string.Empty
                            });
                        }
                }
                catch (Exception e)
                {
                    var k = e;
                }
            }
            return result.AsQueryable();
        }

        public EventEntity Store(EventEntity item)
        {
            var result = Add(item);
            item.Id = result;
            //_context.SaveChanges();
            return item;
        }

        public override void Update(EventEntity Item)
        {
            //base.Update(Item);
            throw new NotImplementedException();
        }
    }

    public interface IEventRepository : IRepository<EventEntity, Guid>
    {
        EventEntity Store(EventEntity item);
    }
}
