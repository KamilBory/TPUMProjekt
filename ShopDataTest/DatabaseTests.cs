using Microsoft.VisualStudio.TestTools.UnitTesting;

using ShopData.Interface;

namespace ShopDataTest
{
    [TestClass]
    public class DatabaseTests
    {
        [TestMethod]
        public void InventoryLifetime()
        {
            const int testCount = 1;
            InventorySize testSize = new InventorySize(3, 2, 1);
            const string testName = "Rock";
            const string testDesc = "Nice rock.";

            IDatabase database = new ShopData.MemoryModel.Database();

            int savedId;
            {
                savedId = database.CreateInventory();
                var newInventory = database.GetInventory(savedId);
                Assert.IsNotNull(newInventory);

                Assert.AreNotEqual(newInventory.GetCount(), testCount);
                Assert.AreNotEqual(newInventory.GetSize(), testSize);
                Assert.AreNotEqual(newInventory.GetName(), testName);
                Assert.AreNotEqual(newInventory.GetDescription(), testDesc);

                newInventory.SetCount(testCount);
                newInventory.SetSize(testSize);
                newInventory.SetName(testName);
                newInventory.SetDescription(testDesc);

                Assert.AreEqual(newInventory.GetCount(), testCount);
                Assert.AreEqual(newInventory.GetSize(), testSize);
                Assert.AreEqual(newInventory.GetName(), testName);
                Assert.AreEqual(newInventory.GetDescription(), testDesc);
            }

            {
                var readInventory = database.GetInventory(savedId);
                Assert.IsNotNull(readInventory);

                Assert.AreEqual(readInventory.GetCount(), testCount);
                Assert.AreEqual(readInventory.GetSize(), testSize);
                Assert.AreEqual(readInventory.GetName(), testName);
                Assert.AreEqual(readInventory.GetDescription(), testDesc);

                Assert.IsTrue(database.DeleteInventory(savedId));
            }

            {
                var readDeletedInventory = database.GetInventory(savedId);
                Assert.IsNull(readDeletedInventory);
            }
        }
    }
}
