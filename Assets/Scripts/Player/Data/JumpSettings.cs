namespace Player.Data
{
    [System.Serializable]
    public class JumpSettings
    {
        public float jumpHeight = 2f;
        public float timeToApex = 0.1f; // Time to reach the Apex
        public float timeApexWait = 0.2f; // Time waiting at Apex (1 input)
        public float timeApexExtraWait = 0.2f; // Time waiting at Apex (keep pressed)
        public float timeDescent = 0.2f; // Time to descent from Apex 
    }
}