using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using com.jds.GUpdater.classes.config;
using com.jds.GUpdater.classes.language.enums;
using log4net;

namespace com.jds.GUpdater.classes.language
{
    public class LanguageHolder
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof (LanguageHolder));
        private static LanguageHolder _instance;
        private readonly Dictionary<String, Language> _languages = new Dictionary<String, Language>();

        private LanguageHolder()
        {
            var directoryInfo = new DirectoryInfo("./language/");

            if (!directoryInfo.Exists)
            {
                NotifyProblem();
                return;
            }

            foreach (FileInfo f in directoryInfo.GetFiles())
            {
                if (f.Name.EndsWith(".gu"))
                {
                    var language = new Language(f);

                    if (language.Name != null && !_languages.ContainsKey(language.Name))
                    {
                        _languages.Add(language.Name, language);
                    }
                }
            }

            string lang = RConfig.Instance.Language ?? "Undefined";

            if (lang.Equals("Undefined"))
            {
                bool set = false;

                Thread thread = Thread.CurrentThread;
                CultureInfo cultureInfo = thread.CurrentUICulture;
                string langShort = cultureInfo.TwoLetterISOLanguageName;

                foreach (Language l in _languages.Values)
                {
                    if (l.ShortName.Equals(langShort))
                    {
                        RConfig.Instance.Language = l.Name;
                        lang = l.Name;
                        set = true;
                    }
                }

                if (!set)
                {
                    RConfig.Instance.Language = "English";
                    lang = "English";
                }
            }

            if (!_languages.ContainsKey(lang))
            {
                if (!_languages.ContainsKey("English"))
                {
                    NotifyProblem();
                }
                RConfig.Instance.Language = "English";
            }

            _log.Info("Load " + _languages.Count + " languages");
        }

        public static LanguageHolder Instance()
        {
            return _instance ?? (_instance = new LanguageHolder()); 
        }

        public Dictionary<string, Language>.KeyCollection Languages
        {
            get { return _languages.Keys; }
        }

        public Language Language
        {
            get
            {
                string lang = RConfig.Instance.Language;

                if (!_languages.ContainsKey(lang))
                {
                    return null;
                }

                return _languages[lang];
            }   
        }

        public string this[WordEnum wordEnum]
        {
            get
            {
                string lang = RConfig.Instance.Language;

                if (!_languages.ContainsKey(lang))
                {
                    return "No lang for " + lang;
                }

                return _languages[lang].GetWord(wordEnum);
            }
        }

        public Image GetImage(PictureName name, PictureType type)
        {
            string lang = RConfig.Instance.Language;

            return !_languages.ContainsKey(lang) ? null : _languages[lang].GetImage(name, type);
        }

        public void NotifyProblem()
        {
            MessageBox.Show("Problem with languages. Please update.");
            Process.GetCurrentProcess().Kill();
        }
    }
}