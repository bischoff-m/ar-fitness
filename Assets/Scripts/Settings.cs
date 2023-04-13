using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Settings : MonoBehaviour
{
    public bool ShowCameraFeed = true;
    public bool ShowFaceMesh = true;

    void Start()
    {
        if (!ShowCameraFeed)
        {
            GameObject camera = GameObject.Find("AR Camera");
            camera.GetComponent<ARCameraBackground>().enabled = false;
        }
    }

    void Update()
    {
        
    }
}
