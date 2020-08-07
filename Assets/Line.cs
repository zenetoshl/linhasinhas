using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public GameObject leftPoint;
    public GameObject RightPoint;

    public void ActivateLeftPoint(){
        leftPoint.SetActive(true);
    }
}
