using System;
using ARFitness.PlayerInput;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace ARFitness.PlayerInput
{
    /// <summary>
    /// The InputProvider class provides an interface to the input device. It is a singleton, so you can access it from
    /// anywhere. In production (running on Android), it will use face tracking. In the editor, it will use the
    /// keyboard. The class instantiates the correct input provider in the static constructor.
    /// </summary>
    public abstract class InputProvider
    {
        // Holds the instance of the input provider
        private static readonly Lazy<InputProvider> Lazy = new(InitInputProvider);

        // The public interface to the input provider
        public static InputProvider Instance => Lazy.Value;

        /// <summary>
        /// Checks if it has access to the camera, instantiates and returns the correct input provider.
        /// </summary>
        private static InputProvider InitInputProvider()
        {
            var cameraManager = GameObject.Find("AR Camera").GetComponent<ARCameraManager>();
            if (cameraManager.permissionGranted)
                return new FaceInputProvider();
            return new KeyboardInputProvider();
        }
        
        /// <summary>
        /// Returns true if the app is running on an actual Android device that has access to the camera.
        /// </summary>
        public static bool IsAndroid => Instance is FaceInputProvider;
        
        /// <summary>
        /// Returns the position of the face relative to the center of the phone screen, normalized to [-1, 1].
        /// </summary>
        public abstract Vector3 GetPos();
    }
    
    /// <summary>
    /// The FaceInputProvider class translates the position of the face of the player into a position on the screen.
    /// An instance of the AR Default Face prefab is spawned as a child of the AR Session Origin. This face object
    /// calls UpdateFace to set the position of the FaceInputProvider.
    /// </summary>
    public class FaceInputProvider : InputProvider
    {
        // TODO: Normalize FacePosition using minimum and maximum, set by calibration
        // TODO: Extrapolate face position when no face is found
        
        private const float InternalMax = 0.5f;
        private const float InternalMin = 0.2f;
        private const float InternalDelta = InternalMax - InternalMin;

        // May be null
        private GameObject FaceObject { get; set; }
        // May not be null
        private Vector3 _facePos = Vector3.zero;

        public void UpdateFace(GameObject face)
        {
            FaceObject = face;
            var pos = face.transform.position;
            _facePos.Set(pos.x, pos.y, Math.Clamp(pos.z, InternalMin, InternalMax));
        }
        
        public override Vector3 GetPos()
        {
            var normalizedY = (_facePos.z - InternalMin) / InternalDelta * 2 - 1;
            return new Vector3(0, normalizedY, 0);
        }
    }
    
    public enum MoveDirection
    {
        Up,
        Down,
    }

    public class KeyboardInputProvider : InputProvider
    {
        
        private Vector3 _pos = Vector3.zero;
        public override Vector3 GetPos() => _pos;

        public void Move(MoveDirection direction)
        {
            _pos += direction switch
            {
                MoveDirection.Up => new Vector3(0, 0.005f, 0),
                MoveDirection.Down => new Vector3(0, -0.005f, 0),
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }
    }
}
