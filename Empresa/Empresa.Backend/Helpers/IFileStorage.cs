
namespace Empresa.Backend.Helpers
{
    public interface IFileStorage
    {

        
        /// <summary>
        /// Guarda una imagen en el almacenamiento local.
        /// </summary>
        /// <param name="nombre">Nombre del archivo de la imagen.</param>
        /// <param name="imagen">Bytes de la imagen.</param>
         void GuardarImagen(string nombre, byte[] imagen);

        /// <summary>
        /// Obtiene una imagen del almacenamiento local.
        /// </summary>
        /// <param name="nombre">Nombre del archivo de la imagen.</param>
        /// <returns>Bytes de la imagen o null si no existe.</returns>
        byte[] ObtenerImagen(string nombre);

        /// <summary>
        /// Elimina una imagen del almacenamiento local.
        /// </summary>  
        /// <param name="nombre">Nombre del archivo de la imagen.</param>
        void EliminarImagen(string nombre);

        /// <summary>
        /// Comprueba si una imagen existe en el almacenamiento local.
        /// </summary>
        /// <param name="nombre">Nombre del archivo de la imagen.</param>
        /// <returns>True si la imagen existe, false en caso contrario.</returns>
        bool ExisteImagen(string nombre);



    }
}
