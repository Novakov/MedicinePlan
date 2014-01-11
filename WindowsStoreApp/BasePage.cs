using Windows.ApplicationModel.Store;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using WindowsStoreApp.Common;

namespace WindowsStoreApp
{
    public abstract class BasePage : Page
    {
        private NavigationHelper navigationHelper;

        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        public static readonly DependencyProperty PageTitleProperty = DependencyProperty.Register(
            "PageTitle", typeof (string), typeof (BasePage), new PropertyMetadata(default(string)));

        public string PageTitle
        {
            get { return (string) GetValue(PageTitleProperty); }
            set { SetValue(PageTitleProperty, value); }
        }

        public BasePage()
        {
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += OnLoadState;
            this.navigationHelper.SaveState += OnSaveState;

            this.PageTitle = (string)App.Current.Resources["AppName"];
        }
      
        protected virtual void OnLoadState(object sender, LoadStateEventArgs e)
        {
        }

       
        protected virtual void OnSaveState(object sender, SaveStateEventArgs e)
        {
        }        

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }
    }
}
