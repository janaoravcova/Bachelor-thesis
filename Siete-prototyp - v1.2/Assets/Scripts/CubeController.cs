using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    private static CubeController cubeController;
    public static CubeController getCubeController() { return cubeController; }
    private Dictionary<string, GameObject> vertices = new Dictionary<string, GameObject>();
    private Cube cubeModel;

    System.Random random = new System.Random();


    private bool editMode = false;

    public Cube getCube()
    {
        return cubeModel;
    }

    public bool isEditMode()
    {
        return editMode;
    }

    public void setEditMode()
    {
        GameObject cube = GameObject.Find("mainCube");
       // cube.GetComponent<markCube>().enabled = !cube.GetComponent<markCube>().enabled;
       // cube.GetComponent<rotateCube>().enabled = !cube.GetComponent<rotateCube>().enabled;
    }

    void Awake()
    {
        //set singleton 
        if (cubeController != null && cubeController != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            cubeController = this;
        }

        //setVertices into dictionary
        vertices.Add("TOP_BACK_RIGHT", GameObject.Find("TOP_BACK_RIGHT"));
        vertices.Add("TOP_FRONT_RIGHT", GameObject.Find("TOP_FRONT_RIGHT"));
        vertices.Add("BOTTOM_BACK_RIGHT", GameObject.Find("BOTTOM_BACK_RIGHT"));
        vertices.Add("BOTTOM_BACK_LEFT", GameObject.Find("BOTTOM_BACK_LEFT"));
        vertices.Add("TOP_BACK_LEFT", GameObject.Find("TOP_BACK_LEFT"));
        vertices.Add("TOP_FRONT_LEFT", GameObject.Find("TOP_FRONT_LEFT"));
        vertices.Add("BOTTOM_FRONT_LEFT", GameObject.Find("BOTTOM_FRONT_LEFT"));
        vertices.Add("BOTTOM_FRONT_RIGHT", GameObject.Find("BOTTOM_FRONT_RIGHT"));

        cubeModel = new Cube();
        cubeModel.generateRandomCube();
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (KeyValuePair<string, GameObject> entry in vertices)
        {
            GameObject cube = entry.Value;
            var cubeRenderer = cube.GetComponent<Renderer>();
            Cube.InnerCube innerCube = (Cube.InnerCube) (Enum.Parse(typeof(Cube.InnerCube), entry.Key, true));
            print(innerCube.ToString());
            cubeRenderer.material.SetColor("_Color", cubeModel.getInnerCubeColor(innerCube));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //set color of a vertex found by name
    public void changeColor(string name, Color color)
    {
        if(name==null || name == ""){
            return;
        }
        GameObject cube;
        try
        {
            cube = vertices[name];
        } catch(KeyNotFoundException e)
        {
            return;
        }
        var cubeRenderer = cube.GetComponent<Renderer>();
        cubeRenderer.material.SetColor("_Color", color);

    }

}
