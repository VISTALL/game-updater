namespace com.jds.Premaker.classes.gui.clistview
{
    /// <summary>
    ///   Interface you must include for a control to be activated embedded useable
    /// </summary>
    public interface CEmbeddedControl
    {
        /// <summary>
        ///   item this control is embedded in
        /// </summary>
        CListItem Item { get; set; }

        /// <summary>
        ///   Sub item this control is embedded in
        /// </summary>
        CListSubItem SubItem { get; set; }

        /// <summary>
        ///   Parent control
        /// </summary>
        CListView ListControl { get; set; }

        /// <summary>
        ///   This returns the current text output as entered into the control right now
        /// </summary>
        /// <returns></returns>
        string GLReturnText();


        /// <summary>
        ///   Called when the control is loaded
        /// </summary>
        /// <param name = "item"></param>
        /// <param name = "subItem"></param>
        /// <param name = "listctrl"></param>
        /// <returns></returns>
        bool GLLoad(CListItem item, CListSubItem subItem, CListView listctrl);

        // populate this control however you wish with item


        /// <summary>
        ///   Called when control is being destructed
        /// </summary>
        void GLUnload();

        // take information from control and return it to the item
    }
}