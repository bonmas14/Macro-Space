using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIStarInfo : MonoBehaviour
{
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private GameObject selector;
    [SerializeField] private TextMeshProUGUI textMesh;

    private Camera cam;
    private StarNode node;

    private void Start()
    {
        cam = Camera.main;
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || node == null)
            infoPanel.SetActive(false);

        if (node != null)
            selector.transform.position = cam.WorldToScreenPoint(node.transform.position);
    }

    public void OnInfoUpdated(StarNode node, Star info)
    {
        this.node = node;
        textMesh.text = info.ToString();
        infoPanel.SetActive(true);
    }
}
