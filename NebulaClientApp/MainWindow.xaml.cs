using System;
using System.IO;
using System.Windows;
using Microsoft.Web.WebView2.Core;

namespace NebulaClientApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Directly point to your NebulaClientweb folder
                string htmlPath = @"C:\NebulaClientweb\index.html";

                // Make sure it exists
                if (!File.Exists(htmlPath))
                {
                    MessageBox.Show($"index.html not found at:\n{htmlPath}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Convert to a URI that WebView2 can load
                string htmlUri = new Uri(htmlPath).AbsoluteUri;

                // Initialize WebView2
                await WebView.EnsureCoreWebView2Async();

                // Load your Nebula Client site
                WebView.CoreWebView2.Navigate(htmlUri);

                // Allow communication from JavaScript
                WebView.CoreWebView2.WebMessageReceived += WebMessageReceived;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing WebView2:\n{ex.Message}", "Nebula Client Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void WebMessageReceived(object? sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            string msg = e.TryGetWebMessageAsString();

            switch (msg)
            {
                case "launch_game":
                    LaunchMinecraft();
                    break;
                case "login":
                    MessageBox.Show("Login clicked — connect backend later.", "Nebula Client");
                    break;
                default:
                    MessageBox.Show($"Unknown message: {msg}", "Nebula Client");
                    break;
            }
        }

        private void LaunchMinecraft()
        {
            try
            {
                System.Diagnostics.Process.Start("minecraft-launcher.exe");
                MessageBox.Show("Launching Minecraft...", "Nebula Client");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to launch Minecraft:\n{ex.Message}", "Nebula Client Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void WebView_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            // Optional: code to run after page fully loads
        }
    }
}
