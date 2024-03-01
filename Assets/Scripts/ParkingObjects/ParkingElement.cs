using System;
using Enums;
using ParkingActions;
using UnityEngine;

namespace ParkingObjects
{
    public class ParkingElement : MonoBehaviour
    {
        [SerializeField] private float _speed;
    
        private BoxCollider _boxCollider;
        private Animator _animator;
        private Orientation _carOrientation = Orientation.Vertical;
        public event Action<ParkingElement> OnCarExitAction;

        private const string AccessBorderTag = "Access";
        private const string FenceBorderTag = "Fence";
        private const string GameObjectTag = "GameObject";

        private BumpIntoAction _bumpIntoAccess;
        private BumpIntoAction _bumpIntoFence;
        private BumpIntoAction _bumpIntoObject;
        
        public BoxCollider GetCollider => _boxCollider;
        public Animator GetAnimator => _animator;
        public float GetSpeed => _speed;
        public Orientation GetOrientation => _carOrientation;

        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider>();
            _animator = GetComponent<Animator>();
            
            DefineActions();
            DefineOrientation();
        }

        private void DefineOrientation()
        {
            if (transform.forward == Vector3.right || transform.forward == Vector3.left)
                _carOrientation = Orientation.Horizontal;
        }

        protected virtual void DefineActions()
        {
            _bumpIntoAccess = new BumpIntoAccess(this);
            _bumpIntoFence = new BumpIntoFence(this);
            _bumpIntoObject = new BumpIntoObject(this);
        }

        public bool Act(Orientation ori, bool isForward)
        {
            if(_carOrientation != ori) return false;
            
            var direction = ori == Orientation.Vertical ? Vector3.forward : Vector3.right;
            PerformAction(direction * (isForward ? 1 : -1));
            return true;
        }

        private void PerformAction(Vector3 dir)
        {
            var ray = new Ray()
            {
                origin = transform.position,
                direction = dir
            };
            if (!Physics.Raycast(ray, out var hit, 100.0f)) return;
            
            if (hit.transform.CompareTag(AccessBorderTag))
                PerformBumpIntoAction(_bumpIntoAccess, dir, hit);
            else if (hit.transform.CompareTag(FenceBorderTag))
                PerformBumpIntoAction(_bumpIntoFence, dir, hit);
            else if (hit.transform.CompareTag(GameObjectTag))
                PerformBumpIntoAction(_bumpIntoObject, dir, hit);
        }

        private void PerformBumpIntoAction(BumpIntoAction action, Vector3 dir, RaycastHit hit)
        {
            action.DoAction(new BumpIntoAction.BumpIntoData()
            {
                Hit = hit,
                Direction = dir
            });
        }
        
        public void InvokeCompleteEvent() => OnCarExitAction?.Invoke(this);
    }
}
