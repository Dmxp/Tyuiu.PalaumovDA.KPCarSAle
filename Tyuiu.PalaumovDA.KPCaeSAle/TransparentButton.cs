using System;
using System.Drawing;
using System.Windows.Forms;

public class TransparentButton : Button
{
    public TransparentButton()
    {
        SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        BackColor = Color.Transparent;
    }

    protected override CreateParams CreateParams
    {
        get
        {
            CreateParams cp = base.CreateParams;
            cp.ExStyle |= 0x20; // WS_EX_TRANSPARENT
            return cp;
        }
    }

    protected override void OnPaint(PaintEventArgs pevent)
    {
        // Необходимо перерисовать фон кнопки, чтобы он был прозрачным
        pevent.Graphics.Clear(Parent.BackColor);
        base.OnPaint(pevent);
    }
}
