using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class LobbyPlayer : MonoBehaviour
{
    [SerializeField] private PlayerColors _playerColors = null;
    [SerializeField] private Image _bodySprite;

    private int _curColorId = 0;

    public int ColorId { get { return _curColorId; } }

    private void Start()
    {
        _bodySprite.color = _playerColors._colors[_curColorId];

        GameObject.Find("LobbyManager").GetComponent<LobbyScreen>().AddPlayer(this);
    }

    public void SwapCharLeft(InputAction.CallbackContext context) 
    {
        if (!context.action.triggered) return;

        Debug.Log("Character swapped left");

        _curColorId--;
        if (_curColorId < 0) _curColorId =
                _playerColors._colors.Count - 1;

        _bodySprite.color = _playerColors._colors[_curColorId];
    }

    public void SwapCharRight(InputAction.CallbackContext context)
    {
        if (!context.action.triggered) return;

        Debug.Log("Character swapped right");

        _curColorId++;
        if (_curColorId == _playerColors._colors.Count)
            _curColorId = 0;

        _bodySprite.color = _playerColors._colors[_curColorId];
    }
}
