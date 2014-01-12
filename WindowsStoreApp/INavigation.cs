namespace WindowsStoreApp
{
    public interface INavigation
    {
        void NavigateTo<TPage>(object parameter = null, NavigationFlags flags = NavigationFlags.None)
            where TPage : BasePage;

        void GoBack();
    }

    public enum NavigationFlags
    {
        None = 0,        
    }
}
