using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorController : MonoBehaviour
{

    private static EditorController editorController;
    public static EditorController getEditorController() { return editorController; }

    GameObject redColor;
    GameObject blueColor;
    GameObject yellowColor;
    GameObject greenColor;
    GameObject markButton;

    public Canvas editor;

    Color currentEditorColor = Color.black;

    bool editMode = false;

    public Color getCurrentEditorColor() { return currentEditorColor; }

    public bool isEditMode() { return editMode; }

    void Awake()
    {
        //set singleton 
        if (editorController != null && editorController != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            editorController = this;
        }
    }
   
    // Start is called before the first frame update
    void Start()
    {
        currentEditorColor = Color.black;

        redColor = GameObject.Find("colorRed");
        redColor.GetComponent<Button>().onClick.AddListener(setColorRed);

        blueColor = GameObject.Find("colorBlue");
        blueColor.GetComponent<Button>().onClick.AddListener(setColorBlue);

        yellowColor = GameObject.Find("colorYellow");
        yellowColor.GetComponent<Button>().onClick.AddListener(setColorYellow);

        greenColor = GameObject.Find("colorGreen");
        greenColor.GetComponent<Button>().onClick.AddListener(setColorGreen);

        markButton = GameObject.Find("markButton");
        markButton.GetComponent<Button>().onClick.AddListener(setEditableTileMapMode);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setMarkingTileMapMode()
    {
        TileMap.getTileMap().setColoringMode(false);
    }

    void setEditableTileMapMode()
    {
        TileMap.getTileMap().setColoringMode(true);
    }

    void setColorRed()
    {
        setEditableTileMapMode();
        Color newCol;
        if (ColorUtility.TryParseHtmlString("#FE0000", out newCol))
            currentEditorColor = newCol;
    }

    void setColorBlue()
    {
        setEditableTileMapMode();
        Color newCol;
        if (ColorUtility.TryParseHtmlString("#01B0F1", out newCol))
            currentEditorColor = newCol;
    }

    void setColorYellow()
    {
        setEditableTileMapMode();
        Color newCol;
        if (ColorUtility.TryParseHtmlString("#FFFF01", out newCol))
            currentEditorColor = newCol;

    }

    void setColorGreen()
    {
        setEditableTileMapMode();
        Color newCol;
        if (ColorUtility.TryParseHtmlString("#92D14F", out newCol))
            currentEditorColor = newCol;

    }
}
