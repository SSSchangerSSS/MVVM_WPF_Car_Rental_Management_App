using ViewModels.Enums;
using System;

namespace ViewModels.Services
{
    /// <summary>
    /// This interface is a Navigation service to navigate between views.
    /// </summary>
    public interface ICarRentalNavigationService
    {
        /// <summary>
        /// Navigate to a view according to given <see cref="ViewModelTypes"/> 
        /// <para><see cref="ArgumentException"/> : Thrown if type does not match any <see cref="ViewModelTypes"/>.</para>
        /// </summary>
        /// <param name="type">type of view model that has binded to a view</param>
        /// <exception cref="ArgumentException"></exception>
        public void Navigate(ViewModelTypes type);
    }
}
