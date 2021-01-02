using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameHandler : MonoBehaviour
{

    public CameraFollow CameraFollow;
    public Transform plyerTransform;

    private void Start() {
        CameraFollow.Setup(() => plyerTransform.position);
        Debug.Log("GameHandler.Start");

        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
        #if UNITY_EDITOR
                Application.targetFrameRate = 40; // targetFrameRate 60 = 120 fps
        #endif
    }
}
