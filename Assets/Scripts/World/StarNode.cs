using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class StarNode : MonoBehaviour
{
    public Star info;

    public event Action<StarNode, Star> OnNodeClick;
    
    private void OnMouseDown()
    {
        OnNodeClick?.Invoke(this, info);
    }
}
