using UnityEngine;

public class BoardCreator : MonoBehaviour
{
    private TileControl[,] m_tiles; // TODO no need to be member variable
    private int m_xWidth, m_zWidth;
    private readonly float m_tileOffset = 0.15f;
    [SerializeField] private GameObject m_tilePrefab;

    //================================================================================================================
    // What does this do
        public TileControl[,] CreateBoardTiles(BoardManager boardManager, int xWidth, int zWitdth)
    {
        m_xWidth = xWidth;
        m_zWidth = zWitdth;
        m_tiles = new TileControl[m_xWidth, m_zWidth];
        for (int i = 0; i < m_xWidth; i++)
        {
            for (int j = 0; j < m_zWidth; j++)
            {
                GameObject tile = Instantiate(m_tilePrefab, new Vector3(i+(m_tileOffset * i), 0, j + (m_tileOffset* j)), m_tilePrefab.transform.rotation, this.transform);
                tile.name = $"tile{i}{j}";
                TileControl controller = tile.GetComponent<TileControl>();
                controller.InitTile(new Vector2(i,j), this);
                controller.m_onTileSelect += boardManager.TileClicked;
                m_tiles[i, j] = tile.GetComponent<TileControl>();
            }
        }
        return m_tiles;
    }
}
