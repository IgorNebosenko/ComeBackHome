namespace CBH.Core.Entity
{
    public class RocketManager
    {
        private GameManager _gameManager;
        public RocketState CurrentRocketState { get; private set; }

        public RocketManager(GameManager gameManager)
        {
            _gameManager = gameManager;
            CurrentRocketState = RocketState.Live;
        }

        private void RocketLoadState()
        {
            _gameManager.HandleRocketState(CurrentRocketState);
        }

        public void SetRocketState(RocketState state)
        {
            CurrentRocketState = state;
        }
    }
}