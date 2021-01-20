using System;
using System.Xml.Serialization;

namespace Tiny
{
    [Serializable]
    public struct BMFontInfoBlock
    {
        [XmlAttribute("face")]
        public string Face;

        [XmlAttribute("size")]
        public int Size;

        [XmlAttribute("bold")]
        public int Bold;

        [XmlAttribute("italic")]
        public int Italic;

        [XmlAttribute("charset")]
        public string CharSet;

        [XmlAttribute("unicode")]
        public int Unicode;

        [XmlAttribute("stretchH")]
        public int StretchHeight;

        [XmlAttribute("smooth")]
        public int Smooth;

        [XmlAttribute("aa")]
        public int SuperSampling;

        [XmlAttribute("padding")]
        public string Padding;

        [XmlAttribute("spacing")]
        public string Spacing;

        [XmlAttribute("outline")]
        public int Outline;
    }
}
