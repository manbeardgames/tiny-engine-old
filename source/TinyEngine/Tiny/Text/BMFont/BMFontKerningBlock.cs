using System;
using System.Xml.Serialization;

namespace Tiny
{
    [Serializable]
    public struct BMFontKerningBlock
    {
        [XmlAttribute("first")]
        public int First;

        [XmlAttribute("second")]
        public int Second;

        [XmlAttribute("amount")]
        public int Amount;
    }
}
