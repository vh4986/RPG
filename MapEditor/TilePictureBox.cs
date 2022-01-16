using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapEditor
{
    public enum ImageType
    {
        Tile,
        Decor
    }
    class TilePictureBox : PictureBox
    {
        public ImageType ImageType { get; set; }
        public TilePictureBox()
        {

        }
    }
}
