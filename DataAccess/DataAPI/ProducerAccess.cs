using DataAccess.Entities;
using DataAccess.EntityModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.DataAcc
{
    public class ProducerAccess : IProducerAccess
    {
        public async Task<List<string>> GetProducerNamesAll()
        {
            List<string> producers = null;

            try
            {
                using (var context = new WarehouseModel())
                {
                    producers = await (from s in context.Producers select s.Name).ToListAsync<string>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return producers.Count > 0 ? producers : null;
        }

        public async Task<bool> AddProducer(Producer producer)
        {
            try
            {
                using (var context = new WarehouseModel())
                {
                    var producerExists = context.Producers.Any(x => x.Name == producer.Name);

                    if (!producerExists)
                    {
                        context.Producers.Add(producer);
                        await context.SaveChangesAsync();
                    }
                    else
                    {
                        throw new Exception("Producer already exists in database.");
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<int> GetProducerId(string name)
        {
            int Id;

            try
            {
                using (var context = new WarehouseModel())
                {
                    Id = await (from s in context.Producers
                                where s.Name == name
                                select s.Id).FirstOrDefaultAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Id;
        }
    }
}
