using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (Enum.IsDefined(typeof(OutputDeviceType),  AppSettings.Settings.OutputDevice))
            {
                OutputDeviceType type;
                Enum.TryParse<OutputDeviceType>(AppSettings.Settings.OutputDevice, out type);

                Writer writer = WriterFactory.Create(type);

                IHelloWriter hwriter = IHelloWorldWriterFactory.Create(writer);
                hwriter.Write();

            }
            else
            {
                throw new ApplicationException(string.Format("Unable to Output to {0}.", AppSettings.Settings.OutputDevice));
            }
        }
    }

    public interface IHelloWriter
    {
        void Write();
    }

    /// <summary>
    /// Class that uses the Writer class and write hello world using the writer.
    /// </summary>
    public class HelloWorldWriter : IHelloWriter
    {
        private Writer _writer;
        public HelloWorldWriter(Writer writer)
        {
            _writer = writer;
        }

        public void Write()
        {
            bool sucess = _writer.Write("Hello World");
        }
    }

    /// <summary>
    /// factory used to create the IHelloWriter objects
    /// </summary>
    public static class IHelloWorldWriterFactory
    {
        public static IHelloWriter Create(Writer Writer)
        {
            return new HelloWorldWriter(Writer);
        }
    }

    /// <summary>
    /// factory used to create the Writer objects
    /// </summary>
    public static class WriterFactory
    {
        public static Writer Create(OutputDeviceType device)
        {
            switch (device)
            {
                case OutputDeviceType.Console:
                    return new ConsoleWriter();
                case OutputDeviceType.Database:
                    return new DatabaseWriter();
                case OutputDeviceType.Mobile:
                    return new MobileWriter();
                default:
                    throw new NotImplementedException(string.Format("Output device type of {0} is not supported.", device));
            }
        }
    }

    public enum OutputDeviceType
    {
        Console,
        Database,
        Mobile
    }
    public abstract class Writer
    {
        abstract public bool Write(string ValueToWrite);
    }

    /// <summary>
    /// Class that writes to the console
    /// </summary>
    public class ConsoleWriter : Writer
    {
        public override bool Write(string ValueToWrite)
        {
            Console.WriteLine(ValueToWrite);
            return true;
        }
    }
    /// <summary>
    /// class that writes to a mobile device
    /// </summary>

    public class MobileWriter : Writer
    {

        public override bool Write(string ValueToWrite)
        {
            throw new NotImplementedException();
        }
    }
    /// <summary>
    /// class that writes to a database
    /// </summary>
    public class DatabaseWriter : Writer
    {

        public DatabaseWriter(){}
        public override bool Write(string ValueToWrite)
        {
            //write to database
            return true;
        }
    }

}



