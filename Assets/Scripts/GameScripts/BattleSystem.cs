using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    private static WaitForSeconds _waitForSeconds3_0 = new(3.0f); // cached waitforseconds for optimization

    public enum BattleState { PlayerTurn, EnemyTurn }
    BattleState m_currentState = BattleState.PlayerTurn;
    CharacterManager m_characterManager;
    BoardActionManager m_actionManager;



    public void InitMatch(CharacterManager characterManager, BoardActionManager boardActionManager)
    {
        m_characterManager = characterManager;
        m_actionManager = boardActionManager;
        m_currentState = BattleState.PlayerTurn;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        Debug.Log("PlayerTurn");
        m_characterManager.SetCharactersInteractable(true);
        m_actionManager.StartTurn(); // Reset actions
    }
    public void EndPlayerTurn()
    {
        // called from scene
        m_characterManager.SetCharactersInteractable(false);
        m_characterManager.CharacterUnclicked(m_characterManager.m_selectedCharacter);
        StartCoroutine(EnemyTurn());
    }
    IEnumerator EnemyTurn()
    {
        Debug.Log("Enemy turn, going straight back to player turn 3, 2, 1");
        yield return _waitForSeconds3_0;
        EndEnemyTurn();
    }
    void EndEnemyTurn()
    {
        PlayerTurn();
    }
}
