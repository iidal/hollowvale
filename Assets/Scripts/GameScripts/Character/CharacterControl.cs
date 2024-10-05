 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterControl : MonoBehaviour
{
    public UnityAction<CharacterControl> m_onCharacterSelect;
    public UnityAction<CharacterControl> m_onCharacterDeselect;
    public TileControl m_tilePosition; // Temporarily public, get via function
    bool m_mouseHover = false;
    [SerializeField] GameObject m_highlightClicked;

    bool m_characterSelected = false;
    [SerializeField] GameObject m_highlightHover;
    public List<Vector2> m_movementCoordinates; // make this private and accessed via a function
                                                // temporary, should be initialized from json config
    public List<Vector2> m_abilityCoordinates; // make this private and accessed via a function
                                                // temporary, should be initialized from json config

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
    public void InitCharacter(TileControl tile)
    {
        m_tilePosition = tile;
        SetTileToCharacter(m_tilePosition);
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
    public void SetTileToCharacter(TileControl newTile)
    {
        m_tilePosition = newTile;
        transform.SetPositionAndRotation(newTile.transform.position, Quaternion.identity);
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
