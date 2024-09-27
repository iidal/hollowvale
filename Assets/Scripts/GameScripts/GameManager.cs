using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] BoardCreator m_boardManager;
    [SerializeField] CharacterManager m_characterManager;

    void Start()
    {
        m_boardManager.CreateBoard();
        m_characterManager.InitCharacters();

    }
}
