using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace svg_memento.utils
{
    public class ObjectUtils
    {
        /// <summary>
        /// Depp clone
        /// Reference: https://stackoverflow.com/questions/129389/how-do-you-do-a-deep-copy-of-an-object-in-net
        /// </summary>
        /// <param name="obj"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T DeepClone<T>(T obj)
        {
            using var ms = new MemoryStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(ms, obj);
            ms.Position = 0;
            return (T) formatter.Deserialize(ms);
        }
    }
}