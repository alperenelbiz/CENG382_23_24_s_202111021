using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class Room
{
    [JsonPropertyName("roomId")]
    public string? RoomId { get; set; }

    [JsonPropertyName("roomName")] 
    public string? RoomName { get; set; }

    [JsonPropertyName("capacity")]
    public int Capacity { get; set; }
}