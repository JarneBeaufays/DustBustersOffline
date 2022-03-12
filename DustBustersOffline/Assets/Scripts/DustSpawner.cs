using UnityEngine;

public class DustSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _dustPrefab;
    [SerializeField] private float _timeBetweenSpawned;

    [SerializeField] private Transform _leftSpawnCorner = null;
    [SerializeField] private Transform _rightSpawnCorner = null;

    private float _deltaSpawn = 0f;

    private void Update()
    {
        // Update spawn rate
        _deltaSpawn += Time.deltaTime;
        if(_deltaSpawn >= _timeBetweenSpawned) 
        {
            Spawn();
            _deltaSpawn = 0f;
        }
    }

    private void Spawn()
    {
        // Position for the dust particle
        Vector3 dustLocation = new Vector3(Random.Range(_leftSpawnCorner.position.x, _rightSpawnCorner.position.x), 0f, Random.Range(_leftSpawnCorner.position.z, _rightSpawnCorner.position.z));
        Instantiate(_dustPrefab, dustLocation, Quaternion.identity);
    }
}
