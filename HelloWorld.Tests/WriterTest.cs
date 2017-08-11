using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.QualityTools.Testing.Fakes;

namespace HelloWorld.Tests
{
    [TestClass]
    public class WriterTest
    {
        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void MobileWriter_Write_NotImplimented()
        {
            //arange
            Writer w = new MobileWriter();
            //act 
            w.Write("test");
            //assert
        }
        [TestMethod]
        public void DatabaseWriter_Write_NotImplimented()
        {
            //arange
            Writer w = new DatabaseWriter();
            //act 
            bool sucess = w.Write("test");
            //assert
            Assert.IsTrue(sucess);
        }

        [TestMethod]
        public void ConsoleWriter_Write()
        {
            //arange
            Writer w = new ConsoleWriter();
            //act 
            bool sucess = w.Write("test");
            //assert
            Assert.IsTrue(sucess);
        }

        [TestMethod]
        public void WriterFactory_ConsoleWriter()
        {
            Writer w = WriterFactory.Create(OutputDeviceType.Console);
            //assert
            Assert.IsInstanceOfType(w, typeof(ConsoleWriter));
        }

        [TestMethod]
        public void WriterFactory_DatabaseWriter()
        {
            Writer w = WriterFactory.Create(OutputDeviceType.Database);
            //assert
            Assert.IsInstanceOfType(w, typeof(DatabaseWriter));
        }

        [TestMethod]
        public void WriterFactory_MobileWriter()
        {
            Writer w = WriterFactory.Create(OutputDeviceType.Mobile);
            //assert
            Assert.IsInstanceOfType(w, typeof(MobileWriter));
        }

        [TestMethod]
        public void HelloWorldFactory_HelloWorldWriter()
        {
            //arange
            Writer w = WriterFactory.Create(OutputDeviceType.Console);
            //act            
            IHelloWriter writer = IHelloWorldWriterFactory.Create(w);

            //assert
            Assert.IsInstanceOfType(writer, typeof(HelloWorldWriter));
        }

        [TestMethod]
        public void HelloWorl_Write()
        {
            string ValueWritten = string.Empty;
            //arange
            Writer w = new HelloWorld.Fakes.StubWriter()
            {
                
                WriteString = (ValueToWrite) => 
                {

                    ValueWritten = ValueToWrite; 
                    return true; 
                }
            };
            IHelloWriter writer = new HelloWorldWriter(w);
            //act  
            writer.Write();

            //assert
            Assert.IsTrue(ValueWritten == "Hello World");
        }

        [TestMethod]
        public void Program_Console()
        {
            string ValueWritten = string.Empty;

            //arange
            Writer w = new HelloWorld.Fakes.StubWriter()
            {

                WriteString = (ValueToWrite) =>
                {

                    ValueWritten = ValueToWrite;
                    return true;
                }
            };

            using (ShimsContext.Create())
            {
                HelloWorld.Fakes.ShimAppSettings.AllInstances.OutputDeviceGet =
                    (a) => { return OutputDeviceType.Console.ToString(); };
                HelloWorld.Fakes.ShimWriterFactory.CreateOutputDeviceType = (o) =>
                {
                    return w;
                };   
                Program.Main(null);

            }

            //act 
            //assert
            Assert.IsTrue(ValueWritten == "Hello World");
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void Program_Console_Exception()
        {
            string ValueWritten = string.Empty;

            //arange
            Writer w = new HelloWorld.Fakes.StubWriter()
            {

                WriteString = (ValueToWrite) =>
                {

                    ValueWritten = ValueToWrite;
                    return true;
                }
            };

            using (ShimsContext.Create())
            {
                HelloWorld.Fakes.ShimAppSettings.AllInstances.OutputDeviceGet =
                    (a) => { return "asdf"; };
                HelloWorld.Fakes.ShimWriterFactory.CreateOutputDeviceType = (o) =>
                {
                    return w;
                };
                Program.Main(null);

            }
        }

    }
}
