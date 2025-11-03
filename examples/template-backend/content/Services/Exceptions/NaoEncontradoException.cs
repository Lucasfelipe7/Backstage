namespace Exceptions {
    [System.Serializable]
    public class NaoEncontradoException : System.Exception
    {
        public NaoEncontradoException() : base("O objeto especificado não foi encontrado.") { }
        public NaoEncontradoException(string message) : base(message) { }
        public NaoEncontradoException(string message, System.Exception inner) : base(message, inner) { }
        protected NaoEncontradoException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}