using Godot;

namespace SquareGen.BottomButtons
{
    public partial class ClearButton : Button
    {
        private readonly PackedScene ResetScene = ResourceLoader.Load<PackedScene>("res://team.tscn");

        public override void _Ready()
        {
            Connect("pressed", this, nameof(OnPressed));
        }

        private void OnPressed()
        {
            GetTree().ChangeSceneTo(ResetScene);
        }
    }
}
