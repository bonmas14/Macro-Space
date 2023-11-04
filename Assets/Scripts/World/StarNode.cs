using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class StarNode : MonoBehaviour
{
    private Star info;

    public event Action<StarNode, Star> OnNodeClick;

    void Start()
    {
        info = StarGenerator.GetStar(transform.position.magnitude);
    }

    private void OnMouseDown()
    {
        OnNodeClick?.Invoke(this, info);
    }
}
