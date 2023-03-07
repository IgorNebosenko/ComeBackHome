namespace CBH.Analytics.Events
{
    public class CrashPlayerRocketEvent : AnalyticsEvent
    {
        public override string Key => "crash_player_rocket";

        public CrashPlayerRocketEvent(int levelId, int maxOpenedLevel, float flyTime, InputData inputData, int countLandingOnLandingPad)
        {
            Data.Add("level_id", levelId);
            Data.Add("max_opened_level", maxOpenedLevel);
            Data.Add("fly_time", flyTime);
            Data.Add("count_boost_button_pressed", inputData.countBoostPressed);
            Data.Add("count_rotate_left_button_pressed", inputData.countRotationLeftPressed);
            Data.Add("count_rotate_right_button_pressed", inputData.countRotationRightPressed);
            Data.Add("boost_time", inputData.timeBoostPressed);
            Data.Add("rotation_left_time", inputData.timeRotationLeftPressed);
            Data.Add("rotation_right_time", inputData.timeRotationRightPressed);
            Data.Add("count_landing_on_landing_pad", countLandingOnLandingPad);
        }
    }
}