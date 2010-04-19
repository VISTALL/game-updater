using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace com.jds.GUpdater.classes.events
{
    public class EventHandlers
    {
        public static void Register(Label la)
        {
            la.Click += LinkGoEvent;
            la.MouseEnter += LinkEnter;
            la.MouseLeave += LinkLeave;

            la.Cursor = Cursors.Hand;
        }

        public static void LinkEnter(object sender, EventArgs e)
        {
            if (sender is Label)
            {
                var label = sender as Label;
                string name = label.Font.Name;
                float size = label.Font.Size;
                GraphicsUnit unit = label.Font.Unit;
                label.Font = new Font(name, size, FontStyle.Underline, unit);
            }
        }

        public static void LinkLeave(object sender, EventArgs e)
        {
            if (sender is Label)
            {
                var label = sender as Label;
                string name = label.Font.Name;
                float size = label.Font.Size;
                GraphicsUnit unit = label.Font.Unit;
                label.Font = new Font(name, size, FontStyle.Regular, unit);
            }
        }

        public static void LinkGoEvent(object sender, EventArgs e)
        {
            if (sender is Label)
            {
                var label = sender as Label;
                var target = label.Tag as String;

                if (target != null)
                {
                    try
                    {
                        Process.Start(target);
                    }
                    catch
                    {
                    }
                }
            }
        }
    }
}