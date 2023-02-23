using DG.Tweening;
using Enums;
using ParkingObjects;
using UnityEngine;

namespace ParkingActions
{
    public class BumpIntoObject : BumpIntoAction
    {
        public BumpIntoObject(ParkingElement element) : base(element)
        {
        }

        public override void DoAction(BumpIntoData data)
        {
            var parkingElement = data.Hit.transform.GetComponent<ParkingElement>();
            var hitColliderBorder = ComputeHitColliderBorder(data, parkingElement);
            var targetPosition = ComputeTargetPosition(data, hitColliderBorder);

            ParkingElement.transform.DOMove(targetPosition, ComputeMoveTime(targetPosition))
                .OnStart(() => MoveAnimation(true))
                .OnComplete(() => MoveAnimation(false));
        }

        private Vector3 ComputeHitColliderBorder(BumpIntoData data, ParkingElement parkingElement)
        {
            var hitColliderBorder = parkingElement.GetCollider.size;

            if (data.Direction.z == 0.0f)
                hitColliderBorder.x = parkingElement.GetOrientation == Orientation.Horizontal
                    ? parkingElement.GetCollider.size.z
                    : parkingElement.GetCollider.size.x;
            else
                hitColliderBorder.z = parkingElement.GetOrientation == Orientation.Horizontal
                    ? parkingElement.GetCollider.size.x
                    : parkingElement.GetCollider.size.z;

            return hitColliderBorder;
        }
    }
}