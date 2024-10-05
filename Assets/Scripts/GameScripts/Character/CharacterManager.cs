using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    enum ActionType {Move, Ability}; // TODO move types, etc to own master file
    [SerializeField] GameObject m_characterPrefab;
    [SerializeField] BoardCreator m_boardManager;   // TODO refactor this away
    PlayableCharacter m_selectedCharacter;
    [SerializeField] GameObject m_actionButtons;
    ActionType m_currentActionType = ActionType.Move;
    void Start()
    {
        m_actionButtons.SetActive(false);
    }

    public void InitCharacters()
    {
        GameObject character = Instantiate(m_characterPrefab);
        PlayableCharacter controller = character.GetComponent<PlayableCharacter>();
        controller.InitCharacter(m_boardManager.GetTileControl());
        controller.m_onCharacterSelect += CharacterClicked;
        controller.m_onCharacterDeselect += CharacterUnclicked;
    }

     void CharacterClicked(PlayableCharacter character)
    {
        if (m_selectedCharacter != null) 
        {
            //other character has been selected, do nothing
            return;            
        }
        character.Selected();
        m_selectedCharacter = character;
        m_actionButtons.SetActive(true);
        TileHighlighting(m_currentActionType, true);
    }

    void CharacterUnclicked(PlayableCharacter character)
    {
        if (m_selectedCharacter != character)
        {
            return;            
        }
        character.Deselected();
        m_actionButtons.SetActive(false);
        TileHighlighting(m_currentActionType, false);
        m_selectedCharacter = null;
    }
    public void MoveCharacter(TileControl tile)
    {
        if (m_selectedCharacter == null)
        {
            //error
            return;
        }
        TileHighlighting(m_currentActionType, false);
        m_selectedCharacter.UpdateTilePosition(tile);
    }
    public void SetSelectedAction(string actionType) //move, ability
    {
        // TODO: hold a list of available actions per turn, remove action from list when done
        ActionType newActionType = m_currentActionType;
        if (actionType == "move")
        {
            newActionType = ActionType.Move;
        }
        else if (actionType == "ability")
        {
            newActionType = ActionType.Ability;
        }
        else
        {
            Debug.LogError("Unrecognized action type" + actionType);
        }

        if (m_currentActionType != newActionType)
        {
            TileHighlighting(m_currentActionType, false);
            TileHighlighting(newActionType, true);
            m_currentActionType = newActionType;
        }
    }

    void TileHighlighting(ActionType action, bool turnOn)
    {
        List<Vector2> tilesToHighlight = GetActionCoordinates(action, m_selectedCharacter);
        foreach (var coords in tilesToHighlight)
        {
            m_boardManager.TilePreviewToggle(coords, turnOn);
        }
    }
    List<Vector2> GetActionCoordinates(ActionType actionType, PlayableCharacter character)
    {
        List<Vector2> actionCoords = new List<Vector2>();
        Vector2 characterCoords = m_selectedCharacter.m_tilePosition.m_coordinates;

        if (actionType == ActionType.Move)
        {
            actionCoords = character.m_movementCoordinates;
        }
        else if (actionType == ActionType.Ability)
        {
            actionCoords = character.m_abilityCoordinates;
        }
        List<Vector2> coordsOnBoard = new List<Vector2>();
        foreach (var coords in actionCoords)
        {
           Vector2 newCoords = new Vector2((int)(characterCoords.x + coords.x), (int)(characterCoords.y + coords.y));
            coordsOnBoard.Add(newCoords);
        }
        return coordsOnBoard;
    }
}
