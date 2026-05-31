namespace Data
{
    public class EffectTimer
    {
        public float Remaining { get; private set; }

        public EffectTimer(float duration)
        {
            Remaining = duration;
        }
        
        public bool IsDone => Remaining <= 0f;
        public void Tick(float dt) => Remaining -= dt;
    }
}