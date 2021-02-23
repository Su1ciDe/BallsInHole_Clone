using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : SingletonBehaviour<LevelGenerator> 
{
    // Photoshop gibi bir uygulamayla oluşturulmuş bir png resimi
    // her bir piksel = obje olacak şekilde Level gridine çevirmeye yarar

    private List<Texture2D> Maps;
    public Transform LevelParent = null;

#pragma warning disable 0649
    [SerializeField] private PixelToObject[] pixelMappings;
#pragma warning restore 0649

    protected override void Awake()
    {
        base.Awake();

        PopulizeLevelMaps();
    }

    private void PopulizeLevelMaps()
    {
        Maps = Resources.LoadAll<Texture2D>("Levels").ToList();
    }

    public void GenerateLevel(int levelIndex)
    {
        Texture2D _map = Maps[levelIndex];

        for (int x = 0; x < _map.width; x++)
        {
            for (int y = 0; y < _map.height; y++)
                GenerateTile(_map, x, y);
        }
    }

    private void GenerateTile(Texture2D map, int x, int y)
    {
        Color pixelColor = map.GetPixel(x, y);

        if (pixelColor.a == 0)
            return;

        foreach (PixelToObject pixelMapping in pixelMappings)
        {
            if (pixelMapping.Color.Equals(pixelColor))
            {
                Vector3 pos = new Vector3(x, pixelMapping.OffsetY, y);
                Instantiate(pixelMapping.Prefab, pos, Quaternion.identity, LevelParent);
            }
        }
    }
}

[System.Serializable]
public class PixelToObject
{
    public string Name;
    public Color Color;
    public GameObject Prefab;
    public float OffsetY;
}