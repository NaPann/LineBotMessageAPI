using Newtonsoft.Json;

namespace WEBAPI.Models
{
    public class AppSettings
    {

        public Dictionary<string, string> AppConfig { get; set; }

    }
 
    public class Events
    {
        [JsonProperty("destination")]
        public string destination { get; set; }
        [JsonProperty("events")]
        public List<EventsList> events { get; set; }
    }
    public class EventsList
    {
        [JsonProperty("replyToken")]
        public string ReplyToken { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("message")]
        public Message Message { get; set; }

        [JsonProperty("source")]
        public Source Source { get; set; }
    }
    public class Message
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }
    public class Source
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("roomId")]
        public string RoomId { get; set; }

        [JsonProperty("groupId")]
        public string GroupId { get; set; }
    }
}
