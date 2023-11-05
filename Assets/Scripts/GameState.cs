using UnityEngine;

public static class GameState
{
    public static SaveFile CurrentState => SaveGame();

    public static GameCreator controller;

    private static SaveFile SaveGame()
    {
        if (controller == null)
            Debug.LogWarning("Save link doesnt exisit");

        return controller.GetState();
    }
}
