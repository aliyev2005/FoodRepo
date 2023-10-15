using System.Text.Json.Serialization;

namespace FoodProject.Enums
{
    //Enums are constants so they are named as such. With all capital letters.
    public enum Cities
    {
        [JsonPropertyName("Baku")]
        BAKU = 1,
        [JsonPropertyName("Ganja")]
        GANJA = 2,
        [JsonPropertyName("Quba")]
        QUBA = 3,
        [JsonPropertyName("Shaki")]
        SHAKI = 4,
        [JsonPropertyName("Shamaxi")]
        SHAMAXI =5,
        [JsonPropertyName("Qabala")]
        QABALA =6,
        [JsonPropertyName("Lankaran")]
        LANKARAN = 7
    }
}
