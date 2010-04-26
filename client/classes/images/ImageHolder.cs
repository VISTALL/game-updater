using System;
using System.Collections.Generic;
using com.jds.AWLauncher.classes.language.enums;

namespace com.jds.AWLauncher.classes.images
{
    public class ImageHolder
    {
        private static ImageHolder _instance;

        private readonly Dictionary<PictureName, ImageInfo> _images = new Dictionary<PictureName, ImageInfo>();

        private ImageHolder()
        {
            foreach (var enu in Enum.GetValues(typeof (PictureName)))
            {
                _images.Add((PictureName) enu, new ImageInfo((PictureName) enu));
            }
        }

        public static ImageHolder Instance()
        {
            return _instance ?? (_instance = new ImageHolder());
        }

        public ImageInfo this[PictureName name]
        {
            get { return _images.ContainsKey(name) ? _images[name] : null; }
        }
    }
}