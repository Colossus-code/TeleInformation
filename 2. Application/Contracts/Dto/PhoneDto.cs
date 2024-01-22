using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Dto
{/// <summary>
/// DTO Of what we retrieve of an json.
/// </summary>
    public class PhoneDto
    {
        [JsonProperty("id")]
        public int PhoneID { get; set; }
        [JsonProperty("name")]
        public string PhoneName { get; set; }
        [JsonProperty("description")]
        public string PhoneDescription { get; set; }
        [JsonProperty("fabricator")]
        public string PhoneFabricatorName { get; set; }
        [JsonProperty("status")]
        public string PhoneStatus { get; set; }
        [JsonProperty("fabricationDate")]
        public DateTime FabricationDate { get; set; }
        [JsonProperty("lastStatusUpdate")]
        public DateTime LastStatusUpdate {  get; set; }

    }
}
