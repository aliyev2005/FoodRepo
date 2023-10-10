using System.Runtime.Serialization;

namespace FoodProject.Enums
{
    public enum Cities
    {
        [EnumMember(Value ="Baku")]
        BAKU,
        [EnumMember(Value = "Ganja")]
        GANJA,
        [EnumMember(Value = "Quba")]
        QUBA,
        [EnumMember(Value = "Shaki")]
        SHAKI,
        [EnumMember(Value = "Shamaxi")]
        SHAMAXI,
        [EnumMember(Value = "Qabala")]
        QABALA,
        /// <summary>
        /// Lankaran
        /// </summary>
        //[EnumMember(Value = "Lankaran")]
        LANKARAN
    }
}
