using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public enum BattleState { PlayerTurn, EnemyTurn } //?
    CharacterManager m_characterManager;
    
    
    public void InitMatch(CharacterManager characterManager)
    {
        m_characterManager = characterManager;
        // start player turn
    }

    void PlayerTurn()
    {
        // Set all players characters interactable
        // Reset actions that can be taken during a turn

    }
    void EndPlayerTurn()
    {
        // called from end turn button


    }
    void EnemyTurn()
    {

    }
    void EndEnemyTurn()
    {

    }
}
