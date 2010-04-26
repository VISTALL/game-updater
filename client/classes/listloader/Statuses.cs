using System.Collections.Generic;
using com.jds.AWLauncher.classes.listloader.enums;

namespace com.jds.AWLauncher.classes.listloader
{
    public class Statuses
    {
        private readonly Dictionary<ListFileType, bool> _statuses = new Dictionary<ListFileType, bool>();

        public Statuses()
        {
            _statuses.Add(ListFileType.CRITICAL, false);
            _statuses.Add(ListFileType.NORMAL, false);
        }

        public bool this[ListFileType type]
        {
            get { return _statuses[type]; }
            set { _statuses[type] = value; }
        }
    }
}