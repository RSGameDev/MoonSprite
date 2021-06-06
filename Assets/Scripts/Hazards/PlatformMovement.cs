using UnityEngine;

namespace Hazards
{
    public class PlatformMovement : MonoBehaviour
    {
        public GameObject[] platforms;
        private Vector3[] _positions;

        public float speed = 1.0f;

        private bool _isTowards = true;
    
        private void Awake()
        {
            _positions = new Vector3[platforms.Length];
            _positions[0] = platforms[0].transform.position;
            _positions[1] = platforms[1].transform.position;
        }

        private void Update()
        {
            if (_isTowards)
            {
                // Move our position a step closer to the target.
                float step =  speed * Time.deltaTime; // calculate distance to move
                transform.position = Vector3.MoveTowards(transform.position, _positions[1], step);

                // Check if the position of the cube and sphere are approximately equal.
                if (Vector3.Distance(transform.position, _positions[1]) < 0.001f)
                {
                    _isTowards = false;
                } 
            }
            else
            {
                float step =  speed * Time.deltaTime; // calculate distance to move
                transform.position = Vector3.MoveTowards(transform.position, _positions[0], step);

                // Check if the position of the cube and sphere are approximately equal.
                if (Vector3.Distance(transform.position, _positions[0]) < 0.001f)
                {
                    _isTowards = true;
                }
            }
        }
    }
}
