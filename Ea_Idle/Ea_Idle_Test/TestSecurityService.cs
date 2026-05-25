using Ea_API.Interfaces;
using Ea_API.Models;
using Ea_API.Services;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;

namespace Ea_Idle_Test
{
    [TestClass]
    public sealed class TestSecurityService
    {
        private readonly ISecurityService _securityService;

        public TestSecurityService()
        {
            _securityService = new SecurityService();
        }

        [TestMethod]
        [DataRow("", "", "Important information is missing!", DisplayName = "All empty")]
        [DataRow("", "Password123", "Important information is missing!", DisplayName = "Username empty")]
        [DataRow("MyUsername", "", "Important information is missing!", DisplayName = "Password empty")]
        [DataRow("This_Username_Has_To_Be_Fifty1_Characters_Long_yay!", "Password123", "The username must be 50 characters or less.", DisplayName = "Username too long")]
        [DataRow("MyUsername", "7Charac", "The password must be 8-30 characters.", DisplayName = "Password too short")]
        [DataRow("MyUsername", "ThisPasswordIsMoreThan30Charact", "The password must be 8-30 characters.", DisplayName = "Password too long")]
        public void LoginValidationReturnsFalse(string username, string password, string message)
        {
            LoginModel loginModel = new(username, "", 0, null, password);

            (bool succes, string? msg) = _securityService.ValidateLoginValues(loginModel);

            Assert.IsFalse(succes);
            Assert.AreEqual<string>(msg, message);
        }

        [TestMethod]
        [DataRow("MyUsername", "Password123", DisplayName = "All normal")]
        [DataRow("This_Username_Has_To_Be_Fifty_Characters_Long_yay!", "Password123", DisplayName = "Username 50 characters")]
        [DataRow("MyUsername", "This_Password_Has_To_Be_Thirty", DisplayName = "Password 30 characters")]
        [DataRow("MyUsername", "8Charact", DisplayName = "Password 8 characters")]
        public void LoginValidationReturnsTrue(string username, string password)
        {
            LoginModel loginModel = new(username, "", 0, null, password);

            (bool succes, string? msg) = _securityService.ValidateLoginValues(loginModel);

            Assert.IsTrue(succes);
            Assert.IsNull(msg);
        }

        [TestMethod]
        [DataRow("", "", "", "", "" ,"Important information is missing!", DisplayName = "All empty")]
        [DataRow("", "mymail@mail.com", "Password123", "Password123", "Player", "Important information is missing!", DisplayName = "Username empty")]
        [DataRow("MyUsername", "", "Password123", "Password123", "Player", "Important information is missing!", DisplayName = "Email empty")]
        [DataRow("MyUsername", "mymail@mail.com", "", "Password123", "Player", "Important information is missing!", DisplayName = "Password empty")]
        [DataRow("MyUsername", "mymail@mail.com", "Password123", "", "Player", "Important information is missing!", DisplayName = "PasswordConfirm empty")]
        [DataRow("MyUsername", "mymail@mail.com", "Password123", "Password123", "", "Important information is missing!", DisplayName = "Role empty")]
        [DataRow("This_Username_Has_To_Be_Fifty1_Characters_Long_yay!", "mymail@mail.com", "Password123", "Password123", "Player", "The username must be 50 characters or less.", DisplayName = "Username too long")]
        [DataRow("MyUsername", "mymailmail.com", "Password123", "Password123", "Player", "The email is not valid.", DisplayName = "Email missing '@'")]
        [DataRow("MyUsername", "mymail@mailcom", "Password123", "Password123", "Player", "The email is not valid.", DisplayName = "Email missing '.'")]
        [DataRow("MyUsername", "mymail@mail.com", "7Charac", "Password123", "Player", "The password must be 8-30 characters.", DisplayName = "Password too short")]
        [DataRow("MyUsername", "mymail@mail.com", "ThisPasswordIsMoreThan30Charact", "Password123", "Player", "The password must be 8-30 characters.", DisplayName = "Password too long")]
        [DataRow("MyUsername", "mymail@mail.com", "Password123", "7Charac", "Player", "The passwords are not the same.", DisplayName = "PasswordConfirm too short")]
        [DataRow("MyUsername", "mymail@mail.com", "Password123", "ThisPasswordIsMoreThan30Charact", "Player", "The passwords are not the same.", DisplayName = "Passwordconfirm too long")]
        [DataRow("MyUsername", "mymail@mail.com", "Password123", "321Password", "Player", "The passwords are not the same.", DisplayName = "Passwords don't match")]
        public void RegisterValidationReturnsFalse(string username, string email, string password, string passConfirm, string role, string message)
        {
            LoginModel loginModel = new(username, role, 0, email, password, passConfirm);

            (bool succes, string? msg) = _securityService.ValidateRegisterValues(loginModel);

            Assert.IsFalse(succes);
            Assert.AreEqual<string>(msg, message);
        }

        [TestMethod]
        [DataRow("MyUsername", "mymail@mail.com", "Password123", "Password123", "Player", DisplayName = "All normal")]
        [DataRow("This_Username_Has_To_Be_Fifty_Characters_Long_yay!", "mymail@mail.com", "Password123", "Password123", "Player", DisplayName = "Username 50 characters")]
        [DataRow("MyUsername", "mymail@mail.com", "This_Password_Has_To_Be_Thirty", "This_Password_Has_To_Be_Thirty", "Player", DisplayName = "Passwords 30 characters")]
        [DataRow("MyUsername", "mymail@mail.com", "8Charact", "8Charact", "Player", DisplayName = "Passwords 8 characters")]
        public void RegisterValidationReturnsTrue(string username, string email, string password, string passConfirm, string role)
        {
            LoginModel loginModel = new(username, role, 0, email, password, passConfirm);

            (bool succes, string? msg) = _securityService.ValidateRegisterValues(loginModel);

            Assert.IsTrue(succes);
            Assert.IsNull(msg);
        }

        [TestMethod]
        [DataRow(24, 20, 30, "The hour must be within 0-23.", DisplayName = "Hour too high")]
        [DataRow(10, 60, 30, "The minute and second must be within 0-59.", DisplayName = "Minute too high")]
        [DataRow(10, 20, 60, "The minute and second must be within 0-59.", DisplayName = "Second too high")]
        [DataRow(-1, 20, 60, "The hour must be within 0-23.", DisplayName = "Hour too low")]
        [DataRow(10, -1, 30, "The minute and second must be within 0-59.", DisplayName = "Minute too low")]
        [DataRow(10, 20, -1, "The minute and second must be within 0-59.", DisplayName = "Second too low")]
        public void TimeLimitValidationReturnsFalse(int hour, int min, int sec, string message)
        {
            (bool succes, string? msg) = _securityService.ValidateTimeLimitValues(hour, min, sec);

            Assert.IsFalse(succes);
            Assert.AreEqual<string>(msg, message);
        }

        [TestMethod]
        [DataRow(10, 20, 30, DisplayName = "All normal")]
        [DataRow(23, 20, 30, DisplayName = "Hour on 23")]
        [DataRow(10, 59, 30, DisplayName = "Minute on 59")]
        [DataRow(10, 20, 59, DisplayName = "Second on 59")]
        [DataRow(0, 20, 30, DisplayName = "Hour on 0")]
        [DataRow(10, 0, 30, DisplayName = "Minute on 0")]
        [DataRow(10, 20, 0, DisplayName = "Second on 0")]
        public void TimeLimitValidationReturnsTrue(int hour, int min, int sec)
        {
            (bool succes, string? msg) = _securityService.ValidateTimeLimitValues(hour, min, sec);

            Assert.IsTrue(succes);
            Assert.IsNull(msg);
        }

        [TestMethod]
        [DataRow("", "Important information is missing!", DisplayName = "Sp empty")]
        [DataRow("-20", "Silver Pennies amount can not be negative.", DisplayName = "Sp negative amount")]
        public void ProgressValidationReturnsFalse(string sp, string message)
        {
            GameProgress progress = new(0);
            progress.SilverPennies = sp;

            (bool succes, string? msg) = _securityService.ValidateProgressValues(progress);

            Assert.IsFalse(succes);
            Assert.AreEqual<string>(msg, message);
        }

        [TestMethod]
        [DataRow("20", DisplayName = "All normal")]
        [DataRow("0", DisplayName = "Sp amount 0")]
        [DataRow("761985439915738113546531342", DisplayName = "Sp very long")]
        public void ProgressValidationReturnsTrue(string sp)
        {
            GameProgress progress = new(0);
            progress.SilverPennies = sp;

            (bool succes, string? msg) = _securityService.ValidateProgressValues(progress);

            Assert.IsTrue(succes);
            Assert.IsNull(msg);
        }

        [TestMethod]
        [DataRow(92750, "The connection code should be 6 digits.", DisplayName = "Code too short")]
        [DataRow(9275021, "The connection code should be 6 digits.", DisplayName = "Code too long")]
        [DataRow(012345, "The connection code should be 6 digits.", DisplayName = "Code start with 0")]
        public void CodeValidationReturnsFalse(int code, string message)
        {
            (bool succes, string? msg) = _securityService.ValidateConnectionCode(code);

            Assert.IsFalse(succes);
            Assert.AreEqual<string>(msg, message);
        }

        [TestMethod]
        [DataRow(927502, DisplayName = "All normal")]
        [DataRow(111111, DisplayName = "Code is all one number")]
        [DataRow(123456, DisplayName = "Code is counting up")]
        public void CodeValidationReturnsTrue(int code)
        {
            (bool succes, string? msg) = _securityService.ValidateConnectionCode(code);

            Assert.IsTrue(succes);
            Assert.IsNull(msg);
        }

        [TestMethod]
        public void CodeGeneratorReturnsValidCode()
        {
            int code = _securityService.GenerateConnectionCode();

            (bool succes, string? msg) = _securityService.ValidateConnectionCode(code);

            Assert.IsTrue(succes);
            Assert.IsNull(msg);
        }

    }
}
