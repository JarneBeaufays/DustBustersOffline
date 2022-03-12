using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuisstofmijtManager : MonoBehaviour
{
    [SerializeField] private GameObject _huisstofmijtPrefab;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private int _amount = 3;

    void Awake()
    {
        for (int i=0; i < _amount; i++)
        {
            SpawnMijt();
        }
    }
    
    public void SpawnMijt()
    {
        Vector3 dustLocation = _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;
        Instantiate(_huisstofmijtPrefab, dustLocation, Quaternion.identity);
    }
}
