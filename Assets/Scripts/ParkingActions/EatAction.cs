using ParkingObjects;

namespace ParkingActions
{
    public class EatAction : AnimationEventAction
    {
        private const string EatAnimationName = "Eat";
        
        public EatAction(ParkingElement element) : base(element)
        {
        }

        public override void DoAction(AnimationEventActionData data)
        {
            ParkingElement.GetAnimator.speed = 1.0f;
            ParkingElement.GetAnimator.SetTrigger(EatAnimationName);
        }
    }
}