using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static float CLOCK = 0.0f;
    public static float MOUSE_SENSITIVITY = 1.0f;
    public static Vector3 playerPosition = new Vector3(0.0f,0.0f,0.0f);
    public static Vector3[] mapLocations;
    public static int mapIndex = 0;
    public static int chunkDimension = 100;

}//
