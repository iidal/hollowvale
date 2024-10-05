 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayableCharacter : Character, IClickable
{
    public UnityAction<PlayableCharacter> m_onCharacterSelect;
    public UnityAction<PlayableCharacter> m_onCharacterDeselect;
    bool m_mouseHover = false;
    [SerializeField] GameObject m_highlightClicked;

    bool m_characterSelected = false;
    [SerializeField] GameObject m_highlightHover;


    void Start()
    {
        m_abilityCoordinates.Add(new Vector2(-1,0));
        m_abilityCoordinates.Add(new Vector2(0,1));
        m_abilityCoordinates.Add(new Vector2(1,0));
        m_abilityCoordinates.Add(new Vector2(0, -1));

        m_movementCoordinates.Add(new Vector2(-2,0));
        m_movementCoordinates.Add(new Vector2(-1,1));
        m_movementCoordinates.Add(new Vector2(-1,0));
        m_movementCoordinates.Add(new Vector2(-1,-1));
        m_movementCoordinates.Add(new Vector2(0, 2));
        m_movementCoordinates.Add(new Vector2(0,1));
        m_movementCoordinates.Add(new Vector2(0,0));
        m_movementCoordinates.Add(new Vector2(0,-1));
        m_movementCoordinates.Add(new Vector2(0, -2));
        m_movementCoordinates.Add(new Vector2(1,1));
        m_movementCoordinates.Add(new Vector2(1,0));
        m_movementCoordinates.Add(new Vector2(1, -1));
        m_movementCoordinates.Add(new Vector2(2,0));


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
        m_characterSelected = true;
        m_highlightClicked.SetActive(true);
        m_highlightHover.SetActive(false);
    }
    public void Deselected()
    {
        m_characterSelected = false;
        m_highlightClicked.SetActive(false);
    }

    void OnMouseDown()
    {
        if (!m_characterSelected)
        {
            m_onCharacterSelect.Invoke(this);
        }
        else
        {
            m_onCharacterDeselect.Invoke(this);
        }
    }
    void OnMouseEnter()
    {
        m_mouseHover = true;
    }

    void OnMouseExit()
    {
        m_mouseHover = false;
    }
}
