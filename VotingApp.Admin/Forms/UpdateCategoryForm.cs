using System.Net.Http;
using System.Windows.Forms;
using VotingApp.Core.Models;
using VotingApp.Core.Repositories;
using VotingApp.Core.Services;

namespace VotingApp.Admin.Forms
{
    public partial class UpdateCategoryForm : Form
    {
        private readonly CategoryRepository _categories;

        private readonly Category _item;

        public UpdateCategoryForm( Category item )
        {
            _item = item;
            InitializeComponent();
            labelPrevName.Text = _item.Name;
            textBoxName.Text = _item.Name;

            HttpClient client = DependencyService.Resolve<HttpClient>();
            _categories = new CategoryRepository( client );
        }

        private async void buttonUpdate_Click(object sender, System.EventArgs e)
        {
            string newName = textBoxName.Text;
            if( string.IsNullOrWhiteSpace( newName ) || newName.Equals( _item.Name ) )
                return;

            _item.Name = newName;
            RepositoryResponse response = await _categories.UpdateAsync( _item.Id, _item );
            if( !response.Success )
            {
                MessageBox.Show( $@"Update error. Code: {response.StatusCode}", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error );

                return;
            }

            DialogResult = DialogResult.OK;
        }
    }
}
