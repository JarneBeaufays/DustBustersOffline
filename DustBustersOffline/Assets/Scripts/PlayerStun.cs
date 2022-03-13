using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStun : MonoBehaviour
{
    [SerializeField] private float _stunDuration = 2f;

    private Player _player;

    private float _deltaStun = 0f;
    private bool _stunned = false;

    private void Start()
    {
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        if (!_stunned) return;

        _deltaStun += Time.deltaTime;
        if(_deltaStun >= _stunDuration) 
        {
            _stunned = false;
            _player.enabled = true;
        }
    }

    public void Stun() 
    {
        _stunned = true;
        _player.enabled = false;
    }
}
