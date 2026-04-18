using Godot;
using SquareGen.Globals;

namespace SquareGen.BottomButtons
{
    public partial class TogglePointsButton : Button
    {
        public override void _Ready()
        {
            Connect("pressed", this, nameof(OnPressed));
        }

        private void OnPressed()
        {
            Settings.SetSetting(Settings.Keys.TRACK_POINTS, !Settings.GetSetting(Settings.Keys.TRACK_POINTS));
        }
    }
}
