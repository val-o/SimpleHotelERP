using System.Windows;
using DataModel;
using Main.View;
using Main.ViewModel;

namespace Main
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public MainEntititesContext MainContext { get; set; }
        
        protected override void OnStartup(StartupEventArgs e)
        {
            HibernatingRhinos.Profiler.Appender.EntityFramework.EntityFrameworkProfiler.Initialize();
            
            base.OnStartup(e);
            //главный контекст
            MainContext = new MainEntititesContext();

            //Стартовое окно выбора отеля
            var hotelsRepository = new HotelsRepository(MainContext);
            var startWindowViewModel = new StartWindowViewModel(hotelsRepository);
            var startWindow = new StartWindow(){DataContext = startWindowViewModel};
            startWindow.Show();
            //подписка на событие выбора отеля
            startWindowViewModel.StartButtonClicked += () =>
            {
                MainContext.CurrentHotel = startWindowViewModel.SelectedHotel;
                //--Создание репозитеориев с данными
                var guestsRepository = new GuestsRepository(MainContext);
                var roomsRepository = new RoomsRepository(MainContext);
                var typesRepository = new TypeOfRoomsRepository(MainContext);
                var registrationRepository = new RegistrationRepository(MainContext);
                var registrationViewRepository = new RegistrationViewRepository(MainContext);
                var paymentsRepository = new PaymentsRepository(MainContext);
                var paymentsViewRepository = new PaymentsViewRepository(MainContext);
                var wishesRepository = new WishesRepository(MainContext);
                var discountsRepository = new DiscountsRepository(MainContext);
                var guestsDiscountsRepository = new GuestsDiscountsRepository(MainContext);
                var servicesRepository = new ServicesRepository(MainContext);
                var realizationRepository = new ServiceRealizationRepository(MainContext);
                //создание модели представления главного окна
                var mainviewmodel = new MainWindowViewModel(hotelsRepository,
                    guestsRepository,
                    roomsRepository,
                    typesRepository,
                    registrationViewRepository,
                    registrationRepository,
                    paymentsRepository,
                    paymentsViewRepository,
                    wishesRepository,
                    discountsRepository,
                    guestsDiscountsRepository,
                    servicesRepository,
                    realizationRepository);
                
                //создание представления главного окна
                var mainview = new MainWindow() {DataContext = mainviewmodel};
                mainview.Show();
                startWindowViewModel = null;
                startWindow.Close();
            };
        }
    }
}
