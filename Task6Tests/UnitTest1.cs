using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task6;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Task6Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            int expectedCount = 1;
            MemoryStream stream = new MemoryStream();
            IEnumerable<LibraryResource> libResources;
            using (LibResRW libResRW = new LibResRW(stream))
            {
                libResRW.Write(new Book
                {
                    ISBN = "ISBN 978-1-4028-9462-6",
                    Title = "book 1;"
                });
                // сохранение в файл.
                libResRW.Flush();
                libResources = libResRW.Read();
            }
            Assert.AreEqual<int>(expectedCount, libResources.Count());
        }
    }
}
