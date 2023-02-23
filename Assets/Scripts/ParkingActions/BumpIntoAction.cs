using ParkingObjects;
using UnityEngine;

namespace ParkingActions
{
    public class BumpIntoAction : ParkingAction<BumpIntoAction.BumpIntoData>
    {
        private const string MoveAnimationName = "Move";
        
        public struct BumpIntoData
        {
            public RaycastHit Hit;
            public Vector3 Direction;
        }

        public BumpIntoAction(ParkingElement element) : base(element)
        {
        }

        public override void DoAction(BumpIntoData data)
        {
            
        }

        protected float ComputeMoveTime(Vector3 dest)
        {
            var baseSpeed = ParkingElement.GetSpeed == 0 ? 1.0f : ParkingElement.GetSpeed;
            var dist = Vector3.Distance(ParkingElement.transform.position, dest);

            return dist / baseSpeed;
        }

        protected Vector3 ComputeTargetPosition(BumpIntoData data, Vector3 hitColliderBorder)
        {
            var hitPosition = data.Hit.transform.position;
            var targetPosition = ParkingElement.transform.position;
                
            if (data.Direction.z == 0.0f)
            {
                var direction = (data.Direction.x < 0) ? 1 : -1; 
                targetPosition.x = hitPosition.x + 
                                   (hitColliderBorder.x / 2.0f + ParkingElement.GetCollider.size.z / 2.0f) * direction;
            }
            else
            {
                var direction = (data.Direction.z < 0) ? 1 : -1; 
                targetPosition.z = hitPosition.z + 
                                   (hitColliderBorder.z / 2.0f + ParkingElement.GetCollider.size.z / 2.0f) * direction;
            }

            return targetPosition;
        }

        protected void MoveAnimation(bool state)
        {
            if (state)
                // Animations been slowed down twice according to element speed
                ParkingElement.GetAnimator.speed = ParkingElement.GetSpeed * 0.5f;
            else
                // When exits animation, then resets animation speed to default value 
                ParkingElement.GetAnimator.speed = 1.0f;
            
            ParkingElement.GetAnimator.SetBool(MoveAnimationName, state);
        }
    }
}