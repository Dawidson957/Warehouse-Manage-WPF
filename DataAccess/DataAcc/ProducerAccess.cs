using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Manage_WPF.Entities;
using Warehouse_Manage_WPF.EntityModel;

namespace DataAccess.DataAcc
{
    public class ProducerAccess
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
            catch(Exception ex)
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
            catch(Exception)
            {
                return false;
            }

            return true;
        }
    }
}
