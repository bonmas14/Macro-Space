using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIStarInfo : MonoBehaviour
{
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private TextMeshProUGUI textMesh;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            infoPanel.SetActive(false);
    }

    public void OnInfoUpdated(StarNode node, Star info)
    {
        textMesh.text = info.ToString();
        infoPanel.SetActive(true);
    }
}
