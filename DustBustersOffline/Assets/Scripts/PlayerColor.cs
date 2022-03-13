using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerColor : MonoBehaviour
{
    [SerializeField] private GameObject _lobbyPlayer;
    [SerializeField] private GameObject _gamePlayer;

    private PlayerInput _playerInput;
    private PlayerElement _playerElement;

    public int PlayerId { get; set; }
    public int ColorId { get; set; }

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.actions.actionMaps[1].Enable();

        _playerElement = GetComponentInChildren<PlayerElement>();

        _lobbyPlayer.SetActive(true);
        _lobbyPlayer.transform.SetParent(GameObject.Find("MainMenu").transform);
        _gamePlayer.SetActive(false);
    }

    public void StartGame() 
    {
        DontDestroyOnLoad(gameObject);

        PlayerId = _playerElement.ID;

        transform.position = new Vector3(Random.Range(-5f, 5f), 1f, Random.Range(-5f, 5f));
        transform.localScale = Vector3.one;

        _playerInput.actions.actionMaps[0].Enable();
        _gamePlayer.GetComponent<Player>().UpdateColors(_lobbyPlayer.GetComponent<LobbyPlayer>().ColorId);
        _gamePlayer.GetComponent<Player>().PlayerId = PlayerId;

        _lobbyPlayer.SetActive(false);
        _gamePlayer.SetActive(true);
    }
}
