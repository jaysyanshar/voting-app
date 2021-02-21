using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace VotingApp.Admin.Pages
{
    public class CategoriesPage : PageBase
    {
        private CategoryMenu _selected;
        public CategoriesPage()
        {
            Title = "Categories";
        }

        public override void Render()
        {
            base.Render();
            Console.WriteLine("1. Add");
            Console.WriteLine("2. View");
            Console.WriteLine("3. Update");
            Console.WriteLine("4. Remove");

            int choice = 0;
            while( choice < 1 || choice > 4 )
            {
                Console.Write("choice: ");
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

            _selected = (CategoryMenu) choice;
        }

        public async Task<bool> TrySelected( HttpClient client )
        {
            switch( _selected )
            {
                case CategoryMenu.Add:
                    break;
                case CategoryMenu.View:
                    break;
                case CategoryMenu.Update:
                    break;
                case CategoryMenu.Remove:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return false;
        }

        private enum CategoryMenu
        {
            Add = 1,
            View = 2,
            Update = 3,
            Remove = 4
        }
    }
}