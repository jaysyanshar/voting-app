using System;
using System.Net.Http;
using System.Threading.Tasks;
using VotingApp.Core.Models;

namespace VotingApp.Admin.Pages
{
    public class MainMenuPage : PageBase
    {
        private const string LOGOUT_URI = @"https://localhost:44303/api/Sessions/Logout";
        private readonly Session _session;

        private MainMenu _selected;

        public MainMenuPage( Session session )
        {
            _session = session;
            Title = "Main Menu";
        }

        public override void Render()
        {
            base.Render();
            Console.WriteLine( "1. Voting Items" );
            Console.WriteLine( "2. Categories" );
            Console.WriteLine( "3. Logout" );

            int choice = 0;
            while( choice < 1 || choice > 3 )
            {
                Console.Write( "choice: " );
                string input = Console.ReadLine();
                try
                {
                    choice = int.Parse( input ?? string.Empty );
                }
                catch( Exception e )
                {
                    RenderError( e );
                }
            }

            _selected = ( MainMenu ) choice;
        }

        public async Task<bool> GotoSelected( HttpClient client )
        {
            switch( _selected )
            {
                case MainMenu.VotingItems:
                    break;
                case MainMenu.Categories:
                    break;
                case MainMenu.Logout:
                    return await TryLogout( client );
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return false;
        }

        private async Task<bool> TryLogout( HttpClient client )
        {
            HttpContent content = new StringContent( string.Empty );
            content.Headers.Add( nameof( Session ), _session.Id );
            try
            {
                HttpResponseMessage response = await client.PutAsync( LOGOUT_URI, content );
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch( HttpRequestException e )
            {
                RenderError( e );
                return false;
            }
        }

        private enum MainMenu
        {
            VotingItems = 1,
            Categories = 2,
            Logout = 3
        }
    }
}