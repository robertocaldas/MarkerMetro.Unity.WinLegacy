using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using MarkerMetro.Unity.WinLegacy.IO;
using Windows.Storage;
using System.Threading.Tasks;
using System.IO;

namespace MarkerMetro.Unity.WinLegacy.Reflection.Tests
{
    [TestClass]
    public class IOTests
    {

        [TestMethod]
        public void File_WriteAllText_RootFile_Success()
        {
            bool success = false;
            string error = String.Empty;
            var localFolder = ApplicationData.Current.LocalFolder;
            string path = Path.Combine(localFolder.Path, @"File.txt");
            try
            {
                File.WriteAllText(path, DateTime.Now.Ticks.ToString());
                success = true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void File_WriteAllText_SubFoldersAndFile_Success()
        {
            bool success = false;
            string error = String.Empty;
            var localFolder = ApplicationData.Current.LocalFolder;
            string path = Path.Combine(localFolder.Path, @"Test\Test2\Test3\Test4\Test5\File.txt");
            try
            {
                File.WriteAllText(path, DateTime.Now.Ticks.ToString());
                success = true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            Assert.IsTrue(success);
        }

        abstract class BaseClass
        {
            public string BaseProperty { get; set; }
        }

        class DerivedClass : BaseClass
        {
            public string DerivedProperty { get; set; }
        }

    }
}
