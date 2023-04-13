using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace ARFitness
{
    public class Settings : MonoBehaviour
    {
        public bool showCameraFeed = false;
        public bool showFaceMesh = false;

        private void Start()
        {
            if (!showCameraFeed)
            {
                var cam = GameObject.Find("AR Camera");
                cam.GetComponent<ARCameraBackground>().enabled = false;
            }
            // TODO: implement option to show/hide the face mesh
        }
    }
}
