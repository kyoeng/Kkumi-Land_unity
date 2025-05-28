using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void onUnityLoaded();


    private void Start()
    {
        onUnityLoaded();
    }
}
