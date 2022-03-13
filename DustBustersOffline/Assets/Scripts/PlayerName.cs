using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerName : MonoBehaviour
{
    [SerializeField] private GameObject _uiPrefab;

    GameObject _listingElement;
    List<TextMeshProUGUI> _textScores = new List<TextMeshProUGUI>();

    //private void Start()
    //{
    //    _listingElement = GameObject.Find("ScoreListing");
    //
    //    transform.position = new Vector3(Random.Range(-5f, 5f), 1f, Random.Range(-5f, 5f));
    //}
    //
    //private void Update()
    //{       
    //    UpdateScoreTexts();
    //}
    //private void UpdateScoreTexts()
    //{
    //    int playerCount = 4; // Fix this!
    //
    //    while (_textScores.Count < playerCount)
    //    {
    //        GameObject ui = Instantiate(_uiPrefab);
    //        _listingElement.transform.SetParent(ui.transform);
    //        _textScores.Add(ui.GetComponentInChildren<TextMeshProUGUI>());
    //    }
    //
    //    while (_textScores.Count > playerCount)
    //    {
    //        int i = _textScores.Count - 1;
    //        GameObject obj = _textScores[i].gameObject;
    //        _textScores.RemoveAt(i);
    //        Destroy(obj.transform.parent.gameObject);
    //    }
    //}
}