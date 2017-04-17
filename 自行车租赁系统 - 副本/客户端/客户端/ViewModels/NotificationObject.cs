using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace 客户端.ViewModels
{
    class NotificationObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        public async void RaisePropertyChanged(string propName)
        {
            await Window.Current.Dispatcher.RunAsync(CoreDispatcherPriority.High,
                () =>
                {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
                });
        }
    }
}
