using System;
using System.Xml.Serialization;

namespace Tiny
{
    [Serializable]
    public struct BMFontCommonBlock
    {
        [XmlAttribute("lineHeight")]
        public int LineHeight;

        [XmlAttribute("base")]
        public int Base;

        [XmlAttribute("scaleW")]
        public int ScaleW;

        [XmlAttribute("scaleH")]
        public int ScaleH;

        [XmlAttribute("pages")]
        public int Pages;

        [XmlAttribute("packed")]
        public int Packed;

        [XmlAttribute("alphaChnl")]
        public int AlphaChannel;

        [XmlAttribute("redChnl")]
        public int RedChannel;

        [XmlAttribute("greenChnl")]
        public int GreenChannel;

        [XmlAttribute("blueChnl")]
        public int BlueChannel;
    }
}
