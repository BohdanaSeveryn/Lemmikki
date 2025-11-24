using Avalonia.Controls;

namespace LemmikkiUI;

public partial class MainWindow : Window
{
    private readonly MainWindowViewModel _vm = new();

    public MainWindow()
    {
        InitializeComponent();
        DataContext = _vm;

        // Automatically load data at startup
        _ = _vm.LoadPets();
        _ = _vm.LoadOwners();
    }
}
