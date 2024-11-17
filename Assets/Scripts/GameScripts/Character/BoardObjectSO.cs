using UnityEngine;

[CreateAssetMenu(fileName = "BoardObject", menuName = "ScriptableObjects/BoardOvjectSO", order = 1)]
public class BoardObjectSO : ScriptableObject
{
    // container for object to be placed on the game board
    // this could also hold health etc values for for example starting match with a health booster
    public GameObject prefab;
    public Vector2 startPosition;
}