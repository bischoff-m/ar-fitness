using System;
using UnityEngine;

/**
 * This class provides an interface to face tracking. You can either subscribe to face update events
 * or read the current face position directly. FacePosition is artifically kept non-nullable, while
 * FaceObject will be null if the camera could not detect a face.
 * 
 * TODO: Normalize FacePosition using minimum and maximum, set by calibration
 * TODO: Implement OnFaceUpdate once needed
 * TODO: Extrapolate face position when no face is found
 * 
 * oben: 0.5
 * unten: 0.2
 */

public class TrackingProvider : MonoBehaviour
{
    readonly static float INTERNAL_MAX = 0.5f;
    readonly static float INTERNAL_MIN = 0.2f;
    readonly static float INTERNAL_DELTA = INTERNAL_MAX - INTERNAL_MIN;

    // May be null
    public GameObject FaceObject { get; private set; } = null;
    // May not be null
    private Vector3 InternalFacePos = Vector3.zero;

    public void SetFace(GameObject faceObject)
    {
        FaceObject = faceObject;
        Vector3 pos = faceObject.transform.position;
        InternalFacePos.Set(pos.x, pos.y, Math.Clamp(pos.z, INTERNAL_MIN, INTERNAL_MAX));
    }

    /// <summary>
    /// Returns the position of the face relative to the center of the phone screen, normalized to [-1, 1].
    /// </summary>
    public Vector3 GetNormalizedPos()
    {
        return new Vector3(0, (InternalFacePos.z - INTERNAL_MIN) / INTERNAL_DELTA * 2 - 1, 0);
    }

    void Update()
    {
        Debug.Log(string.Format("Face Delta: {0} {1}", InternalFacePos, FaceObject == null ? "(Cached)" : ""));
    }
}
