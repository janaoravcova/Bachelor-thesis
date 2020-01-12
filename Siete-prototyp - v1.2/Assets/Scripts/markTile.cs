using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class markTile : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.transform.Equals(GetComponent<Transform>()))
            {
                Debug.Log(GetComponent<Renderer>().transform.position+"clicked prefab");

                Color currentEditorColor = EditorController.getEditorController().getCurrentEditorColor();
                GetComponent<Renderer>().material.color = currentEditorColor;


            }
        }
    }
}
