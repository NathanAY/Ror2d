using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CodeMonkey;
using CodeMonkey.Utils;

public class GameHandler : MonoBehaviour
{

    public CameraFollow CameraFollow;
    public Transform plyerTransform;
    
    private void Start() {
        CameraFollow.Setup(() => plyerTransform.position);
        Debug.Log("GameHandler.Start");

        #if UNITY_EDITOR
                QualitySettings.vSyncCount = 0;  // VSync must be disabled
                Application.targetFrameRate = 60; // targetFrameRate 60 = 120 fps
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
