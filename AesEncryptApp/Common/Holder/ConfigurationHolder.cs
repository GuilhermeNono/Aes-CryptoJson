using System.Text.Json;
using AesEncryptApp.Common.Holder.Container;

namespace AesEncryptApp.Common.Holder;

public static class ConfigurationHolder
{
    static ConfigurationHolder()
    {
        using FileStream file = new FileStream("configuration.json", FileMode.Open, FileAccess.Read);
        var streamReader = new StreamReader(file);
        Configuration = streamReader.ReadToEnd();
    }

    private static string Configuration { get; set; }

    public static T Instantiate<T>(string target) where T : notnull, new()
    {
        var instanceInHolder = HolderContainer.GetInContainer<T>(target);

        if (instanceInHolder is not null)
            return instanceInHolder;

        JsonDocument deserializedConfiguration = JsonDocument.Parse(Configuration);

        if (!deserializedConfiguration.RootElement.TryGetProperty(target, out var rootElement))
            throw new Exception();

        var obj = rootElement.Deserialize<T?>();
        
        if(obj is null)
            throw new Exception("Configuration is empty");
        
        HolderContainer.TryAllocate(obj);
        
        return obj;
    }
}