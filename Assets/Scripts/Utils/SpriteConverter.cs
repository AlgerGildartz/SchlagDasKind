


using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public static class SpriteConverter
{
    public static async Task<Sprite> CreateSpriteFromPath(string filePath, Vector2 pivot)
    {
        Texture2D tex2D;
        Sprite outSprite;

        if (File.Exists(filePath)){
            var fileData = await File.ReadAllBytesAsync(filePath);
            tex2D = new Texture2D(2, 2);
            
            if (tex2D.LoadImage(fileData))
            {
                Texture2D spriteTexture = tex2D;
                outSprite = Sprite.Create(spriteTexture, new Rect(0, 0, spriteTexture.width, spriteTexture.height),pivot);

                return outSprite;
            }
        }
        return null;
    }public static async Task<Sprite> CreateSpriteFromPath(string filePath)
    {
        Texture2D tex2D;
        Sprite outSprite;

        if (File.Exists(filePath)){
            var fileData = await File.ReadAllBytesAsync(filePath);
            tex2D = new Texture2D(2, 2);
            
            if (tex2D.LoadImage(fileData))
            {
                Texture2D spriteTexture = tex2D;
                outSprite = Sprite.Create(spriteTexture, new Rect(0, 0, spriteTexture.width, spriteTexture.height), new Vector2(0.5f,0.5f));

                return outSprite;
            }
        }
        return null;
    }
}
