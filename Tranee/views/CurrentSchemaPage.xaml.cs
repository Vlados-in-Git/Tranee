using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Tranee.viewModels;
using Tranee.views;
using Microsoft.Maui.Controls;
using Tranee.servises;
using Tranee.viewModels;

namespace Tranee.views
{
    public partial class CurrentSchemaPage : ContentPage
    {
        private AddNewSchemaViewModel _viewModel;

        public CurrentSchemaPage(AddNewSchemaViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await _viewModel.LoadData();
        }
    }
}
