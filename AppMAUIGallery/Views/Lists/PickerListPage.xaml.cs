using AppMAUIGallery.Views.Lists.Models;

namespace AppMAUIGallery.Views.Lists;

public partial class PickerListPage : ContentPage {
    public PickerListPage() {
        InitializeComponent();
        PickerControl.ItemsSource = MovieList.GetList();
    }

    private void Button_Clicked(object sender, EventArgs e) {
        var filme = (Movie)PickerControl.SelectedItem;
        InfoMovie.Text =
          $"Id: {filme.Id}\n" +
          $"Nome: {filme.Name}\n" +
          $"Descrição: {filme.Description}\n" +
          $"Duração: {filme.Duration}\n" +
          $"Ano: {filme.LaunchYear}";
    }
}