using System;

[Serializable]
public struct Resource
{
    public string name;
    public int minimalLevel;
    public int price;
    public int quantity;

    public override string ToString()
    {
        return string.Format("{0}: {1} | {2} price per one", name, quantity, price);
    }
}
