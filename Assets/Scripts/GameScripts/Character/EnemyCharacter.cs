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
        if (m_mouseHover)
        {
            m_highlightHover.SetActive(true);
        }
        else
        {
            m_highlightHover.SetActive(false);
        }
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
        Debug.Log("enemy mouse down");
        PlayableCharacter currentCharacter = m_characterManager.m_selectedCharacter;
        if (currentCharacter == null && m_characterManager.m_currentActionType != GameState.ActionType.Ability)
        {
            Debug.Log("DONT attack");
            return;
        }
        if (!IsEnemyInRange())
        {
        Debug.Log("DONT attack");

            return;
        }
        Debug.Log("do attack");
        m_onEnemySelect.Invoke(this);
    }
    void OnMouseEnter()
    {
        m_mouseHover = true;
    }

    void OnMouseExit()
    {
        m_mouseHover = false;
    }
    bool IsEnemyInRange()
    {
        foreach (var coordinates in m_characterManager.m_selectedCharacter.m_abilityCoordinates)
        {
            if (m_tilePosition.m_coordinates.x == coordinates.x && m_tilePosition.m_coordinates.y == coordinates.y)
            {
                return true;
            }
        }
        return false;
    }
}
