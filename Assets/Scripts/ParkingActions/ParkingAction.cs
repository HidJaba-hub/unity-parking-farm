using ParkingObjects;

namespace ParkingActions
{
    public abstract class ParkingAction<T>
    {
        protected readonly ParkingElement ParkingElement;

        protected ParkingAction(ParkingElement element)
        {
            ParkingElement = element;
        }
        
        public abstract void DoAction(T data);
    }
}