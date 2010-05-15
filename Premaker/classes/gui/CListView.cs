using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using com.jds.Premaker.classes.gui.clistview;
using System.Diagnostics;
using System.Drawing.Design;
using System.Reflection;
using System.Drawing.Drawing2D;
using System.ComponentModel.Design;

namespace com.jds.Premaker.classes.gui
{
    #region Enumerations

    /// <summary>
    ///   Types of sorting available
    /// </summary>
    public enum SortTypes
    {
        /// <summary>
        ///   No Sorting
        /// </summary>
        None,
        /// <summary>
        ///   Insertion Sort
        /// </summary>
        InsertionSort,
        /// <summary>
        ///   Merge Sort
        /// </summary>
        MergeSort,
        /// <summary>
        ///   Quick Sort
        /// </summary>
        QuickSort
    }

    /// <summary>
    ///   State of the listview
    /// </summary>
    public enum ListStates
    {
        /// <summary>
        ///   listview is in normal state
        /// </summary>
        stateNone,
        /// <summary>
        ///   an item is selected in listview
        /// </summary>
        stateSelecting,
        /// <summary>
        ///   a column is selected in listview
        /// </summary>
        stateColumnSelect,
        /// <summary>
        ///   a column is being selected in listview
        /// </summary>
        stateColumnResizing
    }

    /// <summary>
    ///   Region reference
    /// </summary>
    public enum GLListRegion
    {
        /// <summary>
        ///   Header Area (Column Header)
        /// </summary>
        header = 0,
        /// <summary>
        ///   Client Area (Items)
        /// </summary>
        client = 1,
        /// <summary>
        ///   Non Client Area (Potentitally not on surface)
        /// </summary>
        nonclient = 2
    }

    /// <summary>
    ///   Style of grid lines in client area
    /// </summary>
    public enum GLGridLineStyles
    {
        /// <summary>
        ///   Do not show a grid at all///
        /// </summary>
        gridNone = 0,
        /// <summary>
        ///   Dashed line grid
        /// </summary>
        gridDashed = 1,
        /// <summary>
        ///   Solid grid line
        /// </summary>
        gridSolid = 2
    }


    /// <summary>
    ///   Grid Line direction
    /// </summary>
    public enum GLGridLines
    {
        /// <summary>
        ///   Horizontal and Vertical lines
        /// </summary>
        gridBoth = 0,
        /// <summary>
        ///   Vertical Grid
        /// </summary>
        gridVertical = 1,
        /// <summary>
        ///   Horizontal Grid
        /// </summary>
        gridHorizontal = 2
    }

    /// <summary>
    ///   Grid type
    /// </summary>
    /// <remarks>
    ///   Normal grid shows lines regardless if items exist or not.  If you choose OnExists the lines will
    ///   only show if there are items present.
    /// </remarks>
    public enum GLGridTypes
    {
        /// <summary>
        ///   Normal lines always present
        /// </summary>
        gridNormal = 0,
        /// <summary>
        ///   Horizontal Lines only present when items exist
        /// </summary>
        gridOnExists = 1
    }

    /// <summary>
    ///   Column Header Styles
    /// </summary>
    public enum GLControlStyles
    {
        /// <summary>
        ///   Common Style
        /// </summary>
        Normal = 0,
        /// <summary>
        ///   Flat look (much like an HTML list header)
        /// </summary>
        SuperFlat = 1,
        /// <summary>
        ///   Windows XP look header
        /// </summary>
        XP = 2
    }


    /// <summary>
    ///   Activated Embedding Types
    /// </summary>
    public enum GLActivatedEmbeddedTypes
    {
        /// <summary>
        ///   Do not use an activated embedded type for this
        /// </summary>
        None,
        /// <summary>
        ///   User fills in Embedded type
        /// </summary>
        UserType,
        /// <summary>
        ///   Text Box.  Used mostly for editable cells.
        /// </summary>
        TextBox,
        /// <summary>
        ///   Combo Box.
        /// </summary>
        ComboBox,
        /// <summary>
        ///   Date Picker
        /// </summary>
        DateTimePicker
    }

    #endregion

    /// <summary>
    ///   Summary description for GlacialList.
    /// </summary>
    public class CListView : Control
    {
        #region Header

        #region Events and Delegates

        #region ListView Events

        /// <summary>
        ///   Click happened inside control.  Use ClickEventArgs to find out origination area.
        /// </summary>
        public event ClickedEventHandler SelectedIndexChanged;

        #endregion

        #region Clicked Events

        /// <summary>
        ///   Clicked Event Handler delegate definition
        /// </summary>
        public delegate void ClickedEventHandler(object source, ClickEventArgs e);

        //int nItem, int nSubItem );
        /// <summary>
        ///   Click happened inside control.  Use ClickEventArgs to find out origination area.
        /// </summary>
        public event ClickedEventHandler ColumnClickedEvent;

        #endregion

        #region Changed Events

        /// <summary>
        ///   Item Changed Event
        /// </summary>
        public event ChangedEventHandler ItemChangedEvent;

        /// <summary>
        ///   Column Changed Event
        /// </summary>
        public event ChangedEventHandler ColumnChangedEvent;

        #endregion

        #region Hover Events

        /// <summary>
        ///   Hover Event delegate definition
        /// </summary>
        public delegate void HoverEventDelegate(object source, HoverEventArgs e);

        /// <summary>
        ///   A hover event has occured.
        /// </summary>
        /// <remarks>
        ///   Use HoverType member of HoverEventArgs to find out if this is a hover origination
        ///   or termination event.
        /// </remarks>
        public event HoverEventDelegate HoverEvent;

        #endregion

        #endregion

        #region VarsDefsProps

        #region Definitions

        private enum WIN32Codes
        {
            WM_GETDLGCODE = 0x0087,
            WM_SETREDRAW = 0x000B,
            WM_CANCELMODE = 0x001F,
            WM_NOTIFY = 0x4e,
            WM_KEYDOWN = 0x100,
            WM_KEYUP = 0x101,
            WM_CHAR = 0x0102,
            WM_SYSKEYDOWN = 0x104,
            WM_SYSKEYUP = 0x105,
            WM_COMMAND = 0x111,
            WM_MENUCHAR = 0x120,
            WM_MOUSEMOVE = 0x200,
            WM_LBUTTONDOWN = 0x201,
            WM_MOUSELAST = 0x20a,
            WM_USER = 0x0400,
            WM_REFLECT = WM_USER + 0x1c00
        }

        private enum DialogCodes
        {
            DLGC_WANTARROWS = 0x0001,
            DLGC_WANTTAB = 0x0002,
            DLGC_WANTALLKEYS = 0x0004,
            DLGC_WANTMESSAGE = 0x0004,
            DLGC_HASSETSEL = 0x0008,
            DLGC_DEFPUSHBUTTON = 0x0010,
            DLGC_UNDEFPUSHBUTTON = 0x0020,
            DLGC_RADIOBUTTON = 0x0040,
            DLGC_WANTCHARS = 0x0080,
            DLGC_STATIC = 0x0100,
            DLGC_BUTTON = 0x2000,
        }

        private const int WM_KEYDOWN = 0x0100;
        private const int VK_LEFT = 0x0025;
        private const int VK_UP = 0x0026;
        private const int VK_RIGHT = 0x0027;
        private const int VK_DOWN = 0x0028;


        private const int CHECKBOX_SIZE = 13;


        private const int RESIZE_ARROW_PADDING = 3;
        private const int MINIMUM_COLUMN_SIZE = 0;

        #endregion

        #region Class Variables

        private int m_nLastSelectionIndex;
        private int m_nLastSubSelectionIndex;

        private ListStates m_nState = ListStates.stateNone;
        private Point m_pointColumnResizeAnchor;
        private int m_nResizeColumnNumber; // the column number thats being resized

        private ArrayList LiveControls = new ArrayList();
        // list of controls currently visible.  THIS IS AN OPTIMIZATION.  This will keep us from having to iterate the entire list beforehand.

        private ArrayList NewLiveControls = new ArrayList();
        private IContainer components;

        private ManagedVScrollBar vPanelScrollBar;
        private ManagedHScrollBar hPanelScrollBar;


        private BorderStrip vertLeftBorderStrip;
        private BorderStrip vertRightBorderStrip;
        private BorderStrip horiBottomBorderStrip;
        private BorderStrip horiTopBorderStrip;
        private BorderStrip cornerBox;


        private Control m_ActivatedEmbeddedControl;

        #endregion

        #region Control Properties

        private GLColumnCollection m_Columns;
        private GLItemCollection m_Items;


        // border
        private bool m_bShowBorder = true;

        private GLGridLineStyles m_GridLineStyle = GLGridLineStyles.gridSolid;
        private GLGridLines m_GridLines = GLGridLines.gridBoth;
        private GLGridTypes m_GridType = GLGridTypes.gridOnExists;

        private int m_nItemHeight = 18;
        private int m_nHeaderHeight = 22;
        //private int							m_nBorderWidth = 2;
        private Color m_colorGridColor = Color.LightGray;
        private Color m_colorSelectionColor = Color.DarkBlue;
        private bool m_bHeaderVisible = true;

        private Color m_SelectedTextColor = Color.White;

        private int m_nMaxHeight;
        private bool m_bAutoHeight = true;
        private bool m_bAllowColumnResize = true;
        private bool m_bFullRowSelect = true;
        private SortTypes m_SortType = SortTypes.InsertionSort;

        private CListItem m_FocusedItem;

        private bool m_bHotColumnTracking;
        private bool m_bHotItemTracking;
        private int m_nHotColumnIndex = -1; // internal hot column
        private int m_nHotItemIndex = -1; // internal hot item index
        private Color m_HotTrackingColor = Color.LightGray; // brush color to use

        private bool m_bUpdating;


        private bool m_bAlternatingColors;
        private Color m_colorAlternateBackground = Color.DarkGreen;
        private Color m_colorSuperFlatHeaderColor = Color.White;

        //private GLControlStyles					m_ControlStyle = GLControlStyles.Normal;
        private GLControlStyles m_ControlStyle = GLControlStyles.Normal;

        private bool m_bItemWordWrap;
        private bool m_bHeaderWordWrap;

        private bool m_bSelectable = true;
        //private int								m_nRowBorderSize = 0;


        private bool m_bHoverEvents;
        private int m_nHoverTime = 1;
        private Point m_ptLastHoverSpot = new Point(0, 0);
        private bool m_bHoverLive; // if a hover event has been sent out (needs to be cancelled later)
        private Timer m_Timer;


        private bool m_bBackgroundStretchToFit = true;


        private ImageList imageList1;

        #region Hover

        /// <summary>
        ///   Items HoverEvents.
        /// </summary>
        [
            Description(
                "Enabling hover events slows the control some but allows you to be informed when a user has hovered over an item."
                ),
            Category("Behavior"),
            Browsable(true)
        ]
        public bool HoverEvents
        {
            get { return m_bHoverEvents; }
            set
            {
                m_bHoverEvents = value;

                if (!DesignMode)
                {
                    if (m_bHoverEvents)
                    {
                        // turn the events off, so we need to add the events
                        m_Timer = new Timer();
                        m_Timer.Interval = m_nHoverTime * 1000; // convert to seconds
                        m_Timer.Tick += m_TimerTick;
                        m_Timer.Start();
                    }
                    else if (m_Timer != null)
                    {
                        // turn the events off
                        m_Timer.Stop();
                        m_Timer = null;
                    }
                }
            }
        }


        ///<summary>
        ///</summary>
        [
            Description("Amount of time in seconds a user hovers before hover event is fired.  Can NOT be zero."),
            Category("Behavior"),
            Browsable(true)
        ]
        public int HoverTime
        {
            get { return m_nHoverTime; }
            set
            {
                if (m_nHoverTime < 1)
                    m_nHoverTime = 1;
                else
                    m_nHoverTime = value;
            }
        }

        #endregion

        /// <summary>
        ///   Items ActivatedEmbeddedControl.
        /// </summary>
        [
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
            Browsable(false)
        ]
        public Control ActivatedEmbeddedControl
        {
            get { return m_ActivatedEmbeddedControl; }
            set { m_ActivatedEmbeddedControl = value; }
        }


        /// <summary>
        ///   Items BackgroundStretchToFit.
        /// </summary>
        [
            RefreshProperties(RefreshProperties.Repaint),
            Description("Whether or not to stretch background to fit inner list area."),
            Category("Behavior"),
            Browsable(true)
        ]
        public bool BackgroundStretchToFit
        {
            get { return m_bBackgroundStretchToFit; }
            set { m_bBackgroundStretchToFit = value; }
        }


        /// <summary>
        ///   Items selectable.
        /// </summary>
        [
            RefreshProperties(RefreshProperties.Repaint),
            Description("Items selectable."),
            Category("Behavior"),
            Browsable(true)
        ]
        public bool Selectable
        {
            get { return m_bSelectable; }
            set { m_bSelectable = value; }
        }


        /// <summary>
        ///   Word wrap in header
        /// </summary>
        [
            RefreshProperties(RefreshProperties.Repaint),
            Description("Word wrap in header"),
            Category("Header"),
            Browsable(true)
        ]
        public bool HeaderWordWrap
        {
            get { return m_bHeaderWordWrap; }
            set
            {
                m_bHeaderWordWrap = value;

                if ((DesignMode) && (Parent != null))
                    Parent.Invalidate(true);
            }
        }


        /// <summary>
        ///   Word wrap in cells
        /// </summary>
        [
            RefreshProperties(RefreshProperties.Repaint),
            Description("Word wrap in cells"),
            Category("Item"),
            Browsable(true)
        ]
        public bool ItemWordWrap
        {
            get { return m_bItemWordWrap; }
            set
            {
                m_bItemWordWrap = value;

                if ((DesignMode) && (Parent != null))
                    Parent.Invalidate(true);
            }
        }


        /// <summary>
        ///   background color to use if flat
        /// </summary>
        [
            RefreshProperties(RefreshProperties.Repaint),
            Description("Color for text in boxes that are selected."),
            Category("Header"),
            Browsable(true)
        ]
        public Color SuperFlatHeaderColor
        {
            get { return m_colorSuperFlatHeaderColor; }
            set
            {
                m_colorSuperFlatHeaderColor = value;

                if ((DesignMode) && (Parent != null))
                    Parent.Invalidate(true);
            }
        }


        /// <summary>
        ///   Overall look of control
        /// </summary>
        [
            RefreshProperties(RefreshProperties.Repaint),
            Description("Overall look of control"),
            Category("Behavior"),
            Browsable(true)
        ]
        public GLControlStyles ControlStyle
        {
            get { return m_ControlStyle; }
            set
            {
                m_ControlStyle = value;

                if ((DesignMode) && (Parent != null))
                {
                    //DI("Calling Invalidate from ControlStyle Property");
                    Parent.Invalidate(true);
                }
            }
        }


        /// <summary>
        ///   Alternating Colors on or off
        /// </summary>
        [
            RefreshProperties(RefreshProperties.Repaint),
            Description("turn xp themes on or not"),
            Category("Item Alternating Colors"),
            Browsable(true)
        ]
        public bool AlternatingColors
        {
            get { return m_bAlternatingColors; }
            set
            {
                m_bAlternatingColors = value;

                if ((DesignMode) && (Parent != null))
                    Parent.Invalidate(true);
            }
        }


        /// <summary>
        ///   second background color if we use alternating colors
        /// </summary>
        [
            RefreshProperties(RefreshProperties.Repaint),
            Description("Color for text in boxes that are selected."),
            Category("Item Alternating Colors"),
            Browsable(true)
        ]
        public Color AlternateBackground
        {
            get { return m_colorAlternateBackground; }
            set
            {
                m_colorAlternateBackground = value;

                if ((DesignMode) && (Parent != null))
                    Parent.Invalidate(true);
            }
        }


        /// <summary>
        ///   Whether or not to show a border.
        /// </summary>
        [
            Description("Whether or not to show a border."),
            Category("Appearance"),
            Browsable(true),
        ]
        public bool ShowBorder
        {
            get { return m_bShowBorder; }
            set
            {
                m_bShowBorder = value;

                if ((DesignMode) && (Parent != null))
                    Parent.Invalidate(true);
            }
        }


        /// <summary>
        ///   Color for text in boxes that are selected
        /// </summary>
        [
            RefreshProperties(RefreshProperties.Repaint),
            Description("Color for text in boxes that are selected."),
            Category("Item"),
            Browsable(true)
        ]
        public Color SelectedTextColor
        {
            get { return m_SelectedTextColor; }
            set { m_SelectedTextColor = value; }
        }


        /// <summary>
        ///   hot tracking
        /// </summary>
        [
            Description("Color for hot tracking."),
            Category("Appearance"),
            Browsable(true)
        ]
        public Color HotTrackingColor
        {
            get { return m_HotTrackingColor; }
            set { m_HotTrackingColor = value; }
        }


        /// <summary>
        ///   Hot Tracking of columns and items
        /// </summary>
        [
            Description("Show hot tracking."),
            Category("Behavior"),
            Browsable(true)
        ]
        public bool HotItemTracking
        {
            get { return m_bHotItemTracking; }
            set { m_bHotItemTracking = value; }
        }

        /// <summary>
        ///   Hot Tracking of columns and items
        /// </summary>
        [
            Description("Show hot tracking."),
            Category("Behavior"),
            Browsable(true)
        ]
        public bool HotColumnTracking
        {
            get { return m_bHotColumnTracking; }
            set { m_bHotColumnTracking = value; }
        }


        /// <summary>
        ///   Show the focus rect or not
        /// </summary>
        [Description("Show Focus Rect on items."), Category("Item"), Browsable(true)]
        public bool ShowFocusRect { get; set; }


        /// <summary>
        ///   auto sorting
        /// </summary>
        [
            RefreshProperties(RefreshProperties.Repaint),
            Description("Type of sorting algorithm used."),
            Category("Behavior"),
            Browsable(true),
        ]
        public SortTypes SortType
        {
            get { return m_SortType; }
            set { m_SortType = value; }
        }


        /// <summary>
        /// </summary>
        [RefreshProperties(RefreshProperties.Repaint), Description("ImageList to be used in listview."),
         Category("Behavior"), Browsable(true),]
        public ImageList ImageList { get; set; }


        /// <summary>
        ///   Allow columns to be resized
        /// </summary>
        [
            RefreshProperties(RefreshProperties.Repaint),
            Description("Allow resizing of columns"),
            Category("Header"),
            Browsable(true)
        ]
        public bool AllowColumnResize
        {
            get { return m_bAllowColumnResize; }
            set { m_bAllowColumnResize = value; }
        }


        /// <summary>
        ///   Control resizes height of row based on size.
        /// </summary>
        [
            RefreshProperties(RefreshProperties.Repaint),
            Description("Do we want rows to automatically adjust height"),
            Category("Item"),
            Browsable(true)
        ]
        public bool AutoHeight
        {
            get { return m_bAutoHeight; }
            set
            {
                m_bAutoHeight = value;
                if ((DesignMode) && (Parent != null))
                    Parent.Invalidate(true);
            }
        }


        /// <summary>
        ///   you want the header to be visible or not
        /// </summary>
        [
            RefreshProperties(RefreshProperties.Repaint),
            Description("Column Headers Visible"),
            Category("Header"),
            Browsable(true)
        ]
        public bool HeaderVisible
        {
            get { return m_bHeaderVisible; }
            set
            {
                m_bHeaderVisible = value;
                if ((DesignMode) && (Parent != null))
                    Parent.Invalidate(true);
            }
        }


        /// <summary>
        ///   Collection of columns
        /// </summary>
        [
            Category("Header"),
            Description("Column Collection"),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
            Editor(typeof(CustomCollectionEditor), typeof(UITypeEditor)),
            Browsable(true)
        ]
        public GLColumnCollection Columns
        {
            get { return m_Columns; }
        }


        /// <summary>
        ///   Collection of items
        /// </summary>
        [
            Category("Item"),
            Description("Items collection"),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
            //Editor(typeof(ItemCollectionEditor), typeof(UITypeEditor)),
            Editor(typeof(CollectionEditor), typeof(UITypeEditor)),
            Browsable(true)
        ]
        public GLItemCollection Items
        {
            get { return m_Items; }
        }


        /// <summary>
        ///   selection bar color
        /// </summary>
        [
            RefreshProperties(RefreshProperties.Repaint),
            Description("Background color to mark selection."),
            Category("Item"),
            Browsable(true),
        ]
        public Color SelectionColor
        {
            get { return m_colorSelectionColor; }
            set { m_colorSelectionColor = value; }
        }


        /// <summary>
        ///   Selection Full Row
        /// </summary>
        [
            RefreshProperties(RefreshProperties.Repaint),
            Description("Allow full row select."),
            Category("Item"),
            Browsable(true)
        ]
        public bool FullRowSelect
        {
            get { return m_bFullRowSelect; }
            set { m_bFullRowSelect = value; }
        }


        /// <summary>
        ///   Allow multiple row selection
        /// </summary>
        [RefreshProperties(RefreshProperties.Repaint), Description("Allow multiple selections."), Category("Item"),
         Browsable(true)]
        public bool AllowMultiselect { get; set; }


        /// <summary>
        ///   Internal border padding
        /// </summary>
        [
            RefreshProperties(RefreshProperties.Repaint),
            Description("Border Padding"),
            Category("Appearance"),
            Browsable(false),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)
        ]
        private int BorderPadding
        {
            get
            {
                if (ShowBorder)
                    return 2;
                else
                    return 0;
            }
            //set	{ m_nBorderWidth = value; }
        }


        /// <summary>
        ///   Grid Line Styles
        /// </summary>
        [
            RefreshProperties(RefreshProperties.Repaint),
            Description("Whether or not to draw gridlines"),
            Category("Grid"),
            Browsable(true)
        ]
        public GLGridLineStyles GridLineStyle
        {
            get { return m_GridLineStyle; }
            set
            {
                m_GridLineStyle = value;

                if ((DesignMode) && (Parent != null))
                {
                    //Invalidate();
                    Parent.Invalidate(true);
                }
            }
        }

        /// <summary>
        ///   What type of grid you want to draw
        /// </summary>
        [
            RefreshProperties(RefreshProperties.Repaint),
            Description("Whether or not to draw gridlines"),
            Category("Grid"),
            Browsable(true)
        ]
        public GLGridTypes GridTypes
        {
            get { return m_GridType; }
            set
            {
                m_GridType = value;

                if ((DesignMode) && (Parent != null))
                {
                    //DI("Calling Invalidate From GLGridTypes");
                    Parent.Invalidate(true);
                }
            }
        }


        /// <summary>
        ///   Grid Lines Type
        /// </summary>
        [
            RefreshProperties(RefreshProperties.Repaint),
            Description("Whether or not to draw gridlines"),
            Category("Grid"),
            Browsable(true)
        ]
        public GLGridLines GridLines
        {
            get { return m_GridLines; }
            set
            {
                m_GridLines = value;

                if ((DesignMode) && (Parent != null))
                {
                    //DI("Calling Invalidate From GLGridLines");
                    Parent.Invalidate(true);
                }
            }
        }


        /// <summary>
        ///   Color of grid lines.
        /// </summary>
        [
            RefreshProperties(RefreshProperties.Repaint),
            Description("Color of the grid if we draw it."),
            Category("Grid"),
            Browsable(true)
        ]
        public Color GridColor
        {
            get { return m_colorGridColor; }
            set
            {
                m_colorGridColor = value;

                if ((DesignMode) && (Parent != null))
                {
                    //DI("Calling Invalidate From GridColor");
                    Parent.Invalidate(true);
                }
            }
        }


        /// <summary>
        ///   how big do we want the individual items to be
        /// </summary>
        [
            RefreshProperties(RefreshProperties.Repaint),
            Description("How high each row is."),
            Category("Item"),
            Browsable(true)
        ]
        public int ItemHeight
        {
            get { return m_nItemHeight; }
            set
            {
                //Debug.WriteLine( "Setting item height to " + value.ToString() );

                //if ( value == 15 )
                //Debug.WriteLine( "stop" );

                m_nItemHeight = value;
                if ((DesignMode) && (Parent != null))
                {
                    //DI("Calling Invalidate From ItemHeight");
                    Parent.Invalidate(true);
                }
            }
        }


        /// <summary>
        ///   Force header height.
        /// </summary>
        [
            RefreshProperties(RefreshProperties.Repaint),
            Description("How high the columns are."),
            Category("Header"),
            Browsable(true)
        ]
        public int HeaderHeight
        {
            get
            {
                if (HeaderVisible)
                    return m_nHeaderHeight;
                else
                    return 0;
            }
            set
            {
                m_nHeaderHeight = value;
                if ((DesignMode) && (Parent != null))
                {
                    //DI("Calling Invalidate From HeaderHeight");
                    Parent.Invalidate(true);
                }
            }
        }


        /// <summary>
        ///   amount of space inside any given cell to borders
        /// </summary>
        [
            Description("Cell padding area"),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
            Browsable(false)
        ]
        public int CellPaddingSize
        {
            get { return 2; } // default I set to 4
        }

        #endregion

        #region Working Properties

        //private int								m_nSortIndex = 0;
        private bool m_bThemesAvailable;

        private IntPtr m_hTheme = IntPtr.Zero;


        /// <summary>
        ///   Are themes available for this control?
        /// </summary>
        [
            Description("Are Themes Available"),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
            Browsable(false)
        ]
        protected bool ThemesAvailable
        {
            get { return m_bThemesAvailable; }
        }


        /// <summary>
        ///   returns a list of only the selected items
        /// </summary>
        [
            Description("Selected Items Array"),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
            Browsable(false)
        ]
        public ArrayList SelectedItems
        {
            get { return Items.SelectedItems; }
        }


        /// <summary>
        ///   returns a list of only the selected items indexes
        /// </summary>
        [
            Description("Selected Items Array Of Indicies"),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
            Browsable(false)
        ]
        public ArrayList SelectedIndicies
        {
            get { return Items.SelectedIndicies; }
        }


        /// <summary>
        ///   currently Hot Column
        /// </summary>
        [
            Description("Currently Focused Column"),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
            Browsable(false)
        ]
        public int HotColumnIndex
        {
            get { return m_nHotColumnIndex; }
            set
            {
                if (m_bHotColumnTracking)
                    if (m_nHotColumnIndex != value)
                    {
                        m_nHotItemIndex = -1;
                        m_nHotColumnIndex = value;

                        if (!DesignMode)
                        {
                            Invalidate(true);
                        }
                    }
            }
        }


        /// <summary>
        ///   Current Hot Item
        /// </summary>
        [
            Description("Currently Focused Item"),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
            Browsable(false)
        ]
        public int HotItemIndex
        {
            get { return m_nHotItemIndex; }
            set
            {
                if (m_bHotItemTracking)
                    if (m_nHotItemIndex != value)
                    {
                        m_nHotColumnIndex = -1;
                        m_nHotItemIndex = value;
                        Invalidate(true);
                    }
            }
        }


        /// <summary>
        ///   Currently focused item
        /// </summary>
        [
            Description("Currently Focused Item"),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
            Browsable(false)
        ]
        public CListItem FocusedItem
        {
            get
            {
                // need to make sure focused item actually exists
                if (m_FocusedItem != null && Items.FindItemIndex(m_FocusedItem) < 0)
                    m_FocusedItem = null; // even though there is a focused item, it doesn't actually exist anymore

                return m_FocusedItem;
            }
            set
            {
                if (m_FocusedItem != value)
                {
                    m_FocusedItem = value;
                    if (!DesignMode)
                    {
                        Invalidate(true);
                    }

                    if (SelectedIndexChanged != null)
                        SelectedIndexChanged(this, new ClickEventArgs(Items.FindItemIndex(value), -1));
                    // never a column sent with selection index change
                }
            }
        }


        /// <summary>
        ///   Current count of items in collection.
        /// </summary>
        [
            Description("Number of items/rows in the list."),
            Category("Behavior"),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
            Browsable(false),
            DefaultValue(0)
        ]
        public int Count
        {
            get { return Items.Count; }
        }


        /// <summary>
        ///   Calculates total height of all rows combined.
        /// </summary>
        [
            Description("All items together height."),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
            Browsable(false)
        ]
        protected int TotalRowHeight
        {
            get { return ItemHeight * Items.Count; }
        }


        /// <summary>
        ///   Number of rows currently visible
        /// </summary>
        [
            Description("Number of rows currently visible in inner rect."),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
            Browsable(false)
        ]
        protected int VisibleRowsCount
        {
            get { return RowsInnerClientRect.Height / ItemHeight; }
        }


        /// <summary>
        ///   Max Height of any given row at any given time.  Used with AutoHeight exclusively.
        /// </summary>
        [
            Description("this will always reflect the most height any item line has needed"),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
            Browsable(false)
        ]
        protected int MaxHeight
        {
            get { return m_nMaxHeight; }
            set
            {
                if (value > m_nMaxHeight)
                {
                    m_nMaxHeight = value;
                    if (AutoHeight)
                    {
                        ItemHeight = MaxHeight;

                        if (!DesignMode)
                        {
                            Invalidate(true);
                        }
                    }
                }
            }
        }


        /// <summary>
        ///   Rect of header area
        /// </summary>
        [
            Description("The rectangle of the header inside parent control"),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
            Browsable(false)
        ]
        protected Rectangle HeaderRect
        {
            get { return new Rectangle(BorderPadding, BorderPadding, Width - (BorderPadding * 2), HeaderHeight); }
        }


        /// <summary>
        ///   Row Client Rectangle
        /// </summary>
        [
            Description("The rectangle of the client inside parent control"),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
            Browsable(false)
        ]
        protected Rectangle RowsClientRect
        {
            get
            {
                int tmpY = HeaderHeight + BorderPadding; // size of the header and the top border

                int tmpHeight = Height - HeaderHeight - (BorderPadding * 2);

                return new Rectangle(BorderPadding, tmpY, Width - (BorderPadding * 2), tmpHeight);
            }
        }


        /// <summary>
        ///   Full Sized rectangle of all columns total width.
        /// </summary>
        [
            Description("Full Sized rectangle of all columns total width."),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
            Browsable(false)
        ]
        public Rectangle RowsRect
        {
            get
            {
                Rectangle rect = new Rectangle();

                rect.X = -hPanelScrollBar.Value + BorderPadding;
                rect.Y = HeaderHeight + BorderPadding;
                rect.Width = Columns.Width;
                rect.Height = VisibleRowsCount * ItemHeight;

                return rect;
            }
        }


        /// <summary>
        ///   The inner rectangle of the client inside parent control taking scroll bars into account.
        /// </summary>
        [
            Description("The inner rectangle of the client inside parent control taking scroll bars into account."),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
            Browsable(false)
        ]
        public Rectangle RowsInnerClientRect
        {
            get
            {
                Rectangle innerRect = RowsClientRect;

                innerRect.Width -= vPanelScrollBar.mWidth; // horizontal bar crosses vertical plane and vice versa
                innerRect.Height -= hPanelScrollBar.mHeight;

                if (innerRect.Width < 0)
                    innerRect.Width = 0;
                if (innerRect.Height < 0)
                    innerRect.Height = 0;

                return innerRect;
            }
        }

        #endregion

        #endregion

        #endregion

        #region Implementation

        #region Initialization

        /// <summary>
        ///   constructor
        /// </summary>
        public CListView()
        {
            m_Columns = new GLColumnCollection(this);
            m_Columns.ChangedEvent += Columns_Changed; // listen to event changes inside the item

            m_Items = new GLItemCollection(this);
            m_Items.ChangedEvent += Items_Changed;

            //components = new System.ComponentModel.Container();
            InitializeComponent();


            Debug.WriteLine(Items.Count.ToString());

            if (!DesignMode)
            {
                if (AreThemesAvailable())
                    m_bThemesAvailable = true;
                else
                    m_bThemesAvailable = false;
            }

            TabStop = true;


            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.Opaque |
                ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer |
                ControlStyles.Selectable |
                ControlStyles.UserMouse,
                true
                );

            BackColor = SystemColors.ControlLightLight;

            hPanelScrollBar = new ManagedHScrollBar();
            vPanelScrollBar = new ManagedVScrollBar();

            hPanelScrollBar.Scroll += OnScroll;
            vPanelScrollBar.Scroll += OnScroll;

            //
            // Creating borders
            //

            //Debug.WriteLine( "Creating borders" );
            vertLeftBorderStrip = new BorderStrip();
            vertRightBorderStrip = new BorderStrip();
            horiBottomBorderStrip = new BorderStrip();
            horiTopBorderStrip = new BorderStrip();
            cornerBox = new BorderStrip();


            SuspendLayout();
            // 
            // hPanelScrollBar
            // 
            hPanelScrollBar.Anchor = AnchorStyles.None;
            hPanelScrollBar.CausesValidation = false;
            hPanelScrollBar.Location = new Point(24, 0);
            hPanelScrollBar.mHeight = 16;
            hPanelScrollBar.mWidth = 120;
            hPanelScrollBar.Name = "hPanelScrollBar";
            hPanelScrollBar.Size = new Size(120, 16);
            hPanelScrollBar.Scroll += hPanelScrollBar_Scroll;
            hPanelScrollBar.Parent = this;
            Controls.Add(hPanelScrollBar);

            // 
            // vPanelScrollBar
            // 
            vPanelScrollBar.Anchor = AnchorStyles.None;
            vPanelScrollBar.CausesValidation = false;
            vPanelScrollBar.Location = new Point(0, 12);
            vPanelScrollBar.mHeight = 120;
            vPanelScrollBar.mWidth = 16;
            vPanelScrollBar.Name = "vPanelScrollBar";
            vPanelScrollBar.Size = new Size(16, 120);
            vPanelScrollBar.Scroll += vPanelScrollBar_Scroll;
            vPanelScrollBar.Parent = this;
            Controls.Add(vPanelScrollBar);


            horiTopBorderStrip.Parent = this;
            horiTopBorderStrip.BorderType = BorderStrip.BorderTypes.btTop;
            horiTopBorderStrip.Visible = true;
            horiTopBorderStrip.BringToFront();


            //this.horiBottomBorderStrip.BackColor=Color.Black;
            horiBottomBorderStrip.Parent = this;
            horiBottomBorderStrip.BorderType = BorderStrip.BorderTypes.btBottom;
            horiBottomBorderStrip.Visible = true;
            horiBottomBorderStrip.BringToFront();

            //this.vertLeftBorderStrip.BackColor=Color.Black;
            vertLeftBorderStrip.BorderType = BorderStrip.BorderTypes.btLeft;
            vertLeftBorderStrip.Parent = this;
            vertLeftBorderStrip.Visible = true;
            vertLeftBorderStrip.BringToFront();

            //this.vertRightBorderStrip.BackColor=Color.Black;
            vertRightBorderStrip.BorderType = BorderStrip.BorderTypes.btRight;
            vertRightBorderStrip.Parent = this;
            vertRightBorderStrip.Visible = true;
            vertRightBorderStrip.BringToFront();

            cornerBox.BackColor = SystemColors.Control;
            cornerBox.BorderType = BorderStrip.BorderTypes.btSquare;
            cornerBox.Visible = false;
            cornerBox.Parent = this;
            cornerBox.BringToFront();

            Name = "GlacialList";

            ResumeLayout(false);
        }

        private void InitializeComponent()
        {
            //if (ControlStyle == GLControlStyles.XP)
            //    Application.EnableVisualStyles();


            components = new Container();
            /**ResourceManager resources = new ResourceManager(typeof(CListView));
            imageList1 = new ImageList(components);
            // 
            // imageList1
            // 
            imageList1.ImageSize = new Size(13, 13);
            imageList1.ImageStream = ((ImageListStreamer)(resources.GetObject("list.ImageStream")));
            imageList1.TransparentColor = Color.Transparent;*/
        }


        /// <summary>
        ///   Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            Debug.WriteLine("Disposing Glacial List.");

            if (m_hTheme != IntPtr.Zero)
                ThemeRoutines.CloseThemeData(m_hTheme);


            if (disposing)
            {
                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion

        #region Activated Embedded Routines

        /// <summary>
        ///   If an activated embedded control exists, remove and unload it
        /// </summary>
        private void DestroyActivatedEmbedded()
        {
            if (m_ActivatedEmbeddedControl != null)
            {
                CEmbeddedControl control = (CEmbeddedControl)m_ActivatedEmbeddedControl;
                control.GLUnload();

                // must do this because the unload may call the changed callback from the items which would call this routine a second time
                if (m_ActivatedEmbeddedControl != null)
                {
                    m_ActivatedEmbeddedControl.Dispose();
                    m_ActivatedEmbeddedControl = null;
                }
            }
        }


        /// <summary>
        ///   Instance the activated embeddec control for this item/column
        /// </summary>
        /// <param name = "nColumn"></param>
        /// <param name = "item"></param>
        /// <param name = "subItem"></param>
        protected void ActivateEmbeddedControl(int nColumn, CListItem item, CListSubItem subItem)
        {
            if (m_ActivatedEmbeddedControl != null)
            {
                m_ActivatedEmbeddedControl.Dispose();
                m_ActivatedEmbeddedControl = null;
            }


            /*
			using activator.createinstance
			typeof()/GetType
			Type t = obj.GetType()
			 */

            if (Columns[nColumn].ActivatedEmbeddedControlTemplate == null)
                return;


            Type type = Columns[nColumn].ActivatedEmbeddedControlTemplate.GetType();
            Control control = (Control)Activator.CreateInstance(type);
            CEmbeddedControl icontrol = (CEmbeddedControl)control;

            if (icontrol == null)
                throw new Exception(@"Control does not implement the CEmbeddedControl interface, can't start");

            icontrol.GLLoad(item, subItem, this);


            //control.LostFocus += new EventHandler( ActivatedEmbbed_LostFocus );
            control.KeyPress += tb_KeyPress;

            control.Parent = this;
            ActivatedEmbeddedControl = control;
            //subItem.Control = control;							// seed the control


            int nYOffset = (subItem.LastCellRect.Height - m_ActivatedEmbeddedControl.Bounds.Height) / 2;
            Rectangle controlBounds;

            if (GridLineStyle == GLGridLineStyles.gridNone)
            {
                // add 1 to x to give border, add 2 to Y because to account for possible grid that you must cover up
                controlBounds = new Rectangle(subItem.LastCellRect.X + 1, subItem.LastCellRect.Y + 1,
                                              subItem.LastCellRect.Width - 3, subItem.LastCellRect.Height - 2);
            }
            else
            {
                // add 1 to x to give border, add 2 to Y because to account for possible grid that you must cover up
                controlBounds = new Rectangle(subItem.LastCellRect.X + 1, subItem.LastCellRect.Y + 2,
                                              subItem.LastCellRect.Width - 3, subItem.LastCellRect.Height - 3);
            }
            //control.Bounds = subItem.LastCellRect;	//new Rectangle( subItem.LastCellRect.X, subItem.LastCellRect.Y + nYOffset, subItem.LastCellRect.Width, subItem.LastCellRect.Height );
            control.Bounds = controlBounds;

            control.Show();
            control.Focus();
        }


        /// <summary>
        ///   check for return (if we get it, deactivate)
        /// </summary>
        /// <param name = "sender"></param>
        /// <param name = "e"></param>
        private void tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Return) || (e.KeyChar == (char)Keys.Escape))
                DestroyActivatedEmbedded();
        }


        //		/// <summary>
        //		/// handle when a control has lost focus
        //		/// </summary>
        //		/// <param name="sender"></param>
        //		/// <param name="e"></param>
        //		private void ActivatedEmbbed_LostFocus(object sender, EventArgs e)
        //		{
        //			Debug.WriteLine( "Embedded control lost focus." );
        //		}

        #endregion

        #region System Overrides

        /// <summary>
        ///   keep certain keys here
        /// </summary>
        /// <param name = "msg"></param>
        /// <returns></returns>
        public override bool PreProcessMessage(ref Message msg)
        {
            if (msg.Msg == WM_KEYDOWN)
            {
                Keys keyCode = ((Keys)(int)msg.WParam);
                // this should turn the key data off because it will match selected keys to ORA them off


                if (keyCode == Keys.Return)
                {
                    DestroyActivatedEmbedded();
                    return true;
                }

                //				Debug.WriteLine("---");
                //				Debug.WriteLine( ModifierKeys.ToString() );
                Debug.WriteLine(keyCode.ToString());

                if ((FocusedItem != null) && (Count > 0) && (Selectable))
                {
                    int nItemIndex = Items.FindItemIndex(FocusedItem);
                    int nPreviousIndex = nItemIndex;

                    if (nItemIndex < 0)
                        return true; // this can't move


                    if ((keyCode == Keys.A) && ((ModifierKeys & Keys.Control) == Keys.Control))
                    {
                        for (int index = 0; index < Items.Count; index++)
                            Items[index].Selected = true;

                        return base.PreProcessMessage(ref msg);
                    }

                    if (keyCode == Keys.Escape)
                    {
                        Items.ClearSelection(); // clear selections
                        FocusedItem = null;

                        return base.PreProcessMessage(ref msg);
                    }

                    if (keyCode == Keys.Down)
                    {
                        // Could be a switch
                        nItemIndex++;
                    }
                    else if (keyCode == Keys.Up)
                    {
                        nItemIndex--;
                    }
                    else if (keyCode == Keys.PageDown)
                    {
                        nItemIndex += VisibleRowsCount;
                    }
                    else if (keyCode == Keys.PageUp)
                    {
                        nItemIndex -= VisibleRowsCount;
                    }
                    else if (keyCode == Keys.Home)
                    {
                        nItemIndex = 0;
                    }
                    else if (keyCode == Keys.End)
                    {
                        nItemIndex = Count - 1;
                    }
                    else if (keyCode == Keys.Space)
                    {
                        if (!AllowMultiselect)
                            Items.ClearSelection(Items[nItemIndex]);

                        Items[nItemIndex].Selected = !Items[nItemIndex].Selected;

                        return base.PreProcessMessage(ref msg);
                    }
                    else
                    {
                        return base.PreProcessMessage(ref msg);
                        // bail out, they only pressed a key we didn't care about (probably a modifier)
                    }

                    // bounds check them
                    if (nItemIndex > Count - 1)
                        nItemIndex = Count - 1;
                    if (nItemIndex < 0)
                        nItemIndex = 0;


                    // move view.  Need to move end -1 to take into account 0 based index
                    if (nItemIndex < vPanelScrollBar.Value) // its out of viewable, move the surface
                        vPanelScrollBar.Value = nItemIndex;
                    if (nItemIndex > (vPanelScrollBar.Value + (VisibleRowsCount - 1)))
                        vPanelScrollBar.Value = nItemIndex - (VisibleRowsCount - 1);


                    if (nPreviousIndex != nItemIndex)
                    {
                        if ((ModifierKeys & Keys.Control) != Keys.Control && (ModifierKeys & Keys.Shift) != Keys.Shift)
                        {
                            // no control no shift
                            m_nLastSelectionIndex = nItemIndex;
                            Items[nItemIndex].Selected = true;
                            Items.ClearSelection(Items[nItemIndex]);
                        }
                        else if ((ModifierKeys & Keys.Shift) == Keys.Shift)
                        {
                            // shift only
                            Items.ClearSelection();

                            // gotta catch when the multi select is NOT set
                            if (!AllowMultiselect)
                            {
                                Items[nItemIndex].Selected = !Items[nItemIndex].Selected;
                            }
                            else
                            {
                                if (m_nLastSelectionIndex >= 0) // ie, non negative so that we have a starting point
                                {
                                    int index = m_nLastSelectionIndex;
                                    do
                                    {
                                        Items[index].Selected = true;
                                        if (index > nItemIndex) index--;
                                        if (index < nItemIndex) index++;
                                    } while (index != nItemIndex);

                                    Items[index].Selected = true;
                                }
                            }
                        }
                        else
                        {
                            // control only
                            m_nLastSelectionIndex = nItemIndex;
                        }

                        // Bypass FocusedItem property, we always want to invalidate from this point
                        FocusedItem = Items[nItemIndex];
                    }
                }
                else
                {
                    // only if non selectable
                    int nMoveIndex = vPanelScrollBar.Value;

                    if (keyCode == Keys.Down)
                    {
                        // Could be a switch
                        nMoveIndex++;
                    }
                    else if (keyCode == Keys.Up)
                    {
                        nMoveIndex--;
                    }
                    else if (keyCode == Keys.PageDown)
                    {
                        nMoveIndex += VisibleRowsCount;
                    }
                    else if (keyCode == Keys.PageUp)
                    {
                        nMoveIndex -= VisibleRowsCount;
                    }
                    else if (keyCode == Keys.Home)
                    {
                        nMoveIndex = 0;
                    }
                    else if (keyCode == Keys.End)
                    {
                        nMoveIndex = Count - VisibleRowsCount;
                    }
                    else
                        return base.PreProcessMessage(ref msg); // we don't know how to deal with this key


                    if (nMoveIndex > (Count - VisibleRowsCount))
                        nMoveIndex = Count - VisibleRowsCount;
                    if (nMoveIndex < 0)
                        nMoveIndex = 0;


                    if (vPanelScrollBar.Value != nMoveIndex)
                    {
                        vPanelScrollBar.Value = nMoveIndex;

                        Invalidate();
                    }
                }
            }
            else
                return base.PreProcessMessage(ref msg); // handle ALL other messages


            return true;
        }

        #endregion

        #region Event Handlers

        /// <summary>
        ///   Timer handler.  This mostly deals with the hover technology with events firing.
        /// </summary>
        /// <param name = "sender"></param>
        /// <param name = "e"></param>
        private void m_TimerTick(object sender, EventArgs e)
        {
            // make sure hover is actually inside control too
            Point pointLocalMouse;
            if (Cursor != null)
                pointLocalMouse = PointToClient(Cursor.Position);
            else
                pointLocalMouse = new Point(9999, 9999);

            int nItem = 0, nColumn = 0, nCellX = 0, nCellY = 0;
            ListStates eState;
            GLListRegion listRegion;
            InterpretCoords(pointLocalMouse.X, pointLocalMouse.Y, out listRegion, out nCellX, out nCellY, out nItem,
                            out nColumn, out eState);


            if ((pointLocalMouse == m_ptLastHoverSpot) && (!m_bHoverLive) && (listRegion != GLListRegion.nonclient))
            {
                Debug.WriteLine("Firing Hover");

                if (HoverEvent != null)
                    HoverEvent(this, new HoverEventArgs(HoverTypes.HoverStart, nItem, nColumn, listRegion));

                m_bHoverLive = true;
            }
            else if ((m_bHoverLive) && (pointLocalMouse != m_ptLastHoverSpot))
            {
                Debug.WriteLine("Cancelling Hover");

                if (HoverEvent != null)
                    HoverEvent(this, new HoverEventArgs(HoverTypes.HoverEnd, -1, -1, GLListRegion.nonclient));

                m_bHoverLive = false;
            }

            m_ptLastHoverSpot = pointLocalMouse;
        }


        /// <summary>
        ///   Item has changed, fire event
        /// </summary>
        /// <param name = "source"></param>
        /// <param name = "e"></param>
        protected void Items_Changed(object source, ChangedEventArgs e)
        {
            //Debug.WriteLine( e.ChangedType.ToString() );
            //if ( e.ChangedType != ChangedTypes.

            // kill activated embedded object
            DestroyActivatedEmbedded();

            if (ItemChangedEvent != null)
                ItemChangedEvent(this, e); // fire the column clicked event

            // only invalidate if an item that is within the visible area has changed
            if (e.Item != null)
            {
                //				int nItemIndex = Items.FindItemIndex( e.Item );
                //				if ( ( nItemIndex >= this.vPanelScrollBar.Value ) && ( nItemIndex <  this.vPanelScrollBar.Value+this.VisibleRowsCount ) )
                if (IsItemVisible(e.Item))
                {
                    Invalidate();
                }
            }
        }


        /// <summary>
        ///   Column has changed, fire event
        /// </summary>
        /// <param name = "source"></param>
        /// <param name = "e"></param>
        public void Columns_Changed(object source, ChangedEventArgs e)
        {
            if (e.ChangedType != ChangedTypes.ColumnStateChanged)
                DestroyActivatedEmbedded(); // kill activated embedded object

            if (ColumnChangedEvent != null)
                ColumnChangedEvent(this, e); // fire the column clicked event

            Invalidate();
        }


        /// <summary>
        ///   When the control receives focus
        /// 
        ///   this routine is the one that makes absolute certain if the embedded control loses focus then 
        ///   the embedded control is destroyed
        /// </summary>
        /// <param name = "e"></param>
        protected override void OnGotFocus(EventArgs e)
        {
            DestroyActivatedEmbedded();

            base.OnGotFocus(e);
        }

        #endregion

        #region HelperFunctions

        /// <summary>
        ///   This is an OPTIMIZED routine to see if an item is visible.
        /// 
        ///   The other method of just checking against the item index was slow becuase it had to walk the entire list, which would massively
        ///   slow down the control when large numbers of items were added.
        /// </summary>
        /// <param name = "item"></param>
        /// <returns></returns>
        public bool IsItemVisible(CListItem item)
        {
            // TODO: change this to only walk to visible items list
            int nItemIndex = Items.FindItemIndex(item);
            if ((nItemIndex >= vPanelScrollBar.Value) && (nItemIndex < vPanelScrollBar.Value + VisibleRowsCount))
                return true;

            return false;
        }

        /// <summary>
        ///   Tell paint to stop worry about updates
        /// </summary>
        public void BeginUpdate()
        {
            m_bUpdating = true;
        }


        /// <summary>
        ///   Tell paint to start worrying about updates again and repaint while your at it
        /// </summary>
        public void EndUpdate()
        {
            m_bUpdating = false;
            Invalidate();
        }


        /// <summary>
        ///   interpret mouse coordinates
        /// 
        ///   ok, I've violated the spirit of this routine a couple times (but no more!).  Do NOT put anything
        ///   functional in this routine.  It is ONLY for analyzing the mouse coordinates.  Do not break this again!
        /// </summary>
        /// <param name = "nScreenX"></param>
        /// <param name = "nScreenY"></param>
        /// <param name = "listRegion"></param>
        /// <param name = "nCellX"></param>
        /// <param name = "nCellY"></param>
        /// <param name = "nItem"></param>
        /// <param name = "nColumn"></param>
        /// <param name = "nState"></param>
        public void InterpretCoords(int nScreenX, int nScreenY, out GLListRegion listRegion, out int nCellX,
                                    out int nCellY, out int nItem, out int nColumn, out ListStates nState)
        {
            nState = ListStates.stateNone;
            nColumn = 0;
            // compiler forces me to set this since it sometimes wont get set if routine falls through early
            nItem = 0;
            nCellX = 0;
            nCellY = 0;

            listRegion = GLListRegion.nonclient;

            /*
			 * Calculate horizontal subitem
			 */
            int nCurrentX = -hPanelScrollBar.Value; // offset the starting point by the current scroll point

            for (nColumn = 0; nColumn < Columns.Count; nColumn++)
            {
                CColumn col = Columns[nColumn];
                // lets find the inner X for the cell
                nCellX = nScreenX - nCurrentX;

                if ((nScreenX > nCurrentX) && (nScreenX < (nCurrentX + col.Width - RESIZE_ARROW_PADDING)))
                {
                    nState = ListStates.stateColumnSelect;

                    break;
                }
                if ((nScreenX >= (nCurrentX + col.Width - RESIZE_ARROW_PADDING)) &&
                    (nScreenX <= (nCurrentX + col.Width + RESIZE_ARROW_PADDING)))
                {
                    // here we need to check see if this is a 0 length column (which we skip to next on) or if this is the last column (which we can't skip)
                    if ((nColumn + 1 == Columns.Count) || (Columns[nColumn + 1].Width != 0))
                    {
                        if (AllowColumnResize)
                            nState = ListStates.stateColumnResizing;

                        //Debug.WriteLine( "Sending our column number " + nColumn.ToString() );
                        return; // no need for this to fall through
                    }
                }

                nCurrentX += col.Width;
            }

            if ((nScreenY >= RowsInnerClientRect.Y) && (nScreenY < RowsInnerClientRect.Bottom))
            {
                // we are in the client area
                listRegion = GLListRegion.client;

                Columns.ClearHotStates();
                HotColumnIndex = -1;

                nItem = ((nScreenY - RowsInnerClientRect.Y) / ItemHeight) + vPanelScrollBar.Value;

                // get inner cell Y
                nCellY = (nScreenY - RowsInnerClientRect.Y) % ItemHeight;


                HotItemIndex = nItem;

                if ((nItem >= Items.Count) || (nItem > (vPanelScrollBar.Value + VisibleRowsCount)))
                {
                    nState = ListStates.stateNone;
                    listRegion = GLListRegion.nonclient;
                }
                else
                {
                    nState = ListStates.stateSelecting;

                    // handle case of where FullRowSelect is OFF and we click on the second part of a spanned column
                    for (int nSubIndex = 0; nSubIndex < Columns.Count; nSubIndex++)
                    {
                        //if ( ( nSubIndex + (Items[nItem].SubItems[nSubIndex].Span-1) ) >= nColumn )
                        if (nSubIndex >= nColumn)
                        {
                            nColumn = nSubIndex;
                            return;
                        }
                    }
                }

                //Debug.WriteLine( "returning client from interpretcoords" );

                return;
            }
            else
            {
                if ((nScreenY >= HeaderRect.Y) && (nScreenY < HeaderRect.Bottom))
                {
                    //Debug.WriteLine( "Found header from interpret coords" );

                    listRegion = GLListRegion.header;

                    HotItemIndex = -1; // we are in the header
                    HotColumnIndex = nColumn;

                    if (((nColumn > -1) && (nColumn < Columns.Count)) && (!Columns.AnyPressed()))
                        if (Columns[nColumn].State == ColumnStates.csNone)
                        {
                            Columns.ClearHotStates();
                            Columns[nColumn].State = ColumnStates.csHot;
                        }
                }
            }
            return;
        }


        /// <summary>
        ///   return the X starting point of a particular column
        /// </summary>
        /// <param name = "nColumn"></param>
        /// <returns></returns>
        public int GetColumnScreenX(int nColumn)
        {
            if (nColumn >= Columns.Count)
                return 0;

            int nCurrentX = -hPanelScrollBar.Value;
            //GetHScrollPoint();			// offset the starting point by the current scroll point
            int nColIndex = 0;
            foreach (CColumn col in Columns)
            {
                if (nColIndex >= nColumn)
                    return nCurrentX;

                nColIndex++;
                nCurrentX += col.Width;
            }

            return 0; // this should never happen;
        }


        /// <summary>
        ///   Sort a column.
        /// 
        ///   Set to virtual so you can write your own sorting
        /// </summary>
        /// <param name = "nColumn"></param>
        public virtual void SortColumn(int nColumn)
        {
            Debug.WriteLine("Column sorting called.");

            if (Count < 2) // nothing to sort
                return;


            if (SortType == SortTypes.InsertionSort)
            {
                CQuickSort sorter = new CQuickSort();

                sorter.NumericCompare = Columns[nColumn].NumericSort;
                sorter.SortDirection = Columns[nColumn].LastSortState;
                sorter.SortColumn = nColumn;
                sorter.GLInsertionSort(Items, 0, Items.Count - 1);
            }
            else if (SortType == SortTypes.MergeSort)
            {
                //this.SortIndex = nColumn;
                CMergeSort mergesort = new CMergeSort();

                mergesort.NumericCompare = Columns[nColumn].NumericSort;
                mergesort.SortDirection = Columns[nColumn].LastSortState;
                mergesort.SortColumn = nColumn;
                mergesort.sort(Items, 0, Items.Count - 1);
            }
            else if (SortType == SortTypes.QuickSort)
            {
                CQuickSort sorter = new CQuickSort();

                sorter.NumericCompare = Columns[nColumn].NumericSort;
                sorter.SortDirection = Columns[nColumn].LastSortState;
                sorter.SortColumn = nColumn;
                sorter.sort(Items); //.QuickSort( Items, 0, Items.Count-1 );
            }


            if (Columns[nColumn].LastSortState == SortDirections.SortDescending)
                Columns[nColumn].LastSortState = SortDirections.SortAscending;
            else
                Columns[nColumn].LastSortState = SortDirections.SortDescending;

            //Items.Sort();
        }


        /// <summary>
        ///   see if themes are available
        /// </summary>
        /// <returns></returns>
        protected bool AreThemesAvailable()
        {
            try
            {
                if ((ThemeRoutines.IsThemeActive() == 1) && (m_hTheme == IntPtr.Zero))
                {
                    m_hTheme = ThemeRoutines.OpenThemeData(m_hTheme, "HEADER");

                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            return false;
        }

        #endregion

        #region Dimensions

        /// <summary>
        ///   Control is resizing, handle invalidations
        /// </summary>
        /// <param name = "e"></param>
        protected override void OnResize(EventArgs e)
        {
            Invalidate();
        }

        #endregion

        #region Drawing

        /// <summary>
        ///   Entry point to paint routines
        /// </summary>
        /// <param name = "e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            if (!DesignMode && (m_bUpdating)) // my best guess on how to implement updating functionality				
                return;

            RecalcScroll(); // at some point I need to move this out of paint.  Doesn't really belong here.


            //Debug.WriteLine( "Redraw called " + DateTime.Now.ToLongTimeString() );
            Graphics g = e.Graphics;


            //if ( Columns.Count > 0 )
            {
                int nInsideWidth;
                if (Columns.Width > HeaderRect.Width)
                    nInsideWidth = Columns.Width;
                else
                    nInsideWidth = HeaderRect.Width;

                /*
				 * draw header
				 */
                if (HeaderVisible)
                {
                    g.SetClip(HeaderRect);
                    DrawHeader(g, new Size(HeaderRect.Width, HeaderRect.Height));
                }


                /*
				 * draw client area
				 */
                g.SetClip(RowsInnerClientRect);
                DrawRows(g);

                // very optimized way of removing controls that aren't visible anymore without having to iterate the entire items list
                foreach (Control control in LiveControls)
                {
                    //Debug.WriteLine( "Setting " + control.ToString() + " to hidden." );
                    control.Visible = false; // make sure the controls that aren't visible aren't shown
                }
                LiveControls = NewLiveControls;
                NewLiveControls = new ArrayList();
            }

            g.SetClip(ClientRectangle);


            base.OnPaint(e);
        }


        /// <summary>
        ///   Draw Header Control
        /// </summary>
        /// <param name = "graphicHeader"></param>
        /// <param name = "sizeHeader"></param>
        public virtual void DrawHeader(Graphics graphicHeader, /*Bitmap bmpHeader,*/ Size sizeHeader)
        {
            if (ControlStyle == GLControlStyles.SuperFlat)
            {
                SolidBrush brush = new SolidBrush(SuperFlatHeaderColor);
                graphicHeader.FillRectangle(brush, HeaderRect);
                brush.Dispose();
            }
            else
            {
                graphicHeader.FillRectangle(SystemBrushes.Control, HeaderRect);
            }


            if (Columns.Count <= 0)
                return;

            // draw vertical lines first, then horizontal lines
            int nCurrentX = (-hPanelScrollBar.Value) + HeaderRect.X;
            foreach (CColumn column in Columns)
            {
                // cull columns that won't be drawn first
                if ((nCurrentX + column.Width) < 0)
                {
                    nCurrentX += column.Width;
                    continue; // skip this column, its not being drawn
                }
                if (nCurrentX > HeaderRect.Right)
                    return; // were past the end of the visible column, stop drawing

                if (column.Width > 0)
                    DrawColumnHeader(graphicHeader, new Rectangle(nCurrentX, HeaderRect.Y, column.Width, HeaderHeight),
                                     column);

                nCurrentX += column.Width; // move the parser
            }
        }


        /// <summary>
        ///   Draw column in header control
        /// </summary>
        /// <param name = "graphicsColumn"></param>
        /// <param name = "rectColumn"></param>
        /// <param name = "column"></param>
        public virtual void DrawColumnHeader(Graphics graphicsColumn, Rectangle rectColumn, CColumn column)
        {
            if (ControlStyle == GLControlStyles.SuperFlat)
            {
                SolidBrush brush = new SolidBrush(SuperFlatHeaderColor);
                graphicsColumn.FillRectangle(brush, rectColumn);
                brush.Dispose();
            }
            else if ((ControlStyle == GLControlStyles.XP) && ThemesAvailable)
            {
                // this is really the only thing we care about for themeing right now inside the control
                IntPtr hDC = graphicsColumn.GetHdc();
                ;

                RECT colrect = new RECT(rectColumn.X, rectColumn.Y, rectColumn.Right, rectColumn.Bottom);
                RECT cliprect = new RECT(rectColumn.X, rectColumn.Y, rectColumn.Right, rectColumn.Bottom);

                if (column.State == ColumnStates.csNone)
                {
                    //Debug.WriteLine( "Normal" );
                    ThemeRoutines.DrawThemeBackground(m_hTheme, hDC, 1, 1, ref colrect, ref cliprect);
                }
                else if (column.State == ColumnStates.csPressed)
                {
                    //Debug.WriteLine( "Pressed" );
                    ThemeRoutines.DrawThemeBackground(m_hTheme, hDC, 1, 3, ref colrect, ref cliprect);
                }
                else if (column.State == ColumnStates.csHot)
                {
                    //Debug.WriteLine( "Hot" );
                    ThemeRoutines.DrawThemeBackground(m_hTheme, hDC, 1, 2, ref colrect, ref cliprect);
                }

                graphicsColumn.ReleaseHdc(hDC);
            }
            else // normal state
            {
                if (column.State != ColumnStates.csPressed)
                    ControlPaint.DrawButton(graphicsColumn, rectColumn, ButtonState.Normal);
                else
                    ControlPaint.DrawButton(graphicsColumn, rectColumn, ButtonState.Pushed);
            }


            // if there is an image, this routine will RETURN with exactly the space left for everything else after the image is drawn (or not drawn due to lack of space)
            if ((column.ImageIndex > -1) && (ImageList != null) && (column.ImageIndex < ImageList.Images.Count))
                rectColumn = DrawCellGraphic(graphicsColumn, rectColumn, ImageList.Images[column.ImageIndex],
                                             HorizontalAlignment.Left);

            DrawCellText(graphicsColumn, rectColumn, column.Text, column.TextAlignment, ForeColor, false, HeaderWordWrap);
        }


        /// <summary>
        ///   Draw client rows of list control
        /// </summary>
        /// <param name = "graphicsRows"></param>
        public virtual void DrawRows(Graphics graphicsRows)
        {
            SolidBrush brush = new SolidBrush(BackColor);
            graphicsRows.FillRectangle(brush, RowsClientRect);
            brush.Dispose();

            // if they have a background image, then display it
            if (BackgroundImage != null)
            {
                if (BackgroundStretchToFit)
                    graphicsRows.DrawImage(BackgroundImage, RowsInnerClientRect.X, RowsInnerClientRect.Y,
                                           RowsInnerClientRect.Width, RowsInnerClientRect.Height);
                else
                    graphicsRows.DrawImage(BackgroundImage, RowsInnerClientRect.X, RowsInnerClientRect.Y);
            }


            // determine start item based on whether or not we have a vertical scrollbar present
            int nStartItem; // which item to start with in this visible pane
            if (vPanelScrollBar.Visible)
                nStartItem = vPanelScrollBar.Value;
            else
                nStartItem = 0;


            Rectangle rectRow = RowsRect;
            rectRow.Height = ItemHeight;

            /* Draw Rows */
            for (int nItem = 0; ((nItem < (VisibleRowsCount + 1)) && ((nItem + nStartItem) < Items.Count)); nItem++)
            {
                DrawRow(graphicsRows, rectRow, Items[nItem + nStartItem], nItem + nStartItem);
                rectRow.Y += ItemHeight;
            }


            if (GridLineStyle != GLGridLineStyles.gridNone)
                DrawGridLines(graphicsRows, RowsInnerClientRect);


            // draw hot tracking column overlay
            if (HotColumnTracking && (HotColumnIndex != -1) && (HotColumnIndex < Columns.Count))
            {
                int nXCursor = -hPanelScrollBar.Value;
                for (int nColumnIndex = 0; nColumnIndex < HotColumnIndex; nColumnIndex++)
                    nXCursor += Columns[nColumnIndex].Width;

                Brush hotBrush =
                    new SolidBrush(Color.FromArgb(75, HotTrackingColor.R, HotTrackingColor.G, HotTrackingColor.B));
                graphicsRows.FillRectangle(hotBrush, nXCursor, RowsInnerClientRect.Y, Columns[HotColumnIndex].Width + 1,
                                           RowsInnerClientRect.Height - 1);

                hotBrush.Dispose();
            }
        }


        /// <summary>
        ///   Draw row at specified coordinates
        /// </summary>
        /// <param name = "graphicsRow"></param>
        /// <param name = "rectRow"></param>
        /// <param name = "item"></param>
        /// <param name = "nItemIndex"></param>
        public virtual void DrawRow(Graphics graphicsRow, Rectangle rectRow, CListItem item, int nItemIndex)
        {
            // row background, if its selected, that trumps all, if not then see if we are using alternating colors, if not draw normal
            // note, this can all be overridden by the sub item background property
            // make sure anything can even be selected before drawing selection rects
            if (item.Selected && Selectable)
            {
                SolidBrush brushBK;
                brushBK = new SolidBrush(Color.FromArgb(255, SelectionColor.R, SelectionColor.G, SelectionColor.B));

                // need to check for full row select here
                if (!FullRowSelect)
                {
                    // calculate how far into the control it goes
                    int nWidthFR = -hPanelScrollBar.Value + Columns.Width;
                    graphicsRow.FillRectangle(brushBK, RowsInnerClientRect.X, rectRow.Y, nWidthFR, rectRow.Height);
                }
                else
                    graphicsRow.FillRectangle(brushBK, RowsInnerClientRect.X, rectRow.Y, RowsInnerClientRect.Width,
                                              rectRow.Height);

                brushBK.Dispose();
            }
            else
            {
                // if the back color of the list doesn't match the back color of the item (AND) the back color isn't white, then override it
                if ((item.BackColor.ToArgb() != BackColor.ToArgb()) && (item.BackColor != Color.White))
                {
                    SolidBrush brushBK = new SolidBrush(item.BackColor);
                    graphicsRow.FillRectangle(brushBK, RowsInnerClientRect.X, rectRow.Y, RowsInnerClientRect.Width,
                                              rectRow.Height);
                    brushBK.Dispose();
                } // check for full row alternate color
                else if (AlternatingColors)
                {
                    // alternating colors are only shown if the row isn't selected
                    int nACItemIndex = Items.FindItemIndex(item);
                    if ((nACItemIndex % 2) > 0)
                    {
                        SolidBrush brushBK = new SolidBrush(AlternateBackground);

                        if (!FullRowSelect)
                        {
                            // calculate how far into the control it goes
                            int nWidthFR = -hPanelScrollBar.Value + Columns.Width;
                            graphicsRow.FillRectangle(brushBK, RowsInnerClientRect.X, rectRow.Y, nWidthFR,
                                                      rectRow.Height);
                        }
                        else
                            graphicsRow.FillRectangle(brushBK, RowsInnerClientRect.X, rectRow.Y,
                                                      RowsInnerClientRect.Width, rectRow.Height);

                        brushBK.Dispose();
                    }
                }
            }


            // draw the row of sub items
            int nXCursor = -hPanelScrollBar.Value + BorderPadding;
            for (int nSubItem = 0; nSubItem < Columns.Count; nSubItem++)
            {
                Rectangle rectSubItem = new Rectangle(nXCursor, rectRow.Y, Columns[nSubItem].Width, rectRow.Height);

                // avoid drawing items that are not in the visible region
                if ((rectSubItem.Right < 0) || (rectSubItem.Left > RowsInnerClientRect.Right))
                    Debug.Write("");
                else
                    DrawSubItem(graphicsRow, rectSubItem, item, item.SubItems[nSubItem], nSubItem);

                nXCursor += Columns[nSubItem].Width;
            }


            // post draw for focus rect and hot tracking
            if ((nItemIndex == HotItemIndex) && HotItemTracking) // handle hot tracking of items
            {
                Color transparentColor = Color.FromArgb(75, HotTrackingColor.R, HotTrackingColor.G, HotTrackingColor.B);
                // 182, 189, 210 );
                Brush hotBrush = new SolidBrush(transparentColor);

                graphicsRow.FillRectangle(hotBrush, RowsInnerClientRect.X, rectRow.Y, RowsInnerClientRect.Width,
                                          rectRow.Height);

                hotBrush.Dispose();
            }


            // draw row borders
            if (item.RowBorderSize > 0)
            {
                Pen penBorder = new Pen(item.RowBorderColor, item.RowBorderSize);
                penBorder.Alignment = PenAlignment.Inset;
                graphicsRow.DrawRectangle(penBorder, rectRow);
                penBorder.Dispose();
            }


            // make sure anything can even be selected before drawing selection rects
            if (Selectable)
                if (ShowFocusRect && (FocusedItem == item)) // deal with focus rect
                    ControlPaint.DrawFocusRectangle(graphicsRow,
                                                    new Rectangle(RowsInnerClientRect.X + 1, rectRow.Y,
                                                                  RowsInnerClientRect.Width - 1, rectRow.Height));
        }


        /// <summary>
        ///   Draw Sub Item (Cell) at location specified
        /// </summary>
        /// <param name = "graphicsSubItem"></param>
        /// <param name = "rectSubItem"></param>
        /// <param name = "item"></param>
        /// <param name = "subItem"></param>
        /// <param name = "nColumn"></param>
        public virtual void DrawSubItem(Graphics graphicsSubItem, Rectangle rectSubItem, CListItem item,
                                        CListSubItem subItem, int nColumn)
        {
            // precheck to make sure this is big enough for the things we want to do inside it
            Rectangle subControlRect = new Rectangle(rectSubItem.X, rectSubItem.Y, rectSubItem.Width, rectSubItem.Height);


            if ((subItem.Control != null) && (!subItem.ForceText))
            {
                // custom embedded control here

                Control control = subItem.Control;

                if (control.Parent != this) // *** CRUCIAL *** this makes sure the parent is the list control
                    control.Parent = this;

                //				Rectangle subrc = new Rectangle( 
                //					subControlRect.X+this.CellPaddingSize, 
                //					subControlRect.Y+this.CellPaddingSize, 
                //					subControlRect.Width-this.CellPaddingSize*2,
                //					subControlRect.Height-this.CellPaddingSize*2 );


                Rectangle subrc = new Rectangle(
                    subControlRect.X,
                    subControlRect.Y + 1,
                    subControlRect.Width,
                    subControlRect.Height - 1);


                Type tp = control.GetType();
                PropertyInfo pi = control.GetType().GetProperty("PreferredHeight");
                if (pi != null)
                {
                    int PreferredHeight = (int)pi.GetValue(control, null);

                    if (((PreferredHeight + CellPaddingSize * 2) > ItemHeight) && AutoHeight)
                        ItemHeight = PreferredHeight + CellPaddingSize * 2;

                    subrc.Y = subControlRect.Y + ((subControlRect.Height - PreferredHeight) / 2);
                }

                NewLiveControls.Add(control); // put it in the new list, remove from old list
                if (LiveControls.Contains(control)) // make sure its in the old list first
                {
                    LiveControls.Remove(control); // remove it from list so it doesn't get put down
                }


                if (control.Bounds.ToString() != subrc.ToString())
                    control.Bounds = subrc; // this will force an invalidation

                if (control.Visible != true)
                    control.Visible = true;
            }
            else // not control based
            {
                // if the sub item color is not the same as the back color fo the control, AND the item is not selected, then color this sub item background

                if ((subItem.BackColor.ToArgb() != BackColor.ToArgb()) && (!item.Selected) &&
                    (subItem.BackColor != Color.White))
                {
                    SolidBrush bbrush = new SolidBrush(subItem.BackColor);
                    graphicsSubItem.FillRectangle(bbrush, rectSubItem);
                    bbrush.Dispose();
                }

                // do we need checkboxes in this column or not?
                if (Columns[nColumn].CheckBoxes)
                    rectSubItem = DrawCheckBox(graphicsSubItem, rectSubItem, subItem.Checked);

                // if there is an image, this routine will RETURN with exactly the space left for everything else after the image is drawn (or not drawn due to lack of space)
                if ((subItem.ImageIndex > -1) && (ImageList != null) && (subItem.ImageIndex < ImageList.Images.Count))
                    rectSubItem = DrawCellGraphic(graphicsSubItem, rectSubItem, ImageList.Images[subItem.ImageIndex],
                                                  subItem.ImageAlignment);

                // deal with text color in a box on whether it is selected or not
                Color textColor;
                if (item.Selected && Selectable)
                    textColor = SelectedTextColor;
                else
                {
                    textColor = ForeColor;
                    if (item.ForeColor.ToArgb() != ForeColor.ToArgb())
                        textColor = item.ForeColor;
                    else if (subItem.ForeColor.ToArgb() != ForeColor.ToArgb())
                        textColor = subItem.ForeColor;
                }

                DrawCellText(graphicsSubItem, rectSubItem, subItem.Text, Columns[nColumn].TextAlignment, textColor,
                             item.Selected, ItemWordWrap);

                subItem.LastCellRect = rectSubItem; // important to ONLY catch the area where the text is drawn
            }
        }


        /// <summary>
        ///   Draw a checkbox on the sub item
        /// </summary>
        /// <param name = "graphicsCell"></param>
        /// <param name = "rectCell"></param>
        /// <param name = "bChecked"></param>
        /// <returns></returns>
        public virtual Rectangle DrawCheckBox(Graphics graphicsCell, Rectangle rectCell, bool bChecked)
        {
            int th, ty, tw, tx;

            th = CHECKBOX_SIZE + (CellPaddingSize * 2);
            tw = CHECKBOX_SIZE + (CellPaddingSize * 2);
            MaxHeight = th; // this will only set if autosize is true

            if ((tw > rectCell.Width) || (th > rectCell.Height))
                return rectCell; // not enough room to draw the image, bail out


            ty = rectCell.Y + CellPaddingSize + ((rectCell.Height - th) / 2);
            tx = rectCell.X + CellPaddingSize;

            if (bChecked)
                graphicsCell.DrawImage(imageList1.Images[1], tx, ty);
            //graphicsCell.FillRectangle( Brushes.YellowGreen, tx, ty, CHECKBOX_SIZE, CHECKBOX_SIZE );
            else
                graphicsCell.DrawImage(imageList1.Images[0], tx, ty);
            //graphicsCell.FillRectangle( Brushes.Red, tx, ty, CHECKBOX_SIZE, CHECKBOX_SIZE );

            // remove the width that we used for the graphic from the cell
            rectCell.Width -= (CHECKBOX_SIZE + (CellPaddingSize * 2));
            rectCell.X += tw;

            return rectCell;
        }


        /// <summary>
        ///   draw the contents of a cell, do not draw any background or associated things
        /// </summary>
        /// <param name = "graphicsCell"></param>
        /// <param name = "rectCell"></param>
        /// <param name = "img"></param>
        /// <param name = "alignment"></param>
        /// <returns>
        ///   returns the area of the cell that is left for you to put anything else on.
        /// </returns>
        public virtual Rectangle DrawCellGraphic(Graphics graphicsCell, Rectangle rectCell, Image img,
                                                 HorizontalAlignment alignment)
        {
            int th, ty, tw, tx;

            th = img.Height + (CellPaddingSize * 2);
            tw = img.Width + (CellPaddingSize * 2);
            MaxHeight = th; // this will only set if autosize is true

            if ((tw > rectCell.Width) || (th > rectCell.Height))
                return rectCell; // not enough room to draw the image, bail out

            if (alignment == HorizontalAlignment.Left)
            {
                ty = rectCell.Y + CellPaddingSize + ((rectCell.Height - th) / 2);
                tx = rectCell.X + CellPaddingSize;

                graphicsCell.DrawImage(img, tx, ty);

                // remove the width that we used for the graphic from the cell
                rectCell.Width -= (img.Width + (CellPaddingSize * 2));
                rectCell.X += tw;
            }
            else if (alignment == HorizontalAlignment.Center)
            {
                ty = rectCell.Y + CellPaddingSize + ((rectCell.Height - th) / 2);
                tx = rectCell.X + CellPaddingSize + ((rectCell.Width - tw) / 2);
                ;

                graphicsCell.DrawImage(img, tx, ty);

                // remove the width that we used for the graphic from the cell
                //rectCell.Width -= (img.Width + (CellPaddingSize*2));
                //rectCell.X += (img.Width + (CellPaddingSize*2));
                rectCell.Width = 0;
            }
            else if (alignment == HorizontalAlignment.Right)
            {
                ty = rectCell.Y + CellPaddingSize + ((rectCell.Height - th) / 2);
                tx = rectCell.Right - tw;

                graphicsCell.DrawImage(img, tx, ty);

                // remove the width that we used for the graphic from the cell
                rectCell.Width -= tw;
            }

            return rectCell;
        }


        /// <summary>
        ///   Draw cell text is used by header and cell to draw properly aligned text in subitems.
        /// </summary>
        /// <param name = "graphicsCell"></param>
        /// <param name = "rectCell"></param>
        /// <param name = "strCellText"></param>
        /// <param name = "alignment"></param>
        /// <param name = "textColor"></param>
        /// <param name = "bSelected"></param>
        /// <param name = "bWordWrap"></param>
        public virtual void DrawCellText(Graphics graphicsCell, Rectangle rectCell, string strCellText,
                                         ContentAlignment alignment, Color textColor, bool bSelected, bool bWordWrap)
        {
            int nInteriorWidth = rectCell.Width - (CellPaddingSize * 2);
            int nInteriorHeight = rectCell.Height - (CellPaddingSize * 2);


            // cell text color will be inverted or changed from caller if this item is already selected
            SolidBrush textBrush;
            textBrush = new SolidBrush(textColor);


            // convert property editor friendly alignment to an alignment we can use for strings
            StringFormat sf = new StringFormat();
            sf.Alignment = GLStringHelpers.ConvertContentAlignmentToHorizontalStringAlignment(alignment);
            sf.LineAlignment = GLStringHelpers.ConvertContentAlignmentToVerticalStringAlignment(alignment);

            SizeF measuredSize;
            if (bWordWrap)
            {
                sf.FormatFlags = 0; // word wrapping is on by default for drawing
                measuredSize = graphicsCell.MeasureString(strCellText, Font, new Point(CellPaddingSize, CellPaddingSize),
                                                          sf);
            }
            else
            {
                // they aren't word wrapping so we need to put the ...'s where necessary
                sf.FormatFlags = StringFormatFlags.NoWrap;
                measuredSize = graphicsCell.MeasureString(strCellText, Font, new Point(CellPaddingSize, CellPaddingSize),
                                                          sf);
                if (measuredSize.Width > nInteriorWidth) // dont truncate if we are doing word wrap
                    strCellText = GLStringHelpers.TruncateString(strCellText, nInteriorWidth, graphicsCell, Font);
            }

            MaxHeight = (int)measuredSize.Height + (CellPaddingSize * 2); // this will only set if autosize is true
            graphicsCell.DrawString(strCellText, Font, textBrush, rectCell
                /*rectCell.X+this.CellPaddingSize, rectCell.Y+this.CellPaddingSize*/, sf);

            textBrush.Dispose();
        }


        /// <summary>
        ///   Draw grid lines in client area
        /// </summary>
        /// <param name = "RowsDC"></param>
        /// <param name = "rect"></param>
        public virtual void DrawGridLines(Graphics RowsDC, Rectangle rect)
        {
            int nStartItem = vPanelScrollBar.Value; /* Draw Rows */
            int nYCursor = rect.Y;
            //for (int nItem = 0; ((nItem < (VisibleRowsCount +1) ) && ((nItem+nStartItem) < Items.Count )); nItem++ )

            Pen p = new Pen(GridColor);
            if (GridLineStyle == GLGridLineStyles.gridDashed)
                p.DashStyle = DashStyle.Dash;
            else if (GridLineStyle == GLGridLineStyles.gridSolid)
                p.DashStyle = DashStyle.Solid;
            else
                p.DashStyle = DashStyle.Solid;


            if ((GridLines == GLGridLines.gridBoth) || (GridLines == GLGridLines.gridHorizontal))
            {
                int nRowsToDraw = VisibleRowsCount + 1;
                if (GridTypes == GLGridTypes.gridOnExists)
                    if (VisibleRowsCount > Count)
                        nRowsToDraw = Count;


                for (int nItem = 0; nItem < nRowsToDraw; nItem++)
                {
                    //Debug.WriteLine( "ItemCount " + Items.Count.ToString() + " Item Number " + nItem.ToString() );
                    nYCursor += ItemHeight;
                    RowsDC.DrawLine(p, 0, nYCursor, rect.Width, nYCursor); // draw horizontal line
                }
            }

            if ((GridLines == GLGridLines.gridBoth) || (GridLines == GLGridLines.gridVertical))
            {
                int nXCursor = -hPanelScrollBar.Value;
                for (int nColumn = 0; nColumn < Columns.Count; nColumn++)
                {
                    nXCursor += Columns[nColumn].Width;
                    RowsDC.DrawLine(p, nXCursor + 1, rect.Y, nXCursor + 1, rect.Bottom); // draw vertical line
                }
            }

            p.Dispose();
        }

        #endregion // drawing

        #region Keyboard

#if false
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			e.Handled = true;
		}
		protected override void OnKeyDown(KeyEventArgs e)
		{
			e.Handled = true;
		}
#endif

        #endregion

        #region Scrolling

        private void OnScroll(object sender, ScrollEventArgs e)
        {
            DestroyActivatedEmbedded();

            Invalidate();
        }


        /// <summary>
        ///   Recalculate scroll bars and control size
        /// </summary>
        private void RecalcScroll() //Graphics g )
        {
            int nSomethingHasGoneVeryWrongSoBreakOut = 0;
            bool bSBChanged;
            do // this loop is to handle changes and rechanges that happen when oen or the other changes
            {
                bSBChanged = false;

                if ((Columns.Width > RowsInnerClientRect.Width) && (hPanelScrollBar.Visible == false))
                {
                    // total width of all the rows is less than the visible rect
                    hPanelScrollBar.mVisible = true;
                    hPanelScrollBar.Value = 0;
                    bSBChanged = true;
                    Invalidate();
                }

                if ((Columns.Width <= RowsInnerClientRect.Width) && hPanelScrollBar.Visible)
                {
                    // total width of all the rows is less than the visible rect
                    hPanelScrollBar.mVisible = false;
                    hPanelScrollBar.Value = 0;
                    bSBChanged = true;
                    Invalidate();
                }

                if ((TotalRowHeight > RowsInnerClientRect.Height) && (vPanelScrollBar.Visible == false))
                {
                    // total height of all the rows is greater than the visible rect
                    vPanelScrollBar.mVisible = true;
                    hPanelScrollBar.Value = 0;
                    bSBChanged = true;
                    Invalidate();
                }

                if ((TotalRowHeight <= RowsInnerClientRect.Height) && vPanelScrollBar.Visible)
                {
                    // total height of all rows is less than the visible rect
                    vPanelScrollBar.mVisible = false;
                    vPanelScrollBar.Value = 0;
                    bSBChanged = true;
                    Invalidate();
                }

                // *** WARNING *** WARNING *** Kludge.  Not sure why this is sometimes hanging.  Fix this.
                if (++nSomethingHasGoneVeryWrongSoBreakOut > 4)
                    break;
            } while (bSBChanged); // this should never really run more than twice


            //Rectangle headerRect = HeaderRect;		// tihs is an optimization so header rect doesnt recalc every time we call it
            Rectangle rectClient = RowsInnerClientRect;

            /*
			 *  now that we know which scrollbars are showing and which aren't, resize the scrollbars to fit those windows
			 */
            if (vPanelScrollBar.Visible)
            {
                vPanelScrollBar.mTop = rectClient.Y;
                vPanelScrollBar.mLeft = rectClient.Right;
                vPanelScrollBar.mHeight = rectClient.Height;
                vPanelScrollBar.mLargeChange = VisibleRowsCount;
                vPanelScrollBar.mMaximum = Count - 1;

                if (((vPanelScrollBar.Value + VisibleRowsCount) > Count))
                // catch all to make sure the scrollbar isnt going farther than visible items
                {
                    vPanelScrollBar.Value = Count - VisibleRowsCount;
                    // an item got deleted underneath somehow and scroll value is larger than can be displayed
                }
            }

            if (hPanelScrollBar.Visible)
            {
                hPanelScrollBar.mLeft = rectClient.Left;
                hPanelScrollBar.mTop = rectClient.Bottom;
                hPanelScrollBar.mWidth = rectClient.Width;

                hPanelScrollBar.mLargeChange = rectClient.Width; // this reall is the size we want to move
                hPanelScrollBar.mMaximum = Columns.Width;

                if ((hPanelScrollBar.Value + hPanelScrollBar.LargeChange) > hPanelScrollBar.Maximum)
                {
                    hPanelScrollBar.Value = hPanelScrollBar.Maximum - hPanelScrollBar.LargeChange;
                }
            }


            if (BorderPadding > 0)
            {
                horiBottomBorderStrip.Bounds = new Rectangle(0, ClientRectangle.Bottom - BorderPadding,
                                                             ClientRectangle.Width, BorderPadding);
                // horizontal bottom picture box
                horiTopBorderStrip.Bounds = new Rectangle(0, ClientRectangle.Top, ClientRectangle.Width, BorderPadding);
                // horizontal bottom picture box

                vertLeftBorderStrip.Bounds = new Rectangle(0, 0, BorderPadding, ClientRectangle.Height);
                // horizontal bottom picture box
                vertRightBorderStrip.Bounds = new Rectangle(ClientRectangle.Right - BorderPadding, 0, BorderPadding,
                                                            ClientRectangle.Height); // horizontal bottom picture box
            }
            else
            {
                if (horiBottomBorderStrip.Visible)
                    horiBottomBorderStrip.Visible = false;
                if (horiTopBorderStrip.Visible)
                    horiTopBorderStrip.Visible = false;
                if (vertLeftBorderStrip.Visible)
                    vertLeftBorderStrip.Visible = false;
                if (vertRightBorderStrip.Visible)
                    vertRightBorderStrip.Visible = false;
            }

            if (hPanelScrollBar.Visible && vPanelScrollBar.Visible)
            {
                if (!cornerBox.Visible)
                    cornerBox.Visible = true;

                cornerBox.Bounds = new Rectangle(hPanelScrollBar.Right, vPanelScrollBar.Bottom, vPanelScrollBar.Width,
                                                 hPanelScrollBar.Height);
            }
            else
            {
                if (cornerBox.Visible)
                    cornerBox.Visible = false;
            }
        }


        /// <summary>
        ///   Handle vertical scroll bar movement
        /// </summary>
        /// <param name = "sender"></param>
        /// <param name = "e"></param>
        private void vPanelScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            Invalidate();
        }


        /// <summary>
        ///   Handle horizontal scroll bar movement
        /// </summary>
        /// <param name = "sender"></param>
        /// <param name = "e"></param>
        private void hPanelScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            Invalidate();
        }

        #endregion

        #region Mouse

        /// <summary>
        ///   OnDoubleclick
        /// 
        ///   if someone double clicks on an area, we need to start a control potentially
        /// </summary>
        /// <param name = "e"></param>
        protected override void OnDoubleClick(EventArgs e)
        {
            Point pointLocalMouse = PointToClient(Cursor.Position);

            //Debug.WriteLine( "Double Click Called" );
            //Debug.WriteLine( "At Cords X " + pointLocalMouse.X.ToString() + " Y " + pointLocalMouse .Y.ToString() );


            int nItem = 0, nColumn = 0, nCellX = 0, nCellY = 0;
            ListStates eState;
            GLListRegion listRegion;
            InterpretCoords(pointLocalMouse.X, pointLocalMouse.Y, out listRegion, out nCellX, out nCellY, out nItem,
                            out nColumn, out eState);

            //Debug.WriteLine( "listRegion " + listRegion.ToString() );

            if ((listRegion == GLListRegion.client) && (nColumn < Columns.Count))
            {
                ActivateEmbeddedControl(nColumn, Items[nItem], Items[nItem].SubItems[nColumn]);
            }

            base.OnDoubleClick(e);
        }


        /// <summary>
        ///   had to put this routine in because of overriden protection level being unchangable
        /// </summary>
        /// <param name = "Sender"></param>
        /// <param name = "e"></param>
        protected void OnMouseDownFromSubItem(object Sender, MouseEventArgs e)
        {
            //Debug.WriteLine( "OnMouseDownFromSubItem called " + e.X.ToString() + " " + e.Y.ToString() );
            Point cp = PointToClient(new Point(MousePosition.X, MousePosition.Y));
            e = new MouseEventArgs(e.Button, e.Clicks, cp.X, cp.Y, e.Delta);
            //Debug.WriteLine( "after " + cp.X.ToString() + " " + cp.Y.ToString() );
            OnMouseDown(e);
        }


        /// <summary>
        ///   Mouse has left the control area
        /// </summary>
        /// <param name = "e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            // clear all the hot tracking
            Columns.ClearHotStates(); // this is the HEADER hot state
            HotItemIndex = -1;
            HotColumnIndex = -1;

            base.OnMouseLeave(e);
        }


        /// <summary>
        ///   mouse button pressed
        /// </summary>
        /// <param name = "e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            //Debug.WriteLine( "Real " + e.X.ToString() + " " + e.Y.ToString() );

            int nItem = 0, nColumn = 0, nCellX = 0, nCellY = 0;
            ListStates eState;
            GLListRegion listRegion;
            InterpretCoords(e.X, e.Y, out listRegion, out nCellX, out nCellY, out nItem, out nColumn, out eState);

            //Debug.WriteLine( nCellX.ToString() + " - " + nCellY.ToString() );


            if (e.Button == MouseButtons.Right) // if its the right button then we don't really care till its released
            {
                base.OnMouseDown(e);
                return;
            }


            //-----------------------------------------------------------------------------------------
            if (eState == ListStates.stateColumnSelect) // Column select
            {
                m_nState = ListStates.stateNone;


                if (SortType != SortTypes.None)
                {
                    Columns[nColumn].State = ColumnStates.csPressed;
                    SortColumn(nColumn);
                }

                if (ColumnClickedEvent != null)
                    ColumnClickedEvent(this, new ClickEventArgs(nItem, nColumn)); // fire the column clicked event

                //Invalidate();
                base.OnMouseDown(e);
                return;
            }
            //---Resizing -----------------------------------------------------------------------------------
            if (eState == ListStates.stateColumnResizing) // resizing
            {
                Cursor.Current = Cursors.VSplit;
                m_nState = ListStates.stateColumnResizing;

                m_pointColumnResizeAnchor = new Point(GetColumnScreenX(nColumn), e.Y); // deal with moving column sizes
                m_nResizeColumnNumber = nColumn;


                base.OnMouseDown(e);
                return;
            }
            //--Item check, if no items exist go no further--
            //if ( Items.Count == 0 )
            //return;

            //---Items --------------------------------------------------------------------------------------
            if (eState == ListStates.stateSelecting)
            {
                // ctrl based multi select ------------------------------------------------------------

                // whatever else this does, it needs to first check to see if the state of the checkbox is changing
                if ((nColumn < Columns.Count) && (Columns[nColumn].CheckBoxes))
                {
                    // there is a checkbox on this control, lets see if the click came in the region
                    if (
                        (nCellX > CellPaddingSize) &&
                        (nCellX < (CellPaddingSize + CHECKBOX_SIZE)) &&
                        (nCellY > CellPaddingSize) &&
                        (nCellY < (CellPaddingSize + CHECKBOX_SIZE))
                        )
                    {
                        // toggle the checkbox
                        if (Items[nItem].SubItems[nColumn].Checked)
                            Items[nItem].SubItems[nColumn].Checked = false;
                        else
                            Items[nItem].SubItems[nColumn].Checked = true;
                    }
                }


                m_nState = ListStates.stateSelecting;

                FocusedItem = Items[nItem];


                if (((ModifierKeys & Keys.Control) == Keys.Control) && AllowMultiselect)
                {
                    m_nLastSelectionIndex = nItem;

                    if (Items[nItem].Selected)
                        Items[nItem].Selected = false;
                    else
                        Items[nItem].Selected = true;


                    base.OnMouseDown(e);
                    return;
                }

                // shift based multi row select -------------------------------------------------------
                if (((ModifierKeys & Keys.Shift) == Keys.Shift) && AllowMultiselect)
                {
                    Items.ClearSelection();
                    if (m_nLastSelectionIndex >= 0) // ie, non negative so that we have a starting point
                    {
                        int index = m_nLastSelectionIndex;
                        do
                        {
                            Items[index].Selected = true;
                            if (index > nItem) index--;
                            if (index < nItem) index++;
                        } while (index != nItem);

                        Items[index].Selected = true;
                    }

                    base.OnMouseDown(e);
                    return;
                }

                // the normal single select -----------------------------------------------------------
                Items.ClearSelection(Items[nItem]);

                // following two if statements deal ONLY with non multi=select where a singel sub item is being selected
                if ((m_nLastSelectionIndex < Count) && (m_nLastSubSelectionIndex < Columns.Count))
                    Items[m_nLastSelectionIndex].SubItems[m_nLastSubSelectionIndex].Selected = false;
                if ((FullRowSelect == false) && ((nItem < Count) && (nColumn < Columns.Count)))
                    Items[nItem].SubItems[nColumn].Selected = true;


                m_nLastSelectionIndex = nItem;
                m_nLastSubSelectionIndex = nColumn;
                Items[nItem].Selected = true;
            }


            base.OnMouseDown(e);
            return;
        }


        /// <summary>
        ///   when mouse moves
        /// </summary>
        /// <param name = "e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            try
            {
                if (m_nState == ListStates.stateColumnResizing)
                {
                    Cursor.Current = Cursors.VSplit;


                    int nWidth;
                    nWidth = e.X - m_pointColumnResizeAnchor.X;

                    if (nWidth <= MINIMUM_COLUMN_SIZE)
                    {
                        nWidth = MINIMUM_COLUMN_SIZE;
                    }

                    CColumn col;
                    col = Columns[m_nResizeColumnNumber];
                    col.Width = nWidth;

                    base.OnMove(e);
                    return;
                }


                int nItem = 0, nColumn = 0, nCellX = 0, nCellY = 0;
                ListStates eState;
                GLListRegion listRegion;
                InterpretCoords(e.X, e.Y, out listRegion, out nCellX, out nCellY, out nItem, out nColumn, out eState);


                if (eState == ListStates.stateColumnResizing)
                {
                    Cursor.Current = Cursors.VSplit;

                    base.OnMove(e);
                    return;
                }

                Cursor.Current = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception throw in GlobalList_MouseMove with text : " + ex);
            }

            base.OnMove(e);
            return;
        }


        /// <summary>
        ///   mouse up
        /// </summary>
        /// <param name = "e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            Cursor.Current = Cursors.Arrow;
            Columns.ClearStates();


            int nItem = 0, nColumn = 0, nCellX = 0, nCellY = 0;
            ListStates eState;
            GLListRegion listRegion;
            InterpretCoords(e.X, e.Y, out listRegion, out nCellX, out nCellY, out nItem, out nColumn, out eState);


            m_nState = ListStates.stateNone;

            base.OnMouseUp(e);
        }

        #endregion

        #endregion  // functionality
    }
}