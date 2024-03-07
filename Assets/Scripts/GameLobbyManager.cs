using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLobbyManager : MonoBehaviour
{
    public void GameStart()
    {
        LoadingSceneManager.LoadScene(2);
    }
}
