using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CSharpTwitterBot.Models.Connpass
{
    public class RootObject
    {
        public int results_returned { get; set; }
        [JsonProperty("events")]
        public Event[] Events { get; set; }
        public int results_start { get; set; }
        public int results_available { get; set; }
    }

    public class Event
    {
        [JsonProperty("event_url")]
        public string EventUrl { get; set; }
        public string event_type { get; set; }
        public string owner_nickname { get; set; }
        public Series series { get; set; }
        public DateTime updated_at { get; set; }
        public string lat { get; set; }
        [JsonProperty("started_at")]
        public DateTime StartedAt { get; set; }
        [JsonProperty("hash_tag")]
        public string HashTag { get; set; } // "JXUG #mspjp #gakusei_wakaru"
        [JsonProperty("title")]
        public string Title { get; set; }
        public int event_id { get; set; }
        public string lon { get; set; }
        public int waiting { get; set; }
        public int? limit { get; set; }
        public int owner_id { get; set; }
        public string owner_display_name { get; set; }
        public string description { get; set; }
        public string address { get; set; }
        public string _catch { get; set; }
        public int accepted { get; set; }
        public DateTime ended_at { get; set; }
        public string place { get; set; }
    }

    public class Series
    {
        public string url { get; set; }
        public int id { get; set; }
        public string title { get; set; }
    }

}
