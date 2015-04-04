using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using TheArtOfDev.HtmlRenderer.WinForms;

namespace Glyphfriend.Helpers
{
	class ImageHelper
	{
		public static BitmapFrame GenerateGlyph(string html)
		{
			try
			{
				// Build the image using the HtmlRenderer
				var imageContent = ImageToBytes(HtmlRender.RenderToImageGdiPlus(html, new Size(16, 16)));
				// Use the image content to construct a BitmapFrame
				return BitmapFrame.Create(new MemoryStream(imageContent));
            }
			catch
			{
				// Something went wrong generating the Glyph
				return null;
			}
        }

		private static byte[] ImageToBytes(Image img)
		{
			ImageConverter converter = new ImageConverter();
			return (byte[])converter.ConvertTo(img, typeof(byte[]));
		}
	}
}
