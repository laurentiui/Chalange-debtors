using Data.Domain.Entity;
using Data.Domain.Exceptions;
using Data.Repository.Interfaces;
using NUnit.Framework;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.UnitTests.Mocks;
using Tests.UnitTests.TestUtilities;

namespace Tests.UnitTests
{
    public class DebtorServiceTest
    {
        private IDebtorsService _debtorsService;
        private IDebtorsRepositoryMock debtorRepository;

        [SetUp]
        public void Setup() {
            var injections = new Injections();
            _debtorsService = injections.debtorsService;

            debtorRepository = injections.debtorsRepository;

            //debtorRepository._list = new List();
            Task.Run(async () => {
                await InitializeRepository();
            }).Wait();
        }

        private async Task InitializeRepository() {

            await debtorRepository.ClearAll();

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
                Name = "Name 20 - we expect to close this",
                Email = "name20@test.com",
                Mobile = "1234",
                Telephone = "5678",
                IsClosed = false
            });
            await debtorRepository.Insert(new Debtor() {
                Id = 100,
                //Number = 10,
                Name = "Name 10",
                Email = "name10@test.com",
                Mobile = "1234",
                Telephone = "5678",
                IsClosed = true
            });
            await debtorRepository.Insert(new Debtor() {
                Id = 101,
                //Number = 10,
                Name = "Name 10",
                Email = "name10@test.com",
                Mobile = "1234",
                Telephone = "5678",
                IsClosed = true
            });
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
                    Id = 100,
                    Name = "new name"
                }));
            Assert.AreEqual("can not update a closed entity", ex.Message);

        }

        [Test]
        public async Task GetListsForUpdateData_ShouldHave2Invalid() {
            var importList = new List<Debtor>() {
                new Debtor() {
                    Id = 100,
                    Name = "dummy"
                },
                new Debtor() {
                    Id = 101,
                    Name = "dummy"
                }
            };
            var listsForUpdateData = await _debtorsService.GetListsForUpdateData(importList);

            Assert.AreEqual(2, listsForUpdateData.ListInvalid.Count);
            Assert.AreEqual(0, listsForUpdateData.ListToIgnore.Count);
            Assert.AreEqual(0, listsForUpdateData.ListToAdd.Count);
            //because we omitted the one with id=10
            Assert.AreEqual(2, listsForUpdateData.ListToClose.Count);
            Assert.AreEqual(0, listsForUpdateData.ListToUpdate.Count);

        }
        [Test]
        public async Task GetListsForUpdateData_ShouldHave2ToIgnore() {
            var importList = new List<Debtor>() {
                new Debtor() {
                    Id = 10,
                    //Number = 10,
                    Name = "Name 10",
                    Email = "name10@test.com",
                    Mobile = "1234",
                    Telephone = "5678"
                },
                new Debtor() {
                    Id = 20,
                    //Number = 10,
                    Name = "Name 20 - we expect to close this",
                    Email = "name20@test.com",
                    Mobile = "1234",
                    Telephone = "5678",
                    IsClosed = false
                }
            };
            var listsForUpdateData = await _debtorsService.GetListsForUpdateData(importList);

            Assert.AreEqual(0, listsForUpdateData.ListInvalid.Count);
            Assert.AreEqual(2, listsForUpdateData.ListToIgnore.Count);
            Assert.AreEqual(0, listsForUpdateData.ListToAdd.Count);
            Assert.AreEqual(0, listsForUpdateData.ListToClose.Count);
            Assert.AreEqual(0, listsForUpdateData.ListToUpdate.Count);

        }

        [Test]
        public async Task GetListsForUpdateData_ShouldHave1ToAdd() {
            var importList = new List<Debtor>() {
                new Debtor() {
                    Id = 200,
                    Name = "Name 200",
                    Email = "name20@test.com",
                    Mobile = "1234",
                    Telephone = "5678"
                }
            };
            var listsForUpdateData = await _debtorsService.GetListsForUpdateData(importList);

            Assert.AreEqual(0, listsForUpdateData.ListInvalid.Count);
            Assert.AreEqual(0, listsForUpdateData.ListToIgnore.Count);
            Assert.AreEqual(1, listsForUpdateData.ListToAdd.Count);
            //because we omitted the one with id=10 and 20
            Assert.AreEqual(2, listsForUpdateData.ListToClose.Count);
            Assert.AreEqual(0, listsForUpdateData.ListToUpdate.Count);

        }

        [Test]
        public async Task GetListsForUpdateData_ShouldHave2ToClose() {
            var importList = new List<Debtor>() {};
            var listsForUpdateData = await _debtorsService.GetListsForUpdateData(importList);

            Assert.AreEqual(0, listsForUpdateData.ListInvalid.Count);
            Assert.AreEqual(0, listsForUpdateData.ListToIgnore.Count);
            Assert.AreEqual(0, listsForUpdateData.ListToAdd.Count);
            //because we omitted the one with id=10 and 20
            Assert.AreEqual(2, listsForUpdateData.ListToClose.Count);
            Assert.AreEqual(0, listsForUpdateData.ListToUpdate.Count);

        }

        [Test]
        public async Task GetListsForUpdateData_ShouldHave1ToUpdate_Name() {
            var importList = new List<Debtor>() {
                new Debtor() {
                    Id = 10,
                    //Number = 10,
                    Name = "Name 10-changed prop",
                    Email = "name10@test.com",
                    Mobile = "1234",
                    Telephone = "5678"
                }
            };
            var listsForUpdateData = await _debtorsService.GetListsForUpdateData(importList);

            Assert.AreEqual(0, listsForUpdateData.ListInvalid.Count);
            Assert.AreEqual(0, listsForUpdateData.ListToIgnore.Count);
            Assert.AreEqual(0, listsForUpdateData.ListToAdd.Count);
            Assert.AreEqual(1, listsForUpdateData.ListToClose.Count);
            Assert.AreEqual(1, listsForUpdateData.ListToUpdate.Count);
        }
        [Test]
        public async Task GetListsForUpdateData_ShouldHave1ToUpdate_MissingProps() {
            var importList = new List<Debtor>() {
                new Debtor() {
                    Id = 10,
                    //Number = 10,
                    Name = "Name 10",
                }
            };
            var listsForUpdateData = await _debtorsService.GetListsForUpdateData(importList);

            Assert.AreEqual(0, listsForUpdateData.ListInvalid.Count);
            Assert.AreEqual(0, listsForUpdateData.ListToIgnore.Count);
            Assert.AreEqual(0, listsForUpdateData.ListToAdd.Count);
            Assert.AreEqual(1, listsForUpdateData.ListToClose.Count);
            Assert.AreEqual(1, listsForUpdateData.ListToUpdate.Count);
        }

        [Test]
        public async Task UpdateData_ShouldHave1More() {

            await InitializeRepository();
            var list = await _debtorsService.ListAll();
            Assert.AreEqual(4, list.Count);

            var importList = new List<Debtor>() {
                new Debtor() {
                    Id = 200,
                    Name = "Name 200",
                    Email = "name20@test.com",
                    Mobile = "1234",
                    Telephone = "5678"
                }
            };

            list = await _debtorsService.UpdateData(importList);
            Assert.AreEqual(5, list.Count);

        }

        [Test]
        public async Task UpdateData_ShouldHave2MoreClosed() {

            await InitializeRepository();
            var list = await _debtorsService.ListAll();
            Assert.AreEqual(4, list.Count);
            Assert.AreEqual(2, list.Count(d => d.IsClosed == true));

            var importList = new List<Debtor>() { };
            list = await _debtorsService.UpdateData(importList);
            Assert.AreEqual(4, list.Count);
            Assert.AreEqual(4, list.Count(d => d.IsClosed == true));

        }

        [Test]
        public async Task UpdateData_ShouldHave1UpdatedRecord() {

            await InitializeRepository();
            var list = await _debtorsService.ListAll();
            Assert.AreEqual(4, list.Count);
            Assert.AreEqual("Name 10", list.First(l => l.Id == 10).Name);

            var importList = new List<Debtor>() {
                new Debtor() {
                    Id = 10,
                    //Number = 10,
                    Name = "Name 10-changed prop",
                    Email = "name10@test.com",
                    Mobile = "1234",
                    Telephone = "5678"
                }
            };
            list = await _debtorsService.UpdateData(importList);

            Assert.AreEqual(4, list.Count);
            Assert.AreEqual("Name 10-changed prop", list.First(l => l.Id == 10).Name);

        }

    }
}
