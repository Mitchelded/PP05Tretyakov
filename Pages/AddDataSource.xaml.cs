using System.Windows;

namespace PP05Tretyakov.Pages;

public partial class AddDataSource : Window
{
    public string Answer => TextBoxDataSource.Text;
    public AddDataSource()
    {
        InitializeComponent();
    }

    private void ButtonOk_OnClick(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
    }
}