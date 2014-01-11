using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WindowsStoreApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageLayout : Page
    {
        public PageLayout()
        {
            this.InitializeComponent();                     
        }       

        public Frame ContentFrame
        {
            get { return this.frameBody; }
        }
    }
}
