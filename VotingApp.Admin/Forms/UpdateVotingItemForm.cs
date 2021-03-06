using System.Net.Http;
using System.Windows.Forms;
using VotingApp.Admin.Repositories;
using VotingApp.Core.Models;
using VotingApp.Core.Services;

namespace VotingApp.Admin.Forms
{
    public partial class UpdateVotingItemForm : Form
    {
        private readonly VotingItemRepository _votingItems;
        private readonly CategoryRepository _categories;
        
        private VotingItem _item;

        public UpdateVotingItemForm( VotingItem item )
        {
            InitializeComponent();

            _item = item;
            HttpClient client = DependencyService.Resolve<HttpClient>();
            _votingItems = new VotingItemRepository( client );
            _categories = new CategoryRepository( client );
        }
    }
}
