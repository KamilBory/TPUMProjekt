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
            InventorySize testSize = new InventorySize(3, 2, 1);
            const string testName = "Rock";
            const string testDesc = "Nice rock.";

            IDatabase database = new ShopData.MemoryModel.Database();
            var repo = database.GetInventoryRepo();

            int savedId, newId1, newId2;
            {
                var inventory = new Inventory();

                inventory.count = testCount;
                inventory.size = testSize;
                inventory.name = testName;
                inventory.description = testDesc;

                savedId = repo.Create(inventory);
            }

            {
                var readInventoryOpt = repo.Get(savedId);
                Assert.IsNotNull(readInventoryOpt);

                var inventory = readInventoryOpt.Value;

                Assert.AreEqual(inventory.count, testCount);
                Assert.AreEqual(inventory.size, testSize);
                Assert.AreEqual(inventory.name, testName);
                Assert.AreEqual(inventory.description, testDesc);

                Assert.IsTrue(repo.Delete(savedId));
            }

            {
                var inventory = new Inventory();

                inventory.count = testCount + 1;
                inventory.size = testSize;
                inventory.name = testName;
                inventory.description = testDesc;

                newId1 = repo.Create(inventory);
            }

            {
                var inventory = new Inventory();

                inventory.count = testCount + 2;
                inventory.size = testSize;
                inventory.name = testName;
                inventory.description = testDesc;

                newId2 = repo.Create(inventory);
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

                Assert.AreEqual(invList[0].count, testCount + 1);
                Assert.AreEqual(invList[1].count, testCount + 2);
            }
        }
    }
}
