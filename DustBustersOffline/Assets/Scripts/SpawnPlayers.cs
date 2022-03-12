using UnityEngine;

public class SpawnPlayers : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;

    private float x = 4.6f;
    private float z = 4.6f;
    private float y = 1f;

    private void Start()
    {
        Vector3 position = new Vector3(Random.Range(-x, x), y, Random.Range(-z, z));
        Instantiate(_playerPrefab, position, Quaternion.identity);
    }
}