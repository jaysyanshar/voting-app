using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Forms;
using VotingApp.Core.Models;
using VotingApp.Core.Repositories;
using VotingApp.Core.Services;

namespace VotingApp.Admin.Forms
{
    public partial class CategoriesForm : Form
    {
        private readonly CategoryRepository _categories;
        private DialogResult _currentResult;

        public CategoriesForm()
        {
            InitializeComponent();

            HttpClient client = DependencyService.Resolve<HttpClient>();
            _categories = new CategoryRepository( client );
        }

        private void CategoriesForm_Load( object sender, EventArgs e )
        {
            LoadCategories();
        }

        private async void LoadCategories()
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

        private void buttonAdd_Click( object sender, EventArgs e )
        {
            AddCategoryForm form = new AddCategoryForm();
            DialogResult dialogResult = form.ShowDialog();
            if( dialogResult != DialogResult.OK )
                return;

            if( _currentResult != DialogResult.OK )
                _currentResult = dialogResult;

            LoadCategories();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            Category selected = ( Category ) listViewCategories.SelectedItems[0].Tag;
            if( selected == null )
                return;

            UpdateCategoryForm form = new UpdateCategoryForm( selected );
            DialogResult dialogResult = form.ShowDialog();
            if( dialogResult != DialogResult.OK )
                return;

            if( _currentResult != DialogResult.OK )
                _currentResult = dialogResult;

            LoadCategories();
        }

        private async void buttonDelete_Click( object sender, EventArgs e )
        {
            Category selected = ( Category ) listViewCategories.SelectedItems[0].Tag;
            if( selected == null )
                return;

            RepositoryResponse response = await _categories.RemoveAsync( selected.Id );
            if( !response.Success )
            {
                MessageBox.Show( $@"Delete error. Code: {response.StatusCode}", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error );

                return;
            }

            _currentResult = DialogResult.OK;

            LoadCategories();
        }

        private void CategoriesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if( DialogResult != DialogResult.OK && _currentResult == DialogResult.OK )
                DialogResult = _currentResult;
        }
    }
}