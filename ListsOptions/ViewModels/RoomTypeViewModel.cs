using Common.DTOs;
using DataLayer.Services;
using System.Collections.ObjectModel;

namespace ListsOptionsUI.ViewModels
{
    public class RoomTypeViewModel : BaseViewModel
    {
        private readonly RoomTypeService roomTypeService;

        public RoomTypeViewModel(RoomTypeService paymentMethodService)
        {
            this.roomTypeService = paymentMethodService;
            RoomTypes = new ObservableCollection<RoomTypeDTO>(this.roomTypeService.GetRoomTypes());
        }

        public ObservableCollection<RoomTypeDTO> RoomTypes { get; set; }

    }
}
