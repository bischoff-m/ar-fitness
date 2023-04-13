using UnityEngine;

namespace ARFitness
{
    public class LevelGenerator : MonoBehaviour
    {
        private void FixedUpdate()
        {
            this.transform.position += new Vector3(-0.04f, 0, 0);
            if (this.transform.position.x < -4)
            {
                this.transform.position = new Vector3(5.5f, 0, 10.5f);
                this.transform.Rotate(180f, 0, 0);
            }
        }
    }
}
