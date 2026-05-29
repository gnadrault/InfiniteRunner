namespace Player.Data
{
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