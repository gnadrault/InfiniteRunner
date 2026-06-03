namespace Player.Data
{
    /// <summary>
    /// Wrapper player states settings
    /// </summary>
    [System.Serializable]
    public class PlayerSettings
    {
        public IdleSettings idle;
        public JumpSettings jump;
        public DieSettings die;
        public ChangeLaneSettings changeLane;
        public SlideSettings slide;
    }
}