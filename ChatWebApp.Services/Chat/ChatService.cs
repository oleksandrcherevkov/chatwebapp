using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatWebApp.Models.Chat;
using Newtonsoft.Json;
using System.Net.Mime;

namespace ChatWebApp.Services.Chat
{
    public class ChatService
    {
        private readonly string _apiUrl;
        public ChatService(IConfiguration config)
        {
            _apiUrl = config["GeneralApiUrlBase"];
        }

        public async Task<ChatCombined> GetChatPagedAsync(GetChatOptions options)
        {
            using(var client = new HttpClient())
            {
                var messagesReques = new HttpRequestMessage()
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"{_apiUrl}/chat/messages"),
                    Content = new StringContent(JsonConvert.SerializeObject(options),
                        Encoding.UTF8, MediaTypeNames.Application.Json)
                };


                var messagesResponce = await client.SendAsync(messagesReques);
                if (!messagesResponce.IsSuccessStatusCode)
                    throw new ArgumentException();

                var chatResponce = await client.GetAsync($"{_apiUrl}/user/{options.ResponcerId}");
                if (!chatResponce.IsSuccessStatusCode)
                    throw new ArgumentException();

                var messagesJson = await messagesResponce.Content.ReadAsStringAsync();
                var chatJson = await chatResponce.Content.ReadAsStringAsync();


                var messages = JsonConvert.DeserializeObject<ICollection<ChatMessageBlock>>(messagesJson);
                var chat = JsonConvert.DeserializeObject<ChatViewBlock>(chatJson);

                var combined = new ChatCombined()
                {
                    UserId = options.UserId,
                    Page = options.PageNum,
                    ResponcerInfo = chat,
                    Messages = messages?.Reverse().ToList() ?? new List<ChatMessageBlock>()
                };

                return combined;
            }
            
        }

        public async Task<ChatMessageBlock> GetMessageAsync(int messageId)
        {
            using (var client = new HttpClient())
            {
                var messageResponce = await client.GetAsync($"{_apiUrl}/chat/messages/{messageId}");

                if (!messageResponce.IsSuccessStatusCode)
                    throw new ArgumentException();

                var messageJson = await messageResponce.Content.ReadAsStringAsync();
                var message = JsonConvert.DeserializeObject<ChatMessageBlock>(messageJson);

                return message;
            }
        }

        public async Task<int> CreateMessageAsync(ChatMessage message)
        {
            using (var client = new HttpClient())
            {
                var postReqest = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri($"{_apiUrl}/chat/messages"),
                    Content = new StringContent(JsonConvert.SerializeObject(message),
                        Encoding.UTF8, MediaTypeNames.Application.Json)
                };

                var postResponce = await client.SendAsync(postReqest);

                if (postResponce.IsSuccessStatusCode)
                    return 1;

                return 0;
            }
        }

        public async Task<int> UpdateMessgeTextAsync(ChatMessage message)
        {
            using (var client = new HttpClient())
            {
                var postReqest = new HttpRequestMessage()
                {
                    Method = HttpMethod.Put,
                    RequestUri = new Uri($"{_apiUrl}/chat/messages"),
                    Content = new StringContent(JsonConvert.SerializeObject(message),
                        Encoding.UTF8, MediaTypeNames.Application.Json)
                };

                var postResponce = await client.SendAsync(postReqest);

                if (postResponce.IsSuccessStatusCode)
                    return 1;

                return 0;
            }
        }

        public async Task<int> EraseMessageAsync(int messageId)
        {
            using (var clien = new HttpClient())
            {
                var putResonce = await clien.PutAsync($"{_apiUrl}/chat/messages/{messageId}", null);

                if (putResonce.IsSuccessStatusCode)
                    return 1;

                return 0;
            }
        }

        public async Task<int> DeleteMessageAsync(int messageId)
        {
            using (var clien = new HttpClient())
            {
                var deleteResonce = await clien.DeleteAsync($"{_apiUrl}/chat/messages/{messageId}");

                if (deleteResonce.IsSuccessStatusCode)
                    return 1;

                return 0;
            }
        }

    }
}
