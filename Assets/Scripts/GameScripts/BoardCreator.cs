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
    TileControl m_selectedTile;

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
                controller.InitTile(new Vector2(j,i), this);
                controller.m_onTileSelect += TileClicked;
                controller.m_onTileDeselect += TileUnclicked;
                m_tiles[j, i] = tile.GetComponent<TileControl>();
            }
        }
    }
    void TileClicked(TileControl tile)
    {
        if (m_selectedTile != null) 
        {
            //other tile has been selected, do nothing
            return;            
        }
        tile.TileSelected();
        m_selectedTile = tile;
    }

    void TileUnclicked(TileControl tile)
    {
        if (m_selectedTile != tile)
        {
            return;            
        }
        tile.TileDeselected();
        m_selectedTile = null;
    }

    public bool IsBoardIdling()
    {   //Add documentation what this function does
        //TODO probs rename, but when other logic is added to here
        if(m_selectedTile == null){
            return true;
        }
        return false;
    }
    public TileControl GetTileControl(){
        //TODO return some tile for example for character initiation position
        return m_tiles[0,0];
    }
}
