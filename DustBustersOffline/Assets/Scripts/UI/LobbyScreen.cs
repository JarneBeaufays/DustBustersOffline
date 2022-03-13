using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class LobbyScreen : MonoBehaviour
{  
    public void StartGame()
    {
        foreach(PlayerColor player in GameObject.FindObjectsOfType<PlayerColor>())
        {
            player.StartGame();
        }

        SceneManager.LoadScene("Game");
    }
}
