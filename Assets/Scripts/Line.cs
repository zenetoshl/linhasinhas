using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public GameObject leftPoint;
    public GameObject rightPoint;
    public SpriteRenderer sprite;

    public bool selected = false;

    private void OnMouseUpAsButton() {
        if (selected) return;
        //play animation
        sprite.color = Color.blue;
        selected = true;
        LinesManager.instance.ClickLine(this);
    }

    public void ActivateLeftPoint(){
        leftPoint.SetActive(true);
    }

    public void ActivateRightPoint(){
        rightPoint.SetActive(true);
    }


}
