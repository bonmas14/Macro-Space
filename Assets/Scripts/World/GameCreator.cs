using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameCreator : MonoBehaviour
{
    [SerializeField] private GameObject starPrefab;
    [SerializeField] private UIStarInfo starInfo;

    private List<StarNode> stars;
    private List<ShipNode> ships;

    void Start()
    {
        GameState.controller = this;

        if (GameSave.HasGameSaveFile())
            LoadGame(GameSave.LoadGameSave());
        else
            LoadGame(GameGenerator.GenerateGame());

        GameSave.SaveGame();
    }
    
    private void LoadGame(SaveFile gameSave)
    {
        stars = new List<StarNode>();

        for (int i = 0; i < gameSave.stars.Length; i++)
        {
            var node = Instantiate(starPrefab, this.transform).GetComponent<StarNode>();

            node.Info = gameSave.stars[i];
            node.OnNodeClick += starInfo.OnInfoUpdated;

            stars.Add(node);
        }
    }

    public SaveFile GetState()
    {
        Star[] stars = new Star[this.stars.Count];

        for (int i = 0; i < this.stars.Count; i++)
        {
            stars[i] = this.stars[i].Info;
        }

        return new SaveFile(stars);
    }
}
