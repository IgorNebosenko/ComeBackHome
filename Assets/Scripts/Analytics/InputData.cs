namespace CBH.Analytics
{
    public class InputData
    {
        public int countBoostPressed;
        public int countRotationLeftPressed;
        public int countRotationRightPressed;

        public float timeBoostPressed;
        public float timeRotationLeftPressed;
        public float timeRotationRightPressed;

        public void Reset()
        {
            countBoostPressed = 0;
            countRotationLeftPressed = 0;
            countRotationRightPressed = 0;

            timeBoostPressed = 0f;
            timeRotationLeftPressed = 0f;
            timeRotationRightPressed = 0f;
        }
    }
}