using System.Net.Http;
using System.Windows.Forms;
using VotingApp.Core.Models;
using VotingApp.Core.Repositories;
using VotingApp.Core.Services;

namespace VotingApp.Admin.Forms
{
    public partial class AddCategoryForm : Form
    {
        private readonly CategoryRepository _categories;

        public AddCategoryForm()
        {
            InitializeComponent();

            HttpClient client = DependencyService.Resolve<HttpClient>();
            _categories = new CategoryRepository( client );
        }

        private async void buttonAdd_Click(object sender, System.EventArgs e)
        {
            string name = textBoxName.Text;
            if( string.IsNullOrWhiteSpace( name ) )
                return;

            Category item = new Category
            {
                Name = name
            };

            RepositoryResponse<Category> response = await _categories.AddAsync( item );
            if( !response.Success )
            {
                MessageBox.Show( $@"Add category error. Code: {response.StatusCode}", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error );
                return;
            }

            DialogResult = DialogResult.OK;
        }
    }
}
