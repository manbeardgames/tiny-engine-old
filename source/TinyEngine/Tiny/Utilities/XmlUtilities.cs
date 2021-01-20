using System;
using System.Xml;

namespace Tiny
{
    public static class XmlUtilities
    {
        /// <summary>
        ///     Returns a value that indicates if the <see cref="XmlElement"/> contains a
        ///     child <see cref="XmlElement"/> with the given <paramref name="childElementName"/> value.
        /// </summary>
        /// <param name="element">
        ///     The <see cref="XmlElement"/> to check.
        /// </param>
        /// <param name="childElementName">
        ///     A <see cref="string"/> value that contains the name of the child element to validate.
        /// </param>
        /// <returns>
        ///     
        /// </returns>
        public static bool ContainsChildElement(this XmlElement element, string childElementName)
        {
            return element[childElementName] != null;
        }

        /// <summary>
        ///     Returns a value that indicates if the <see cref="XmlElement"/> contains
        ///     an attibute with the <paramref name="attributeName"/> value provided.
        /// </summary>
        /// <param name="element">
        ///     The <see cref="XmlElement"/> to check.
        /// </param>
        /// <param name="attributeName">
        ///     A <see cref="string"/> value that defines the name of the attribute
        ///     to check for.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the given element has an attribute with the given name;
        ///     otherwise; <c>false</c>.
        /// </returns>
        public static bool ContainsAttribute(this XmlElement element, string attributeName)
        {
            return element.Attributes[attributeName] != null;
        }

        /// <summary>
        ///     Returns the value of an XmlAttribute as a <see cref="string"/> value type.
        /// </summary>
        /// <param name="element">
        ///     The <see cref="XmlElement"/> that contains the attribute value.
        /// </param>
        /// <param name="attributeName">
        ///     A <see cref="string"/> value that defines the anme of the attribute to
        ///     pull the result from.
        /// </param>
        /// <returns>
        ///     The value of the XmlAttribute as a <see cref="string"/> value type.
        /// </returns>
        /// <exception cref="Exception">
        ///     Thrown if the <see cref="XmlElement"/> does not contain a <see cref="XmlAttribute"/>
        ///     with the name provided in <paramref name="attributeName"/>.
        /// </exception>
        public static string GetStringAttribute(this XmlElement element, string attributeName)
        {
            if (!element.ContainsAttribute(attributeName))
            {
                throw new Exception($"The element {element.Name} does not contain an attribute named {attributeName}");
            }

            return element.Attributes[attributeName].InnerText;
        }


        /// <summary>
        ///     Returns the value of an XmlAttribute as a <see cref="string"/> value type.
        /// </summary>
        /// <param name="element">
        ///     The <see cref="XmlElement"/> that contains the attribute value.
        /// </param>
        /// <param name="attributeName">
        ///     A <see cref="string"/> value that defines the anme of the attribute to
        ///     pull the result from.
        /// </param>
        /// <param name="defaultValue">
        ///     A <see cref="string"/> value that defines the default value to return if the
        ///     <see cref="XmlElement"/> does not contain a <see cref="XmlAttribute"/> with
        ///     the name provided in <paramref name="attributeName"/>
        /// </param>
        /// <returns>
        ///     The value of the XmlAttribute as a <see cref="int"/> value type; if the
        ///     XmlAttribute exists; otherwise, the default value is returned.
        /// </returns>
        public static string GetStringAttribute(this XmlElement element, string attributeName, string defaultValue)
        {
            if (!element.ContainsAttribute(attributeName))
            {
                return defaultValue;
            }

            return element.Attributes[attributeName].InnerText;
        }

        /// <summary>
        ///     Returns the value of an XmlAttribute as a <see cref="int"/> value type.
        /// </summary>
        /// <param name="element">
        ///     The <see cref="XmlElement"/> that contains the attribute value.
        /// </param>
        /// <param name="attributeName">
        ///     A <see cref="string"/> value that defines the anme of the attribute to
        ///     pull the result from.
        /// </param>
        /// <returns>
        ///     The value of the XmlAttribute as a <see cref="int"/> value type.
        /// </returns>
        /// <exception cref="Exception">
        ///     Thrown if the <see cref="XmlElement"/> does not contain a <see cref="XmlAttribute"/>
        ///     with the name provided in <paramref name="attributeName"/>.
        /// </exception>
        public static int GetIntAttribute(this XmlElement element, string attributeName)
        {
            if (!element.ContainsAttribute(attributeName))
            {
                throw new Exception($"The element {element.Name} does not contain an attribute named {attributeName}");
            }

            return Convert.ToInt32(element.Attributes[attributeName].InnerText);
        }


        /// <summary>
        ///     Returns the value of an XmlAttribute as a <see cref="int"/> value type.
        /// </summary>
        /// <param name="element">
        ///     The <see cref="XmlElement"/> that contains the attribute value.
        /// </param>
        /// <param name="attributeName">
        ///     A <see cref="string"/> value that defines the anme of the attribute to
        ///     pull the result from.
        /// </param>
        /// <param name="defaultValue">
        ///     A <see cref="int"/> value that defines the default value to return if the
        ///     <see cref="XmlElement"/> does not contain a <see cref="XmlAttribute"/> with
        ///     the name provided in <paramref name="attributeName"/>
        /// </param>
        /// <returns>
        ///     The value of the XmlAttribute as a <see cref="int"/> value type; if the
        ///     XmlAttribute exists; otherwise, the default value is returned.
        /// </returns>
        public static int GetIntAttribute(this XmlElement element, string attributeName, int defaultValue)
        {
            if (!element.ContainsAttribute(attributeName))
            {
                return defaultValue;
            }

            return Convert.ToInt32(element.Attributes[attributeName].InnerText);
        }

        /// <summary>
        ///     Returns the value of an XmlAttribute as a <see cref="float"/> value type.
        /// </summary>
        /// <param name="element">
        ///     The <see cref="XmlElement"/> that contains the attribute value.
        /// </param>
        /// <param name="attributeName">
        ///     A <see cref="string"/> value that defines the anme of the attribute to
        ///     pull the result from.
        /// </param>
        /// <returns>
        ///     The value of the XmlAttribute as a <see cref="float"/> value type.
        /// </returns>
        /// <exception cref="Exception">
        ///     Thrown if the <see cref="XmlElement"/> does not contain a <see cref="XmlAttribute"/>
        ///     with the name provided in <paramref name="attributeName"/>.
        /// </exception>
        public static float GetFloatAttribute(this XmlElement element, string attributeName)
        {
            if (!element.ContainsAttribute(attributeName))
            {
                throw new Exception($"The element {element.Name} does not contain an attribute named {attributeName}");
            }

            return Convert.ToSingle(element.Attributes[attributeName].InnerText);
        }


        /// <summary>
        ///     Returns the value of an XmlAttribute as a <see cref="float"/> value type.
        /// </summary>
        /// <param name="element">
        ///     The <see cref="XmlElement"/> that contains the attribute value.
        /// </param>
        /// <param name="attributeName">
        ///     A <see cref="string"/> value that defines the anme of the attribute to
        ///     pull the result from.
        /// </param>
        /// <param name="defaultValue">
        ///     A <see cref="float"/> value that defines the default value to return if the
        ///     <see cref="XmlElement"/> does not contain a <see cref="XmlAttribute"/> with
        ///     the name provided in <paramref name="attributeName"/>
        /// </param>
        /// <returns>
        ///     The value of the XmlAttribute as a <see cref="float"/> value type; if the
        ///     XmlAttribute exists; otherwise, the default value is returned.
        /// </returns>
        public static float GetFloatAttribute(this XmlElement element, string attributeName, float defaultValue)
        {
            if (!element.ContainsAttribute(attributeName))
            {
                return defaultValue;
            }

            return Convert.ToSingle(element.Attributes[attributeName].InnerText);
        }
    }
}
