using System.Drawing;

namespace com.jds.GUpdater.classes.gui
{
    public interface IImageInfo
    {
        Image NormalImage();
        Image EnterImage();
        Image PressedImage();
        Image ActiveImage();
        Image DisableImage();
    }
}