using ARFitness.PlayerInput;
using UnityEngine;

namespace ARFitness
{
    /// <summary>
    /// The main object that is controlled by the player.
    /// Movement control is either based on AR face tracking or keyboard input, depending on the platform.
    /// </summary>
    public class Actor : MonoBehaviour
    {
        // Used to dampen the movement of the actor
        private Vector3 _velocity = Vector3.zero;
        
        private void Update()
        {
            UpdatePos();
            
            // Move the actor to the position that is given by the input provider
            var oldPos = this.transform.position;
            var newPos = new Vector3(oldPos.x, InputProvider.Instance.GetPos().y * 4, oldPos.z);
            this.transform.position = Vector3.SmoothDamp(oldPos, newPos, ref _velocity, 0.05f);
        }

        /// <summary>
        /// In case of control via keyboard input, this tells the input provider whether keys were pressed.
        /// </summary>
        private static void UpdatePos()
        {
            if (InputProvider.IsAndroid) return;
            var provider = InputProvider.Instance as KeyboardInputProvider;
            
            if (Input.GetKey(KeyCode.UpArrow))
                provider!.Move(MoveDirection.Up);
            if (Input.GetKey(KeyCode.DownArrow))
                provider!.Move(MoveDirection.Down);
        }
    }
}
