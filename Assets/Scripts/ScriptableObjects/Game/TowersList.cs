using System.Collections.Generic;
using ScriptableObjects.Variables;
using UnityEngine;

namespace ScriptableObjects.Game
{
    
    [CreateAssetMenu(menuName = "ScriptableObjects/Game/Towers", fileName = "New Towers List")]
    public class TowersList : GenericVariableSO<List<TowerData>>
    {
        public void AddTowerData(TowerData inTowerData, string inModPath)
        {
            if (value == null)
                value = new List<TowerData>();

            if (inTowerData.nameReference.Equals(""))
                inTowerData.nameReference = "Base";
            
            

            inTowerData.CreateTowerSprite(inModPath);
            value.Add(inTowerData);
        }
        
        public void AddTowerData(List<TowerData> inTowersData, string inModPath)
        {
            foreach (TowerData towerData in inTowersData)
            {
                AddTowerData(towerData, inModPath);
            }
        }
    }
}