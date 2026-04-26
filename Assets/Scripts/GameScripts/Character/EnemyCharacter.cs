using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemyCharacter : Character, IClickable
{
    public UnityAction<EnemyCharacter> m_onEnemySelect;
    [SerializeField] GameObject m_highlightHover;
    bool m_mouseHover = false;
    void Start()
    {
        m_damageAmount = 100;
        m_currentHealth = 200;
        m_highlightHover.SetActive(false);
    }
    void FixedUpdate()
    {
        // if (m_mouseHover)
        // {
        //     // TODO also check if needed to do mouse hover = ability that affects enemy
        //     m_highlightHover.SetActive(true);
        // }
        // else
        // {
        //     m_highlightHover.SetActive(false);
        // }
    }
    public void Selected()
    {
        TakeDamage(m_characterManager.m_selectedCharacter.m_damageAmount);
    }
    public void Deselected()
    {

    }
    void OnMouseDown()
    {
        // if (!GetCharacterInteractable())
        // {
        //     return;
        // }
        // Debug.Log("mouse down on enemy");

        // // TODO redirect from here to character manager to handle actions

        // PlayableCharacter currentCharacter = m_characterManager.m_selectedCharacter;
        // if (currentCharacter == null && m_characterManager.m_currentActionType != GameState.ActionType.Ability)
        // {
        //     Debug.Log("DONT attack current char null and action not ability");
        //     // TODO highlight enemy movement/ ability (maybe simplest to highlight both at the same time and use different colors)
        //     return;
        // }
        // if (!IsTargetCharacterInRange())
        // {
        //     Debug.Log("DONT attack, not in range");

        //     return;
        // }
        // Debug.Log("do attack");
        // m_onEnemySelect.Invoke(this);
    }
    void OnMouseEnter()
    {
        // if (!GetCharacterInteractable())
        // {
        //     return;
        // }
        // if (m_characterManager.m_currentActionType == GameState.ActionType.Move)
        // {
        //     // No need to highlight enemy is player has selected movement
        //     // TODO ability can affect also affect other own character (heal), so that needs checking too
        //     return;
        // }
        // m_mouseHover = true;
    }

    void OnMouseExit()
    {
        // no interactable check just to be safe
       // m_mouseHover = false;
    }
}
