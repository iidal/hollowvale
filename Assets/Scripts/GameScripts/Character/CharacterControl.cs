using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterControl : MonoBehaviour
{
    public UnityAction<CharacterControl> m_onCharacterSelect;
    public UnityAction<CharacterControl> m_onCharacterDeselect;
    TileControl m_tilePosition;
    bool m_mouseHover = false;
    [SerializeField] GameObject m_highlightClicked;

    bool m_characterSelected = false;
    [SerializeField] GameObject m_highlightHover;

    void Start()
    {
        m_highlightHover.SetActive(false);
    }
    public void InitCharacter(TileControl tile)
    {
        m_tilePosition = tile;
        transform.SetPositionAndRotation(m_tilePosition.transform.position, Quaternion.identity);
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
    public void CharacterSelected()
    {
        m_characterSelected = true;
        m_highlightClicked.SetActive(true);
        m_highlightHover.SetActive(false);
    }
    public void CharacterDeselected()
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
