using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStun : MonoBehaviour
{
    [SerializeField] private GameObject _stunnedParticles;
    [SerializeField] private float _stunDuration = 2f;

    private ParticleSystem[] _particlesList;

    private Player _player;

    private float _deltaStun = 0f;
    private bool _stunned = false;

    private void Start()
    { 
        _player = GetComponent<Player>();
        _particlesList = _stunnedParticles.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem ps in _particlesList) ps.Stop();
    }

    private void Update()
    {
        if (!_stunned) return;

        _deltaStun += Time.deltaTime;
        if(_deltaStun >= _stunDuration) 
        {
            _stunned = false;
            _player.enabled = true;
            foreach (ParticleSystem ps in _particlesList) ps.Stop();
        }
    }

    public void Stun() 
    {
        if (_stunned) return;

        _stunned = true;
        _player.enabled = false;
        _deltaStun = 0f;

        foreach (ParticleSystem ps in _particlesList) ps.Play();

        Debug.Log("Player stunned");
    }
}
