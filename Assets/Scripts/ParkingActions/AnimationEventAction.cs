using ParkingObjects;

namespace ParkingActions
{
    public class AnimationEventAction : ParkingAction<AnimationEventAction.AnimationEventActionData>
    {
        public struct AnimationEventActionData
        {
            public float Speed;
            public int Loops;
        }

        public AnimationEventAction(ParkingElement element) : base(element)
        {
        }

        public override void DoAction(AnimationEventActionData data)
        {
        }
    }
}