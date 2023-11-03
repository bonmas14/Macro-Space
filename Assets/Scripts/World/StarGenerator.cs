using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StarGenerator
{
    private static TextAsset file;

    public static Star GetStar()
    {
        if (file == null)
            file = Resources.Load("starnames") as TextAsset;

        var text = file.text.Split('\n');

        var name = text[Random.Range(0, text.Length)];

        return new Star(Random.Range(1, 10000), name.Trim('\n', ' '));
    }
}

public struct Star
{
    public readonly int id;
    public readonly string name;

    public Star(int id, string name)
    {
        this.id = id;
        this.name = name;
    }

    public override string ToString()
    {
        return string.Format("PL-{0:0000} \"{1}\"\n", id, name);
    }
}
