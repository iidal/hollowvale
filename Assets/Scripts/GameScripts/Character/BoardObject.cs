using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoardObject : MonoBehaviour
{
    private GameState.BoardObjType m_objectType;// = GameState.BoardObjType.Invalid;
    public uint m_currentHealth;
    public uint m_maxHealth;
    public uint m_currentArmor;
    public uint m_damageAmount;
    public uint m_healAmount;

    public TileControl m_tilePosition; // Temporarily public, get via function
    Vector2 m_coordsPosition;
        //================================================================================================================
    public GameState.BoardObjType BoardObjType
    {
        get { return m_objectType;}
        set { m_objectType = value; }
    }
    public Vector2 GetCoordinates()
    {
        return m_coordsPosition;
    }
    //================================================================================================================
    public void UpdateTilePosition(TileControl tile)
    {
        m_coordsPosition = tile.m_coordinates;

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
}
