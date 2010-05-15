using System;
using System.Diagnostics;

namespace com.jds.Premaker.classes.gui.clistview
{
    /// <summary>
    ///   Summary description for CMergeSort.
    /// </summary>
    internal class CMergeSort
    {
        /// <summary>
        ///   Direction this sorting routine will move items
        /// </summary>
        private SortDirections m_SortDirection = SortDirections.SortDescending;

        public bool NumericCompare { get; set; }


        public bool StopRequested { get; set; }


        public int SortColumn { get; set; }


        public SortDirections SortDirection
        {
            get { return m_SortDirection; }
            set { m_SortDirection = value; }
        }


        public void sort(GLItemCollection items, int low_0, int high_0)
        {
            int lo = low_0;
            int hi = high_0;
            if (lo >= hi)
                return;

            int mid = (lo + hi) / 2;


            sort(items, lo, mid);
            sort(items, mid + 1, hi);


            int end_lo = mid;
            int start_hi = mid + 1;
            while ((lo <= end_lo) && (start_hi <= hi))
            {
                if (StopRequested)
                    return;

                if (CompareItems(items[lo], items[start_hi], CompareDirection.LessThan))
                {
                    lo++;
                }
                else
                {
                    CListItem T = items[start_hi];
                    for (int k = start_hi - 1; k >= lo; k--)
                        items[k + 1] = items[k];

                    items[lo] = T;
                    lo++;
                    end_lo++;
                    start_hi++;
                }
            }
        }


        private bool CompareItems(CListItem item1, CListItem item2, CompareDirection direction)
        {
            bool dir = false;

            if (direction == CompareDirection.GreaterThan)
                dir = true;

            if (SortDirection == SortDirections.SortAscending)
                dir = !dir; // flip it


            if (!NumericCompare)
            {
                if (dir)
                    return (item1.SubItems[SortColumn].Text.CompareTo(item2.SubItems[SortColumn].Text) < 0);
                else
                    return (item1.SubItems[SortColumn].Text.CompareTo(item2.SubItems[SortColumn].Text) > 0);
            }
            else
            {
                try
                {
                    double n1 = Double.Parse(item1.SubItems[SortColumn].Text);
                    double n2 = Double.Parse(item2.SubItems[SortColumn].Text);


                    if (dir)
                        return (n1 < n2);
                    else
                        return (n1 > n2);
                }
                catch (Exception ex)
                {
                    // no numeric value (bad bad)
                    Debug.WriteLine(ex.ToString());
                    return false;
                }
            }
        }

        #region Nested type: CompareDirection

        private enum CompareDirection
        {
            GreaterThan,
            LessThan
        } ;

        #endregion
    }
}