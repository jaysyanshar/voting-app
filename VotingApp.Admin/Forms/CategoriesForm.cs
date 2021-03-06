using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Forms;
using VotingApp.Admin.Repositories;
using VotingApp.Core.Models;
using VotingApp.Core.Services;

namespace VotingApp.Admin.Forms
{
    public partial class CategoriesForm : Form
    {
        private readonly CategoryRepository _categories;

        public CategoriesForm()
        {
            InitializeComponent();

            HttpClient client = DependencyService.Resolve<HttpClient>();
            _categories = new CategoryRepository( client );
        }

        private async void CategoriesForm_Load(object sender, EventArgs e)
        {
            RepositoryResponse<IEnumerable<Category>> response = await _categories.GetManyAsync();
            if( !response.Success )
            {
                MessageBox.Show( $@"Load error. Code: {response.StatusCode}", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error );
                return;
            }

            GenerateListView( response.Content );
        }

        private void GenerateListView( IEnumerable<Category> categories )
        {
            listViewCategories.Items.Clear();
            foreach( Category category in categories )
            {
                string[] row = { category.Name, category.Id };
                ListViewItem lvi = new ListViewItem( row )
                {
                    Tag = category
                };

                listViewCategories.Items.Add( lvi );
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {

        }
    }
}
