using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Forms;
using VotingApp.Core.Models;
using VotingApp.Core.Repositories;
using VotingApp.Core.Services;

namespace VotingApp.Admin.Forms
{
    public partial class AddVotingItemForm : Form
    {
        private readonly VotingItemRepository _votingItems;
        private readonly CategoryRepository _categories;

        public AddVotingItemForm()
        {
            InitializeComponent();

            dateTimePickerDueDate.MinDate = DateTime.Now.Add( TimeSpan.FromDays( 1 ) );

            HttpClient client = DependencyService.Resolve<HttpClient>();
            _votingItems = new VotingItemRepository( client );
            _categories = new CategoryRepository( client );
        }

        private async void buttonAdd_Click( object sender, EventArgs e )
        {
            VotingItem item = new VotingItem
            {
                Name = textBoxName.Text,
                Description = richTextBoxDescription.Text,
                DueDate = dateTimePickerDueDate.Value,
                Categories = comboBoxCategories.Text
            };

            RepositoryResponse<VotingItem> response = await _votingItems.AddAsync( item );
            if( !response.Success )
            {
                MessageBox.Show( $@"Add voting item error. Code: {response.StatusCode}", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error );

                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void AddVotingItemForm_Load( object sender, EventArgs e )
        {
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
    }
}