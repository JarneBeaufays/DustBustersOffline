using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LobbyScreen : MonoBehaviour
{
    List<LobbyPlayer> _players = new List<LobbyPlayer>();

    public int GetColorId(int playerId) 
    {
        return _players[playerId].ColorId;
    }

    public void AddPlayer(LobbyPlayer player) 
    {
        _players.Add(player);
    }

    public void StartGame()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene("Game");
    }
}
