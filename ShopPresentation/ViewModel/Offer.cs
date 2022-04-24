using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ShopPresentation.ViewModel
{
    public class Offer
    {
        private Grid offer = new Grid();
        private TextBlock name = new TextBlock();
        private TextBlock description = new TextBlock();
        private TextBlock price = new TextBlock();
        private Button add = new Button();

        public Offer(int index) // Pass the product in the constructor as well ???
        {
            // Swap with getters of the product
            name.Name = "name" + index.ToString();
            name.Text = "Title" + index.ToString();
            description.Name = "description" + index.ToString();
            description.Text = "Description";
            price.Name = "price" + index.ToString();
            price.Text = "Price";

            add.Name = "Button" + index.ToString();
            add.Content = "Add to Cart";

            name.Width = 100; 
            name.HorizontalAlignment = HorizontalAlignment.Left; 
            name.VerticalAlignment = VerticalAlignment.Center; 
            name.TextAlignment = TextAlignment.Center; 
            name.FontSize = 22; 
            name.FontWeight = FontWeights.Bold; 
            name.Margin = new Thickness(10, 0, 0, 0);
            description.Width = 500;
            description.Height = 70;
            description.HorizontalAlignment = HorizontalAlignment.Left;
            description.VerticalAlignment = VerticalAlignment.Center;
            description.TextAlignment = TextAlignment.Center;
            description.FontSize = 18;
            description.Margin = new Thickness(120, 0, 0, 0);
            price.Width = 70;
            price.Height = 30;
            price.HorizontalAlignment = HorizontalAlignment.Right;
            price.VerticalAlignment = VerticalAlignment.Center;
            price.TextAlignment = TextAlignment.Center;
            price.FontSize = 20;
            price.Margin = new Thickness(0, 0, 180, 0);
            add.Height = 40;
            add.Width = 100;
            add.VerticalAlignment = VerticalAlignment.Center;
            add.HorizontalAlignment = HorizontalAlignment.Right;
            add.Margin = new Thickness(0, 0, 40, 0);
            add.Background = Brushes.Aqua;
            add.Foreground = Brushes.Black;
            add.FontSize = 18;

            offer.Children.Add(name);
            offer.Children.Add(description);
            offer.Children.Add(price);
            offer.Children.Add(add);
        }

        public Grid GetGrid()
        {
            return offer;
        }
    }
}
