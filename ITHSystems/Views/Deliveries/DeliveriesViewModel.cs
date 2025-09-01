using CommunityToolkit.Mvvm.ComponentModel;
using ITHSystems.DTOs;
using ITHSystems.Views.Home;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITHSystems.Views.Deliveries
{
    public partial class DeliveriesViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ObservableCollection<ModuleDTO?> deliveriesModules;

        public DeliveriesViewModel()
        {
            DeliveriesModules = new(BuildHomeModules.GetDeliveriesModules().OrderBy(x => x.Order));
        }


    }
}
