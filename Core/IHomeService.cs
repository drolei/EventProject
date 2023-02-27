using System.ComponentModel.DataAnnotations;

namespace EventProject.Core
{
    public interface IHomeService
    {
        string GetValuesFromLog();
    }

    public class EventData
    {
       public string Text { get; set; } = string.Empty;

       public uint SomeValue { get; set; }
    }
}
