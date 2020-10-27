using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocalStack.Service.DynamoDb
{
    public interface IDynamoService
    {
        public Task WriteToDb(string text);
        public Task<IEnumerable<string>> GetAllFromDb();
    }
}