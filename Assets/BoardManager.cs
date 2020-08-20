using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {
    public int sizeX;
    public int sizeY;
    public float spacing;
    public Square[, ] squares;
    public GameObject squarePrefab;

    private Vector3 firstPos;

    public GameObject pointPrefab;
    public Transform pointsParent;

    private void Awake () {
        squares = new Square[sizeY, sizeX];
    }

    // Start is called before the first frame update
    void Start () {
        firstPos = CalculateFistPos ();
        float xFirstPos = firstPos.x;
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
            firstPos += new Vector3 (xFirstPos, 0, 0);
        }
        InstantiateAllPoints ();

    }

    private Vector3 CalculateFistPos () {
        float xSpacing = -this.spacing;
        float ySpacing = this.spacing;
        if (sizeX % 2 == 1 && sizeY % 2 == 1) {
            return (new Vector3 (((sizeX - 1) / 2) * xSpacing, ((sizeY - 1) / 2) * ySpacing, 0));
        } else if (sizeX % 2 == 1 && sizeY % 2 == 0) {
            return (new Vector3 (((sizeX - 1) / 2) * xSpacing, (((sizeY) / 2) * ySpacing) - (ySpacing / 2), 0));
        } else if (sizeX % 2 == 0 && sizeY % 2 == 1) {
            return (new Vector3 ((((sizeX) / 2) * xSpacing) - (xSpacing / 2), (((sizeY - 1) / 2) * ySpacing), 0));
    }
    return (new Vector3 ((((sizeX) / 2) * xSpacing) - (xSpacing / 2), (((sizeY) / 2) * ySpacing) - (ySpacing / 2), 0));
}

Square InstantiateSquare (Vector3 pos) {
    Square instantiated = GameObject.Instantiate (squarePrefab, pos, Quaternion.identity).GetComponent<Square> ();
    instantiated.transform.SetParent (this.transform);
    return instantiated;
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
    GameObject point = GameObject.Instantiate (pointPrefab, pos, Quaternion.identity);
    point.transform.SetParent (pointsParent);

}
}