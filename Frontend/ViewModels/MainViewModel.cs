using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.ViewModels
{
    public class MainViewModel
    {
        public ObservableCollection<MedicineStatus> Medicines { get; set; }
    }
}
