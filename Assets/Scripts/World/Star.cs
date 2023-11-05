using System;
using UnityEngine;

[Serializable]
public struct Star
{
    public int id;
    public int level;
    public string name;
    public Vector2 position;
    public Resource[] resources;

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
