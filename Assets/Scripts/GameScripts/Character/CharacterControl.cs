using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    TileControl m_tilePosition;
    bool m_mouseHover = false;
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
    void OnMouseDown()
    {

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
