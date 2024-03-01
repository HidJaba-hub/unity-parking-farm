using UnityEngine;

namespace Runner.Moving
{
    public class MoveCharacter : MonoBehaviour
    {
        public Animator farmerAnimator;
        public float playerSpeed = 10.0f;
        public float jumpHeight = 1.0f;
        public float firstLanePos, laneDistance, sideSpeed;
    
        private Vector3 _playerVelocity;
        private Vector3 _move;

        private float gravityValue = -9.81f;

        private int _laneNumber=1, _laneCount=2;

        private CharacterController _controller;
        private AnimationToggler _animationToggler;

        private void Start()
        {

            TryGetComponent<CharacterController>(out _controller);
            _animationToggler = new AnimationToggler(farmerAnimator);
        
            _move = new Vector3(0,0,1);
        }
        void Update()
        {
            if (_controller.isGrounded && Input.GetKeyDown(KeyCode.W))
            {
                _animationToggler.FarmerJump();
                _playerVelocity.y = 0;
                while (_playerVelocity.y < 3f)
                {
                    _playerVelocity.y += Mathf.Sqrt(jumpHeight * -3 * gravityValue);
                }
            }
            else
                _playerVelocity.y += gravityValue * Time.smoothDeltaTime;
        
            _move.z = playerSpeed;
            _move *= Time.deltaTime;
            _move += _playerVelocity * Time.smoothDeltaTime;
        
            CheckInput();
        
            _controller.Move(_move);
        
            Vector3 newPos = transform.position;
            newPos.x = Mathf.Lerp(newPos.x, firstLanePos + (_laneNumber * laneDistance), Time.smoothDeltaTime*sideSpeed);
            transform.position = newPos;
        }
        void CheckInput()
        {
            int sign = 0;

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                sign = 1;
                _animationToggler.FarmerTurn(sign);
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                sign = -1;
                _animationToggler.FarmerTurn(sign);
            }
            else
            {
                return;
            }
        
            _laneNumber += sign;
            _laneNumber = Mathf.Clamp(_laneNumber, 0, _laneCount);
        }
    }
}
