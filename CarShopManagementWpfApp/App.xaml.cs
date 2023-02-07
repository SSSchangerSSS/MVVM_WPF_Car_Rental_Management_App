using Models.Models;
using System.Windows;
using ViewModels.Enums;
using ViewModels.Services;
using ViewModels.Store;
using ViewModels.ViewModels.CarViewModels;
using ViewModels.ViewModels.CustomerViewModels;
using ViewModels.ViewModels.MainMenuViewModels;
using ViewModels.ViewModels.MainWindowViewModels;
using ViewModels.ViewModels.RentalViewModels;
using ViewModels.ViewModelsFactory;
using Views.Windows;
using Microsoft.Extensions.DependencyInjection;
using Models.Models.CarModels;
using Models.Models.CustomerModels;
using Models.Models.RentalModels;
using Models.DTOs;
using Models.DbContexts;
using Models.Services.Crud;
using Models.Services.Validators.Cars;
using Models.Services.Validators.Customers;
using Models.Services.Validators.Rentals;
using Models.Store;
using Models.Services.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

namespace CarRentalManagementWpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IHost _host;
        protected override async void OnStartup(StartupEventArgs e)
        {
            _host = CreateHostBuilder().Build();
            await _host.StartAsync();
            using (CarRentalDbContext dbContext = _host.Services.GetRequiredService<CarRentalDbContextFactory>().CreateDbContext())
            {
                dbContext.Database.Migrate();
            }
            var managementStore = _host.Services.GetRequiredService<Models.Store.ManagementStore>();
            await managementStore.Load();
            var navigationService = _host.Services.GetRequiredService<ICarRentalNavigationService>();
            navigationService.Navigate(ViewModelTypes.MainMenu);
            Window window = _host.Services.GetRequiredService<MainWindow>();
            window.Show();

            base.OnStartup(e);
        }
        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            base.OnExit(e);
        }
        private static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    //requirements to create ViewModles and Navigate between them
                    services.AddSingleton<ICarRentalNavigationService, CarRentalNavigationService>();
                    services.AddSingleton<IManagement, Management>();
                    services.AddSingleton<IViewModelFactory, ViewModelFactory>();
                    //models
                    services.AddTransient<IGModelList<Customer, CustomerDTO>, GModelList<Customer, CustomerDTO>>();
                    services.AddTransient<IGModelList<Car, CarDTO>, GModelList<Car, CarDTO>>();
                    services.AddTransient<IGModelList<Rental, RentalDTO>, GModelList<Rental, RentalDTO>>();
                    //validators
                    services.AddTransient<IGConflictValidator<Customer, CustomerDTO>, CustomerConflictValidator>();
                    services.AddTransient<IGConflictValidator<Car, CarDTO>, CarConflictValidator>();
                    services.AddTransient<IGConflictValidator<Rental, RentalDTO>, RentalConflictValidator>();
                    services.AddTransient<IGModelIsInNotExpiredRentalValidator<Customer>, CustomerIsInNotExpiredRentalValidator>();
                    services.AddTransient<IGModelIsInNotExpiredRentalValidator<Car>, CarIsInNotExpiredRentalValidator>();
                    //automapper
                    services.AddAutoMapper(cfg => cfg.CreateMap<Customer, CustomerDTO>().ReverseMap());
                    services.AddAutoMapper(cfg => cfg.CreateMap<Car, CarDTO>().ReverseMap());
                    services.AddAutoMapper(cfg => cfg.CreateMap<Rental, RentalDTO>().ReverseMap());
                    //services
                    services.AddTransient<IGcrud<CustomerDTO>, Gcrud<CustomerDTO>>();
                    services.AddTransient<IGcrud<CarDTO>, Gcrud<CarDTO>>();
                    services.AddTransient<IGcrud<RentalDTO>, Gcrud<RentalDTO>>();
                    //stores
                    services.AddSingleton<ManagementStore>();
                    services.AddSingleton<NavigationStore>();
                    services.AddSingleton<TViewModelStore<CustomerViewModel>>();
                    services.AddSingleton<TViewModelStore<CarViewModel>>();
                    services.AddSingleton<TViewModelStore<DisplayRentalViewModel>>();
                    //ViewModels
                    services.AddTransient<MainViewModel>();
                    services.AddTransient<MainMenuViewModel>();
                    services.AddTransient<CustomersViewModel>(services =>
                    CustomersViewModel.LoadViewModel(services.GetRequiredService<ManagementStore>(),
                    services.GetRequiredService<ICarRentalNavigationService>(),
                    services.GetRequiredService<TViewModelStore<CustomerViewModel>>()));
                    services.AddTransient<AddCustomerViewModel>();
                    services.AddTransient<EditCustomerViewModel>();
                    services.AddTransient<CarsViewModel>(services =>
                    CarsViewModel.LoadViewModel(services.GetRequiredService<ManagementStore>(),
                    services.GetRequiredService<ICarRentalNavigationService>(),
                    services.GetRequiredService<TViewModelStore<CarViewModel>>()));
                    services.AddTransient<AddCarViewModel>();
                    services.AddTransient<EditCarViewModel>();
                    services.AddTransient(services =>
                    RentalsViewModel.LoadViewModel(services.GetRequiredService<ManagementStore>(),
                    services.GetRequiredService<ICarRentalNavigationService>(),
                    services.GetRequiredService<TViewModelStore<DisplayRentalViewModel>>()));
                    services.AddTransient(services =>
                    AddRentalViewModel.LoadViewModel(services.GetRequiredService<ManagementStore>(),
                    services.GetRequiredService<ICarRentalNavigationService>()));
                    services.AddTransient(services =>
                    EditRentalViewModel.LoadViewModel(services.GetRequiredService<ManagementStore>(),
                    services.GetRequiredService<ICarRentalNavigationService>(),
                    services.GetRequiredService<TViewModelStore<DisplayRentalViewModel>>()));
                    //ViewModlesDelegateFactories
                    services.AddTransient<CreateViewModel<MainMenuViewModel>>(services => { return () => services.GetRequiredService<MainMenuViewModel>(); });
                    services.AddTransient<CreateViewModel<CustomersViewModel>>(services => { return () => services.GetRequiredService<CustomersViewModel>(); });
                    services.AddTransient<CreateViewModel<AddCustomerViewModel>>(services => { return () => services.GetRequiredService<AddCustomerViewModel>(); });
                    services.AddTransient<CreateViewModel<EditCustomerViewModel>>(services => { return () => services.GetRequiredService<EditCustomerViewModel>(); });
                    services.AddTransient<CreateViewModel<EditCustomerViewModel>>(services => { return () => services.GetRequiredService<EditCustomerViewModel>(); });
                    services.AddTransient<CreateViewModel<CarsViewModel>>(services => { return () => services.GetRequiredService<CarsViewModel>(); });
                    services.AddTransient<CreateViewModel<AddCarViewModel>>(services => { return () => services.GetRequiredService<AddCarViewModel>(); });
                    services.AddTransient<CreateViewModel<EditCarViewModel>>(services => { return () => services.GetRequiredService<EditCarViewModel>(); });
                    services.AddTransient<CreateViewModel<RentalsViewModel>>(services => { return () => services.GetRequiredService<RentalsViewModel>(); });
                    services.AddTransient<CreateViewModel<AddRentalViewModel>>(services => { return () => services.GetRequiredService<AddRentalViewModel>(); });
                    services.AddTransient<CreateViewModel<EditRentalViewModel>>(services => { return () => services.GetRequiredService<EditRentalViewModel>(); });
                    //DbContext
                    string connectionString = hostContext.Configuration.GetConnectionString("Default");
                    services.AddSingleton(services => { return new CarRentalDbContextFactory(connectionString); });
                    //init MainWindow
                    services.AddSingleton(services => new MainWindow()
                    {
                        DataContext = services.GetRequiredService<MainViewModel>()
                    });
                });
        }
    }
}
