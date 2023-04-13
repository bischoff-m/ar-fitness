using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TrackingFace : MonoBehaviour
{
    private TrackingProvider Tracking;

    void Start()
    {
        GameObject settingsObj = GameObject.Find("Settings");
        Tracking = settingsObj.GetComponent<TrackingProvider>();

        if (!settingsObj.GetComponent<Settings>().ShowFaceMesh)
            this.GetComponent<ARFaceMeshVisualizer>().enabled = false;
    }

    void Update()
    {
        Tracking.SetFace(this.gameObject);
    }
}
