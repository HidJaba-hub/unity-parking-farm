using ParkingObjects;

namespace ParkingActions
{
    public class IdleAction : AnimationEventAction
    {
        private const string IdleAnimationName = "Idle";
        
        public IdleAction(ParkingElement element) : base(element)
        {
        }

        public override void DoAction(AnimationEventActionData data)
        {
            ParkingElement.GetAnimator.speed = data.Speed;
            ParkingElement.GetAnimator.SetTrigger(IdleAnimationName);
        }
    }
}