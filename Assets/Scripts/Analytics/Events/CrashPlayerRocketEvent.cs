namespace CBH.Analytics.Events
{
    public class CrashPlayerRocketEvent : AnalyticsEvent
    {
        public override string Key => "crash_player_rocket";

        public CrashPlayerRocketEvent(int levelId, int maxOpenedLevel, float flyTime, int countBoostButtonPressed,
            int countRotateLeftButtonPressed, int countRotateRightButtonPressed, float boostTime,
            float rotationLeftTime, float rotationRightTime, int countLandingOnLandingPad)
        {
            Data.Add("level_id", levelId);
            Data.Add("max_opened_level", maxOpenedLevel);
            Data.Add("fly_time", flyTime);
            Data.Add("count_boost_button_pressed", countBoostButtonPressed);
            Data.Add("count_rotate_left_button_pressed", countRotateLeftButtonPressed);
            Data.Add("count_rotate_right_button_pressed", countRotateRightButtonPressed);
            Data.Add("boost_time", boostTime);
            Data.Add("rotation_left_time", rotationLeftTime);
            Data.Add("rotation_right_time", rotationRightTime);
            Data.Add("count_landing_on_landing_pad", countLandingOnLandingPad);
        }
    }
}