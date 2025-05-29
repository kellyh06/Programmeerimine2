using System.Collections.ObjectModel;
using System.Windows.Input;
using KooliProjekt.WpfApp.Api;
using WpfApp1.Api;

namespace KooliProjekt.WpfApp
{
    public class MainWindowViewModel : NotifyPropertyChangedBase
    {
        public ObservableCollection<Artist> Lists { get; private set; }
        public ICommand NewCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public Predicate<Artist> ConfirmDelete { get; set; }

        private readonly IApiClient _apiClient;

        public MainWindowViewModel() : this(new ApiClient())
        {
        }

        public MainWindowViewModel(IApiClient apiClient)
        {
            _apiClient = apiClient;

            Lists = new ObservableCollection<Artist>();

            NewCommand = new RelayCommand<Artist>(
                // Execute
                list =>
                {
                    SelectedItem = new Artist();
                }
            );

            SaveCommand = new RelayCommand<Artist>(
                async list =>
                {
                    try
                    {
                        await _apiClient.Save(SelectedItem);
                        await Load();
                    }
                    catch (Exception ex)
                    {
                        OnError?.Invoke(ex.Message);
                    }
                },
                list =>
                {
                    return SelectedItem != null;
                }
            );


            DeleteCommand = new RelayCommand<Artist>(
                async list =>
                {
                    try
                    {
                        if (ConfirmDelete != null)
                        {
                            var result = ConfirmDelete(SelectedItem);
                            if (!result)
                            {
                                return;
                            }
                        }

                        await _apiClient.Delete(SelectedItem.Id);
                        Lists.Remove(SelectedItem);
                        SelectedItem = null;
                    }
                    catch (Exception ex)
                    {
                        OnError?.Invoke(ex.Message);
                    }
                },
                list =>
                {
                    return SelectedItem != null;
                }
            );

        }

        public async Task Load()
        {
            try
            {
                Lists.Clear();

                var artists = await _apiClient.List();

                foreach (var artist in artists)
                {
                    Lists.Add(artist);
                }
            }
            catch (Exception ex)
            {
                OnError?.Invoke(ex.Message);
            }
        }


        private Artist _selectedItem;
        public Artist SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                NotifyPropertyChanged();

            }
        }

        public Action<object> OnError { get; internal set; }
    }
}
