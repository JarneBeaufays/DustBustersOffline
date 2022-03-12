using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyScreen : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab = null;

    private Transform _playerElementContainer;

    private void Start()
    {
        _playerElementContainer = GameObject.Find("MainMenu").transform;
    }

    public void AddPlayer() 
    {
        GameObject obj = Instantiate(_playerPrefab);
        obj.transform.SetParent(_playerElementContainer);
    }

    public void StartGame() 
    {
        SceneManager.LoadScene("Game");
    }
}
