using Adventure_Works_Desktop_App;
using Adventure_Works_Desktop_App.Globals;

namespace Adventure_Works_Desktop_App_Tests
{
    [TestClass]
    public sealed class TestLogin
    {
        [TestMethod]
        public void TestBadValidation()
        {
            LoginBackend backend = new LoginBackend();
            string testUsername = "admin";
            string testPassword = "123";

            bool test = backend.ValidateCredentials(testUsername, testPassword);

            Assert.IsFalse(test);
        }

        [TestMethod]
        public void TestGoodValidation()
        {
            LoginBackend backend = new LoginBackend();
            string testUsername = "admin";
            string testPassword = "password";

            bool test = backend.ValidateCredentials(testUsername, testPassword);
            
            Assert.IsTrue(test);
        }

        [TestMethod]
        public void TestBadDisplayName()
        { 
            LoginBackend backend = new LoginBackend();
            string testUsername = "admin";
            string testPassword = "password";
            string displayName = "Esque";

            if (!backend.ValidateCredentials(testUsername, testPassword))
            {
                Assert.Fail();
            }

            Assert.AreNotEqual(displayName, backend.UserDisplayName);
        }

        [TestMethod]
        public void TestCorrectDisplayName()
        {
            LoginBackend backend = new LoginBackend();
            string testUsername = "admin";
            string testPassword = "password";
            string displayName = "admin";

            if (!backend.ValidateCredentials(testUsername, testPassword))
            {
                Assert.Fail();
            }

            Assert.AreEqual(displayName, backend.UserDisplayName);
        }
    }
}
