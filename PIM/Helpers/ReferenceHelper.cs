using Newtonsoft.Json;

namespace PIM.Helpers;

public class ReferenceHelper
{
    public static T DeepClone<T>(T instance) => JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(instance));
}
