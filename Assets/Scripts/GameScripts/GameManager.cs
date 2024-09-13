using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] BoardCreator m_boardManager;
    [SerializeField] GameObject m_characterPrefab;
    void Start()
    {
        m_boardManager.CreateBoard();
        GameObject character = Instantiate(m_characterPrefab);
        character.GetComponent<CharacterControl>().InitCharacter(m_boardManager.GetTileControl());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
