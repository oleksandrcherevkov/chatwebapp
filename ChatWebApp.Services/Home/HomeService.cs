using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatWebApp.Models.Chat;
using ChatWebApp.Models.Group;
using ChatWebApp.Models.Home;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace ChatWebApp.Services.Home
{
    public class HomeService
    {
        private readonly string _apiUrl;
        public HomeService(IConfiguration config)
        {
            _apiUrl = config["GeneralApiUrlBase"];
        }
        public async Task<GroupsChatsCombined> GetBlocksAsync(int userId)
        {
            using(var client = new HttpClient())
            {
                var chatsResponce = await client.GetAsync($"{_apiUrl}/user/list/{userId}");
                var groupsResponce = await client.GetAsync($"{_apiUrl}/group/list/{userId}");

                //Task.WaitAll(chatsReqest, groupsRequest);

                var chatsJson = await chatsResponce.Content.ReadAsStringAsync();
                var groupsJson = await groupsResponce.Content.ReadAsStringAsync();

                //Task.WaitAll(chatsJson, groupsJson);

                var chats = JsonConvert.DeserializeObject<ICollection<ChatViewBlock>>(chatsJson)?.ToList();
                var groups = JsonConvert.DeserializeObject<ICollection<GroupViewBlock>>(groupsJson)?.ToList();

                var combined = new GroupsChatsCombined()
                {
                    UserId = userId,
                    Chats = chats ?? new List<ChatViewBlock>(),
                    Groups = groups ?? new List<GroupViewBlock>()
                };

                return combined;
            }
        } 
    }
}
