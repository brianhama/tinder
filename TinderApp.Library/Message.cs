using Newtonsoft.Json;

namespace TinderApp.Lib
{
    public class Msg
    {
        [JsonProperty("created_date")]
        public string CreatedDate { get; set; }

        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("match_id")]
        public string MatchId { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("sent_date")]
        public string SentDate { get; set; }

        [JsonProperty("timestamp")]
        public object Timestamp { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("__v")]
        public int? V { get; set; }
    }

    public class NewOutgoingMessageResponse
    {
        [JsonProperty("created_date")]
        public string CreatedDate { get; set; }

        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }

        public Msg ToMsg()
        {
            Msg msg = new Msg();
            msg.CreatedDate = CreatedDate;
            msg.From = From;
            msg.Message = Message;
            msg.Timestamp = CreatedDate;
            return msg;
        }
    }
}