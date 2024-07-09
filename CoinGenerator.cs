using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class colorToPrefab {
    public GameObject prefab;
    public Color color;
}

public class CoinGenerator : MonoBehaviour
{
    public Texture2D coinMap;
    public colorToPrefab[] colortoPrefab;
    public GameObject parentObj;

    public float positionScale = 0.5f;
    public float sizeScale = 0.15f;
    
    public float yOffset = -2.0f;
    public float xOffset = 20f;


    // Start is called before the first frame update
    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        for(int x = 0; x < coinMap.width; x++)
        {
            for (int y = 0; y < coinMap.height; y++)
            {
                GenerateCoins(x,y);
            }
        }
    }
    
    void GenerateCoins(int x, int y)
    {
        // map each pixel of the coinmap to colors
        Color mapColor = coinMap.GetPixel(x,y);
        foreach(colorToPrefab obj in colortoPrefab)
        {
            if (obj.color.Equals(mapColor))
            {
                // scale the x and y of the coins so that they appear on a different position on the game screen
                Vector2 pos = new Vector2(x * positionScale + xOffset, y * positionScale + yOffset);
                GameObject coin = Instantiate(obj.prefab, pos, Quaternion.identity, parentObj.transform);
                coin.transform.localScale = new Vector3(sizeScale, sizeScale, sizeScale); // Scale the coin size
                return;
            }
        }
    }
    
}
