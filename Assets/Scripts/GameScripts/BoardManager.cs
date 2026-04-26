using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Playables;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private BoardCreator m_boardCreator;
    private TileControl[,] m_tiles;
    [SerializeField] private int m_xWidth, m_zWidth;
    [SerializeField] private CharacterManager m_characterManager;

    //================================================================================================================
    void Start()
    {

    }
    public void CreateBoard()
    {
        m_xWidth = 5;
        m_zWidth = 6;
        m_tiles = m_boardCreator.CreateBoardTiles(this, m_xWidth, m_zWidth);
        Debug.Log("Board created");
    }

    //================================================================================================================
    // What does this do   
    public TileControl[,] GetTiles()
    {
        return m_tiles;
    }

    //================================================================================================================
    // What does this do   
    public void TileClicked(TileControl tile)
    {
        Debug.Log("Tile clicked");
        BoardObject tileContent = m_characterManager.GetTileContent(tile.m_coordinates);
        if (m_characterManager.m_selectedCharacter == null)
        {
            if (tileContent == null)
            {
                return;
            }
            if (tileContent.BoardObjType == GameState.BoardObjType.Playable)
            {

                PlayableCharacter playable = (PlayableCharacter)tileContent;
                // TODO char might be on cooldown, cant be selected then
                // if(playable char is on cooldown) continue;
                m_characterManager.CharacterSelected(playable);
                return;
            }
            if (tileContent.BoardObjType == GameState.BoardObjType.Enemy || tileContent.BoardObjType == GameState.BoardObjType.Playable)
            {
                // TODO highlight previewing 
                // TODO hightlight previewing wihth all actions
            }
            return;
        }
        // playable character has been selected
        if (m_characterManager.m_currentActionType == GameState.ActionType.Move)
        {
            if (tileContent == null)
            {
                m_characterManager.MoveCharacter(tile);
            }
            return;
        }
        else if (m_characterManager.m_currentActionType == GameState.ActionType.Ability)
        {
            if (tileContent != null)
            {
                // TODO ability, (includes more deciding if can do(no healing enemy etc))
            }
            return;
        }
    }
    //================================================================================================================
    // What does this do 
    public TileControl GetTileControl(Vector2 coordinates)
    {
        //TODOreturn some tile for example for character initiation position
        return m_tiles[(int)coordinates.x, (int)coordinates.y];
    }

    //================================================================================================================
    // What does this do 


    //================================================================================================================
    // What does this do
    public void TilePreviewToggle(Vector2 coords, bool previewOn)
    {
        if (coords.x >= 0 && coords.y >= 0
            && coords.x < m_xWidth && coords.y < m_zWidth)
        {
            TileControl tile = m_tiles[(int)coords.x, (int)coords.y];
            if (previewOn)
            {
                tile.TilePreviewOn();
            }
            else
            {
                tile.TilePreviewOff();
            }
        }
    }
}
