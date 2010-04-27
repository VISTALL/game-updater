using System;
using System.Drawing;
using System.Windows.Forms;
using com.jds.AWLauncher.classes.forms;
using com.jds.AWLauncher.classes.utils;

namespace com.jds.AWLauncher.classes.events
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
                
                if (label.Tag != null)
                {
                    string name = label.Font.Name;
                    float size = label.Font.Size;
                    GraphicsUnit unit = label.Font.Unit;
                    label.Font = new Font(name, size, FontStyle.Underline, unit);
                }
            }
        }

        public static void LinkLeave(object sender, EventArgs e)
        {
            if (sender is Label)
            {
                var label = sender as Label;
                if (label.Tag != null)
                {
                    string name = label.Font.Name;
                    float size = label.Font.Size;
                    GraphicsUnit unit = label.Font.Unit;
                    label.Font = new Font(name, size, FontStyle.Regular, unit);
                }
            }
        }

        public static void LinkGoEvent(object sender, EventArgs e)
        {
            if (sender is Label)
            {
                var label = sender as Label;
                var target = label.Tag as String;
                if(target != null)
                {
                    switch(target)
                    {
                        case "ASSEMBLY":
                            if (!MainForm.Instance.TabbedPane.IsSelectionDisabled)
                            {
                                PropertyForm.Instance().ShowDialog(MainForm.Instance);
                            }
                            break;
                        default:
                            ProcessR.Start(target);
                            break;
                    }
                }
            }
        }
    }
}