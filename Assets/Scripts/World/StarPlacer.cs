using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

public class StarPlacer : MonoBehaviour
{
    [SerializeField] private GameObject starPrefab;
    [SerializeField] private int planetCount = 1000;
    [SerializeField] private int width = 400;
    [SerializeField] private int height = 200;
    [SerializeField] private float minimalDistance = 10;

    private const int maxAttempts = 100;

    private List<StarNode> stars;

    void Start()
    {
        stars = new List<StarNode>();

        int attempts = 0;

        for (int i = 0; i < planetCount; i++)
        {
            var x = Random.Range(-width + Random.Range(-20, 20), width + Random.Range(-20, 20)) + Mathf.PerlinNoise1D(i);
            var y = Random.Range(-height + Random.Range(-20, 20), height + Random.Range(-20, 20)) + Mathf.PerlinNoise1D(i);

            var position = new Vector3(x, y, 0);

            if (stars.Any(x => Vector3.Distance(x.transform.position, position) <= minimalDistance))
            {
                i--;
                
                if (attempts < maxAttempts)
                    attempts++;
                else
                    break;

                continue;
            }

            attempts = 0;

            var star = Instantiate(starPrefab, position, Quaternion.identity, this.transform);
            stars.Add(star.GetComponent<StarNode>());
        }
    }
}
