using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CodeMonkey;
using CodeMonkey.Utils;
using GridPathfindingSystem;

public class GameHandler : MonoBehaviour
{

    public static GridPathfinding gridPathfinding;
    
    public CameraFollow CameraFollow;
    public Transform plyerTransform;

    private void Awake()
    {
        Vector3 pathfindingLowerLeft = transform.Find("PathfindingLowerLeft").position;
        Vector3 pathfindingUpperRight = transform.Find("PathfindingUpperRight").position;
        
        gridPathfinding = new GridPathfinding(pathfindingLowerLeft, pathfindingUpperRight, 5f);
    }

    private void Start() {
        CameraFollow.Setup(() => plyerTransform.position);
        Debug.Log("GameHandler.Start");

        #if UNITY_EDITOR
                QualitySettings.vSyncCount = 0;  // VSync must be disabled
                Application.targetFrameRate = 40; // targetFrameRate 60 = 120 fps
        #endif
        // int i = 0;
        // FunctionPeriodic.Create(() => {
            // CMDebug.TextPopupMouse("Ding "+i+"!");
            // i++;
        // }, .5f);
    }

    private void Update() {
        
    }

}
