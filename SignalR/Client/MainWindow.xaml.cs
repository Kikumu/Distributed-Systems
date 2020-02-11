using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.AspNetCore.SignalR.Client;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        protected HubConnection HubConnection;
        public void GetMessage(string username, string message)
        {
            this.Dispatcher.Invoke(() =>
            {
                var chat = $"{username}: {message}";
                Messagebox1.Items.Add(chat);
               // ListBox.Items
            });
        }
       
        public MainWindow()
        {
            InitializeComponent();
            HubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:44363/ChatHub")
                .Build();
            HubConnection.On<string, string>("GetMessage",
                new Action<string, string>((username, message) =>
                GetMessage(username, message)));
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //Task.Run(() => send());
            try
            {
                //HubConnection.InvokeAsync("BroadcastMessage", user1.Text, Message.Text);
                await HubConnection.InvokeAsync("BroadcastMessage", user1.Text, Message.Text);
            }
            catch (Exception ex)
            {
                Messagebox1.Items.Add(ex.Message);
            }

        }

        

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Task.Run(() => connect());
            try
            {
                await HubConnection.StartAsync();
                Messagebox1.Items.Add("Connection opened");
            }
            catch
            {
                Messagebox1.Items.Add("Connection failed");
            }

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        //private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{

        //}
    }
}
