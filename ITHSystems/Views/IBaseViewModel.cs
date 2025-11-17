using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITHSystems.Views
{
    public interface IBaseViewModel
    {
        Task PushRelativePageAsync<T>() where T : ContentPage;
    }
}
