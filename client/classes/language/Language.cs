using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml;
using com.jds.AWLauncher.classes.language.enums;
using com.jds.AWLauncher.classes.zip;

namespace com.jds.AWLauncher.classes.language
{
    public class Language
    {
        private readonly Dictionary<PictureName, Dictionary<PictureType, Image>> _images =
            new Dictionary<PictureName, Dictionary<PictureType, Image>>();

        private readonly Dictionary<WordEnum, string> _words = new Dictionary<WordEnum, string>();

        public Language(FileInfo fileInfo)
        {
            var zipInputStream = new ZipInputStream(fileInfo.OpenRead());

            ZipEntry entry = null;
            while ((entry = zipInputStream.GetNextEntry()) != null)
            {
                if (entry.IsFile)
                {
                    var data = new byte[entry.Size];
                    zipInputStream.Read(data, 0, data.Length);

                    if (entry.Name.Equals("Language.xml"))
                    {
                        ParseLang(data);
                    }
                    else if (entry.Name.EndsWith(".png"))
                    {
                        string[] sp = entry.Name.Replace(".png", "").Split('_');
                        string name = sp[0];
                        string type = sp[1];

                        if (Enum.IsDefined(typeof (PictureName), name) && Enum.IsDefined(typeof (PictureType), type))
                        {
                            ParseImage(data, (PictureName) Enum.Parse(typeof (PictureName), name),
                                       (PictureType) Enum.Parse(typeof (PictureType), type));
                        }
                    }
                }
            }

            zipInputStream.Close();
        }

        public String Name { get; set; }
        public String ShortName { get; set; }

        private void ParseImage(byte[] data, PictureName name, PictureType type)
        {
            Image img = Image.FromStream(new MemoryStream(data));
            if (!_images.ContainsKey(name))
            {
                _images.Add(name, new Dictionary<PictureType, Image>());
            }

            if (_images[name].ContainsKey(type))
            {
                _images[name][type] = img;
            }
            else
            {
                _images[name].Add(type, img);
            }
        }

        private void ParseLang(byte[] data)
        {
            var xmlTextReader = new XmlTextReader(new MemoryStream(data));
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlTextReader);

            foreach (XmlNode node in xmlDocument.ChildNodes)
            {
                if (node.Name == "language")
                {
                    XmlAttribute nameLang = node.Attributes["name"];
                    XmlAttribute shortNameLang = node.Attributes["short"];
                    if (nameLang == null || shortNameLang == null)
                    {
                        break;
                    }
                    Name = nameLang.Value;
                    ShortName = shortNameLang.Value;
                    foreach (XmlNode langNode in node.ChildNodes)
                    {
                        switch (langNode.Name)
                        {
                            case "words":
                                foreach (XmlNode wordNode in langNode.ChildNodes)
                                {
                                    if (wordNode.Name == "word")
                                    {
                                        if (wordNode.Attributes["name"] != null && wordNode.Attributes["val"] != null)
                                        {
                                            string name = wordNode.Attributes["name"].Value;
                                            string val = wordNode.Attributes["val"].Value;
                                            if (Enum.IsDefined(typeof (WordEnum), name))
                                            {
                                                var wordEnum = (WordEnum) Enum.Parse(typeof (WordEnum), name);

                                                if (!_words.ContainsKey(wordEnum))
                                                {
                                                    _words.Add(wordEnum, val);
                                                }
                                            }
                                        }
                                    }
                                }
                                break;
                        }
                    }
                }
            }

            xmlTextReader.Close();
        }

        public String GetWord(WordEnum enu)
        {
            if (!_words.ContainsKey(enu))
            {
                return "Not find: " + enu;
            }

            return _words[enu];
        }

        public Image GetImage(PictureName name, PictureType type)
        {
            return !_images.ContainsKey(name) ? null : (!_images[name].ContainsKey(type) ? null : _images[name][type]);
        }
    }
}