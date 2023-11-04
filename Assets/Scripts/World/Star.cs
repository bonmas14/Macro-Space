using System;
using UnityEngine;

[Serializable]
public struct Star
{
    public int id;
    public Vector2 position;
    public string name;
    public int level;
    public Resource[] resources;

    public Star(int id, string name, int level, Resource[] resources, Vector2 position)
    {
        this.id = id;
        this.name = name;
        this.level = level;
        this.resources = resources;
        this.position = position;
    }

    private readonly int GetResourcesCount(Resource[] resources)
    {
        int count = 0;

        for (int i = 0; i < resources.Length; i++)
        {
            count += resources[i].quantity;
        }

        return count;
    }

    public override string ToString()
    {
        return string.Format("PL-{0:0000} \"{1}\" Level: {2}\n Resources: {3}", id, name, level, GetResourcesCount(resources));
    }
}
