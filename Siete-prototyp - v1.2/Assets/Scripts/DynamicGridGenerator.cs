using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicGridGenerator : MonoBehaviour
{
    public GameObject tileItem;
    public GameObject innerTileItem;

    int gridWidth = 5;
    int gridHeight = 5;

    float tileOffset = 6.05f;
    float defaultOffset = -10f;

    public void Start()
    {
        CreateTileMap();
    }

    public void Update()
    {

    }

    public void CreateTileMap()
    {
        for(int i=0; i<gridWidth; i++)
        {
            for(int j=0; j<gridHeight; j++)
            {
                GameObject tileObject = Instantiate(tileItem);
                tileObject.transform.localScale += new Vector3(5,5,5);
                tileObject.transform.position = new Vector3(i * tileOffset + 5 + defaultOffset, j*tileOffset + 5 + defaultOffset, 0);
                Material material = new Material(Shader.Find("Specular"));
                material.color = Color.gray;
                tileObject.GetComponent<Renderer>().material = material;

            }
        }
    }
}
