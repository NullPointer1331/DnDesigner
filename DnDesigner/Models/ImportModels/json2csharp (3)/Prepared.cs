using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace DnDesigner.5eTools{ 

    public class Prepared
    {
        [JsonProperty("1")]
        public List<string> _1 { get; set; }

        [JsonProperty("3")]
        public List<string> _3 { get; set; }

        [JsonProperty("5")]
        public List<string> _5 { get; set; }

        [JsonProperty("7")]
        public List<string> _7 { get; set; }

        [JsonProperty("9")]
        public List<string> _9 { get; set; }

        [JsonProperty("17")]
        public List<_17> _17 { get; set; }
    }

}