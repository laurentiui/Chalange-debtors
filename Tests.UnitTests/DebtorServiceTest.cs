using Data.Domain.Entity;
using Data.Domain.Exceptions;
using NUnit.Framework;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.UnitTests.TestUtilities;

namespace Tests.UnitTests
{
    public class DebtorServiceTest
    {
        private IDebtorsService _debtorsService;

        [SetUp]
        public void Setup() {
            var injections = new Injections();
            _debtorsService = injections.debtorsService;

            var debtorRepository = injections.debtorsRepository;

            //debtorRepository._list = new List();
            Task.Run(async () => {
                await debtorRepository.Insert(new Debtor() {
                    Id = 10,
                    //Number = 10,
                    Name = "Name 10",
                    Email = "name10@test.com",
                    Mobile = "1234",
                    Telephone = "5678",
                    IsClosed = false
                });
                await debtorRepository.Insert(new Debtor() {
                    Id = 20,
                    //Number = 10,
                    Name = "Name 10",
                    Email = "name10@test.com",
                    Mobile = "1234",
                    Telephone = "5678",
                    IsClosed = true
                });
            }).Wait();
        }

        [Test]
        public async Task Test_InsertWithExistingNumberFails() {

            var ex = Assert.ThrowsAsync<Exception>(async () => await _debtorsService.Insert(new Debtor() {
                    Id = 10
                }));
            Assert.AreEqual("Cannot insert explicit value for identity column", ex.Message);

        }

        [Test]
        public async Task Test_CanNotUpdateAClosedEntity() {
            var ex = Assert.ThrowsAsync<DebtorEntityException>(async () => await
                _debtorsService.Update(new Debtor() {
                    Id = 20,
                    Name = "new name"
                }));
            Assert.AreEqual("can not update a closed entity", ex.Message);

        }
    }
}
