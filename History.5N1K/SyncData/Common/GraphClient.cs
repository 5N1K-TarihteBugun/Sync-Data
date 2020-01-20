using System;
using System.Threading.Tasks;
using GraphQL.Client;
using GraphQL.Common.Request;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SyncData.Data;

namespace SyncData.Common
{
    public class GraphQlDataClient
    {
        private readonly string _url;

        public GraphQlDataClient(string url)
        {
            _url = url;
        }

        public GraphQLClient GetClient()
        {
            var graphClient = new GraphQLClient(_url);
            
            return graphClient;
        }

        public async Task<object> CreateHistory(History history)
        {
            try
            {
                var query = new GraphQLRequest
                {
                    Query = @"
mutation($input: [History_insert_input!]!) {
  insert_History(objects: $input) {
    affected_rows
  }
}",
                    Variables = new {input = history}
                };


                var response = await GetClient().PostAsync(query);
                return response.GetDataFieldAs<object>("insert_History");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            
        }
    }
}