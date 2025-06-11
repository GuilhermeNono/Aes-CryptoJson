namespace AesEncryptApp.Common.Holder.Container;

public static class HolderContainer
{
    private static readonly Dictionary<object, object> Container = [];

    public static bool TryAllocate<T>(T instance) where T : notnull => !Container.TryAdd(instance.GetType().Name, instance);

    public static T? GetInContainer<T>(string reference) where T : notnull
    {
        if(!Container.TryGetValue(reference, out var instance));
        return (T?)instance;
    }
}