using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class markCube : MonoBehaviour
{
    Camera mainCamera;
     

    void Awake()
    {
        mainCamera = Camera.main;
        GameObject cube = GameObject.Find("mainCube");

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        { 
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Color currentEditorColor = EditorController.getEditorController().getCurrentEditorColor();
                CubeController.getCubeController().changeColor(hit.collider.name, currentEditorColor);
            }
        }
    }
}
