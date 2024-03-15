using Azure.Core;
using System.IO;
using System.Net.Mime;

namespace Empresa.Backend.Helpers
{
    public class FileStorage 
    {
        private readonly string _rutaAlmacenamiento;
        public FileStorage(string rutaAlmacenamiento)
        {
            _rutaAlmacenamiento = rutaAlmacenamiento;

        }


       public void EliminarImagen(string nombre)
        {
            string rutaArchivo = Path.Combine(_rutaAlmacenamiento, nombre);
            if (File.Exists(rutaArchivo))
            {
                File.Delete(rutaArchivo);
            }
        }

       public bool ExisteImagen()
        {
            
            return File.Exists(_rutaAlmacenamiento);
        }

        public string CrearCarpetaAleatoria()
        {
            string fechaHoraActual = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            string nombreCarpeta = $"Carpeta_{fechaHoraActual}";

            string rutaCarpeta = Path.Combine(_rutaAlmacenamiento, nombreCarpeta);

            Directory.CreateDirectory(rutaCarpeta);

            return rutaCarpeta;
        }

        public string GuardarImagen(string nombre, byte[] imagen)
        {
            string rutaArchivo = Path.Combine(_rutaAlmacenamiento, nombre);
            File.WriteAllBytes(rutaArchivo, imagen);
            return rutaArchivo;


        }

        public byte[] ObtenerImagen()
        {    
            if (File.Exists(_rutaAlmacenamiento))
            {
                return File.ReadAllBytes(_rutaAlmacenamiento);
            }
            return null;
        }
        public  IFormFile ConvertirBytesAFormFile(byte[] bytes, string nombreArchivo)
        {
            var memoriaStream = new MemoryStream(bytes);
            var formFile = new FormFile(memoriaStream, 0, bytes.Length, nombreArchivo, nombreArchivo)
            {
                Headers=new HeaderDictionary(),
                ContentType="image/jpeg",
                ContentDisposition= $"form-data; name=\"file\"; filename=\"{nombreArchivo}\""


        };

            return formFile;
        }

        public static async Task<byte[]> ConvertIFormFileToBytesAsync(IFormFile file)
        {

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }

        }


    }
}
