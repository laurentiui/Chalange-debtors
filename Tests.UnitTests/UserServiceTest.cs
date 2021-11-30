using Data.Domain.Entity;
using NUnit.Framework;
using Services.Implementations;
using Services.Interfaces;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.UnitTests.Mocks;
using Tests.UnitTests.TestUtilities;

namespace Tests.UnitTests
{
    public class UserServiceTest
    {
        private IUserService _userService;

        [SetUp]
        public void Setup()
        {
            var injections = new Injections();
            _userService = injections.userService;

            var userRepository = injections.userRepository;
            userRepository.Insert(new User()
            {
                Email = "existing-not-allow@test.com",
                Password = Utilities.Crypt.CreateMD5("1"),
                IsAllowed = false
            });
            userRepository.Insert(new User()
            {
                Email = "existing-and-allowed@test.com",
                Password = Utilities.Crypt.CreateMD5("1"),
                IsAllowed = true
            });
            userRepository.Insert(new User()
            {
                ConfirmToken = "we-want-to-confirm-this"
            });
        }

        [Test]
        public void Test_Login_EmailNotExists_ThrowException()
        {
            var ex = Assert.ThrowsAsync<ArgumentException>(() => _userService.LoginAsync("non-existing", "testpass"));
            Assert.AreEqual("user not not found", ex.Message);
        }
        [Test]
        public void Test_Login_IsPassWrong_ThrowException()
        {
            var ex = Assert.ThrowsAsync<ArgumentException>(() => _userService.LoginAsync("existing-not-allow@test.com", "testpass-wrong"));
            Assert.AreEqual("pass not not found", ex.Message);
        }
        [Test]
        public void Test_Login_IsNotAllowed_ThrowException()
        {
            var ex = Assert.ThrowsAsync<ArgumentException>(() => _userService.LoginAsync("existing-not-allow@test.com", "1"));
            Assert.AreEqual("user is blocked", ex.Message);
        }
        [Test]
        public async Task Test_Login_Ok_GetUser()
        {
            var user = await _userService.LoginAsync("existing-and-allowed@test.com", "1");
            Assert.NotNull(user);
        }

        [Test]
        public void Test_RegisterUser_EmailExists_ThrowsError()
        {
            var ex = Assert.ThrowsAsync<ArgumentException>(() => _userService.RegisterAsync("does-not-matter", "existing-not-allow@test.com", "1234"));
            Assert.AreEqual("email already exists", ex.Message);
        }
        [Test]
        public async Task Test_RegisterUser_EmailIsNew_ReceiveConfirmToken()
        {
            var user = await _userService.RegisterAsync("new-user-name", "new-user-name@test.com", "1");
            Assert.NotNull(user);
            Assert.NotNull(user.ConfirmToken);
            Assert.IsFalse(user.IsAllowed);
        }

        [Test]
        public void Test_ConfirmUser_ConfirmTokenWrong_ThrowsError()
        {
            var ex = Assert.ThrowsAsync<ArgumentException>(() => _userService.ConfirmUserAsync("non-existing-token"));
            Assert.AreEqual("confirmation token failed", ex.Message);
        }

        [Test]
        public async Task Test_ConfirmUser_ConfirmTokenCorect_UserIsRegistered()
        {
            var user = await _userService.ConfirmUserAsync("we-want-to-confirm-this");
            Assert.NotNull(user);
            Assert.IsNull(user.ConfirmToken);
            Assert.IsTrue(user.IsAllowed);
        }
    }
}
