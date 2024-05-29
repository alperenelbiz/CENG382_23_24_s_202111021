using System.ComponentModel.DataAnnotations;

public class Room
{
    public int Id { get; set; }

    [Required]
    public string RoomName { get; set; }

    [Required]
    public int Capacity { get; set; }
}