using System;
using System.Collections.Generic;
using System.Text;
using Logic = ShopLogic.Interface;
using Data = ShopData.Interface;


namespace ShopPresentation.ViewModel
{
    class InitializeItems
    {
        Data.IDatabase _database;

        public InitializeItems(Data.IDatabase db) 
        {
            _database = db;
        }
        
        public void FillItemList()
        {
            //OfferItem _smarthphone = new OfferItem();
            //_smarthphone.SetParams("Phone", "TBA", 5, 350);
            //OfferItem _keyboard = new OfferItem();
            //_keyboard.SetParams("Keyboard", "TBA", 15, 50);
            //OfferItem _mouse = new OfferItem();
            //_mouse.SetParams("Mouse", "TBA", 31, 25);
            //OfferItem _fridge = new OfferItem();
            //_fridge.SetParams("Fridge", "TBA", 2, 950);
            //OfferItem _monitor = new OfferItem();
            //_monitor.SetParams("Monitor", "TBA", 7, 120);
            
            var offerRepo = _database.GetOfferRepo();
            var inventoryRepo = _database.GetInventoryRepo();
            offerRepo.Create(new Data.Offer { inventoryId = inventoryRepo.Create(new Data.Inventory { name = "Phone", description = "TBA", count = 5, size = new Data.InventorySize { depth = 15, width = 15, height = 15 } }), sellPrice = 350});
            offerRepo.Create(new Data.Offer { inventoryId = inventoryRepo.Create(new Data.Inventory { name = "Keyboard", description = "TBA", count = 5, size = new Data.InventorySize { depth = 15, width = 15, height = 15 } }), sellPrice = 350 });
            offerRepo.Create(new Data.Offer { inventoryId = inventoryRepo.Create(new Data.Inventory { name = "Mouse", description = "TBA", count = 5, size = new Data.InventorySize { depth = 15, width = 15, height = 15 } }), sellPrice = 350 });
            offerRepo.Create(new Data.Offer { inventoryId = inventoryRepo.Create(new Data.Inventory { name = "Fridge", description = "TBA", count = 5, size = new Data.InventorySize { depth = 15, width = 15, height = 15 } }), sellPrice = 350 });
            offerRepo.Create(new Data.Offer { inventoryId = inventoryRepo.Create(new Data.Inventory { name = "Monitor", description = "TBA", count = 5, size = new Data.InventorySize { depth = 15, width = 15, height = 15 } }), sellPrice = 350 });
        }
    }
}
