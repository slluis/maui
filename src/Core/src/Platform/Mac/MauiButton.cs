using System;
using AppKit;

namespace Microsoft.Maui.Platform
{
    public class MauiButton : NSButton
    {
        public event EventHandler? MouseLeftUp;
        public event EventHandler? MouseLeftDown;

        public override bool IsFlipped => true;

        public MauiButton()
        {
            BezelStyle = NSBezelStyle.Rounded;
        }

        public override void MouseDown(NSEvent theEvent)
        {
            base.MouseDown(theEvent);
            MouseLeftDown?.Invoke(this, EventArgs.Empty);
        }

        public override void MouseUp(NSEvent theEvent)
        {
            base.MouseUp(theEvent);
            MouseLeftUp?.Invoke(this, EventArgs.Empty);
        }

        public NSImageView ImageView
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}