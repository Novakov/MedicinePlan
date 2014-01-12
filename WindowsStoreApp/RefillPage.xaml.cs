using WindowsStoreApp.ViewModels;
namespace WindowsStoreApp
{    
    public sealed partial class RefillPage : BasePage
    {
        public RefillPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var vm = new RefillViewModel((string)e.Parameter, App.Instance.Supplies, this);

            this.PageTitle = string.Format("Refill {0}", vm.MedicineName);

            this.DataContext = vm;
        }
    }
}
