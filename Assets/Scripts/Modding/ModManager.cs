using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MoonSharp.Interpreter;
using Newtonsoft.Json;
using ScriptableObjects.Game;
using UnityEngine;

public class ModManager : MonoBehaviour
{
    [SerializeField] private Transform _panel;
    [SerializeField] private GameObject _modTogglePrefab;
    [SerializeField] private TowersList _towersList;
    [SerializeField] private EnemyList _enemyList;
    [SerializeField] private WaveList _waveList;
    [SerializeField] private TowerMod _towerModList;

    public readonly string ModFolderPath = Application.streamingAssetsPath + "/Mods/";
    private const string TowersJsonFileName = "towers.json";
    private const string EnemiesJsonFileName = "enemies.json";
    private const string WavesJsonFileName = "waves.json";

    private readonly List<GameObject> _modToggles = new ();
    private Dictionary<string, bool> _previousMods = new ();

    private void Start()
    {
        ModList.LoadModListJson();
        _previousMods = ModList.Mods.ToDictionary(mod => mod.Key, mod => mod.Value);
        InitFolder();
        RefreshMods();
        ApplyMods();
    }

    public void RefreshMods()
    {
        ReadMods();
        DisplayMods();
    }

    private void InitFolder()
    {
        Directory.CreateDirectory(ModFolderPath);
    }
    
    private void ReadMods()
    {
        _modToggles.ForEach(Destroy);
        
        var modFolders = Directory.GetDirectories(ModFolderPath);
        
        foreach (var modFolder in modFolders)
        {
            if (!ModList.Mods.ContainsKey(modFolder))
                ModList.Mods.Add(modFolder, false);
        }

        var modsToRemove = ModList.Mods.Where(mod => !modFolders.Contains(mod.Key)).ToList();
        modsToRemove.ForEach(mod => ModList.Mods.Remove(mod.Key));
    }
    
    private void DisplayMods()
    {
        foreach (var modPath in ModList.Mods.Keys)
        {
            var obj = Instantiate(_modTogglePrefab, _panel);
            _modToggles.Add(obj);
            if (!obj.TryGetComponent(out ModToggle modToggle)) throw new Exception("The prefab does not have the ModToggle.cs script attached.");

            
            modToggle.Toggle.isOn = ModList.Mods[modPath];
            modToggle.Label.text = ConvertPathToName(modPath);
            modToggle.Path = modPath;
            modToggle.ValueChanged += OnToggleMod;
        }
    }

    private void OnToggleMod(string key, bool status)
    {
        ModList.Mods[key] = status;
    }

    public void CancelSelection()
    {
        ModList.Mods = _previousMods.ToDictionary(mod => mod.Key, mod => mod.Value);
    }

    private void ClearData()
    {
        if(_towersList.value != null)
            _towersList.value.Clear();
        if (_enemyList.value != null)
            _enemyList.value.Clear();
        if (_waveList.value != null)
            _waveList.value.Clear();
        if (_towerModList.value != null)
            _towerModList.value.Clear();

    }

    public void ApplyMods()
    {
        ClearData();
        
        ModList.SaveModListList();
        _previousMods = ModList.Mods.ToDictionary(mod => mod.Key, mod => mod.Value);
        foreach (var mod in ModList.Mods)
        {
            if (!mod.Value) continue;
                
            OpenMod(mod.Key);
        }
    }

    private void OpenMod(string modPath)
    {
        var files = Directory.EnumerateFiles(modPath)
            .Where(file => file.ToLower().EndsWith(".json") || file.ToLower().EndsWith(".lua")).ToList();

        OpenModFiles(files, modPath);
    }

    private void OpenModFiles(List<string> modFiles, string inModPath)
    {
        foreach (var file in modFiles)
        {
            var filename = Path.GetFileName(file);
            var extension = Path.GetExtension(file);
            
            string jsonString = "";
            if (extension == ".json")
                jsonString = OpenJsonFile(file);
            
            switch (filename)
            {
                case TowersJsonFileName:
                    List<TowerData> towerData = Utils.JsonConverter.GenericParseJson<List<TowerData>>(jsonString);
                    _towersList.AddTowerData(towerData, inModPath);
                    break;
                case EnemiesJsonFileName:
                    List<EnemyData> enemyData = Utils.JsonConverter.GenericParseJson<List<EnemyData>>(jsonString);
                    _enemyList.AddEnemyData(enemyData, inModPath);
                    break;
                case WavesJsonFileName:
                    List<WaveData> waveData = Utils.JsonConverter.GenericParseJson<List<WaveData>>(jsonString);
                    _waveList.AddWaveData(waveData);
                    break;
                default:
                    if (extension != ".lua") break;

                    var luaString = File.ReadAllText(file);
                    Script script = new Script();

                    script.DoString(luaString);

                    DynValue res = script.Globals.Get("Tower_name");
                    var struc = new TowerModStruct
                    {
                        lua = script,
                        towerName = res.String
                    };
                    _towerModList.AddTowerMod(struc);
                    break;
            }
        }
    }
    
    private string OpenJsonFile(string filePath)
    {
        using var streamReader = new StreamReader(filePath);
        var json = streamReader.ReadToEnd();

        return json;
    }

    private void OpenLuaFile(string filePath)
    {
        Debug.Log($"OPEN {filePath}");
    }

    private string ConvertPathToName(string path)
    {
        return Path.GetFileName(Path.GetDirectoryName(path +"/"));
    }
}
