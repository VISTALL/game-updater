using System.Drawing;
using com.jds.AWLauncher.classes.gui;
using com.jds.AWLauncher.classes.language;
using com.jds.AWLauncher.classes.language.enums;

namespace com.jds.AWLauncher.classes.images
{
    public class ImageInfo : IImageInfo
    {
        private readonly PictureName _name;

        public ImageInfo(PictureName name)
        {
            _name = name;
        }

        #region IImageInfo Members

        public Image NormalImage()
        {
            return LanguageHolder.Instance().GetImage(_name, PictureType.NORMAL);
        }

        public Image EnterImage()
        {
            return LanguageHolder.Instance().GetImage(_name, PictureType.FOCUS);
        }

        public Image PressedImage()
        {
            return LanguageHolder.Instance().GetImage(_name, PictureType.DOWN);
        }

        public Image ActiveImage()
        {
            return LanguageHolder.Instance().GetImage(_name, PictureType.ACTIVE);
        }

        public Image DisableImage()
        {
            return LanguageHolder.Instance().GetImage(_name, PictureType.DISABLE);
        }

        #endregion
    }
}