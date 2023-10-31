using ScriptableObjects.Variables;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [Header("Base")]
    [SerializeField] private Slider _sliderHpBase;
    [SerializeField] private FloatVariable _maxHpBase;
    [SerializeField] private FloatVariable _currentHpBase;

    private void OnEnable()
    {
        Base.OnLooseHealthPoint += UpdateHpSlider;
    }

    private void OnDisable()
    {
        Base.OnLooseHealthPoint -= UpdateHpSlider;
    }
    
    private void Start()
    {
        UpdateHpSlider();
    }

    private void UpdateHpSlider()
    {
        _sliderHpBase.value = _currentHpBase.value / _maxHpBase.value;
    }

}
