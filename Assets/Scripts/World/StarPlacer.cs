using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StarPlacer : MonoBehaviour
{
    [SerializeField] private GameObject starPrefab;
    [SerializeField] private UIStarInfo starInfo;

    private List<StarNode> stars;

    void Start()
    {
        if (GameSave.HasGameSaveFile())
            LoadGame(GameSave.LoadGameSave());
        else
            LoadGame(GameGenerator.GenerateGame());

        GameSave.SaveGame(new SaveFile(GetStars()));
    }


    private Star[] GetStars()
    {
        Star[] output = new Star[stars.Count];

        for (int i = 0; i < stars.Count; i++)
        {
            output[i] = stars[i].info;
        }
        
        return output;
    }

    private void LoadGame(SaveFile gameSave)
    {
        stars = new List<StarNode>();

        for (int i = 0; i < gameSave.stars.Length; i++)
        {
            var star = Instantiate(starPrefab, gameSave.stars[i].position, Quaternion.identity, this.transform);

            var node = star.GetComponent<StarNode>();

            node.info = gameSave.stars[i];
            node.OnNodeClick += starInfo.OnInfoUpdated;

            stars.Add(node);
        }
    }
}
