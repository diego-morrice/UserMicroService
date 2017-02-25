using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.User.Entities;

namespace DomainUnitTest
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void Create_Valid_User_Expected_Result()
        {            
            var address = new Address("Rio de Janeiro", "RJ", "Brazil");
            var personalData = new PersonalData("Diego Morrice Jardim de Lima", DateTime.Today, "m", null, null);
            var user = new User("dmorrice", "dmorrice@mail.com", Guid.NewGuid().ToString(), null, address, personalData, false);
            Assert.IsTrue(user.IsValid);
        }
        [TestMethod]
        public void Create_Valid_User_No_Address()
        {
            var address = new Address();            
            var personalData = new PersonalData("Diego Morrice Jardim de Lima", DateTime.Today, "m", null, null);
            var user = new User("dmorrice", "dmorrice@mail.com", Guid.NewGuid().ToString(), null, address, personalData, false);
            Assert.IsTrue(user.IsValid);
        }
        [TestMethod]
        public void Create_Valid_User_No_PersonalData()
        {
            var address = new Address("Rio de Janeiro", "RJ", "Brazil");
            var personalData = new PersonalData();
            var user = new User("dmorrice", "dmorrice@mail.com", Guid.NewGuid().ToString(), null, address, personalData, false);
            Assert.IsTrue(user.IsValid);
        }

        [TestMethod]
        public void Create_Valid_User_No_Address_No_PersonalData()
        {
            var user = new User("dmorrice", "dmorrice@mail.com", Guid.NewGuid().ToString(), null, null, null, false);
            Assert.IsTrue(user.IsValid);
        }

        [TestMethod]
        public void Create_Invalid_User()
        {           
            var user = new User();
            Assert.IsFalse(user.IsValid);
        }

        [TestMethod]
        public void Create_Invalid_User_No_Email()
        {
            var user = new User("dmorrice", null, Guid.NewGuid().ToString(), null, null, null, false);
            Assert.IsFalse(user.IsValid);
        }

        [TestMethod]
        public void Create_Invalid_User_Bad_Email()
        {
            var user = new User("dmorrice", "dmorrice.mail.com", Guid.NewGuid().ToString(), null, null, null, false);
            Assert.IsFalse(user.IsValid);
        }

        [TestMethod]
        public void Create_Invalid_User_No_Login()
        {
            var user = new User(null, "dmorrice@mail", Guid.NewGuid().ToString(), null, null, null, false);
            Assert.IsFalse(user.IsValid);
        }

    }
}
