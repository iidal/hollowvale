using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] GameObject m_characterPrefab;
    [SerializeField] BoardCreator m_boardManager;   // TODO refactor this away
    CharacterControl m_selectedCharacter;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }

    void CharacterUnclicked(CharacterControl character)
    {
        if (m_selectedCharacter != character)
        {
            return;            
        }
        character.CharacterDeselected();
        m_selectedCharacter = null;
    }
}
