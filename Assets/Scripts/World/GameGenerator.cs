using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GameGenerator
{
    private const int planetCount = 400;
    private const int width = 400;
    private const int height = 200;
    private const float minimalDistance = 20;
    private const float planetDestroyPercents = 50;
    private const int maxAttempts = 100;
    
    private static TextAsset names;
    private static TextAsset modifiers;

    public const int baseLevel = 1;
    public const float levelMultiplierByDistance = 0.05f;
    public const int baseResources = 1000;
    public const float resourcesMultiplierByDistance = 0.02f;

    public static Star GetStar(Vector2 position, float offsetFromCenter)
    {
        if (names == null)
            names = Resources.Load("starnames") as TextAsset;
        if (modifiers == null)
            modifiers = Resources.Load("modifier") as TextAsset;

        var namesList = names.text.Split('\n');
        var modifiersList = modifiers.text.Split('\n');

        var name = namesList[Random.Range(0, namesList.Length)].Trim('\n', ' ');
        var modifier = modifiersList[Random.Range(0, modifiersList.Length)].Trim('\n', ' ');
        var id = Random.Range(1, 10).ToString();

        var fullname = string.Format("{0} {1}-{2}", name, modifier, id);

        int level = (int)(baseLevel + baseLevel * levelMultiplierByDistance * offsetFromCenter);
        int quantity = (int)(baseResources + baseResources * resourcesMultiplierByDistance * offsetFromCenter);

        return new Star(Random.Range(1, 10000), fullname, level, ResourceGenerator.GetResources(quantity, level), position);
    }

    public static SaveFile GenerateGame()
    {
        var stars = new List<Star>(planetCount);

        int attempts = 0;

        for (int i = 0; i < planetCount; i++)
        {
            var x = Random.Range(-width + Random.Range(-20, 20), width + Random.Range(-20, 20)) + Mathf.PerlinNoise1D(i);
            var y = Random.Range(-height + Random.Range(-20, 20), height + Random.Range(-20, 20)) + Mathf.PerlinNoise1D(i);

            var position = Matrix4x4.Scale(new Vector3(width, height, 1)) * (Vector3)Random.insideUnitCircle;

            float probability = Mathf.Clamp01(Mathf.PerlinNoise(x, y));

            if (Random.Range(0f, 1f) <= probability)
            {
                i--;
                continue;
            }

            if (stars.Any(x => Vector3.Distance(x.position, position) <= minimalDistance))
            {
                i--;

                if (attempts < maxAttempts)
                    attempts++;
                else
                    break;

                continue;
            }

            var star = GetStar(position, position.magnitude);
            stars.Add(star);

            attempts = 0;
        }

        for (int i = 0; i < (planetCount * (planetDestroyPercents / 100f)); i++)
        {
            int deleteIndex = Random.Range(0, stars.Count);

            stars.RemoveAt(deleteIndex);
        }

        return new SaveFile(stars.ToArray());
    }
}

public static class ResourceGenerator
{
    public static Resource[] GetResources(int quantity, int level)
    {
        return new Resource[]
        {
            new Resource("Metal", 2, (int)(Mathf.Exp(0.01f * -Mathf.PI * ((float)quantity / GameGenerator.baseResources) * 1.01f) * 10), quantity)
        };
    }
}
