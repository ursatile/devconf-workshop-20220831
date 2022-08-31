namespace Messages;
public class Greeting
{
    public string Name { get; set; } = "DevConf";
    public string Language { get; set; } = "en-GB";

    public DateTime Created { get; set; } = DateTime.UtcNow;

    public override string ToString()
    {
        return $"{Name} {Language} at {Created:O}";
    }
}
