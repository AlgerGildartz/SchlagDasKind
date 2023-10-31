using ScriptableObjects.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Game/Enemies", fileName = "New Enemies List")]
public class EnemyList : GenericVariableSO<List<EnemyData>>
{
    public void AddEnemyData(EnemyData inEnemyData,string path)
    {
        if (value == null)
            value = new List<EnemyData>();

        if (inEnemyData.nameReference.Equals(""))
            inEnemyData.nameReference = "Base";

        inEnemyData.CreateSpriteFromPath(path);
        value.Add(inEnemyData);
    }

    public void AddEnemyData(List<EnemyData> inEnemyData,string path)
    {
        foreach (EnemyData enemyData in inEnemyData)
        {
            AddEnemyData(enemyData, path);
        }
    }
}
