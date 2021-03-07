using System;
using System.Net.Http;
using System.Windows.Forms;
using VotingApp.Client.Forms;
using VotingApp.Core.Services;

namespace VotingApp.Client
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            RegisterDependencies();
            Application.Run(new VotingItemsForm());
        }

        private static void RegisterDependencies()
        {
            DependencyService.RegisterSingleton<HttpClient, HttpClient>(new HttpClient());
        }
    }
}
