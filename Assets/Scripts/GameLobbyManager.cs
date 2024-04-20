using UnityEngine;

public class GameLobbyManager : MonoBehaviour
{
    public void GameStart()
    {
        LoadingSceneManager.LoadScene(2);
    }
}
