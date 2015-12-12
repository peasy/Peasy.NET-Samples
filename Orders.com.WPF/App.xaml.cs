using AutoMapper;
using Orders.com.DAL.EF.Entities;
using Orders.com.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Orders.com.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App() : base()
        {
            this.Dispatcher.UnhandledException += Dispatcher_UnhandledException;
            Mapper.CreateMap<Category, CategoryEntity>().ReverseMap();
            Mapper.CreateMap<Customer, CustomerEntity>().ReverseMap();
            Mapper.CreateMap<InventoryItem, InventoryItemEntity>().ReverseMap();
            Mapper.CreateMap<Order, OrderEntity>().ReverseMap();
            Mapper.CreateMap<OrderItem, OrderItemEntity>().ReverseMap();
            Mapper.CreateMap<Product, ProductEntity>().ReverseMap();
        }

        void Dispatcher_UnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            var message = e.Exception.Message;
            if (e.Exception is InvalidOperationException && e.Exception.Message == "Sequence contains no matching element")
                message = "This item no longer exists.  Try refreshing your list";

            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }
    }
}
