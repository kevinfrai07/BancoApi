namespace Banco.Domain.Exceptions
{
    /// <summary>
    /// Clase de tipo exception personalizada
    /// </summary>
    public class BancoException : Exception
    {
        /// <summary>
        ///Constructores para excepcion
        /// </summary>
        public BancoException() : base()
        {
        }

        public BancoException(string message) : base(message)
        {
        }

        public BancoException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}