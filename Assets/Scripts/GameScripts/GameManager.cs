using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] BoardCreator m_boardManager;
    [SerializeField] CharacterManager m_characterManager;
    [SerializeField] BattleSystem m_battleSystem;

    void Start()
    {
        m_boardManager.CreateBoard();
        m_characterManager.InitEnemies();
        m_characterManager.InitCharacters();
        m_battleSystem.InitMatch(m_characterManager);
    }
}
