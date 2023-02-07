using ViewModels.Enums;
using ViewModels.ViewModels;
using System;

namespace ViewModels.ViewModelsFactory
{
    /// <summary>
    /// This is a factory interface for creating viewModels  
    /// </summary>
    public interface IViewModelFactory
    {
        /// <summary>
        /// Creates a viewModel According to given <see cref="ViewModelTypes"/> type
        /// <para><see cref="ArgumentException"/> : Thrown if type does not match any <see cref="ViewModelTypes"/>.</para>
        /// </summary>
        /// <param name="type">type of viewModel that will be created.</param>
        /// <returns>Created viewModel according to type.</returns>
        /// <exception cref="ArgumentException"></exception>
        ViewModelBase CreateViewModel(ViewModelTypes type)
;
    }

}
