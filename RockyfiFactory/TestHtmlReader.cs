using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;

namespace RockyfiFactory
{
    class TestHtmlReader
    {
        public static byte[] GenerateByteFromMemory(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (var memstream = new MemoryStream())
            {
                stream.CopyTo(memstream);
                return memstream.ToArray();
            }
        }

        public static StreamReader GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return new StreamReader(stream);
        }

        public static StreamReader GenerateFromEmbed(string path)
        {
            return GenerateStreamFromString(System.Text.UTF8Encoding.UTF8.GetString(GenerateByteFromMemory(path)));
        }

        public static void CLL(string path)
        {
            Console.WriteLine("---------------------------------------");
            foreach (var node in Rockyfi.HtmlReader.Parse(GenerateFromEmbed(path)))
            {
                Console.WriteLine(node.ToString());
            }
            Console.WriteLine("--------------xxxxx-------------------------");
        }

        public void Test()
        {
            //CLL("RockyfiFactory.doc.Atom_Example.xml");
            //CLL("RockyfiFactory.doc.Attributes.html");
            //CLL("RockyfiFactory.doc.RDF_Example.xml");
            //CLL("RockyfiFactory.doc.RSS_Example.xml");
            CLL("RockyfiFactory.doc.Svg.html");

            Console.ReadKey();
        }
    }
}
