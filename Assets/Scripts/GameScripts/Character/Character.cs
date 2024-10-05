using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    // This is a base class for characters that can move, use abilities, take and do damage, heal...

    //Character stats
    uint m_currentHealth;
    uint m_maxHealth;
    uint m_currentArmor;
    uint m_damageAmount;
    uint m_healAmount;

    //Character positioning, action ranges
    public TileControl m_tilePosition; // Temporarily public, get via function
    public List<Vector2> m_movementCoordinates; // make this private and accessed via a function
                                                // temporary, should be initialized from json config
    public List<Vector2> m_abilityCoordinates; // make this private and accessed via a function
                                                // temporary, should be initialized from json config

    public void InitCharacter(TileControl tile)
    {
        UpdateTilePosition(tile);
    }
    public void UpdateTilePosition(TileControl tile)
    {
        m_tilePosition = tile;
        transform.SetPositionAndRotation(tile.transform.position, Quaternion.identity);
    }
    public void TakeDamage(uint damage)
    {
        // take damage first from armor
        // take remaining damage from health
        // poison, etc., will be taken from health, not armor TODO add damage type
    }
    public void ReduceHealth(uint damage)
    {
        m_currentHealth -= damage;
    }
    public void ReduceArmor(uint damage)
    {
        m_currentArmor -= damage;
    }
    public void Heal(uint heal)
    {
        m_currentHealth += heal; //TODO clamp between 0-m_maxhealth
    }
}
