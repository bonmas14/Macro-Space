using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMover : MonoBehaviour
{

    [SerializeField] private Bounds borders;

    [SerializeField] private float minSize = 10f;
    [SerializeField] private float maxSize = 100f;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float zoomSpeed = 2f;

    private Camera view;

    private void Start()
    {
        view = GetComponent<Camera>();
    }

    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        float scroll = Mathf.Pow(Input.mouseScrollDelta.y * zoomSpeed, 2);
        
        if (Input.mouseScrollDelta.y < 0)
            scroll *= scroll / -scroll;

        view.orthographicSize = Mathf.Min(maxSize, Mathf.Max(minSize, view.orthographicSize - scroll));

        var position = new Vector3(horizontal * moveSpeed / (minSize / view.orthographicSize), vertical * moveSpeed / (minSize / view.orthographicSize));

        transform.position += position;

        if (!borders.Contains(transform.position))
            PullBack();
    }

    private void PullBack()
    {
        var pullTo = borders.ClosestPoint(transform.position);

        var pullback = (transform.position - pullTo) * (moveSpeed / 2f);
        
        transform.position -= pullback / moveSpeed;
    }
}
