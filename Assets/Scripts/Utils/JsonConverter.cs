using Newtonsoft.Json;
using UnityEngine;

namespace Utils
{
    public static class JsonConverter
    {
    
        public static T GenericParseJson<T>(string jsonString)
        {
            T deserializeObject;
            try {
                deserializeObject = JsonConvert.DeserializeObject<T>(jsonString);
            } catch (JsonSerializationException e) {
                Debug.LogWarning("Erreur produite par la deserialization du json qui renvoie une liste vide. RAS");
                Debug.LogWarning(e.Message);
                return default(T);
            }

            return deserializeObject;
        }
    }
}