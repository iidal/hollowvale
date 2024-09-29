using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] GameObject m_characterPrefab;
    [SerializeField] BoardCreator m_boardManager;   // TODO refactor this away
    CharacterControl m_selectedCharacter;
    [SerializeField] GameObject m_actionButtons;

    void Start()
    {
        m_actionButtons.SetActive(false);
    }

    public void InitCharacters()
    {
        GameObject character = Instantiate(m_characterPrefab);
        CharacterControl controller = character.GetComponent<CharacterControl>();
        controller.InitCharacter(m_boardManager.GetTileControl());
        controller.m_onCharacterSelect += CharacterClicked;
        controller.m_onCharacterDeselect += CharacterUnclicked;
    }
     void CharacterClicked(CharacterControl character)
    {
        if (m_selectedCharacter != null) 
        {
            //other character has been selected, do nothing
            return;            
        }
        character.CharacterSelected();
        m_selectedCharacter = character;
        m_actionButtons.SetActive(true);
        List<Vector2> tilesToHighlight = character.m_movementCoordinates;
        Vector2 characterCoords = character.m_tilePosition.m_coordinates;
        foreach (var coords in tilesToHighlight)
        {   //this is duplicated code, refactor
            Vector2 highlightCoords = new Vector2((int)(characterCoords.x + coords.x), (int)(characterCoords.y + coords.y));
            m_boardManager.TilePreviewOn(highlightCoords);
        }
    }

    void CharacterUnclicked(CharacterControl character)
    {
        if (m_selectedCharacter != character)
        {
            return;            
        }
        character.CharacterDeselected();
        m_selectedCharacter = null;
        m_actionButtons.SetActive(false);


        List<Vector2> tilesToHighlight = character.m_movementCoordinates;
        Vector2 characterCoords = character.m_tilePosition.m_coordinates;
        foreach (var coords in tilesToHighlight)
        {   //this is duplicated code, refactor
            Vector2 highlightCoords = new Vector2((int)(characterCoords.x + coords.x), (int)(characterCoords.y + coords.y));
            m_boardManager.TilePreviewOff(highlightCoords);
        }

    }
    public void MoveCharacter(TileControl tile)
    {
        if(m_selectedCharacter == null){
            //error
            return;
        }
        List<Vector2> tilesToHighlight = m_selectedCharacter.m_movementCoordinates;
        Vector2 characterCoords = m_selectedCharacter.m_tilePosition.m_coordinates;
        foreach (var coords in tilesToHighlight)
        {   //this is duplicated code, refactor (although here we could loop all tiles and turn all highlighst off)
            Vector2 highlightCoords = new Vector2((int)(characterCoords.x + coords.x), (int)(characterCoords.y + coords.y));
            m_boardManager.TilePreviewOff(highlightCoords);
        }
        m_selectedCharacter.SetTileToCharacter(tile);
    }
}
