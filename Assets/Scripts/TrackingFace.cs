using ARFitness.PlayerInput;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace ARFitness
{
    public class TrackingFace : MonoBehaviour
    {
        private FaceInputProvider _faceInput;

        private void Start()
        {
            if (InputProvider.IsAndroid)
            {
                _faceInput = InputProvider.Instance as FaceInputProvider;
                _faceInput!.UpdateFace(this.gameObject);
            }

            // Hide face mesh if disabled in settings
            var settingsObj = GameObject.Find("Settings");
            if (!settingsObj.GetComponent<Settings>().showFaceMesh)
                this.GetComponent<ARFaceMeshVisualizer>().enabled = false;
        }

        private void Update()
        {
            _faceInput.UpdateFace(this.gameObject);
        }
    }
}
