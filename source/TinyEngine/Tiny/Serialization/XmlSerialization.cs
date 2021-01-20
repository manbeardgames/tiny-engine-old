using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Tiny
{
    public static class Xml
    {
        /// <summary>
        ///     Loads a <see cref="XmlDocument"/> from the the file at the given
        ///     <paramref name="filepath"/> value.
        /// </summary>
        /// <param name="filepath">
        ///     A <see cref="string"/> value that contains the fully qualified
        ///     absolute path to the <c>.xml</c> file to load.
        /// </param>
        /// <returns>
        ///     A <see cref="XmlDocument"/> instance of the loaded file.
        /// </returns>
        public static XmlDocument LoadXmlDocument(string filepath)
        {
            XmlDocument xml = new XmlDocument();

            using (FileStream fileStram = File.OpenRead(filepath))
            {
                xml.Load(fileStram);
            }

            return xml;
        }

        /// <summary>
        ///     Save an object of type <typeparamref name="T"/> to disk as XML.
        /// </summary>
        /// <typeparam name="T">
        ///     The <c>type</c> of the object being saved.
        /// </typeparam>
        /// <param name="obj">
        ///     The object of type <typeparamref name="T"/> to save.
        /// </param>
        /// <param name="filename">
        ///     A <see cref="string"/> value contining the fully qualified path to the
        ///     file to write to.  If the file does not exist, it will be created. If the
        ///     file arleady exists, it will be overwritten.
        /// </param>
        /// <returns>
        ///     <c>true</c> if serialzing and saving the object to disk is successful;
        ///     otherwise, <c>false</c>.
        /// </returns>
        public static bool SerializeAs<T>(T obj, string filename) where T : new()
        {
            bool success = false;
            FileStream fileStream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None);

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(fileStream, obj);
                success = true;
            }
            catch
            {
                success = false;
            }
            finally
            {
                fileStream.Close();
            }

            return success;
        }

        /// <summary>
        ///     Loads an object of type <typeparamref name="T"/> from an XML file.
        /// </summary>
        /// <typeparam name="T">
        ///     The <c>type</c> of the object being loaded.
        /// </typeparam>
        /// <param name="fileName">
        ///     A <see cref="string"/> value contining the fully qualified path to the
        ///     file to read.
        /// </param>
        /// <param name="obj">
        ///     When this method returns, contains the result of deserializing the XML file
        ///     to an object of type <typeparamref name="T"/>
        /// </param>
        /// <returns>
        ///     <c>true</c> if loading the file and deserializing it is successful;
        ///     otherwise, <c>false</c>.
        /// </returns>
        public static bool DeserializeAs<T>(string fileName, out T obj) where T : new()
        {
            bool success = false;
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);

            try
            {
                obj = default(T);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                obj = (T)serializer.Deserialize(fileStream);
                success = true;
            }
            catch (Exception ex)
            {
                obj = default(T);
                success = false;
            }
            finally
            {
                fileStream.Close();
            }

            return success;
        }
    }
}
