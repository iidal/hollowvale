using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class TileControl : MonoBehaviour
{
    public UnityAction<TileControl> m_onTileSelect;
    public UnityAction<TileControl> m_onTileDeselect;
    [SerializeField] GameObject m_highlightHover;
    [SerializeField] GameObject m_highlightClicked;

    Vector2 m_coordinates;
    BoardCreator m_boardManager;
    bool m_mouseHover = false;
    public bool m_tileSelected = false;

    void Start()
    {
        m_highlightHover.SetActive(false);
        m_highlightClicked.SetActive(false);
    }

    void FixedUpdate()
    {
        if (m_boardManager.IsBoardIdling())
        {
            // no tile has been selected, do hover highlighting
            TileHovering();
        }
    }
    void TileHovering()
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

    public void InitTile(Vector2 coordinates, BoardCreator manager)
    {
        m_coordinates = coordinates;
        m_boardManager = manager;
    }

    public void TileSelected()
    {
        m_tileSelected = true;
        m_highlightClicked.SetActive(true);
        m_highlightHover.SetActive(false);
    }
    public void TileDeselected()
    {
        m_tileSelected = false;
        m_highlightClicked.SetActive(false);
    }

    void OnMouseDown()
    {
        if (!m_tileSelected)
        {
            m_onTileSelect.Invoke(this);
        }
        else
        {
            m_onTileDeselect.Invoke(this);
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
