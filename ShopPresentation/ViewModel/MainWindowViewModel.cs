using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using Logic = ShopLogic.Interface;
using Data = ShopData.Interface;
using System.ComponentModel;

namespace ShopPresentation.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Logic.ILogic logic;
        private Logic.IClientLogic clientLogic;

        public ICommand CreateShopCartCommand;

        public MainWindowViewModel()
        {
            Data.IDatabase database = new ShopData.MemoryModel.Database();

            InitDatabase(database);

            logic = new ShopLogic.Basic.Logic(database);
            var id = logic.RegisterClient("Janek", "Nowak", "xd");
            clientLogic = logic.GetClientLogic(id, "xd");
        }

        private void InitDatabase(Data.IDatabase database)
        {
            var offerRepo = database.GetOfferRepo();
            var inventoryRepo = database.GetInventoryRepo();
            var deliveryOptionRepo = database.GetDeliveryOptionRepo();

            offerRepo.Create(new Data.Offer { inventoryId = inventoryRepo.Create(new Data.Inventory { name = "Phone", description = "Phone description", count = 5, size = new Data.InventorySize { depth = 15, width = 15, height = 15 } }), sellPrice = 350 });
            offerRepo.Create(new Data.Offer { inventoryId = inventoryRepo.Create(new Data.Inventory { name = "Keyboard", description = "Keyboard description", count = 5, size = new Data.InventorySize { depth = 15, width = 15, height = 15 } }), sellPrice = 350 });
            offerRepo.Create(new Data.Offer { inventoryId = inventoryRepo.Create(new Data.Inventory { name = "Mouse", description = "Mouse description", count = 5, size = new Data.InventorySize { depth = 15, width = 15, height = 15 } }), sellPrice = 350 });
            offerRepo.Create(new Data.Offer { inventoryId = inventoryRepo.Create(new Data.Inventory { name = "Fridge", description = "Fridge description", count = 5, size = new Data.InventorySize { depth = 15, width = 15, height = 15 } }), sellPrice = 350 });
            offerRepo.Create(new Data.Offer { inventoryId = inventoryRepo.Create(new Data.Inventory { name = "Monitor", description = "Monitor description", count = 5, size = new Data.InventorySize { depth = 15, width = 15, height = 15 } }), sellPrice = 350 });

            deliveryOptionRepo.Create(new Data.DeliveryOption { name = "Courier", price = 10, maxSize = new Data.InventorySize { depth = 50, width = 50, height = 50 } });
            deliveryOptionRepo.Create(new Data.DeliveryOption { name = "Self-pickup", price = 10, maxSize = new Data.InventorySize { depth = 5000, width = 5000, height = 5000 } });
        }

        private Visibility showShopFront = Visibility.Visible;
        private Visibility showCart = Visibility.Hidden;
        private Visibility showOrders = Visibility.Hidden;
        private StackPanel stackPanel;

        public Visibility ShowShopFront
        {
            get => showShopFront;

            set
            {
                showShopFront = Visibility.Visible;
                showCart = Visibility.Hidden;
                showOrders = Visibility.Hidden;
            }
        }

        public Visibility ShowCart
        {
            get => showCart;

            set
            {
                showShopFront = Visibility.Hidden;
                showCart = Visibility.Visible;
                showOrders = Visibility.Hidden;
            }
        }

        public Visibility ShowOrders
        {
            get => showCart;

            set
            {
                showShopFront = Visibility.Hidden;
                showCart = Visibility.Hidden;
                showOrders = Visibility.Visible;
            }
        }

        public void SetView()
        {
            for(int i = 0; i < clientLogic.GetAllOffers().Length; i++)
            {
                Offer o = new Offer(new OfferItem { name = clientLogic.GetAllOffers()[i].name, description = clientLogic.GetAllOffers()[i].description, availableCount = clientLogic.GetAllOffers()[i].availableCount, sellPrice = clientLogic.GetAllOffers()[i].sellPrice});
                stackPanel.Children.Add(o.GetGrid());
            }
        }
    }
}
