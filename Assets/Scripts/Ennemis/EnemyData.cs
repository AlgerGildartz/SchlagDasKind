using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public class EnemyData 
{
    public int lifePoint;
    public string enemyVisual;
    public string nameReference;
    public float walkSpeed;
    public int GoldEarned;
    public float enemyDamageOverride;


    private Sprite _enemySprite;

    public Sprite EnemySprite => _enemySprite;
    public void SetEnemyFromData(Enemy toSet)
    {
        toSet.lifePoint = lifePoint;
        toSet.walkSpeed = walkSpeed;
        toSet.GoldEarned = GoldEarned;
        toSet.enemyDamageOverride = enemyDamageOverride;
        toSet.nameReference = nameReference;
        toSet.sprite = _enemySprite;
    }
    public async Task<bool> CreateSpriteFromPath( string modPath)
    {
        Texture2D tex2D;
        string finalPath = modPath +"/"+ enemyVisual;
        finalPath = finalPath.Replace("//", "/");

        if (File.Exists(finalPath))
        {
            var fileData = await File.ReadAllBytesAsync(finalPath);
            tex2D = new Texture2D(2, 2);

            if (tex2D.LoadImage(fileData))
            {
                Texture2D spriteTexture = tex2D;
                _enemySprite = Sprite.Create(spriteTexture, new Rect(0, 0, spriteTexture.width, spriteTexture.height), new Vector2(0.5f, 0.31f));

                return true;
            }
        }

        return false;
    }
}
