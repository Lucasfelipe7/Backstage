namespace Exceptions {
    [System.Serializable]
    public class NaoInativadoException : System.Exception
    {
        public NaoInativadoException() : base("O objeto especificado não está inativado.") { }
        public NaoInativadoException(string message) : base(message) { }
        public NaoInativadoException(string message, System.Exception inner) : base(message, inner) { }
        protected NaoInativadoException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}