using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LinesManager : MonoBehaviour {
    public static LinesManager instance;

    public event Action<Line> OnLineClick;

    private void Awake () {
        instance = this;
    }

    public void ClickLine (Line l) {
        if (OnLineClick != null) {
            OnLineClick (l);
        }
    }
}