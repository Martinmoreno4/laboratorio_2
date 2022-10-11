using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SerializacionXml_Herencia
{
    public static class ClaseSerializadora<T>
    {
        static string ruta;
        static ClaseSerializadora()
        {
            ruta = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);//establece ruta de escritorio
            ruta += @"\Archivos-serializacion";
        }

        public static void Escribir(T datos, string archivo)
        {
            //string completa = ruta + @"\Serializadora_" + archivo + DateTime.Now.ToString("HH_mm_ss") + ".xml";
            string completa = ruta + @"\Serializadora_" + archivo + ".xml";

            try
            {
                if (!Directory.Exists(ruta))
                {
                    Directory.CreateDirectory(ruta);
                }

                using (StreamWriter sw = new StreamWriter(completa))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                    xmlSerializer.Serialize(sw, datos);
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Error en el archivo {completa}");
            }
        }

        public static T Leer(string nombre)
        {
            string archivo = string.Empty;

            T datos = default;

            try
            {
                if (Directory.Exists(ruta))
                {
                    string[] archivosEnRuta = Directory.GetFiles(ruta);

                    foreach (string archivoEnRuta in archivosEnRuta)
                    {
                        if(archivoEnRuta.Contains(nombre))
                        {
                            archivo = archivoEnRuta;
                            break;
                        }
                    }

                    if (archivo != null)
                    {
                        using (StreamReader sr = new StreamReader(archivo))
                        {
                            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

                            datos = (T)xmlSerializer.Deserialize(sr);//casteo
                        }
                    }
                }
                return datos;

            }
            catch (Exception e)
            {
                throw new Exception($"Error en el archivo {archivo}");
            }
        }
    }
}
