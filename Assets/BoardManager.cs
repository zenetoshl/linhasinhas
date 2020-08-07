using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {
    public int sizeX;
    public int sizeY;
    public float spacing;
    public Square[, ] squares;
    public GameObject squarePrefab;
    public Vector3 firstPos;

    private void Awake () {
        squares = new Square[sizeX, sizeY];
    }

    // Start is called before the first frame update
    void Start () {
        Square instantiated;
        for (int i = 0; i < sizeY; i++) {
            for (int j = 0; j < sizeX; j++) {
                squares[i, j] = InstantiateSquare (firstPos);
                instantiated = squares[i, j];
                if (i == 0 && j == 0) { //corner
                    instantiated.IsntantiateLines (null, null);
                } else if (i == 0) { //top
                    instantiated.IsntantiateLines (squares[i, j - 1].lines[2], null);
                } else if (j == 0) { //left
                    instantiated.IsntantiateLines (null, squares[i - 1, j].lines[3]);
                } else { //default
                    instantiated.IsntantiateLines (squares[i, j - 1].lines[2], squares[i - 1, j].lines[3]);
                }

                firstPos += new Vector3 (spacing, 0, 0);
            }
            firstPos -= new Vector3 (firstPos.x, spacing, 0);
        }

    }

    Square InstantiateSquare (Vector3 pos) {
        return GameObject.Instantiate (squarePrefab, pos, Quaternion.identity).GetComponent<Square> ();
    }

    // Update is called once per frame
    void Update () {

    }
}