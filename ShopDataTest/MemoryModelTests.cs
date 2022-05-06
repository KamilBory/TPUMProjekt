using Microsoft.VisualStudio.TestTools.UnitTesting;

using ShopData.Interface;

namespace ShopDataTest
{
    [TestClass]
    public class MemoryModelTests
    {
        [TestMethod]
        public void ObjectLifetime()
        {
            const int testCount = 1;
            const string testName = "Rock";
            const string testDesc = "Nice rock.";

            IDatabase database = new ShopData.MemoryModel.Database();
            var repo = database.GetOfferRepo();

            int savedId, newId1, newId2;
            {
                var offer = database.CreateOffer();

                offer.count = testCount;
                offer.name = testName;
                offer.description = testDesc;

                savedId = repo.Create(offer);
            }

            {
                var offer = repo.Get(savedId);
                Assert.IsNotNull(offer);

                Assert.AreEqual(offer.count, testCount);
                Assert.AreEqual(offer.name, testName);
                Assert.AreEqual(offer.description, testDesc);

                Assert.IsTrue(repo.Delete(savedId));
            }

            {
                var offer = database.CreateOffer();

                offer.count = testCount + 1;
                offer.name = testName;
                offer.description = testDesc;

                newId1 = repo.Create(offer);
            }

            {
                var offer = database.CreateOffer();

                offer.count = testCount + 2;
                offer.name = testName;
                offer.description = testDesc;

                newId2 = repo.Create(offer);
            }

            {
                Assert.IsNull(repo.Get(savedId));
                Assert.IsNotNull(repo.Get(newId1));
                Assert.IsNotNull(repo.Get(newId2));
            }

            {
                var idList = repo.ListIds();
                Assert.AreEqual(idList.Length, 2);
            }

            {
                var invList = repo.List();

                Assert.AreEqual(invList[0].Value.count, testCount + 1);
                Assert.AreEqual(invList[1].Value.count, testCount + 2);
            }
        }
    }
}
