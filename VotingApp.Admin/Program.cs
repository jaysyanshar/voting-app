using System;
using System.Net.Http;
using System.Windows.Forms;
using VotingApp.Admin.Forms;
using VotingApp.Core.Services;

namespace VotingApp.Admin
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
            Application.Run(new LoginForm());
        }

        private static void RegisterDependencies()
        {
            DependencyService.RegisterSingleton<HttpClient, HttpClient>( new HttpClient() );
        }
    }
}
