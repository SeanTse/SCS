using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using demoCore.MVVM.Model;

namespace demoCore.Tests
{
    [TestClass()]
    public class MainTests
    {
        [TestMethod()]
        public void XmlLoadTest()
        {
            Analysis analysis = new("steiner");
            analysis.LoadFromXml("Steiner.xml");
            Assert.AreEqual(analysis.LandmarkCount, 3);
        }
    }
}