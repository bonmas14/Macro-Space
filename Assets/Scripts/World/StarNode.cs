using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class StarNode : MonoBehaviour
{
    public Star Info
    {
        get => GetInfo();
        set => SetStar(value);
            
    }

    private Star star;
    private Resource[] resources;

    private Star GetInfo()
    {
        star.resources = resources;

        return star;
    }

    private void SetStar(Star value)
    {
        transform.position = value.position;
        resources = value.resources;
        star = value;
    }

    public event Action<StarNode, Star> OnNodeClick;
    
    private void OnMouseDown()
    {
        OnNodeClick?.Invoke(this, Info);
    }
}
