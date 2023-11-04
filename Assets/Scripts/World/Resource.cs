using System;

[Serializable]
public struct Resource
{
    public string name;
    public int minimalLevel;
    public int price;
    public int quantity;

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
