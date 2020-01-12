using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstLevelSolver: MonoBehaviour
{

    //decides if a matrix - map contains any of the cube nets... algorithm goes as follows: for each position in columns 0,1,2 and rows 0,1 count hash - a decimal number that is represented by a binary sequence that corresponds
    //to rows of matrix 3x4 starting on that specific position
    public void IsNetCorrect()
    {
        int[] hashCodes = new int[6];
        int size = 0;
        for(int i=0; i<2; i++)
        {
            for(int j=0; j<3; j++)
            {
                hashCodes[size++] = countHash(i, j);
            }
        }
        for (int i=0; i<10; i++)
        {
            for(int j=0; j<6; j++)
            {
                if (CubeController.getCubeController().getCube().cubeNets3x3[i] == hashCodes[j])
                {
                    Debug.Log("solved!!");
                    return;
                }
            }

        }


    }

    public void Solve()
    {
        int[] startingCordinates = TileMap.getTileMap().initializeSolver();
        if (startingCordinates == null)
        {
            Debug.Log("No tile has been marked yet");
            return;
        }
        int startRow = startingCordinates[0];
        int startCol = startingCordinates[1];

        Debug.Log(startRow + ", " + startCol);

        Queue<int[]> q = new Queue<int[]>();
        q.Enqueue(TileMap.getTileMap().getAdjacencyMatrix()[startRow, startCol]);

        string tileHash = TileMap.getTileMap().getCorner(startRow, startCol);
        string cubeFaceHash = CubeController.getCubeController().getCube().getCurrentFace();

        List<int[]> visited = new List<int[]>();
        visited.Add(new int[] { startRow, startCol });

        Debug.Log("solved: "+compareRecursively(cubeFaceHash, startRow, startCol, CubeController.getCubeController().getCube().getConfiguration(), visited));


    }


    public bool compareRecursively(string cubeFace, int row, int col, int[] cubeConfiguration, List<int[]> visited)
    {
        string tileHash = TileMap.getTileMap().getCorner(row, col);
        Debug.Log("tilehash" + tileHash);
        if (cubeFace.Equals(tileHash))
        {
            return true;
        }
        else
        {
            return false;
        }
        bool isSame = true;
        int[] tile = TileMap.getTileMap().getAdjacencyMatrix()[row, col];
        if (tile[0] == 1 && visited.Contains(new int[] { row - 1, col }))
        {
            //rotate down
            visited.Add(new int[] { row - 1, col });
            cubeConfiguration = rotateDown(cubeConfiguration);
            cubeFace = CubeController.getCubeController().getCube().getFaceHash(cubeConfiguration[0]);
            isSame = isSame && compareRecursively(cubeFace, row - 1, col, cubeConfiguration, visited);
        } else if (tile[1] == 1 && visited.Contains(new int[] { row, col + 1 }))
        {
            //rotate left
            visited.Add(new int[] { row, col + 1 });
            cubeConfiguration = rotateLeft(cubeConfiguration);
            cubeFace = CubeController.getCubeController().getCube().getFaceHash(cubeConfiguration[0]);
            isSame = isSame && compareRecursively(cubeFace, row, col + 1, cubeConfiguration, visited);
        } else if(tile[2] == 1 && visited.Contains(new int[] { row+1, col}))
        {
            //rotate up
            visited.Add(new int[] { row + 1, col });
            cubeConfiguration = rotateUp(cubeConfiguration);
            cubeFace = CubeController.getCubeController().getCube().getFaceHash(cubeConfiguration[0]);
            isSame = isSame && compareRecursively(cubeFace, row + 1, col, cubeConfiguration, visited);
        } else if(tile[3] == 1 && visited.Contains(new int[] {row, col-1 }))
        {
            //rotate right
            visited.Add(new int[] { row, col - 1 });
            cubeConfiguration = rotateRight(cubeConfiguration);
            cubeFace = CubeController.getCubeController().getCube().getFaceHash(cubeConfiguration[0]);
            isSame = isSame && compareRecursively(cubeFace, row, col - 1, cubeConfiguration, visited);
        }

        return isSame;
    }




    public int[] rotateDown(int[] configuration)
    {
        int[] downRotation = CubeController.getCubeController().getCube().downRotation;
        int[] result = new int[6];
        for(int i=0; i<6; i++)
        {
            int index = downRotation[i];
            result[i] = configuration[index];
        }
        return result;
    }

    public int[] rotateLeft(int[] configuration)
    {
        int[] downRotation = CubeController.getCubeController().getCube().leftRotation;
        int[] result = new int[6];
        for (int i = 0; i < 6; i++)
        {
            int index = downRotation[i];
            result[i] = configuration[index];
        }
        return result;
    }

    public int[] rotateUp(int[] configuration)
    {
        int[] downRotation = CubeController.getCubeController().getCube().upRotation;
        int[] result = new int[6];
        for (int i = 0; i < 6; i++)
        {
            int index = downRotation[i];
            result[i] = configuration[index];
        }
        return result;
    }

    public int[] rotateRight(int[] configuration)
    {
        int[] downRotation = CubeController.getCubeController().getCube().rightRotation;
        int[] result = new int[6];
        for (int i = 0; i < 6; i++)
        {
            int index = downRotation[i];
            result[i] = configuration[index];
        }
        return result;
    }

    public int countHash(int r, int c)
    {
        int[,] map = TileMap.getTileMap().getMap();

        string hashString = "";
        for(int i=r; i<r+4; i++)
        {
            for(int j=c; j<c+3; j++)
            {
                hashString += map[i, j];
            }
        }
        return Convert.ToInt32(hashString, 2);
    }

    public bool compareFaceHashCodes(string hash1, string hash2)
    {
        string temp = hash1 + hash1;
        if (temp.Contains(hash2))
        {
            return true;
        }
        return false;
    }

}
