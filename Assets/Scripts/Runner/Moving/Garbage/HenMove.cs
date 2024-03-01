using System.Collections.Generic;
using UnityEngine;

namespace Runner.Moving
{
    public class HenMove : MonoBehaviour
    {
        public Animator farmerAnimator;
        public float playerSpeed = 10.0f;
        public float jumpHeight = 1.0f;
        public float firstLanePos, laneDistance, sideSpeed;
        public int side = 0;
        public Dictionary<float, int>.Enumerator instructions;
    
        private Vector3 _playerVelocity;
        private Vector3 _move;

        private float gravityValue = -9.81f;

        private int _laneNumber=1, _laneCount=2;

        private CharacterController _controller;
        private AnimationToggler _animationToggler;
        private RaycastHit _hit;
        private float _chooseSidePoint = 0;

        private void Awake()
        {

            TryGetComponent<CharacterController>(out _controller);
            _animationToggler = new AnimationToggler(farmerAnimator);
        
            _move = new Vector3(0,0,1);
        }
        void Update()
        {
            _move.z = playerSpeed;
            _move *= Time.deltaTime;
            _move += _playerVelocity * Time.smoothDeltaTime;
        
            ChangeRoute();
        
            _controller.Move(_move);
        
            Vector3 newPos = transform.position;
            newPos.x = Mathf.Lerp(newPos.x, firstLanePos + (_laneNumber * laneDistance), Time.smoothDeltaTime*sideSpeed);
            transform.position = newPos;
        }

        void ChangeRoute()
        {
            if (_chooseSidePoint != 0 && transform.position.z >= _chooseSidePoint)
            {
                side = instructions.Current.Value;
                /*if (side == 0)
            {
                _laneNumber = 1;
            }*/
                _laneNumber = side + 1;
                _chooseSidePoint = 0;
            }
            if (_chooseSidePoint == 0 && instructions.MoveNext())
            {
                _chooseSidePoint = instructions.Current.Key - 3;
            }
            //_laneNumber += side;
            _laneNumber = Mathf.Clamp(_laneNumber, 0, _laneCount);
        }
    }
}
