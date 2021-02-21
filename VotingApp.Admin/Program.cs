using System;
using System.Net.Http;
using System.Threading.Tasks;
using VotingApp.Admin.Pages;
using VotingApp.Core.Models;

namespace VotingApp.Admin
{
    internal class Program
    {
        private static readonly HttpClient _client = new HttpClient();
        private static Session _session;

        private static void Main( string[] args )
        {
            LoginPage loginPage = new LoginPage();
            LoginState:
            Console.Clear();
            loginPage.Render();
            Task<Session> tLogin = loginPage.TryLogin( _client );
            tLogin.Wait();

            if( tLogin.Result == null )
                goto LoginState;

            _session = tLogin.Result;

            MainMenuPage mainMenuPage = new MainMenuPage( _session );
            MainMenuState:
            Console.Clear();
            mainMenuPage.Render();
            Task<bool> tMainMenu = mainMenuPage.GotoSelected( _client );
            tMainMenu.Wait();

            if( tMainMenu.Result == false )
                goto MainMenuState;

            Console.WriteLine( "Done" );
        }

        private static void Login()
        {

        }
    }
}