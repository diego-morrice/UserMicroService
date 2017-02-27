using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleInjector;
using Infrastructure.CrossCutting.InversionOfControl;
using Domain.User.Interfaces.Services;
using Domain.User.Entities;
using Infrastructure.CrossCutting.Tools.Extensions;

namespace DomainUnitTest
{
    [TestClass]
    public class UserServiceTest
    {
        private IUserService _userService;
        private Container _container;

        [TestInitialize]
        public void Init()
        {
            _container = IoC.Container;
            _container.BeginLifetimeScope();
            _userService = IoC.GetInstance<IUserService>();
        }

        [TestMethod]
        public void SignIn_User_Valid()
        {
            string autenticateToken = null;
            var _user = new User("Diego Morrice", "dlima@mail.com", "tokenfacebook", null, null, null);
            var validation = _userService.SignIn(_user, out autenticateToken);
            Assert.IsTrue(validation.IsValid && autenticateToken.IsNotNullNorWhiteSpace());
        }

        [TestMethod]
        public void SignIn_User_Anonymous_Or_Guest_Valid()
        {
            string autenticateToken = null;
            var _user = new User(null, null, null, null, null, null, true);
            var validation = _userService.SignIn(_user, out autenticateToken);
            Assert.IsTrue(validation.IsValid && autenticateToken.IsNotNullNorWhiteSpace());
        }        

        [TestMethod]        
        public void SignIn_User_Invalid_No_Facebook_Or_Google_Token()
        {
            string autenticateToken = null;
            var _user = new User("Diego Morrice", "dlimatwo@mail.com", null, null, null, null);
            var validation = _userService.SignIn(_user, out autenticateToken);
            Assert.IsFalse(validation.IsValid && autenticateToken.IsNull());
        }

        [TestMethod]
        public void SignIn_User_Invalid_Null_User()
        {
            string autenticateToken = null;
            var _user = new User(null, null, null, null, null, null);
            var validation = _userService.SignIn(_user, out autenticateToken);
            Assert.IsFalse(validation.IsValid && autenticateToken.IsNull());
        }
    }
}
