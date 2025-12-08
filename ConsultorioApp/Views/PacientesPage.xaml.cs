using ConsultorioApp.ViewModels;

namespace ConsultorioApp.Views;

public partial class PacientesPage : ContentPage
{
    private readonly PacientesViewModel _viewModel;

    public PacientesPage(PacientesViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.CarregarPacientesAsync();
    }
}
