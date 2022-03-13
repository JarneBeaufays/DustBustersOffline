using UnityEngine;
using TMPro;

public class PlayerElement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private int _id;

    public int ID { get { return _id; } }

    private void Start()
    {
        // Connect to correct parent object
        transform.SetParent(GameObject.Find("MainMenu").transform);
        
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0f);
        transform.localScale = new Vector3(5.5f, 6.2f, 5.5f);

        // Find local ID
        PlayerElement[] playerElements = FindObjectsOfType<PlayerElement>();
        _id = playerElements.Length - 1;                    // -1 bcs we don't count this gameobjects' component

        _text.text = "Player - " + (_id + 1).ToString();    // +1 bcs visual 0-index arrays are shit
    }
}
