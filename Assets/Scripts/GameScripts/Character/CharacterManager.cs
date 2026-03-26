using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

//public enum ActionType {Move, Ability};

public class CharacterManager : MonoBehaviour
{
    [SerializeField] BoardCreator m_boardManager;   // TODO refactor this away
    [SerializeField] BoardActionManager m_actionManager;

    // Prefabs and characters
    Dictionary<string, PlayableCharacter> m_playableCharacters = new();
    Dictionary<string, EnemyCharacter> m_enemyCharacters = new();
    [SerializeField] GameObject m_characterPrefab;
    [SerializeField] GameObject m_enemyPrefab;
    public PlayableCharacter m_selectedCharacter;
    EnemyCharacter m_selectedEnemy;

    // Actions etc
    // TODO should m_currentActionType actually be tracked in board action manager so its not in two places
    public GameState.ActionType m_currentActionType = GameState.ActionType.Move;

    void Start()
    {

    }

    public void InitCharacters()
    {
        GameObject character = Instantiate(m_characterPrefab);
        PlayableCharacter controller = character.GetComponent<PlayableCharacter>();
        controller.InitCharacter(m_boardManager.GetTileControl(new Vector2(2, 2)), this);
        controller.m_onCharacterSelect += CharacterClicked;
        controller.m_onCharacterDeselect += CharacterUnclicked;
        m_playableCharacters.Add("some_id", controller); // TODO figure out id management maybe idk
    }
    //================================================================================================================
    public void InitEnemies()
    {
        GameObject enemy = Instantiate(m_enemyPrefab);
        EnemyCharacter controller = enemy.GetComponent<EnemyCharacter>();
        controller.InitCharacter(m_boardManager.GetTileControl(new Vector2(1, 1)), this);
        controller.m_onEnemySelect += EnemyClicked;
        m_enemyCharacters.Add("some_id", controller);
    }
    //================================================================================================================
    public void SetCharactersInteractable(bool interactable)
    {
        foreach (var character in m_playableCharacters)
        {
            character.Value.SetCharacterInteractable(interactable);
        }
        foreach (var character in m_enemyCharacters)
        {
            character.Value.SetCharacterInteractable(interactable);
        }
    }
    //================================================================================================================
    void CharacterClicked(PlayableCharacter character)
    {
        if (m_selectedCharacter != null)
        {
            //other character has been selected, do nothing
            return;
        }
        m_currentActionType = (GameState.ActionType)m_actionManager.GetFirstUnusedAction();
        Debug.Log("character clicked " + m_currentActionType);
        character.Selected();
        m_selectedCharacter = character;
        m_actionManager.ShowActionButtons(m_currentActionType);
        TileHighlighting(m_currentActionType, true);
    }

    //================================================================================================================
    public void CharacterUnclicked(PlayableCharacter character)
    {
        if (m_selectedCharacter != character)
        {
            return;
        }
        character.Deselected();
        m_actionManager.HideActionButtons();
        TileHighlighting(m_currentActionType, false);
        m_selectedCharacter = null;
    }
    //================================================================================================================
    void EnemyClicked(EnemyCharacter enemy)
    {
        Debug.Log("character manager enemy clicked");
        enemy.Selected();
        m_actionManager.ActionTaken(GameState.ActionType.Ability);
    }
    //================================================================================================================
    public void MoveCharacter(TileControl tile)
    {
        //  TODO this function should also include abilities 
        // executing an action is done by selecting a tile, so attack/heal is checked by what character is in tile
        // what about actions that affect multiple tiles
        if (m_selectedCharacter == null)
        {
            //error
            return;
        }

        Debug.Log("MoveCharacter");
        TileHighlighting(m_currentActionType, false);
        m_selectedCharacter.UpdateTilePosition(tile);
        m_actionManager.ActionTaken(GameState.ActionType.Move);
    }
    //================================================================================================================
    public void SetSelectedAction(string actionType) //move, ability
    {
        // TODO REFACTOR either this takes int or string, that is then casted to Actiontype in Gamestate, applies to BoardActionManager.ActionPreview too
        GameState.ActionType newActionType = m_currentActionType;
        if (actionType == "move")
        {
            newActionType = GameState.ActionType.Move;
        }
        else if (actionType == "ability")
        {
            newActionType = GameState.ActionType.Ability;
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
    void TileHighlighting(GameState.ActionType action, bool turnOn)
    {
        List<Vector2> tilesToHighlight = m_selectedCharacter.GetActionCoordinates(action);
        if (action == GameState.ActionType.Move)
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
        foreach (var tile in tilesList)
        {
            if (tile.x < 0 || tile.y < 0 || tile.x > tiles.GetLength(0) - 1 || tile.y > tiles.GetLength(1) - 1)
            {
                continue;
            }
            if (tiles[(int)tile.x, (int)tile.y].IsTileAvailable())
            {
                allowedTiles.Add(tile);
            }
        }
        return allowedTiles;
    }
    //================================================================================================================
    // TODO USE THIS
    public bool IsTargetCharacterInRange(Character dstCharacter)
    {
        // TODO define a type for coordinates set? using coordSet = List<Vector2>
        Character srcCharacter = m_selectedCharacter;
        List<Vector2> absAbilityCoords = new();
        foreach (Vector2 abilityCoords in srcCharacter.m_abilityCoordinates)
        {
            var x = abilityCoords.x + srcCharacter.m_tilePosition.m_coordinates.x;
            var y = abilityCoords.y + srcCharacter.m_tilePosition.m_coordinates.y;
            absAbilityCoords.Add(new Vector2(x, y));
        }
        foreach (var coordinates in absAbilityCoords)
        {
            if (dstCharacter.m_tilePosition.m_coordinates.x == coordinates.x && dstCharacter.m_tilePosition.m_coordinates.y == coordinates.y)
            {
                return true;
            }
        }
        return false;
    }

    //================================================================================================================
    public void EndTurn()
    {
        // TODO collect here all needed actions when turn ends, so no need to expose stuff to BattleSystem
    }
}