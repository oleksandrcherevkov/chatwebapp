using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatWebApp.Models.Group;
using Newtonsoft.Json;
using System.Net.Mime;

namespace ChatWebApp.Services.Group
{
    public class GroupService
    {
        private readonly string _apiUrl;
        public GroupService(IConfiguration config)
        {
            _apiUrl = config["GeneralApiUrlBase"];
        }

        public async Task<GroupCombined> GetGroupPagedAsync(GetGroupOptions options)
        {
            using(var client = new HttpClient())
            {
                var messagesRequest = new HttpRequestMessage()
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"{_apiUrl}/group/messages"),
                    Content = new StringContent(JsonConvert.SerializeObject(options),
                        Encoding.UTF8, MediaTypeNames.Application.Json)
                };


                var messagesResponce = await client.SendAsync(messagesRequest);
                var groupResponce = await client.GetAsync($"{_apiUrl}/group/{options.GroupId}");


                var messagesJson = await messagesResponce.Content.ReadAsStringAsync();
                var groupsJson = await groupResponce.Content.ReadAsStringAsync();


                var messages = JsonConvert.DeserializeObject<ICollection<GroupMessageBlock>>(messagesJson);
                var group = JsonConvert.DeserializeObject<GroupWithMembers>(groupsJson);


                var combined = new GroupCombined()
                {
                    UserId = options.UserId,
                    GroupInfo = group,
                    Page = options.PageNum,
                    Messages = messages?.Reverse().ToList() ?? new List<GroupMessageBlock>()
                };
                return combined;
            }
            
        }

        public async Task<GroupMessageBlock> GetMessageAsync(int messageId)
        {
            using(var client = new HttpClient())
            {
                var messageResponce = await client.GetAsync($"{_apiUrl}/group/messages/{messageId}");

                if(!messageResponce.IsSuccessStatusCode)
                    return null;

                var messageJson = await messageResponce.Content.ReadAsStringAsync();
                var message = JsonConvert.DeserializeObject<GroupMessageBlock>(messageJson);

                return message;
            }
        }

        public async Task<int> CreateMessgeAsync(GroupMessage message)
        {
            using(var client = new HttpClient()) 
            {
                var postReqest = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri($"{_apiUrl}/group/messages"),
                    Content = new StringContent(JsonConvert.SerializeObject(message),
                        Encoding.UTF8, MediaTypeNames.Application.Json)
                };

                var postResponce = await client.SendAsync(postReqest);

                if (postResponce.IsSuccessStatusCode)
                    return 1;

                return 0;
            }
        }

        public async Task<int> UpdateMessgeTextAsync(GroupMessage message)
        {
            using (var client = new HttpClient())
            {
                var putReqest = new HttpRequestMessage()
                {
                    Method = HttpMethod.Put,
                    RequestUri = new Uri($"{_apiUrl}/group/messages"),
                    Content = new StringContent(JsonConvert.SerializeObject(message),
                        Encoding.UTF8, MediaTypeNames.Application.Json)
                };

                var postResponce = await client.SendAsync(putReqest);

                if (postResponce.IsSuccessStatusCode)
                    return 1;

                return 0;
            }
        }

        public async Task<int> EraseMessageAsync(int messageId)
        {
            using (var clien = new HttpClient())
            {
                var putResonce = await clien.PutAsync($"{_apiUrl}/group/messages/{messageId}", null);

                if (putResonce.IsSuccessStatusCode)
                    return 1;

                return 0;
            }
        }

        public async Task<int> DeleteMessageAsync(int messageId) 
        { 
            using(var clien = new HttpClient())
            {
                var deleteResonce = await clien.DeleteAsync($"{_apiUrl}/group/messages/{messageId}");

                if (deleteResonce.IsSuccessStatusCode)
                    return 1;

                return 0;
            }
        }

    }
    
}
