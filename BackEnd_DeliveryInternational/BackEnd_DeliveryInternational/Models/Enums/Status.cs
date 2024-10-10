﻿using System.Text.Json.Serialization;

namespace BackEnd_DeliveryInternational.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Status
    {
        InProcess,
        Delivered
    }
}
