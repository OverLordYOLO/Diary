using Microsoft.VisualStudio.TestTools.UnitTesting;
using Diary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Diary.Tests
{
    [TestClass()]
    public class ClassFactoryTests
    {
        string XMLSettings = @"<?xml version = ""1.0"" encoding = ""utf-8"" ?>
                    <root>
                        <pair>  
                            <interface>ITestClassWithParams</interface>
                            <class>TestClassWithParams</class>
                        </pair>
                        <pair>  
                            <interface>ITestClassNoParams</interface>
                            <class>TestClassNoParams</class>
                        </pair>
                    </root>".Replace(Environment.NewLine, "");
        XDocument settings = XDocument.Parse(@"<?xml version = ""1.0"" encoding = ""utf-8"" ?>
                    <root>
                        <pair>  
                            <project>Diary.Tests</project>
                            <interface>ITestClassWithParams</interface>
                            <class>TestClassWithParams</class>
                        </pair>
                        <pair>  
                            <project>Diary.Tests</project>
                            <interface>ITestClassNoParams</interface>
                            <class>TestClassNoParams</class>
                        </pair>
                    </root>".Replace(Environment.NewLine, ""));
        string executingAssemblyName = System.Reflection.Assembly.GetExecutingAssembly().FullName;

        [TestMethod()]
        [TestCategory("Create")]
        public void AssertCreateFunctionWithParamsCreatesCorrectObject()
        {
            ClassFactory classFactory = new ClassFactory(settings, executingAssemblyName);
            int integerParam = 7;
            string textParam = "TEST_STRING";
            ITestClassWithParams etalon_object = new TestClassWithParams(integerParam, textParam);
            ITestClassWithParams created_object = classFactory.Create<ITestClassWithParams>(integerParam, textParam);

            Assert.IsInstanceOfType((TestClassWithParams)created_object, typeof(TestClassWithParams));
            Assert.AreEqual<ITestClassWithParams>(created_object, etalon_object);


        }

        [TestMethod()]
        [TestCategory("Create")]
        public void AssertCreateFunctionWithoutParamsCreatesCorrectObject()
        {
            ClassFactory classFactory = new ClassFactory(settings, executingAssemblyName);

            ITestClassNoParams etalon_object = new TestClassNoParams();
            ITestClassNoParams created_object = classFactory.Create<ITestClassNoParams>();


            Assert.IsInstanceOfType((TestClassNoParams)created_object, typeof(TestClassNoParams));
            Assert.AreEqual<ITestClassNoParams>(created_object, etalon_object);
        }
    }
    /// <summary>
    /// Helper classes and interfaces
    /// </summary>
    /// 

    public interface ITestClassWithParams
    {
        int integer { get; }
        string text { get; }

    }
    public interface ITestClassNoParams
    {
        string testFunction();
    }
    public class TestClassWithParams : ITestClassWithParams
    {
        public int integer { get; private set; }
        public string text { get; private set; }
        public TestClassWithParams(int integer, string text)
        {
            this.integer = integer;
            this.text = text;
        }
    }
    public class TestClassNoParams : ITestClassNoParams
    {
        public string testFunction()
        {
            return "TEST_STRING";
        }
    }
}