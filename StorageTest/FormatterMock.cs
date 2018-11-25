using System.IO;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace StorageTest {
    public class FormatterMock : IFormatter {
        public SerializationBinder Binder { get; set; }
        public StreamingContext Context { get; set; }
        public Stream Stream { get; set; }
        public object ToSerialize { get; set; }
        public object Output { get; set; }
        ISurrogateSelector IFormatter.SurrogateSelector { get; set; }
        public bool ThrowException { get; set; }
        public int WaitMs { get; set; }

        public FormatterMock() { }

        public object Deserialize(Stream serializationStream) {
            if (ThrowException)
                throw new System.Exception();
            if (WaitMs > 0)
                Task.Delay(WaitMs);
            Stream = serializationStream;
            return Output;
        }

        public void Serialize(Stream serializationStream, object graph) {
            if (ThrowException)
                throw new System.Exception();
            if (WaitMs > 0)
                Task.Delay(WaitMs);
            Stream = serializationStream;
            ToSerialize = graph;
        }
    }
}
