using UnityEngine;

public class Actor : MonoBehaviour
{
    private TrackingProvider Tracking;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        Tracking = GameObject.Find("Settings").GetComponent<TrackingProvider>();
    }

    void Update()
    {
        Vector3 oldPos = this.transform.position;
        Vector3 newPos = new Vector3(oldPos.x, Tracking.GetNormalizedPos().y * 4, oldPos.z);
        this.transform.position = Vector3.SmoothDamp(oldPos, newPos, ref velocity, 0.05f);
        Debug.Log(string.Format("Actor Pos: {0}", Tracking.GetNormalizedPos().y));
    }
}
