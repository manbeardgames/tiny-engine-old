using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Tiny
{
    [Serializable]
    [XmlRoot("font")]
    public struct BMFontFile
    {
        [XmlElement("info")]
        public BMFontInfoBlock Info;

        [XmlElement("common")]
        public BMFontCommonBlock Common;

        [XmlArray("pages")]
        [XmlArrayItem("page")]
        public List<BMFontPageBlock> Pages;

        [XmlArray("chars")]
        [XmlArrayItem("char")]
        public List<BMFontCharBlock> Chars;

        [XmlArray("kernings")]
        [XmlArrayItem("kerning")]
        public List<BMFontKerningBlock> Kernings;
    }
}
