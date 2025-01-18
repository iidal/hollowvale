using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;

public enum ActionType {Move, Ability};

public class CharacterManager : MonoBehaviour
{
    [SerializeField] BoardCreator m_boardManager;   // TODO refactor this away
    
    // Prefabs and characters
    Dictionary<string, PlayableCharacter> m_playableCharacters = new Dictionary<string, PlayableCharacter>();
    Dictionary<string, EnemyCharacter> m_enemyCharacters = new Dictionary<string, EnemyCharacter>();
    [SerializeField] GameObject m_characterPrefab;
    [SerializeField] GameObject m_enemyPrefab;
    public PlayableCharacter m_selectedCharacter;
    EnemyCharacter m_selectedEnemy;
    
    // Actions etc
    [SerializeField] GameObject m_actionButtons;
    public ActionType m_currentActionType = ActionType.Move;
    void Start()
    {
        m_actionButtons.SetActive(false);
    }

    public void InitCharacters()
    {
        GameObject character = Instantiate(m_characterPrefab);
        PlayableCharacter controller = character.GetComponent<PlayableCharacter>();
        controller.InitCharacter(m_boardManager.GetTileControl(new Vector2(2,2)), this);
        controller.m_onCharacterSelect += CharacterClicked;
        controller.m_onCharacterDeselect += CharacterUnclicked;
        m_playableCharacters.Add("some_id", controller); // TODO figure out id management maybe idk
    }
    //================================================================================================================
    public void InitEnemies()
    {
        GameObject enemy = Instantiate(m_enemyPrefab);
        EnemyCharacter controller = enemy.GetComponent<EnemyCharacter>();
        controller.InitCharacter(m_boardManager.GetTileControl(new Vector2(1,1)), this);
        controller.m_onEnemySelect += EnemyClicked;
    }
//================================================================================================================
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
//================================================================================================================
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
    //================================================================================================================
    void EnemyClicked(EnemyCharacter enemy)
    {
        Debug.Log("character manager enemy clicked");
        enemy.Selected();
    }
    //================================================================================================================
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
    //================================================================================================================
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
    //================================================================================================================
    void TileHighlighting(ActionType action, bool turnOn)
    {
        List<Vector2> tilesToHighlight = m_selectedCharacter.GetActionCoordinates(action);
        if(action == ActionType.Move)
        {
            tilesToHighlight = MovementTilesChecking(tilesToHighlight);
        }
        foreach (var coords in tilesToHighlight)
        {
            m_boardManager.TilePreviewToggle(coords, turnOn);
        }
    }
    //================================================================================================================
    // For tiles for movement, remove tiles where moving is not possible (tile is occupied)
    List<Vector2> MovementTilesChecking(List<Vector2> tilesList)
    {
        List<Vector2> allowedTiles = new List<Vector2>();
        TileControl[,] tiles = m_boardManager.GetTiles();
        foreach(var tile in tilesList)
        {
            Debug.Log(tile.x + "and " + tile.y);
            if(tile.x < 0 || tile.y < 0 || tile.x > tiles.GetLength(0)-1 || tile.y > tiles.GetLength(1)-1)
            {
                continue;
            }
            if(tiles[(int)tile.x, (int)tile.y].IsTileAvailable())
            {
                allowedTiles.Add(tile);
            }
        }
        return allowedTiles;
    }
}
