using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Engine
{
    public class BaseNotificationClass : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //CallerMemberName sees what called this function and try to get its name. 
        //If it was a property setter, it will take the name of the property.
        //Don't need to explicitly pass in name of property, as it can be detected.
        //Still have to leave as a param that can be passed as sometimes when a property is changed, notification
        //changed events are raised for other properties.
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
