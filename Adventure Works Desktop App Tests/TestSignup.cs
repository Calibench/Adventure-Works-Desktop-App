using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adventure_Works_Desktop_App.Globals;
using Adventure_Works_Desktop_App.SignUpPage.Backend;

namespace Adventure_Works_Desktop_App_Tests
{
    [TestClass]
    public sealed class TestSignup
    {
        [TestMethod]
        public void TestFailCheckUnique()
        {
            AccountData data = new AccountData("admin", "password");

            bool check = new SignUpBackend().CheckUnique(data);

            Assert.IsFalse(check);
        }

        [TestMethod]
        public void TestPassCheckUnique()
        {
            AccountData data = new AccountData("Esque", "password");

            bool check = new SignUpBackend().CheckUnique(data);

            Assert.IsTrue(check);
        }
    }
}
