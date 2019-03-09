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
        XDocument settings;
        string executingAssemblyName;
        ClassFactory classFactory;

        [TestInitialize()]
        public void TestInitialization()
        {
            executingAssemblyName = System.Reflection.Assembly.GetExecutingAssembly().FullName;
            settings = XDocument.Parse(@"<?xml version = ""1.0"" encoding = ""utf-8"" ?>
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
            classFactory = new ClassFactory(settings, executingAssemblyName);
        }

        [TestMethod()]
        [TestCategory("Create")]
        public void CreateFunctionWithParamsCreatesCorrectObject()
        {
            int integerParam = 7;
            string textParam = "TEST_STRING";
            ITestClassWithParams etalon_object = new TestClassWithParams(integerParam, textParam);
            ITestClassWithParams created_object = classFactory.Create<ITestClassWithParams>(integerParam, textParam);

            Assert.ReferenceEquals(etalon_object, created_object);
        }

        [TestMethod()]
        [TestCategory("Create")]
        public void CreateFunctionWithoutParamsCreatesCorrectObject()
        {
            ITestClassNoParams etalon_object = new TestClassNoParams();
            ITestClassNoParams created_object = classFactory.Create<ITestClassNoParams>();

            Assert.ReferenceEquals(etalon_object, created_object);
        }

        [TestMethod()]
        [TestCategory("Create")]
        public void CreateFunctionWithParams_Object_PropertyDeepCheck()
        {
            int integerParam = 7;
            string textParam = "TEST_STRING";
            ITestClassWithParams etalon_object = new TestClassWithParams(integerParam, textParam);
            ITestClassWithParams created_object = classFactory.Create<ITestClassWithParams>(integerParam, textParam);

            Assert.AreEqual(etalon_object.integer, created_object.integer);
            Assert.AreEqual(etalon_object.text, created_object.text);
        }

        [TestMethod()]
        [TestCategory("Create")]
        public void CreateFunctionWithoutParams_Object_PropertyDeepCheck()
        {
            ITestClassNoParams etalon_object = new TestClassNoParams();
            ITestClassNoParams created_object = classFactory.Create<ITestClassNoParams>();

            Assert.AreEqual(etalon_object.testFunction(), created_object.testFunction());
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