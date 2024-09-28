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

    public Vector2 m_coordinates; //temporary public, get via fucntion
    BoardCreator m_boardManager;
    bool m_mouseHover = false;
    bool m_tilePreviewOn = false;

    void Start()
    {
        m_highlightHover.SetActive(false);
        m_highlightClicked.SetActive(false);
    }

    void FixedUpdate()
    {
        //tiles are higlighted only if they are previeved for movement or abilities
        if (m_tilePreviewOn)
        {
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

    public void TilePreviewOn()
    {
        m_tilePreviewOn = true;
        m_highlightClicked.SetActive(true);
        m_highlightHover.SetActive(false);
    }
    public void TilePreviewOff()
    {
        m_tilePreviewOn = false;
        m_highlightClicked.SetActive(false);
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
