using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class BoardCreator : MonoBehaviour
{
    //TODO rename and/or restructure this/other boardmanaging logic scripts
    private TileControl[,] m_tiles;
    [SerializeField] private int m_xWidth, m_zWidth;
    private readonly float m_tileOffset = 0.15f;
    [SerializeField] private GameObject m_tilePrefab;
    [SerializeField] private CharacterManager m_characterManager;

    public void CreateBoard()
    {
        m_tiles = new TileControl[m_xWidth, m_zWidth];
        for (int i = 0; i < m_xWidth; i++)
        {
            for (int j = 0; j < m_zWidth; j++)
            {
                GameObject tile = Instantiate(m_tilePrefab, new Vector3(i+(m_tileOffset * i), 0, j + (m_tileOffset* j)), m_tilePrefab.transform.rotation, this.transform);
                tile.name = $"tile{i}{j}";
                TileControl controller = tile.GetComponent<TileControl>();
                controller.InitTile(new Vector2(i,j), this);
                controller.m_onTileSelect += TileClicked;
                m_tiles[i, j] = tile.GetComponent<TileControl>();
            }
        }
    }
    void TileClicked(TileControl tile)
    {
        m_characterManager.MoveCharacter(tile);
    }

    public TileControl GetTileControl(){
        //TODOreturn some tile for example for character initiation position
        return m_tiles[2,2];
    }

    public void TilePreviewOn(Vector2 coords)
    { 
        if (coords.x >= 0 && coords.y >= 0
            && coords.x < m_xWidth && coords.y < m_zWidth)
        {
            TileControl tile = m_tiles[(int)coords.x, (int)coords.y];
            tile.TilePreviewOn();
        }
    }
    public void TilePreviewOff(Vector2 coords)
    {
        if (coords.x >= 0 && coords.y >= 0
            && coords.x < m_xWidth && coords.y < m_zWidth)
        {
            TileControl tile = m_tiles[(int)coords.x, (int)coords.y];
            tile.TilePreviewOff();
        }
    }
}
