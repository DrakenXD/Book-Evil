using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEnemyController : MonoBehaviour
{
    [SerializeField] private Image UI_barLife;
    [SerializeField] private Image UI_mark;

    [SerializeField] private GameObject UI_canvas;


    

    public void BarLife(float min, float max)
    {
        UI_barLife.fillAmount = min / max;
    }

    public void BarMark(float min, float max)
    {
        UI_mark.fillAmount = min / max;
    }

    public void BarVisible(bool active)
    {
        UI_canvas.SetActive(active);
    }
}
