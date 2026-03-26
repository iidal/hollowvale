using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // This is a base class for characters that can move, use abilities, take and do damage, heal...
    public CharacterManager m_characterManager;
    //Character stats
    public uint m_currentHealth;
    public uint m_maxHealth;
    public uint m_currentArmor;
    public uint m_damageAmount;
    public uint m_healAmount;
    [SerializeField] private bool m_interactable = false;

    //Character positioning, action ranges
    public TileControl m_tilePosition; // Temporarily public, get via function
    public List<Vector2> m_movementCoordinates; // make this private and accessed via a function
                                                // temporary, should be initialized from json config
    public List<Vector2> m_abilityCoordinates; // make this private and accessed via a function
                                               // temporary, should be initialized from json config

    //================================================================================================================
    public void InitCharacter(TileControl tile, CharacterManager characterManager)
    {
        Debug.Log("init character");
        UpdateTilePosition(tile);
        m_characterManager = characterManager;
    }
    //================================================================================================================
    
    public void SetCharacterInteractable(bool interactable)
    {
        m_interactable = interactable;
    }
    //================================================================================================================
    public bool GetCharacterInteractable()
    {
        return m_interactable;
    }
    //================================================================================================================
    public void UpdateTilePosition(TileControl tile)
    {
        if (m_tilePosition != null)
        {
            m_tilePosition.SetTileAvailability(true);
        }
        tile.SetTileAvailability(false);

        m_tilePosition = tile;
        transform.SetPositionAndRotation(m_tilePosition.transform.position, Quaternion.identity);
    }

    //================================================================================================================
    public void TakeDamage(uint damage)
    {
        Debug.Log("Character Take damage");

        // take damage first from armor
        // take remaining damage from health
        // poison, etc., will be taken from health, not armor TODO add damage type
        ReduceHealth(damage);
    }

    //================================================================================================================
    public void ReduceHealth(uint damage)
    {
        Debug.Log("health " + m_currentHealth.ToString());
        m_currentHealth -= damage;
        Debug.Log("new health " + m_currentHealth.ToString());
    }

    //================================================================================================================
    public void ReduceArmor(uint damage)
    {
        m_currentArmor -= damage;
    }

    //================================================================================================================
    public void Heal(uint heal)
    {
        m_currentHealth += heal; //TODO clamp between 0-m_maxhealth
    }

    //================================================================================================================
    // Return characters action coordinates relative to the board and characters position
    public List<Vector2> GetActionCoordinates(GameState.ActionType actionType)
    {
        List<Vector2> actionCoords = new();
        Vector2 characterCoords = m_tilePosition.m_coordinates;
        switch (actionType)
        {
            case GameState.ActionType.Move:
                actionCoords = m_movementCoordinates;
                break;
            case GameState.ActionType.Ability:
                actionCoords = m_abilityCoordinates;
                break;
            default:
                Debug.Log("Unrecognized ActionType, returning empty List<Vector2>");
                break;
        }
        List<Vector2> coordsOnBoard = new List<Vector2>();
        foreach (var coords in actionCoords)
        {
            Vector2 newCoords = new Vector2((int)(characterCoords.x + coords.x), (int)(characterCoords.y + coords.y));
            coordsOnBoard.Add(newCoords);
        }
        return coordsOnBoard;
    }
    //================================================================================================================
    // TODO this should be as generic as possible, take the coordinates as input, etc
    // also this logic should be handled by a manager
    // REMOVE THIS AND USE THE ONE IN CHARACTER MANAGER
    public bool IsTargetCharacterInRange()
    {
        // TODO define a type for coordinates set? using coordSet = List<Vector2>
        Character srcCharacter = m_characterManager.m_selectedCharacter;
        List<Vector2> absAbilityCoords = new();
        foreach (Vector2 abilityCoords in srcCharacter.m_abilityCoordinates)
        {
            var x = abilityCoords.x + srcCharacter.m_tilePosition.m_coordinates.x;
            var y = abilityCoords.y + srcCharacter.m_tilePosition.m_coordinates.y;
            absAbilityCoords.Add(new Vector2(x, y));
        }
        Debug.Log("isenemyinrange pos " + m_tilePosition.m_coordinates.x + " " + m_tilePosition.m_coordinates.y);
        foreach (var coordinates in absAbilityCoords)
        {
            Debug.Log("abilitycoords " + coordinates.x + " " + coordinates.y);
            if (m_tilePosition.m_coordinates.x == coordinates.x && m_tilePosition.m_coordinates.y == coordinates.y)
            {
                return true;
            }
        }
        return false;
    }
}
