using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube
{
    System.Random random = new System.Random();
    Color[] colors = new Color[4];
    string currentFace;


    //cube nets 3x4 => 111010010010 hash = 3730
    //110011010010 = 110011010010
    //110010011010 = 3226
    //110010010011 = 3219
    //100111010010 = 2514
    //100110011010 = 2458
    //100110010011 = 2451
    //100110011001 = 2457
    //010111010010 = 1490
    //010110011010 = 1434
    //cubeNet 2x5 => 1010110101 hash = 693
    public int[] cubeNets3x3 = { 3730, 3282, 3226, 3219, 2514, 2458, 2451, 2457, 1490, 1434};
    public int cubeNet2x5 = 693;

    //frontface -> right -> right -> right -> bottom ->up
    int[,] hashCalculationInstructions = { { 0, 1, 2, 3 }, { 2, 6, 3, 7 }, { 5, 4, 7, 3 }, { 4, 0, 6, 2 }, { 2, 3, 6, 7 }, { 6, 1, 4, 6 } };

    //faces hash -> each face is represented by a string consisitng of numbers from set={0,1,2,3,4} -> representing colors of each corner clockwise (starting corner does not have to be the same)
    string[] faceHash = new string[6];

    //rotation position -> number on each index represents, number at which position from previous configuration should come to that index
    public int[] rightRotation = { 3, 0, 1, 2, 4, 5};
    public int[] leftRotation = { 1, 2, 3, 0, 4, 5};
    public int[] upRotation = { 4, 1, 5, 3, 2, 0};
    public int[] downRotation = { 5, 1, 4, 3, 0, 2};

    //initial configuration -> number at each index is actually an index into array faceHash saying, which face is facing in front
    int[] configuration = { 0, 1, 2, 3, 4, 5 };


    public enum InnerCube
    {
        TOP_FRONT_RIGHT = 0,
        TOP_FRONT_LEFT = 1,
        TOP_BACK_RIGHT = 2,
        TOP_BACK_LEFT = 3,

        BOTTOM_FRONT_RIGHT = 4,
        BOTTOM_FRONT_LEFT = 5,
        BOTTOM_BACK_RIGHT = 6,
        BOTTOM_BACK_LEFT = 7
    }

    public Cube()
    {
        generateColors();
        currentFace = faceHash[configuration[0]];
    }

    public string getFaceHash(int i)
    {
        return faceHash[i];
    }

    public int[] getConfiguration()
    {
        return configuration;
    }

    public void generateColors()
    {
        string[] colorHex = new string[] { "#FE0000", "#01B0F1", "#FFFF01", "#92D14F" };
        for(int i=0; i<4; i++)
        {
            ColorUtility.TryParseHtmlString(colorHex[i], out colors[i]);
        }
    }

    public void generateRandomCube()
    {
        for(int i=0; i<8; i++)
        {
            int rndIndex = random.Next(4);
            innerCubesColor[i] = colors[rndIndex];
        }

    }


    private Color[] innerCubesColor = new Color[8];

    public void setInnerCubeColor(InnerCube innerCube, Color color)
    {
        innerCubesColor[(int)innerCube] = color;
    }

    public Color getInnerCubeColor(InnerCube innerCube)
    {
        return innerCubesColor[(int)innerCube];
    }

    public string getCurrentFace()
    {
        return currentFace;
    }

    public void createFacesHash()
    {
        for(int i=0; i<6; i++)
        {
            string hashString = "";
            for(int j=0; j<4; j++)
            {
                int index = hashCalculationInstructions[i, j];
                hashString += getColorsIndex(innerCubesColor[index]);
            }
            faceHash[i] = hashString;
        }
    }

    public int getColorsIndex(Color color)
    {
        for(int i=0; i<4; i++)
        {
            if (colors[i] == color)
            {
                return i + 1;
            }
        }
        return 0;
    }
}
