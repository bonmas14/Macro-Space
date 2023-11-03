using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StarPlacer : MonoBehaviour
{
    [SerializeField] private GameObject starPrefab;
    [SerializeField] private int planetCount = 1000;
    [SerializeField] private int width = 400;
    [SerializeField] private int height = 200;
    [SerializeField] private float minimalDistance = 10;
    [SerializeField] private float planetDestroyPercents = 50;

    [SerializeField] private UIStarInfo starInfo;

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

            var position = Matrix4x4.Scale(new Vector3(width, height, 1)) * (Vector3)Random.insideUnitCircle;

            float probability = Mathf.Clamp01(Mathf.PerlinNoise(x, y));

            if (Random.Range(0f, 1f) <= probability)
            {
                i--;
                continue;
            }

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

            var node = star.GetComponent<StarNode>();
            node.OnNodeClick += starInfo.OnInfoUpdated;
            
            stars.Add(node);
        }

        for (int i = 0; i < (planetCount * (planetDestroyPercents / 100f)); i++)
        {
            int deleteIndex = Random.Range(0, stars.Count);

            Destroy(stars[deleteIndex].gameObject);

            stars.RemoveAt(deleteIndex);
        }
    }
}
