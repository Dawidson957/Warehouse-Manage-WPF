using DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.DataAcc
{
    public interface IProducerAccess
    { 
        Task<bool> AddProducer(Producer producer);

        Task<int> GetProducerId(string name);

        Task<List<string>> GetProducerNamesAll();
    }
}