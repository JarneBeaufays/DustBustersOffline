using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private float _loadingTime = 5f;
    [SerializeField] private float _randomTime = 5f;
    private float _deltaTime = 0f;

    private void Start()
    {
        _loadingTime += Random.Range(0f, _randomTime) - (_loadingTime / 2f);
        Debug.Log("Lobby scene loading: " + _loadingTime.ToString());
    }

    private void Update()
    {
        _deltaTime += Time.deltaTime;
        if (_deltaTime >= _loadingTime)
        {
            SceneManager.LoadScene("Lobby");
        }
    }
}