using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;

namespace LocalStack.Service.DynamoDb
{
    public class DynamoService: IDynamoService
    {
        private AmazonDynamoDBClient _client;

        public DynamoService()
        {
            
            AmazonDynamoDBConfig clientConfig = new AmazonDynamoDBConfig();
            clientConfig.RegionEndpoint = RegionEndpoint.USEast1;
            clientConfig.ServiceURL = "http://localhost:4566";
            BasicAWSCredentials credentials = new BasicAWSCredentials("xxx","xxx");
            AmazonDynamoDBClient client = new AmazonDynamoDBClient(credentials, clientConfig);
            _client = client;
        }
        
        
        public async Task WriteToDb(string text)
        {
            
            if (!(await _client.ListTablesAsync()).TableNames.Contains("test"))
                await _client.CreateTableAsync(new CreateTableRequest
                {
                    TableName = "test",
                    AttributeDefinitions = new List<AttributeDefinition>
                    {
                        new AttributeDefinition
                        {
                            AttributeName = "word",
                            AttributeType = ScalarAttributeType.S
                        }
                    },
                    ProvisionedThroughput = new ProvisionedThroughput
                    {
                        ReadCapacityUnits = 10,
                        WriteCapacityUnits = 5
                    },
                    KeySchema = new List<KeySchemaElement>
                    {
                        new KeySchemaElement
                        {
                            AttributeName = "word",
                            KeyType = KeyType.HASH
                        }
                    }
                });
            
            await _client.PutItemAsync("test", new Dictionary<string, AttributeValue>()
            {
                {"word", new AttributeValue{S = text}}
            });
        }

        public async Task<IEnumerable<string>> GetAllFromDb()
        {
            var response = await _client.ScanAsync(new ScanRequest
            {
                TableName = "test"
            });

            return response.Items.Select(x => x["word"].S);
        }
    }
}