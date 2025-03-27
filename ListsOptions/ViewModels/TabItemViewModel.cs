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
        #region fields
        private bool isActive;
        private readonly Action<TabItemViewModel> closeAction;
        #endregion
        #region Constructor
        public TabItemViewModel(string title, object view, Action<TabItemViewModel> closeAction, Action<TabItemViewModel> selectAction)
        {
            Title = title;
            View = view;
            this.closeAction = closeAction;
            IsActive = true;
            CloseCommand = new RelayCommand(o => this.closeAction(this));
            SelectTabCommand = new RelayCommand(o => selectAction(this)); // Свързване с командата
        }
        #endregion
        #region Properties
        public string Title { get; }
        public object View { get; }
        public ICommand CloseCommand { get; }

        public ICommand SelectTabCommand { get; } // Команда за избор на таб

        public bool IsActive
        {
            get => isActive;
            set
            {
                if (isActive != value)
                {
                    isActive = value;
                    OnPropertyChanged(nameof(IsActive));
                }
            }
        }
        #endregion
    }
}
