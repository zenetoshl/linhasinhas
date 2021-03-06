﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour {
    public Line[] lines = new Line[4];
    public GameObject linePrefab;
    private bool scored = false;
    public SpriteRenderer sprite;

    public const float space = 0.6f;

    private void Start() {
        LinesManager.instance.OnLineClick += CheckPoint;
    }

    public void IsntantiateLines (Line left, Line top) {
        Vector3 pos = this.transform.position;

        if (left == null) {
            lines[0] = IsntantiateLine (new Vector3 (pos.x - space, pos.y, 0), new Vector3 (0, 0, 90f)); //left
            lines[0].transform.SetParent(this.transform);
        } else
            lines[0] = left.GetComponent<Line> ();
        if (top == null) {
            lines[1] = IsntantiateLine (new Vector3 (pos.x, pos.y + space, 0), new Vector3 (0, 0, 0)); //top
            lines[1].transform.SetParent(this.transform);
        } else
            lines[1] = top.GetComponent<Line> ();

        lines[2] = IsntantiateLine (new Vector3 (pos.x + space, pos.y, 0), new Vector3 (0, 0, 90f)); //right
        lines[2].transform.SetParent(this.transform);

        lines[3] = IsntantiateLine (new Vector3 (pos.x, pos.y - space, 0), new Vector3 (0, 0, 0)); //bottom
        lines[3].transform.SetParent(this.transform);
    }

    Line IsntantiateLine (Vector3 pos, Vector3 rotation) {
        return GameObject.Instantiate (linePrefab, pos, Quaternion.Euler(rotation)).GetComponent<Line> ();
    }

    private bool ContainsLine(Line l){
        foreach (Line line in lines)
        {
            if(l == line){
                return true;
            }
        }
        return false;
    }

    private bool DidScore(){
        foreach (Line line in lines)
        {
            if(!line.selected){
                return false;
            }
        }
        return true;
    }

    public void CheckPoint(Line line){
        if (scored || !ContainsLine(line)){
            return;
        }

        if(DidScore()){
            scored = true;
            sprite.color = Color.blue;
        }
    }
}