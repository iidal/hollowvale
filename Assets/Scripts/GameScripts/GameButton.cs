using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class GameButton : MonoBehaviour
{
    [SerializeField] Button m_button;
    [SerializeField] Image m_buttonImage;
    [SerializeField] Sprite m_normalSprite;
    [SerializeField] Sprite m_highlightSprite;
    [SerializeField] Sprite m_inactiveSprite;

    void Start()
    {
        m_button = GetComponent<Button>();
        m_buttonImage = GetComponent<Image>();
        Reset();
    }
    public void Reset()
    {
        m_buttonImage.sprite = m_normalSprite;
        m_button.interactable = true;
    }
    public void Highlight(bool on)
    {
        if(on)
        {
            m_buttonImage.sprite = m_highlightSprite;
            return;
        }
        if(m_button.IsInteractable())
        {
            m_buttonImage.sprite = m_normalSprite;
        }
        else{
            m_buttonImage.sprite = m_inactiveSprite;
        }

    }
    public void Inactivate()
    {
        m_buttonImage.sprite = m_inactiveSprite;
        m_button.interactable = false;
    }
}
