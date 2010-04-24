using com.jds.GUpdater.classes.games.attributes;
using com.jds.GUpdater.classes.games.gui;
using com.jds.GUpdater.classes.games.propertyes.impl;

namespace com.jds.GUpdater.classes.games
{
    public enum Game
    {
        [EnumName("Aion")] [EnumProperty(typeof (AionProperty))] [EnumPane(typeof (SimpleGamePanel))] AION,
        [EnumName("Lineage 2")] [EnumProperty(typeof (Lineage2Property))] [EnumPane(typeof (SimpleGamePanel))] LINEAGE2
    }
}