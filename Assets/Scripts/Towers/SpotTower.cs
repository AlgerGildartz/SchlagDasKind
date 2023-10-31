using System;
using System.Collections.Generic;
using Monnaie;
using ScriptableObjects.Game;
using ScriptableObjects.Variables;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Towers
{
    public class SpotTower : MonoBehaviour
    {
        [SerializeField] private IntVariable _moneySO;

        [Header("Previews")] 
        [SerializeField] private List<TowerPreview> _previewsList;
        
        [Header("Interface")]
        [SerializeField] private GameObject _interface;
        [SerializeField] private GameObject _arrows;

        [SerializeField] private TowersList _towersList;
        [SerializeField] private TowerMod _towerModList;

        [FormerlySerializedAs("_towerPrefabs")]
        [Header("Prefabs")] 
        [SerializeField] private List<GameObject> _baseTowerPrefabs;

        [SerializeField] private GameObject _basePrefab;
        private Sprite _baseSprite => _basePrefab.GetComponent<Tower>().TowerSprite;

        private int _actualIndex = 0;
        private int _maxIndex => _baseTowerPrefabs.Count + _towersList.value.Count - 1;
        
        private bool _interfaceOn;

        private void Start()
        {
            InitInterface();
            InitPreviews();
        }

        private void InitPreviews()
        {
            int index = _actualIndex;
            foreach (TowerPreview towerPreview in _previewsList)
            {
                if (index > _maxIndex)
                    index = 0;
                InitPreviewFromIndex(index++, towerPreview);
            }
            
        }

        private void InitPreviewFromIndex(int index, TowerPreview preview)
        {
            if(index < _baseTowerPrefabs.Count)
            {
                if (!_baseTowerPrefabs[index].TryGetComponent(out Tower tower))
                    throw new Exception("GameObject Tower does not have Tower Script!!!!");
                
                preview.ChangePreview(tower.GoldCost, (int)tower.BulletPower, 1/tower.TimeBetweenShoot, (int)tower.Range, tower.TowerSprite);
            }
            else
            {
                index -= _baseTowerPrefabs.Count;
                TowerData towerData = _towersList.value[index];
                preview.ChangePreview(towerData.goldCost, (int)towerData.bulletDamage, 1/towerData.timeBetweenShoot, (int)towerData.range, towerData.TowerSprite != null? towerData.TowerSprite : _baseSprite);
            }
        }

        private void InitInterface()
        {
            _interface.SetActive(false);
            _interfaceOn = false;
            _arrows.SetActive(_maxIndex >= 3);
        }

        public void ToggleInterface()
        {
            _interfaceOn = !_interfaceOn;
            _interface.SetActive(_interfaceOn);
        }

        public void InstantiateTower(GameObject prefab)
        {
            var go = Instantiate(prefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        
        #region Buttons

        public void ClickLeftArrow()
        {
            if (--_actualIndex < 0)
                _actualIndex = _maxIndex;

            InitPreviews();
        }

        public void ClickRightArrow()
        {
            if (++_actualIndex > _maxIndex)
                _actualIndex = 0;
            
            InitPreviews();
        }

        public void ClickSpot(int index)
        {
            int clicIndex = (_actualIndex + index)%(_maxIndex + 1);
            
            
            if (clicIndex < _baseTowerPrefabs.Count)
            {
                if (!_baseTowerPrefabs[clicIndex].TryGetComponent(out Tower tower)) return;

                if (PayTower(tower.GoldCost))
                    InstantiateTower(tower.gameObject);
                else
                    ToggleInterface();
            }
            else
            {
                clicIndex -= _baseTowerPrefabs.Count;
                
                if (PayTower(_towersList.value[clicIndex].goldCost))
                {
                    GameObject newTowerObject = Instantiate(_basePrefab, transform.position, Quaternion.identity);

                    var towerScript = newTowerObject.GetComponent<Tower>();
                    foreach (var mod in _towerModList.value) 
                    {
                        if(mod.towerName.Equals(_towersList.value[clicIndex].nameReference))
                            towerScript.luaCode = mod.lua;
                    }

                    
                    _towersList.value[clicIndex].CreateTowerFromData(towerScript);
                    Destroy(gameObject);
                }
            }
        }

        private bool PayTower(int towerGoldCost)
        {
            if (_moneySO.value >= towerGoldCost)
            {
                MoneySystem.AddMoney(-towerGoldCost);
                return true;
            }

            return false;
        }

        #endregion
    }
}