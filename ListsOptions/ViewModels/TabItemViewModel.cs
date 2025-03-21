using ListsOptionsUI.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ListsOptionsUI.ViewModels
{
    public class TabItemViewModel : BaseViewModel
    {
        private bool _isActive;
        private readonly Action<TabItemViewModel> _closeAction;
        public TabItemViewModel(string title, object view, Action<TabItemViewModel> closeAction, Action<TabItemViewModel> selectAction)
        {
            Title = title;
            View = view;
            _closeAction = closeAction;
            IsActive = true;
            CloseCommand = new RelayCommand(o => _closeAction(this));
            SelectTabCommand = new RelayCommand(o => selectAction(this)); // Свързване с командата
        }
        public string Title { get; }
        public object View { get; }
        public ICommand CloseCommand { get; }

        public ICommand SelectTabCommand { get; } // Команда за избор на таб

        public bool IsActive
        {
            get => _isActive;
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                    OnPropertyChanged(nameof(IsActive));
                }
            }
        }

    }
}
