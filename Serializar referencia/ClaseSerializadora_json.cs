using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SerializacionJson
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
            string completa = ruta + @"\SerializadoraJson_" + archivo + ".json";

            try
            {
                if (!Directory.Exists(ruta))
                {
                    Directory.CreateDirectory(ruta);
                }
                string objetoJson = JsonSerializer.Serialize(datos);

                File.WriteAllText(completa, objetoJson); 
            }
            catch (Exception e)
            {
                throw new Exception($"Error en el archivo {completa}");
            }
        }

        public static T Leer(string nombre)
        {
            string archivo = string.Empty;
            string completo = @"\SerializadoraJson_" + nombre + ".json";

            T datos = default;

            try
            {
                if (Directory.Exists(ruta))
                {
                    string[] archivosEnRuta = Directory.GetFiles(ruta);

                    foreach (string archivoEnRuta in archivosEnRuta)
                    {
                        if(archivoEnRuta.Contains(completo))
                        {
                            archivo = archivoEnRuta;
                            break;
                        }
                    }

                    if (archivo != null)
                    {
                        string archivoJson = File.ReadAllText(archivo);
                        datos = (T)JsonSerializer.Deserialize<T>(archivoJson);//casteo
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
