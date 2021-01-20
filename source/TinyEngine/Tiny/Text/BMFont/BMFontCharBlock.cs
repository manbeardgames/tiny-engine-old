using System;
using System.Xml.Serialization;

namespace Tiny
{
    [Serializable]
    public struct BMFontCharBlock
    {
        [XmlAttribute("id")]
        public int ID;

        [XmlAttribute("x")]
        public int X;

        [XmlAttribute("y")]
        public int Y;

        [XmlAttribute("width")]
        public int Width;

        [XmlAttribute("height")]
        public int Height;

        [XmlAttribute("xoffset")]
        public int XOffset;

        [XmlAttribute("yoffset")]
        public int YOffset;

        [XmlAttribute("xadvance")]
        public int XAdvance;

        [XmlAttribute("page")]
        public int Page;

        [XmlAttribute("chnl")]
        public int Channel;
    }
}
