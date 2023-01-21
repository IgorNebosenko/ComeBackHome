namespace CBH.Core.Entity
{
    public interface IInput
    {
        public bool EnabledBoost { get; }
        public float RotationDirection { get; }
        
        void Init();
        void Update(float deltaTime);
        void ResetInput();
    }
}