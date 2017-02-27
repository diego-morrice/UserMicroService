using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.User.Entities;

namespace DomainUnitTest
{
    [TestClass]
    public class AddressTest
    {       

        [TestMethod]
        public void Create_Valid_Address()
        {
            var address = new Address();
            Assert.IsTrue(address.IsValid);
        }

        [TestMethod]
        public void Create_Valid_Address_All_Information()
        {
            var address = new Address("Progress street", "9ª floor", "1020", "RJ", "Rio de Janeiro", "BRAZIL", "20240060");
            Assert.IsTrue(address.IsValid);
        }
        [TestMethod]
        public void Create_Valid_Address_Half_Information_one()
        {
            var address = new Address("Progress street", null, "1020", null, "Rio de Janeiro", "BRAZIL", null);
            Assert.IsTrue(address.IsValid);
        }
        [TestMethod]
        public void Create_Valid_Address_Half_Information_two()
        {
            var address = new Address(null, null, null, "RJ", "Rio de Janeiro", "BRAZIL", "20240060");
            Assert.IsTrue(address.IsValid);
        }
        [TestMethod]
        public void Create_Invalid_Address_Street_Invalid()
        {
            var address = new Address("Progress street 1020 9ª floor Rio de Janeiro, RJ - BRAZIL - 20240060",
                "9ª floor, point of interest(EXTRA Hyper Market)", "1020", "RJ", "Rio de Janeiro", "BRAZIL", "20240060");
            Assert.IsFalse(address.IsValid);
        }
        [TestMethod]
        public void Create_Invalid_Address_Number_Invalid()
        {
            var address = new Address("Progress street", "9ª floor", "102000", "RJ", "Rio de Janeiro", "BRAZIL", "20240060");
            Assert.IsFalse(address.IsValid);
        }
        [TestMethod]
        public void Create_Invalid_Address_Country_Invalid()
        {
            var address = new Address("Progress street", "9ª floor", "10200", "RJ", "Rio de Janeiro", "Rio de Janeiro, RJ - BRAZIL", "20240060");
            Assert.IsFalse(address.IsValid);
        }
        [TestMethod]
        public void Create_Invalid_Address_State_Invalid()
        {
            var address = new Address("Progress street", "9ª floor", "10200", "Progress street 1020 9ª floor Rio de Janeiro, RJ - BRAZIL - 20240060", "Rio de Janeiro", "BRAZIL", "20240060");
            Assert.IsFalse(address.IsValid);
        }
        [TestMethod]
        public void Create_Invalid_Address_City_Invalid()
        {
            var address = new Address("Progress street", "9ª floor", "10200", "RJ", "Progress street 1020 9ª floor Rio de Janeiro, RJ - BRAZIL - 20240060", "BRAZIL", "20240060");
            Assert.IsFalse(address.IsValid);
        }
        [TestMethod]
        public void Create_Invalid_Address_zipCode_Invalid()
        {
            var address = new Address("Progress street", "9ª floor", "10200", "RJ", "Rio de Janeiro", "BRAZIL", "00000000000000000000");
            Assert.IsFalse(address.IsValid);
        }

    }
}
