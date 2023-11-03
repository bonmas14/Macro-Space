using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class StarNode : MonoBehaviour
{
    public Star info { get; private set; }

    public event Action<StarNode, Star> OnNodeClick;

    void Start()
    {
        info = StarGenerator.GetStar();
    }

    private void OnMouseDown()
    {
        OnNodeClick?.Invoke(this, info);
    }
}
