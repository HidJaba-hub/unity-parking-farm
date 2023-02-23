using System;
using DG.Tweening;
using ParkingActions;
using UnityEngine;

namespace ParkingObjects
{
    public class Horse : ParkingElement
    {
        private EatAction _eatAction;
        private IdleAction _idleAction;

        private const int DefaultAnimatorLayerIndex = 0;
        private const string TrueIdleStateName = "Idle";

        private float _randomActionTimer;
        [SerializeField] private float _randomActionDelay;

        private void Start()
        {
            _randomActionTimer = 0.0f;
            
            _idleAction.DoAction(new AnimationEventAction.AnimationEventActionData()
            {
                Speed = 0.02f,
                Loops = 1
            });
        }

        protected override void DefineActions()
        {
            base.DefineActions();

            _eatAction = new EatAction(this);
            _idleAction = new IdleAction(this);
        }

        private void Update()
        {
            InvokeRandomAction();
        }

        private void InvokeRandomAction()
        {
            _randomActionTimer += Time.deltaTime;
            
            if(_randomActionTimer >= _randomActionDelay)
            {
                if (GetAnimator.GetCurrentAnimatorStateInfo(DefaultAnimatorLayerIndex).IsName(TrueIdleStateName))
                {
                    TryInvokeAction();
                }

                _randomActionTimer = 0.0f;
            }        
        }

        private void TryInvokeAction()
        {
            int rnd = UnityEngine.Random.Range(0, 100);

            if (rnd <= 25)
            {
                rnd = UnityEngine.Random.Range(1, 3);

                switch (rnd)
                {
                    case 1:
                        _eatAction.DoAction(new AnimationEventAction.AnimationEventActionData());
                        break;
                    case 2:
                        _idleAction.DoAction(new AnimationEventAction.AnimationEventActionData()
                        {
                            Speed = UnityEngine.Random.Range(0.0f, 0.75f),
                            Loops = 1
                        });
                        break;
                }
            }
        }
    }
}