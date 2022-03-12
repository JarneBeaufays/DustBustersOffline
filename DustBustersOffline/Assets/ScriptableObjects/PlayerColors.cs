using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PlayerColors", menuName = "ScriptableObjects/PlayerColors", order = 1)]
public class PlayerColors : ScriptableObject
{
    public List<Color> _colors = new List<Color>();
}