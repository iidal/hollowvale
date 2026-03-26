using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BoardActionManager : MonoBehaviour
{
    Dictionary<GameState.ActionType, bool> m_actions;
    [SerializeField] GameButton m_moveButton;
    [SerializeField] GameButton m_abilityButton;
    [SerializeField] GameButton m_endTurnButton;
    [SerializeField] GameObject m_actionButtons;


    public void Init()
    {
        m_actions = new Dictionary<GameState.ActionType, bool>
        {
            { GameState.ActionType.Move, true},
            { GameState.ActionType.Ability, true }

        };
        m_actionButtons.SetActive(false);
    }

    public void StartTurn()
    {
        foreach (var key in m_actions.Keys.ToList()) 
        {
            m_actions[key] = true;
        }
    }

    public void ShowActionButtons(GameState.ActionType action)
    {
        Debug.Log("ShowActionbuttons " + action);
        m_actionButtons.SetActive(true);
        ActionPreview((int)action);
    }

    public void HideActionButtons()
    {
        m_actionButtons.SetActive(false);
    }
// ========================================================================================
// Highlighting buttons with sprite changes
// TODO this could be combined with character managers action selection a little bit so no need to call two functions from editor
    public void ActionPreview(int actionIndex)
    {
        // TODO REFACTOR either this takes int or string, that is then casted to Actiontype in Gamestate, applies to CharacterManager.SetSelectedAction too
        GameState.ActionType actionType = GameState.IntToGameStateEnum(actionIndex);
        // highlight the chosen action button
        switch (actionType)
        {
            case GameState.ActionType.Move:
                m_moveButton.Highlight(true);
                m_abilityButton.Highlight(false);
                break;
            case GameState.ActionType.Ability:
                m_moveButton.Highlight(false);
                m_abilityButton.Highlight(true);
                break;
            default:
                Debug.LogError("Trying to get invalid ActionType from ActionPreview");
                break;
        }
    }
    //=======================================================================================================

    public void ActionTaken(GameState.ActionType actionType)
    {
        Debug.Log("ActionTaken " + actionType);
        m_actions[actionType] = false;
        switch (actionType)
        {
            case GameState.ActionType.Ability:
                m_abilityButton.Inactivate();
                break;
            case GameState.ActionType.Move:
                m_moveButton.Inactivate();
                break;
            case GameState.ActionType.EndTurn:
                ResetTurn();
                break;
            default:
                break;
        }

        foreach(var abil in m_actions){
            Debug.Log("ab: " + abil.Key + " state: " + abil.Value);
        }

        if (m_actions.Values.Any() != true) // make sure this works
        {
            ResetTurn();
        }
    }
    //=======================================================================================================
    void ResetTurn()
    {
        m_moveButton.Reset();
        m_abilityButton.Reset();
    }

    //=======================================================================================================
    // Return first action index that has not been used yet during a turn
    public int GetFirstUnusedAction()
    {
        foreach (var key in m_actions.Keys.ToList())
        {
            if(m_actions[key] == true){
                return (int)(key);
            }
        }
        Debug.LogWarning("Trying to get an unused action but all actions have been used, should not get here");
        return 0;
    }
}
