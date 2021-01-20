using System;
using System.Xml.Serialization;

namespace Tiny
{
    [Serializable]
    public struct BMFontPageBlock
    {
        [XmlAttribute("id")]
        public int ID;

        [XmlAttribute("file")]
        public string File;
    }
}
