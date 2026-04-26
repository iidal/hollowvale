using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] BoardManager m_boardManager;
    [SerializeField] CharacterManager m_characterManager;
    [SerializeField] BattleSystem m_battleSystem;
    [SerializeField] BoardActionManager m_boardActionManager;

    void Start()
    {

        m_boardManager.CreateBoard();

        m_characterManager.InitCharacters(); // temp, do in board manager/creation from config
        m_characterManager.InitEnemies();
        // TODO add all board objects need to be added to character manager m_boardObjecst

        m_boardActionManager.Init();
        m_battleSystem.InitMatch(m_characterManager, m_boardActionManager);
    }

    // This class should have references to characters
    // ie for example the character doesnt control checking and doing damage
    // a manager handles doing actions on one character and applying those to other characters
}
