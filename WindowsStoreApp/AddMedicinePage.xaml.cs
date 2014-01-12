using Windows.UI.Xaml.Navigation;
using WindowsStoreApp.ViewModels;
using MedicinePlan;

namespace WindowsStoreApp
{    
    public sealed partial class AddMedicinePage : BasePage
    {
        public AddMedicinePage()
        {
            this.InitializeComponent();

            this.PageTitle = "Add medicine";
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.DataContext = new AddMedicineViewModel(App.Instance.Supplies, this);
        }
    }
}
