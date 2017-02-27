using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.User.Entities;

namespace DomainUnitTest
{
    [TestClass]
    public class PersonalDataTest
    {
        [TestMethod]
        public void Create_Valid_personalData()
        {
            var personalData = new PersonalData();
            Assert.IsTrue(personalData.IsValid);
        }

        [TestMethod]
        public void Create_Valid_PersonalData_All_Information()
        {
            var personalData = new PersonalData("Diego Morrice Jardim de Lima", DateTime.MinValue, "M", "5521992660785", "55");
            Assert.IsTrue(personalData.IsValid);
        }
        [TestMethod]
        public void Create_Valid_PersonalData_Half_Information_one()
        {
            var personalData = new PersonalData("Diego Morrice Jardim de Lima", DateTime.MinValue, null, null, null);
            Assert.IsTrue(personalData.IsValid);
        }
        [TestMethod]
        public void Create_Valid_PersonalData_Half_Information_Two()
        {
            var personalData = new PersonalData(null, DateTime.MinValue, null, "5521992660785", null);
            Assert.IsTrue(personalData.IsValid);
        }
                
        [TestMethod]
        public void Create_Invalid_PersonalData_Phone_Invalid_one()
        {
            var personalData = new PersonalData(null, DateTime.MinValue, null, "00", null);
            Assert.IsFalse(personalData.IsValid);
        }
        [TestMethod]
        public void Create_Invalid_PersonalData_Phone_Invalid_two()
        {
            var personalData = new PersonalData(null, DateTime.MinValue, null, "0000000000000000000", null);
            Assert.IsFalse(personalData.IsValid);
        }

        [TestMethod]
        public void Create_Invalid_PersonalData_Gender_Invalid_One()
        {
            var personalData = new PersonalData(null, DateTime.MinValue,"Masc", null, null);
            Assert.IsFalse(personalData.IsValid);
        }

        [TestMethod]
        public void Create_Invalid_PersonalData_Gender_Invalid_Two()
        {
            var personalData = new PersonalData(null, DateTime.MinValue, "I", null, null);
            Assert.IsFalse(personalData.IsValid);
        }

        [TestMethod]
        public void Create_Invalid_PersonalData_ContryCode_Invalid_One()
        {
            var personalData = new PersonalData(null, DateTime.MinValue, null, null, "0");
            Assert.IsFalse(personalData.IsValid);
        }

        [TestMethod]
        public void Create_Invalid_PersonalData_ContryCode_Invalid_Two()
        {
            var personalData = new PersonalData(null, DateTime.MinValue, null, null, "000");
            Assert.IsFalse(personalData.IsValid);
        }       
    }
}
