using HotelApp.BusinessLayer.Services;
using HotelApp.Core.DTOs;
using HotelApp.Core.Interfaces;
using System.Collections.ObjectModel;

namespace ListsOptionsUI.ViewModels
{
    public class RoomTypeViewModel : BaseViewModel
    {
        private readonly IRoomTypeService roomTypeService;

        public RoomTypeViewModel(IRoomTypeService paymentMethodService , IUserSessionService userSessionService)
            : base (userSessionService)
        {
            this.roomTypeService = paymentMethodService;
            RoomTypes = new ObservableCollection<RoomTypeDTO>(this.roomTypeService.GetRoomTypes());
        }

        public ObservableCollection<RoomTypeDTO> RoomTypes { get; set; }

    }
}
