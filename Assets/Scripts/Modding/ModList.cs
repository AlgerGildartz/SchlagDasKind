using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using Application = UnityEngine.Application;

public static class ModList
{
    // string: path, bool: status
    public static Dictionary<string, bool> Mods = new ();
    
    private static readonly string _savePath = Application.persistentDataPath + "/Config/ModList.json";

    public static void SaveModListList()
    {
        ClearModList();
        WriteModListJson(Mods);
    }
    
    private static void ClearModList()
    {
        if (FileExists())
            File.Delete(_savePath);
    }

    private static void WriteModListJson(Dictionary<string, bool> modsList)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(_savePath));
        File.WriteAllText(_savePath, JsonConvert.SerializeObject(modsList, Formatting.Indented));
    }
    
    public static void LoadModListJson()
    {
        if (!FileExists())
            InitFile();
        
        using var streamReader = new StreamReader(_savePath);
        var json = streamReader.ReadToEnd();
        
        var newModList = Utils.JsonConverter.GenericParseJson<Dictionary<string, bool>>(json);
        Mods = newModList.ToDictionary(mod => mod.Key, mod => mod.Value);
    }

    private static void InitFile()
    {
        WriteModListJson(new Dictionary<string, bool>());
    }

    private static bool FileExists()
    {
        if (Directory.Exists(Path.GetDirectoryName(_savePath)))
        {
            if (File.Exists(_savePath))
                return true;
        }
        
        return false;
    }
}
