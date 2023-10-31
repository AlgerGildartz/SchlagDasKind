using TMPro;
using UnityEngine;

public class TowerPreview : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _goldCost;
    
    [Header("Stats")]
    [SerializeField] private TextMeshProUGUI _speedText;
    [SerializeField] private TextMeshProUGUI _rangeText;
    [SerializeField] private TextMeshProUGUI _attackText;

    [Header("Sprites")] 
    [SerializeField] private SpriteRenderer _towerImg;


    public void ChangePreview(int goldCost, int attackValue, float speedValue, int rangeValue, Sprite towerSprite)
    {
        _goldCost.text = goldCost.ToString();
        _attackText.text = attackValue.ToString();
        _speedText.text = speedValue.ToString("F1");
        _rangeText.text = rangeValue.ToString();
        _towerImg.sprite = towerSprite;
    }
}
