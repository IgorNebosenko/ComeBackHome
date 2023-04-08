namespace CBH.Core.Configs.Levels
{
    public class LevelDataPair
    {
        public int buildIndex;
        public LevelDataConfig levelDataConfig;

        public LevelDataPair(int buildIndex, LevelDataConfig levelDataConfig)
        {
            this.buildIndex = buildIndex;
            this.levelDataConfig = levelDataConfig;
        }
    }
}