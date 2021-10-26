using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEnemyController : MonoBehaviour
{
    [SerializeField] private Transform enemy;
  

    [SerializeField] private Image UI_barLife;
    [SerializeField] private Image UI_mark;

    [SerializeField] private GameObject UI_canvas;


    private void LateUpdate()
    {
        transform.localPosition = new Vector3(enemy.position.x -1.8f,1.2f,1.8f);
        transform.localEulerAngles = new Vector3(0 , 0, 0);
    }

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
