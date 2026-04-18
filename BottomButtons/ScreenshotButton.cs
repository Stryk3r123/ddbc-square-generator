using Godot;
using System.Threading.Tasks;

namespace SquareGen.BottomButtons
{
    public partial class ScreenshotButton : Button
    {
        public override void _Ready()
        {
            Connect("pressed", this, nameof(OnPressed));
        }

        private async Task OnPressed()
        {
            GetParent<Node2D>().Hide();

            await ToSignal(GetTree().CreateTimer(0.05f), "timeout");

            Image image = GetViewport().GetTexture().GetData();
            image.FlipY();
            JavaScript.DownloadBuffer(image.SavePngToBuffer(), "team.png");
            GetParent<Node2D>().Show();
        }
    }
}
