using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Glyphfriend.Core
{
    public class LazyImage
    {
        public Uri Path { get; set; }

        ImageSource _image;
        public ImageSource Image
        {
            get
            {
                if (_image == null)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        _image = BitmapFrame.Create(Path);

                    });
                }
                return _image;
            }
        }
    }
}
