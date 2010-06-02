#region Usage

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Reflection;
using com.jds.AWLauncher.classes.forms;
using com.jds.AWLauncher.classes.games;
using com.jds.AWLauncher.classes.games.attributes;
using com.jds.AWLauncher.classes.games.propertyes;
using com.jds.AWLauncher.classes.language;
using com.jds.AWLauncher.classes.language.attributes;
using com.jds.AWLauncher.classes.language.enums;
using com.jds.AWLauncher.classes.registry;
using com.jds.AWLauncher.classes.registry.attributes;
using com.jds.AWLauncher.classes.version_control.gui;
using log4net;
using Microsoft.Win32;

#endregion

namespace com.jds.AWLauncher.classes.config
{
    public class RConfig : RegistryProperty
    {
        #region Variables

        private const String KEY_NEW = "Software\\AWars.net\\AWLauncher";

        private static readonly ILog _log = LogManager.GetLogger(typeof (RConfig));

        private readonly Dictionary<Game, GameProperty> _games = new Dictionary<Game, GameProperty>();
        private String _language;

        #endregion

        #region Instance

        private static RConfig _instance;

        public static RConfig Instance
        {
            get { return _instance ?? (_instance = new RConfig()); }
        }

        #endregion

        #region Методы загрузки и выгрузки в реест настроек

        public void init()
        {
            Array enums = Enum.GetValues(typeof (Game));
            foreach (object e in enums)
            {
                var r =
                    (EnumProperty)
                    e.GetType().GetField(e.ToString()).GetCustomAttributes(typeof (EnumProperty), false).GetValue(0);
                try
                {
                    var instance =
                        (GameProperty) r.Type.InvokeMember(null, BindingFlags.CreateInstance, null, null, null);
                    _games.Add((Game) e, instance);
                }
                catch (Exception e2)
                {
                    _log.Info("Exception: " + e2, e2);
                }
            }

            select();

            //TODO убрать
            try
            {
                RegistryKey key = Registry.CurrentUser;
                if (key.OpenSubKey("Software\\J Develop Station\\GUpdater") != null)
                    key.DeleteSubKeyTree("Software\\J Develop Station\\GUpdater");
            }
            catch
            {
            }
        }

        public void save()
        {
            insert();

            foreach (GameProperty prop in _games.Values)
            {
                prop.insert();
            }
        }

        #endregion

        #region Игровые методы

        public GameProperty getGameProperty(Game g)
        {
            return _games[g];
        }

        #endregion

        #region Properties

        [RegistryPropertyKey("Language", null)]
        [LanguageDisplayName(WordEnum.LANGUAGE)]
        [LanguageDescription(WordEnum.LANGUAGE_DESCRIPTION)]
        [TypeConverter(typeof (LanguagePropertyConventer))]
        [Editor(typeof (LanguagePropertyEditor), typeof (UITypeEditor))]
        public String Language
        {
            get
            {
                return _language;
            }
            set
            {
                _language = value;

                PropertyForm.Instance().ChangeLanguage();
                AssemblyPage.Instance().ChangeLanguage();
                //MainForm.Instance.ChangeLanguage();
            }
        }

        [RegistryPropertyKey("CheckCriticalOnStart", false)]
        [LanguageDisplayName(WordEnum.CHECK_CRITICAL_ON_START)]
        [LanguageDescription(WordEnum.CHECK_CRITICAL_ON_START_DESCRIPTION)]
        public bool CheckCriticalOnStart { get; set; }

        [RegistryPropertyKey("CheckVersionOnStart", true)]
        [LanguageDisplayName(WordEnum.CHECK_VERSION_ON_START)]
        [LanguageDescription(WordEnum.CHECK_VERSION_ON_START_DESCRIPTION)]
        public bool CheckVersionOnStart { get; set; }

        [Browsable(false)]
        [RegistryPropertyKey("ActiveGame", Game.AION)]
        public Game ActiveGame { get; set; }

        [Browsable(false)]
        [RegistryPropertyKey("X", -1)]
        public int X { get; set; }

        [Browsable(false)]
        [RegistryPropertyKey("Y", -1)]
        public int Y { get; set; }

        #endregion

        public override string getKey()
        {
            return KEY_NEW;
        }
    }
}