using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Forms;
using VotingApp.Admin.Models;
using VotingApp.Admin.Repositories;
using VotingApp.Core.Models;
using VotingApp.Core.Services;

namespace VotingApp.Admin.Forms
{
    public partial class UpdateVotingItemForm : Form
    {
        private readonly VotingItemRepository _votingItems;
        private readonly CategoryRepository _categories;

        private readonly VotingItem _item;

        public UpdateVotingItemForm( VotingItem item )
        {
            InitializeComponent();

            _item = item;
            HttpClient client = DependencyService.Resolve<HttpClient>();
            _votingItems = new VotingItemRepository( client );
            _categories = new CategoryRepository( client );
        }

        private void UpdateVotingItemForm_Load( object sender, System.EventArgs e )
        {
            textBoxName.Text = _item.Name;
            richTextBoxDescription.Text = _item.Description;
            dateTimePickerDueDate.Value = _item.DueDate;
            LoadCategories();
        }

        private async void LoadCategories()
        {
            RepositoryResponse<IEnumerable<Category>> response = await _categories.GetManyAsync();
            if( !response.Success )
            {
                MessageBox.Show( $@"Load categories error. Code: {response.StatusCode}", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error );

                return;
            }

            comboBoxCategories.Items.Clear();
            foreach( Category category in response.Content )
            {
                CategoryItem categoryItem = new CategoryItem
                {
                    Id = category.Id,
                    Name = category.Name
                };

                comboBoxCategories.Items.Add( categoryItem );
            }

            comboBoxCategories.SelectedIndex = 0;
        }

        private void buttonUpdate_Click( object sender, System.EventArgs e )
        {
            _item.Name = textBoxName.Text;
            _item.Description = richTextBoxDescription.Text;
            _item.DueDate = dateTimePickerDueDate.Value;
            _item.Categories = comboBoxCategories.Text;

            RunUpdate();
        }

        private async void RunUpdate()
        {
            RepositoryResponse response = await _votingItems.UpdateAsync( _item.Id, _item );
            if( !response.Success )
            {
                MessageBox.Show( $@"Update error. Code: {response.StatusCode}", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error );
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

    }
}