using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ModToggle : MonoBehaviour
{
    public TextMeshProUGUI Label;
    public Toggle Toggle;
    [HideInInspector] public string Path;
    public event Action<string, bool> ValueChanged;

    public void ClickToggle(bool status)
    {
        ValueChanged?.Invoke(Path, status);
    }
}
