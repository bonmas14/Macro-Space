using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class StarGenerator
{
    private static TextAsset names;
    private static TextAsset modifiers;

    public const int baseLevel = 1;
    public const float levelMultiplierByDistance = 0.05f;
    public const int baseResources = 1000;
    public const float resourcesMultiplierByDistance = 0.02f;

    public static Star GetStar(float offsetFromCenter)
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

        return new Star(Random.Range(1, 10000), fullname, (int)(baseLevel + baseLevel * levelMultiplierByDistance * offsetFromCenter), (int)(baseResources + baseResources * resourcesMultiplierByDistance * offsetFromCenter));
    }
}

public static class ResourceGenerator
{
    public static Resource[] GetResources(int quantity, int level)
    {
        return new Resource[]
        {
            new Resource("Metal", 2, (int)(Mathf.Exp(0.01f * -Mathf.PI * ((float)quantity / StarGenerator.baseResources) * 1.01f) * 10), quantity)
        };
    }
}

public struct Resource
{
    public readonly string name;
    public readonly int minimalLevel;
    public readonly int price;
    public readonly int quantity;

    public Resource(string name, int minimalLevel, int price, int quantity)
    {
        this.name = name;
        this.minimalLevel = minimalLevel;
        this.price = price;
        this.quantity = quantity;
    }

    public override string ToString()
    {
        return string.Format("{0}: {1} | {2} price per one", name, quantity, price);
    }
}

public struct Star
{
    public readonly int id;
    public readonly string name;
    public readonly int level;
    public readonly Resource[] resources;

    public Star(int id, string name, int level, int quantity)
    {
        this.id = id;
        this.name = name;
        this.level = level;
        this.resources = ResourceGenerator.GetResources(quantity, level);
    }

    public override string ToString()
    {
        return string.Format("PL-{0:0000} \"{1}\" Level: {2}\n{3}", id, name, level, GetResources(resources));
    }

    private string GetResources(Resource[] resources)
    {
        var builder = new StringBuilder("Resources:\n");

        for (int i = 0; i < resources.Length; i++)
        {
            builder.AppendLine(resources[i].ToString());
        }

        return builder.ToString();
    }
}
