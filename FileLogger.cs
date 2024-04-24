using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

public class FileLogger : ILogger
{
  private readonly string _filePath;

  private List<LogRecord> _logs = new List<LogRecord>();
    public FileLogger(string filePath)
    {
        _filePath = filePath;
    }

    public void Log(LogRecord log)
    {
    try
    {
      List<LogRecord> logs;

      // Check if the file exists and load existing logs (optional for persistence)
      if (File.Exists(_filePath))
      {
        string existingJson = File.ReadAllText(_filePath);
        logs = JsonSerializer.Deserialize<List<LogRecord>>(existingJson) ?? new List<LogRecord>(); // Handle potential deserialization errors
      }
      else
      {
        logs = new List<LogRecord>();
      }

      logs.Add(log); // Add the new log record to the list

      var options = new JsonSerializerOptions { WriteIndented = true };
      string json = JsonSerializer.Serialize(logs, options);

      File.WriteAllText(_filePath, json); // Overwrite the entire file with updated list
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error logging reservations: {ex.Message}");
    }
    }

}