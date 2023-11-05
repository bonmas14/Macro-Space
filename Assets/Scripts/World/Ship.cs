using System;
using UnityEngine;

[Serializable]
public struct Ship
{
    public int id;
    public string name;

    public ShipTypes type;
    public ShipTraits traits;
    public Resource[] resources;

    public Quaternion rotation;
    public Vector2 position;
}


[Serializable]
public enum ShipTypes
{
    Miner, 
    Trader,
    Attacker,
    Ruins
}

[Serializable]
public struct ShipTraits
{
    public float attackStrenght;
    public float attackSpeed;
    public AttackType attackType;

    public float mineStrength;
    public float mineSpeed;

    public int resourcesCapacity;
}

[Serializable]
public enum AttackType
{
    Physical,
    Electric
}
