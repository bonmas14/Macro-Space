using System;
using UnityEngine;

public class ShipNode : MonoBehaviour
{
    public Ship Info 
    {
        get => GetInfo();
        set => SetShip(value);
    }

    private Ship ship;

    private Resource[] resources;

    private void SetShip(Ship value)
    {
        transform.position = value.position; 
        transform.rotation = value.rotation;
        resources = value.resources;

        // TODO: load required sprite

        ship = value;
    }

    private Ship GetInfo()
    {
        ship.position = transform.position;
        ship.rotation = transform.rotation;
        ship.resources = resources;

        return ship;
    }

    void Update()
    {

    }
}
