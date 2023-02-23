using DG.Tweening;
using ParkingObjects;
using UnityEngine;

namespace ParkingActions
{
    public class BumpIntoFence : BumpIntoAction
    {
        public override void DoAction(BumpIntoData data)
        {
            var hitColliderBorder = data.Hit.transform.GetComponent<BoxCollider>().size;
            var targetPosition = ComputeTargetPosition(data, hitColliderBorder);
            
            ParkingElement.transform.DOMove(targetPosition, ComputeMoveTime(targetPosition))
                .SetEase(Ease.Linear)
                .OnStart(() => MoveAnimation(true))
                .OnComplete(() => MoveAnimation(false));;
        }

        public BumpIntoFence(ParkingElement element) : base(element)
        {
        }
    }
}