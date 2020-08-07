using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public GameObject leftPoint;
    public GameObject rightPoint;

    public void ActivateLeftPoint(){
        leftPoint.SetActive(true);
    }

    public void ActivateRightPoint(){
        rightPoint.SetActive(true);
    }
}
