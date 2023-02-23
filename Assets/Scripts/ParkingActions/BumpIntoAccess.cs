using DG.Tweening;
using Entities;
using ParkingObjects;
using Unity.VisualScripting;
using UnityEngine;

namespace ParkingActions
{
    public class BumpIntoAccess : BumpIntoAction
    {
        public override void DoAction(BumpIntoData data)
        {
            var hitColliderBorder = data.Hit.transform.GetComponent<BoxCollider>().size;
            var targetPosition = ComputeTargetPosition(data, hitColliderBorder);

            ParkingElement.transform.DOMove(targetPosition, ComputeMoveTime(targetPosition))
                .SetEase(Ease.Linear)
                .OnComplete(() => DoComplete(data.Hit.transform.GetComponent<Exit>()))
                .OnStart(() => MoveAnimation(true));
        }

        private void DoComplete(Exit exit)
        {
            ParkingElement.transform.Follow(exit.exitPath, ParkingElement.GetSpeed, () =>
            {
                MoveAnimation(false);
                ParkingElement.InvokeCompleteEvent();
            });
        }

        public BumpIntoAccess(ParkingElement element) : base(element)
        {
        }
    }
}