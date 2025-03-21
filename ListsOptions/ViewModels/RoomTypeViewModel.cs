using Common.DTOs;
using DataLayer.Services;
using System.Collections.ObjectModel;

namespace ListsOptionsUI.ViewModels
{
    public class RoomTypeViewModel : BaseViewModel
    {
        private readonly RoomTypeService roomTypeService;
        private string newPaymentMethod;

        public RoomTypeViewModel(RoomTypeService paymentMethodService)
        {
            this.roomTypeService = paymentMethodService;
            RoomTypes = new ObservableCollection<RoomTypeDTO>(this.roomTypeService.GetRoomTypes());
            //AddPaymentMethodCommand = new RelayCommand(AddPaymentMethod);
        }

        public ObservableCollection<RoomTypeDTO> RoomTypes { get; set; }

        //public ICommand AddPaymentMethodCommand { get; }


        //private void AddPaymentMethod(object o)
        //{
        //    if (!string.IsNullOrWhiteSpace(NewPaymentMethod) && RoomTypes.All(p => !p.Name.ToLower().Contains(NewPaymentMethod.ToLower())))
        //    {
        //        roomTypeService.AddPaymentMethod(NewPaymentMethod);
        //        RoomTypes.Add(new PaymentMethodModel { Name = NewPaymentMethod, IsSystemDefined = true });
        //        NewPaymentMethod = "";
        //        //roomTypeService.SaveChanges();
        //    }
        //}
    }
}
