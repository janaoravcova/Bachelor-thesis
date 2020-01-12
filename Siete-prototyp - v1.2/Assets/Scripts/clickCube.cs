using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickCube : MonoBehaviour
{
    // Update is called once per frame
    void Update() {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                GetComponent<Renderer>().material.color = Color.red;
                Debug.Log(hit);
                Bounds bounds = GetComponent<Renderer>().bounds;
                Debug.Log(bounds.center);
            }

        }
    }
}
