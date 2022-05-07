using System;
using System.Collections.Generic;
using System.Text;
using Logic = ShopLogic.Interface;
using Data = ShopData.Interface;

namespace ShopPresentation.ViewModel
{
    public class CartWindowViewModel
    {
        private Logic.ILogic logic;
        private Logic.IClientLogic clientLogic;

        public CartWindowViewModel()
        {
            Data.IDatabase database = new ShopData.MemoryModel.Database();

            logic = new ShopLogic.Basic.Logic(database);
            var id = logic.RegisterClient("Janek", "Nowak", "xd");
            clientLogic = logic.GetClientLogic(id, "xd");
            var cartId = clientLogic.CreateShoppingCart();
        }

        public void SetCartView()
        { 

        }

        public void StartOrder()
        {

        }
    }
}
