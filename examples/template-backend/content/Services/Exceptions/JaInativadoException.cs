namespace Exceptions {
    [System.Serializable]
    public class JaInativadoException : System.Exception
    {
        public JaInativadoException() : base("O objeto especificado já foi inativado.") { }
        public JaInativadoException(string message) : base(message) { }
        public JaInativadoException(string message, System.Exception inner) : base(message, inner) { }
        protected JaInativadoException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}