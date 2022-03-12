using UnityEngine;

public class LightningSpawner : MonoBehaviour
{
    [Header("--- Gameobjects & Components ---")]
    [SerializeField] private GameObject _prefabToSpawn;
    [SerializeField] private Transform _leftCorner;
    [SerializeField] private Transform _rightCorner;

    [Header("--- Gameplay ---")]
    [SerializeField] private float _timeBetweenSpawns = 20f;
    [SerializeField] private float _randomTimeToAddSub = 10f;

    private float _actualSpawnTime = 0f;
    private float _deltaSpawn = 0f;

    private void Start()
    {
        _actualSpawnTime = _timeBetweenSpawns + Random.Range(0f, _randomTimeToAddSub) - (_randomTimeToAddSub / 2f);
    }

    private void Update()
    {
        _deltaSpawn += Time.deltaTime;
        if (_deltaSpawn >= _actualSpawnTime) 
        {
            _deltaSpawn = 0f;
            Spawn();
        }
    }

    private void Spawn() 
    {
        _actualSpawnTime = _timeBetweenSpawns + Random.Range(0f, _randomTimeToAddSub) - (_randomTimeToAddSub / 2f);

        float x = Random.Range(_leftCorner.position.x, _rightCorner.position.x);
        float z = Random.Range(_rightCorner.position.z, _leftCorner.position.z);
        Instantiate(_prefabToSpawn, new Vector3(x, transform.position.y, z), Quaternion.identity);
    }
}
