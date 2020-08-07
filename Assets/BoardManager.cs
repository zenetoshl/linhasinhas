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

    public GameObject pointPrefab;

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
                instantiated.transform.SetParent (this.transform);
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
        InstantiateAllPoints ();

    }

    Square InstantiateSquare (Vector3 pos) {
        return GameObject.Instantiate (squarePrefab, pos, Quaternion.identity).GetComponent<Square> ();
    }

    void InstantiateAllPoints () {
        for (int i = 0; i < sizeY; i++) {
            for (int j = 0; j < sizeX - 1; j++) {
                float xPos = ((squares[i, j].lines[1].transform.position.x + squares[i, j + 1].lines[1].transform.position.x) / 2);
                if (i == 0) {
                    InstantiatePoint (new Vector3 (xPos, squares[i, j].lines[1].transform.position.y, squares[i, j].lines[1].transform.position.z));
                }
                if (j == 0) {
                    if (i == sizeY - 1) {
                        InstantiatePoint (new Vector3 (squares[i, j].lines[0].transform.position.x, squares[i, j].lines[3].transform.position.y, squares[i, j].lines[3].transform.position.z));
                    }
                    InstantiatePoint (new Vector3 (squares[i, j].lines[0].transform.position.x, squares[i, j].lines[1].transform.position.y, squares[i, j].lines[1].transform.position.z));
                }
                if (j == sizeX - 2) {
                    if (i == 0) {
                        InstantiatePoint (new Vector3 (squares[i, j + 1].lines[2].transform.position.x, squares[i, j + 1].lines[1].transform.position.y, squares[i, j].lines[1].transform.position.z));
                    }
                    InstantiatePoint (new Vector3 (squares[i, j + 1].lines[2].transform.position.x, squares[i, j + 1].lines[3].transform.position.y, squares[i, j].lines[3].transform.position.z));
                }
                InstantiatePoint (new Vector3 (xPos, squares[i, j].lines[3].transform.position.y, squares[i, j].lines[3].transform.position.z));
            }
        }
    }

    void InstantiatePoint (Vector3 pos) {
        GameObject.Instantiate (pointPrefab, pos, Quaternion.identity);
    }
}