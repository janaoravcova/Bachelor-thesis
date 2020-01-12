using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap 
{
    private static TileMap tileMap = new TileMap();
    public static TileMap getTileMap() { return tileMap; }

    private bool coloringMode;
    private int[,] map = new int[5, 5];
    private int[,][] mapAdjacencyMatrix = new int[5,5][];
    private string[,] mapCornerColors = new string[5, 5];//clockwise starting in left top corner - but it does not really matter unless its clockwise order




    public bool isColoringMode() { return coloringMode; }
    public void setColoringMode(bool coloringMode) { this.coloringMode = coloringMode; }

    private TileMap()
    {
        //construct map informations
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                map[i, j] = 0;
                mapCornerColors[i, j] = "0000";
                mapAdjacencyMatrix[i, j] = new int[] { 0, 0, 0, 0 };
            }
        }
    }

    public int[,] getMap() { return map; }
    public int[,][] getAdjacencyMatrix() { return mapAdjacencyMatrix; }
    public string getCorner(int row, int col) { return mapCornerColors[row, col]; }

    public void markTileInMap(int row, int col, int value) 
    {
        map[row, col] = value;
        updateAdjacencyMatrix(row, col, value);
    }

    //updates adjacency matrix after a tile was marked
    public void updateAdjacencyMatrix (int row, int col, int value)
    {
        //up
        if (row > 0)
        {
            mapAdjacencyMatrix[row - 1, col][2] = value;
            if(map[row-1, col] == 1)
            {
                mapAdjacencyMatrix[row, col][0] = 1;
            }
        }

        //down 
        if(row < 5)
        {
            mapAdjacencyMatrix[row + 1, col][0] = value;
            if(map[row+1, col] == 1)
            {
                mapAdjacencyMatrix[row, col][2] = 1;
            }
        }

        //left 
        if (col > 0)
        {
            mapAdjacencyMatrix[row, col - 1][1] = value;
            if(map[row, col-1] == 1)
            {
                mapAdjacencyMatrix[row, col][3] = 1;
            }
        }

        //right
        if (col < 5)
        {
            mapAdjacencyMatrix[row, col + 1][3] = value;
            if(map[row, col+1] == 1)
            {
                mapAdjacencyMatrix[row, col][1] = 1;
            }
        }
    }

    public void markTileCorner(int row, int col, int cornerIndex, Color color)
    {
        string tileHashCode = mapCornerColors[row, col];
        tileHashCode.Insert(cornerIndex, CubeController.getCubeController().getCube().getColorsIndex(color).ToString());
        mapCornerColors[row, col] = tileHashCode;
    }


    public int[] initializeSolver()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (map[i,j] == 1)
                {
                    return new int[] { i, j };
                }
            }
        }
        return null;
    }


    public void printMap()
    {
        string mapString = "";
        for(int i=0; i<5; i++)
        {
            for(int j=0; j<5; j++)
            {
                mapString += map[i, j] + ",";
            }
            mapString += "/";
           // mapString += "\n";
        }
        Debug.Log(mapString);

    }
}
