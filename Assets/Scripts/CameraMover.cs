using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMover : MonoBehaviour
{

    [SerializeField] private int width = 400;
    [SerializeField] private int height = 200;

    [SerializeField] private float minSize = 10f;
    [SerializeField] private float maxSize = 100f;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float pullbackSpeed = 0.1f;
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

        PullBack();
    }

    private void PullBack()
    {
        Vector4 direction = Matrix4x4.Scale(new Vector3(width, height, 0)) * ((Vector2)transform.position).normalized;

        if (direction.magnitude <= Mathf.Min(width, height))
            return;

        float xSpeed = 0;
        float ySpeed = 0;

        if ((width - Mathf.Abs(transform.position.x)) < 0)
            xSpeed = (width - Mathf.Abs(transform.position.x)) * (Mathf.Abs(transform.position.x) / transform.position.x);
        
        if ((height - Mathf.Abs(transform.position.y)) < 0)
            ySpeed = (height - Mathf.Abs(transform.position.y)) * (Mathf.Abs(transform.position.y) / transform.position.y);

        Vector2 pullback = new Vector2(xSpeed, ySpeed);
        transform.position += (Vector3)pullback * pullbackSpeed;
    }
}
