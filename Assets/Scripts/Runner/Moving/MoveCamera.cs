using UnityEngine;

namespace Runner.Moving
{
    public class MoveCamera : MonoBehaviour
    {
        public Transform target;

        private Vector3 _startDistance, _move;
        private void Awake()
        {
            _startDistance = transform.position - target.position;
        }

        void Update()
        {
            //Vector3 move = new Vector3(0, 0, 1);
            //transform.Translate(move * (Time.deltaTime * _cameraSpeed));
            _move = target.position + _startDistance;
            // Debug.Log(_move);
        
            _move.x = 0;
            _move.y = _startDistance.y;

            transform.position = _move;
        }
    }
}
