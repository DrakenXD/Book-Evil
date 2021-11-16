using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBook : MonoBehaviour
{
    [SerializeField] private GameObject BookClosed;
    [SerializeField] private GameObject BookOpen;
    
    [SerializeField] private Image Icon_Magic;

    
    public void UpdateBook(bool open)
    {
        if (open)
        {
            BookClosed.SetActive(false);
            BookOpen.SetActive(true);
        }
        else
        {
            BookOpen.SetActive(false);
            BookClosed.SetActive(true);
        }
    }

    public void SetMagic(Sprite _icon)
    {
        Icon_Magic.sprite = _icon;
    }
}
