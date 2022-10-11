using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SerializacionXml
{
    
    public static class ClaseSerializadora
    {
        static string ruta;
        static ClaseSerializadora()
        {
            ruta = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);//establece ruta de escritorio
            ruta += @"\Archivos-serializacion";
        }

        public static void Escribir(Personaje pj)
        {
            string completa = ruta + @"\Serializadora_" + DateTime.Now.ToString("HH_mm_ss") + ".xml";

            try
            {
                if(!Directory.Exists(ruta))
                {
                    Directory.CreateDirectory(ruta);
                }

                using (StreamWriter sw = new StreamWriter(completa))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(Personaje));
                    xmlSerializer.Serialize(sw, pj);
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Error en el archivo {completa}");
            }
        }

        public static void EscribirLista(List<Personaje> pjs)
        {
            //string completa = ruta + @"\Serializadora_" + DateTime.Now.ToString("HH_mm_ss") + ".xml";
            string completa = ruta + @"\Serializadora_lista.xml";

            try
            {
                if (!Directory.Exists(ruta))
                {
                    Directory.CreateDirectory(ruta);
                }

                using (StreamWriter sw = new StreamWriter(completa))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Personaje>));
                    xmlSerializer.Serialize(sw, pjs);
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Error en el archivo {completa}");
            }
        }

        public static Personaje Leer()
        {
            string completa = ruta + @"\Serializadora.xml";

            Personaje pj = null;

            try
            {
                if (!Directory.Exists(ruta))
                {
                    Directory.CreateDirectory(ruta);
                }

                using (StreamReader sr = new StreamReader(completa))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(Personaje));

                    pj = (Personaje)xmlSerializer.Deserialize(sr);//casteo
                }

                return pj;
            }
            catch (Exception e)
            {
                throw new Exception($"Error en el archivo {completa}");
            }
        }

        public static List<Personaje> LeerLista()
        {
            string completa = ruta + @"\Serializadora_lista.xml";

            List<Personaje> pj = null;

            try
            {
                if (!Directory.Exists(ruta))
                {
                    Directory.CreateDirectory(ruta);
                }

                using (StreamReader sr = new StreamReader(completa))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Personaje>));

                    pj = (List<Personaje>)xmlSerializer.Deserialize(sr);//casteo
                }

                return pj;
            }
            catch (Exception e)
            {
                throw new Exception($"Error en el archivo {completa}");
            }
        }
    }
}
