using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{

    private static TileController tileController;
    public static TileController getTileController() { return tileController; }

    public GameObject innerTileItem;


    public Color markedColor;
    Color initialColor;
    bool editMode;


    // Start is called before the first frame update
    void Start()
    {
        initialColor = GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (TileMap.getTileMap().isColoringMode())
        {
            if (GetComponent<Collider>().Raycast(ray, out hitInfo, Mathf.Infinity))
            {

                Vector3 center = GetComponent<Renderer>().bounds.center;
                Vector3 topLeftBound = new Vector3(center.x - 1, center.y + 1, center.z);
                Vector3 topRightBound = new Vector3(center.x + 1, center.y + 1, center.z);
                Vector3 botLeftBound = new Vector3(center.x - 1, center.y - 1, center.z);
                Vector3 botRightBound = new Vector3(center.x + 1, center.y - 1, center.z);


                Vector3 min = GetComponent<Renderer>().bounds.min;
                Vector3 max = GetComponent<Renderer>().bounds.max;

                Material material = new Material(Shader.Find("Specular"));
                material.color = EditorController.getEditorController().getCurrentEditorColor();

                int j = ((int)(hitInfo.point.x + 8f)) / 6;
                int i = ((int)(22.2 - hitInfo.point.y)) / 6;

                //detect left top corner hit
                if (hitInfo.point.x <= topLeftBound.x && hitInfo.point.x > min.x && hitInfo.point.y < max.y && hitInfo.point.y >= topLeftBound.y)
                {
                    Debug.Log("detect left top corner hit");
                    GameObject topLeftTile = Instantiate(innerTileItem);
                    topLeftTile.transform.localScale += new Vector3(1,1,1);
                    topLeftTile.GetComponent<Renderer>().transform.position = new Vector3(min.x + 1, center.y + 2, center.z-0.2f);

                    topLeftTile.GetComponent<Renderer>().material = material;

                    TileMap.getTileMap().markTileCorner(i, j, 0, material.color);
                    return;

                }
                //detect right top corner hit
                if (hitInfo.point.x >= topRightBound.x &&  hitInfo.point.x < max.x && hitInfo.point.y >= topRightBound.y && hitInfo.point.y < max.y)
                {
                    Debug.Log("detect right top corner hit");
                    GameObject topRightTile = Instantiate(innerTileItem);
                    topRightTile.transform.localScale += new Vector3(1, 1, 1);
                    topRightTile.GetComponent<Renderer>().transform.position = new Vector3(center.x + 2, center.y + 2, center.z - 0.2f);

                    topRightTile.GetComponent<Renderer>().material = material;

                    TileMap.getTileMap().markTileCorner(i, j, 1, material.color);
                    return;
                }
                //detect left bottom corner hit
                if (hitInfo.point.x <= botRightBound.x && hitInfo.point.x > min.x && hitInfo.point.y <= botLeftBound.y && hitInfo.point.y > min.y)
                {
                    Debug.Log("detect left btm corner hit");
                    GameObject bottomLeftTile = Instantiate(innerTileItem);
                    bottomLeftTile.transform.localScale += new Vector3(1, 1, 1);
                    bottomLeftTile.GetComponent<Renderer>().transform.position = new Vector3(min.x + 1, center.y-2, center.z - 0.2f);
                    bottomLeftTile.GetComponent<Renderer>().material = material;

                    TileMap.getTileMap().markTileCorner(i, j, 2, material.color);
                    return;
                }
                //detect right bottom corner hit
                if(hitInfo.point.x >= botRightBound.x && hitInfo.point.x < max.x && hitInfo.point.y > min.y && hitInfo.point.y <= botRightBound.y)
                {
                    Debug.Log("detect right btm corner hit");
                    GameObject bottomRightTile = Instantiate(innerTileItem);
                    bottomRightTile.transform.localScale += new Vector3(1, 1, 1);
                    bottomRightTile.GetComponent<Renderer>().transform.position = new Vector3(center.x + 2 , center.y - 2, center.z - 0.2f);
                    bottomRightTile.GetComponent<Renderer>().material = material;

                    TileMap.getTileMap().markTileCorner(i, j, 3, material.color);
                    return;
                }

                return;
            }

        }
        if (GetComponent<Collider>().Raycast(ray, out hitInfo, Mathf.Infinity))
        {

            Bounds bounds = GetComponent<Renderer>().bounds;
            Debug.Log(bounds.center + ", " + bounds.min + ", " + bounds.max + "point:"+hitInfo.point);

            //count position of tile to mark it in the tilemap matrix
            int j = ((int)(hitInfo.point.x + 8f)) / 6;
            int i = ((int)(22.2 - hitInfo.point.y)) / 6;
            Debug.Log("row:" + i+ "calculation hitInfo.point.x+8="+(hitInfo.point.x+8f));
            Debug.Log("col:" + i);


            if (GetComponent<Renderer>().material.color == markedColor)
            {
                GetComponent<Renderer>().material.color = initialColor;
                TileMap.getTileMap().markTileInMap(i, j, 0);
                TileMap.getTileMap().printMap();

            }
            else
            {
                GetComponent<Renderer>().material.color = markedColor;
                TileMap.getTileMap().markTileInMap(i, j, 1);
                TileMap.getTileMap().printMap();

            }
        } 
    }


    public void setEditMode(bool editMode)
    {
        this.editMode = editMode;
    }

    public bool getEditMode()
    {
        return editMode;
    }
}
