using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BedrockLauncher.Bootstrap
{
    public class NewProgressBar : ProgressBar
    {
        public NewProgressBar()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            SolidBrush brush = null;
            Rectangle rec = new Rectangle(0, 0, this.Width, this.Height);
            double scaleFactor = (((double)Value - (double)Minimum) / ((double)Maximum - (double)Minimum));

            brush = new SolidBrush(this.BackColor);
            e.Graphics.FillRectangle(brush, 0, 0, rec.Width, rec.Height);

            rec.Width = (int)((rec.Width * scaleFactor) - 2);
            rec.Height -= 2;
            brush = new SolidBrush(this.ForeColor);
            e.Graphics.FillRectangle(brush, 0, 0, rec.Width, rec.Height);
        }
    }
}
