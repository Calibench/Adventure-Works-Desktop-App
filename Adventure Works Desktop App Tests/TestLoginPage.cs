using Moq;
using Adventure_Works_Desktop_App.Globals;
using Adventure_Works_Desktop_App.Login.FrontEnd;

namespace Adventure_Works_Desktop_App_Tests
{
    [TestClass]
    public sealed class TestLoginPage
    {
        [TestMethod]
        public void TestLogin()
        {
            Mock<LoginForm> myMock = new Mock<LoginForm>();
            myMock.CallBase = true;
 
            var signupButtonClickMethod = typeof(LoginForm).GetMethod("loginButton_Click", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            Assert.IsNotNull(signupButtonClickMethod, "loginButton_Click method not found.");

            signupButtonClickMethod.Invoke(myMock.Object, new object[] { new object(), EventArgs.Empty });
        }
    }
}
