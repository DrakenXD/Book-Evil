using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Image UI_barLife;
    [SerializeField] private Image UI_mark;



    public void BarLife(float min, float max)
    {
        UI_barLife.fillAmount = min / max;
    }

    public void BarMark(float min, float max)
    {
        UI_mark.fillAmount = min / max;
    }
}
