﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PigTool.Helpers;
using Shared;
using PigTool.Views;
using Xamarin.Forms;

namespace PigTool.ViewModels.DataViewModels
{
    public class ManureSaleViewModel : LoggedInViewModel, INotifyPropertyChanged
    {
        bool isEditMode, isCreationMode;
        private bool editExistingMode;
        private DateTime date;
        private double? volumeSold;
        private string volumeUnitType;
        private double? amountRecieved;
        private string soldTo;
        private string otherSoldTo, otherUnitType;
        private string paymentType;
        private double? paymentValue;
        private double? transportationCost;
        private double? otherCosts;
        private string comment;
        List<PickerToolHelper> volumeUnitTypeListOfOptions;
        List<PickerToolHelper> soldToListOfOptions;
        List<PickerToolHelper> paymentTypeListOfOptions;
        ManureSaleItem _itemForEditing;

        //Button Clicks
        public Command SaveButtonClicked { get; }
        public Command ResetButtonClicked { get; }
        public Command DeleteButtonClicked { get; }
        public Command EditButtonClicked { get; }

        #region translations
        public string ManureSaleTitleTranslation { get; set; }
        public string DateTranslation { get; set; }
        public string VolumeSoldTranslation { get; set; }
        public string SoldToTranslation { get; set; }
        public string OtherSoldToTranslation { get; set; }
        public string OtherUnitTypeTranslation { get; set; }
        public string ManureAmountRecievedTranslation { get; set; }
        public string AnyOtherPaymentTranslation { get; set; }
        //public string PaymentTypeTranslation { get; set; }
        //public string PaymentValueTranslation { get; set; }
        public string TransportationCostTranslation { get; set; }
        public string OtherCostTranslation { get; set; }
        public string CommentTranslation { get; set; }
        public string SaveTranslation { get; set; }
        public string ResetTranslation { get; set; }
        public string EditTranslation { get; set; }
        public string DeleteTranslation { get; set; }
        public string PickerUnitTranslation { get; set; }
        public string PickerPaymentTypeTranslation { get; set; }
        public string PickerSoldToTranslation { get; set; }
        #endregion

        #region Manure item fields
        public DateTime Date
        {
            get => date;
            set
            {
                if (date != value)
                {
                    date = value;
                    OnPropertyChanged(nameof(Date));
                }
            }
        }
        public double? VolumeSold
        {
            get => volumeSold;
            set
            {
                if (value != volumeSold)
                {
                    volumeSold = value;
                    OnPropertyChanged(nameof(VolumeSold));
                }
            }
        }
        public string VolumeUnitType
        {
            get => volumeUnitType;
            set
            {
                if (value != volumeUnitType)
                {
                    volumeUnitType = value;
                    OnPropertyChanged(nameof(VolumeUnitType));
                }
            }
        }

        public string SoldTo
        {
            get => soldTo;
            set
            {
                if (value != soldTo)
                {
                    soldTo = value;
                    OnPropertyChanged(nameof(SoldTo));
                }
            }
        }
        public string OtherSoldTo
        {
            get => otherSoldTo;
            set
            {
                if (value != otherSoldTo)
                {
                    otherSoldTo = value;
                    OnPropertyChanged(nameof(OtherSoldTo));
                }
            }
        }

        public string OtherUnitType
        {
            get => otherUnitType;
            set
            {
                if (value != otherUnitType)
                {
                    otherUnitType = value;
                    OnPropertyChanged(nameof(OtherUnitType));
                }
            }
        }
        

        public double? AmountRecieved
        {
            get => amountRecieved;
            set
            {
                if (value != amountRecieved)
                {
                    amountRecieved = value;
                    OnPropertyChanged(nameof(AmountRecieved));
                }
            }
        }
        public string PaymentType
        {
            get => paymentType;
            set
            {
                if (value != paymentType)
                {
                    paymentType = value;
                    OnPropertyChanged(nameof(PaymentType));
                }
            }
        }
        public double? PaymentValue
        {
            get => paymentValue;
            set
            {
                if (value != paymentValue)
                {
                    paymentValue = value;
                    OnPropertyChanged(nameof(PaymentValue));
                }
            }
        }

        public double? TransportationCost
        {
            get => transportationCost;
            set
            {
                if (value != transportationCost)
                {
                    transportationCost = value;
                    OnPropertyChanged(nameof(TransportationCost));
                }
            }
        }
        public double? OtherCosts
        {
            get => otherCosts;
            set
            {
                if (value != otherCosts)
                {
                    otherCosts = value;
                    OnPropertyChanged(nameof(OtherCosts));
                }
            }
        }
        public string Comment
        {
            get => comment;
            set
            {
                if (value != comment)
                {
                    comment = value;
                    OnPropertyChanged(nameof(Comment));
                }
            }
        }

        #endregion

        #region Dropdown Lists
        public List<PickerToolHelper> VolumeUnitTypeListOfOptions
        {
            get { return volumeUnitTypeListOfOptions; }
            set
            {
                volumeUnitTypeListOfOptions = value;
                OnPropertyChanged(nameof(VolumeUnitTypeListOfOptions));
            }
        }

        public List<PickerToolHelper> SoldToListOfOptions
        {
            get { return soldToListOfOptions; }
            set
            {
                soldToListOfOptions = value;
                OnPropertyChanged(nameof(SoldToListOfOptions));
            }
        }

        public List<PickerToolHelper> PaymentTypeListOfOptions
        {
            get { return paymentTypeListOfOptions; }
            set
            {
                paymentTypeListOfOptions = value;
                OnPropertyChanged(nameof(PaymentTypeListOfOptions));
            }
        }

        private PickerToolHelper selectedVolumeUnitType;

        public PickerToolHelper SelectedVolumeUnitType
        {
            get { return selectedVolumeUnitType; }
            set
            {
                if (selectedVolumeUnitType != value)
                {
                    DisplayOtherUnitType = value?.TranslationRowKey == Constants.OTHER;
                    selectedVolumeUnitType = value;
                    OnPropertyChanged(nameof(SelectedVolumeUnitType));
                }
            }
        }

        private PickerToolHelper selectedSoldTo;

        public PickerToolHelper SelectedSoldTo
        {
            get { return selectedSoldTo; }
            set
            {
                if (selectedSoldTo != value)
                {
                    DisplayOtherSoldTo = value?.TranslationRowKey == Constants.OTHER;
                    selectedSoldTo = value;
                    OnPropertyChanged(nameof(SelectedSoldTo));
                }
            }
        }

        private PickerToolHelper selectedPaymentType;

        public PickerToolHelper SelectedPaymentType
        {
            get { return selectedPaymentType; }
            set
            {
                if (selectedPaymentType != value)
                {
                    selectedPaymentType = value;
                    OnPropertyChanged(nameof(SelectedPaymentType));
                }
            }
        }
        #endregion

        #region Hidden Fields
        private bool displayOtherSoldTo;
        private bool displayOtherUnitType;

        public bool DisplayOtherSoldTo
        {
            get => displayOtherSoldTo;
            set
            {
                if (displayOtherSoldTo != value)
                {
                    displayOtherSoldTo = value;
                    OnPropertyChanged(nameof(DisplayOtherSoldTo));
                }
            }
        }

        public bool DisplayOtherUnitType
        {
            get => displayOtherUnitType;
            set
            {
                if (displayOtherUnitType != value)
                {
                    displayOtherUnitType = value;
                    OnPropertyChanged(nameof(DisplayOtherUnitType));
                }
            }
        }

        #endregion

        public bool IsEditMode
        {

            get { return isEditMode; }
            set
            {
                if (value != isEditMode)
                {
                    isEditMode = value;
                    IsCreationMode = !value;
                    OnPropertyChanged(nameof(IsEditMode));
                }

            }
        }
        public bool IsCreationMode
        {

            get { return isCreationMode; }
            set
            {
                if (value != isCreationMode)
                {
                    isCreationMode = value;
                    OnPropertyChanged(nameof(IsCreationMode));
                }

            }
        }
        public bool EditExistingMode { get => editExistingMode; set { if (editExistingMode != value) { editExistingMode = value; OnPropertyChanged(nameof(EditExistingMode)); } } }
        public bool CreationMode { get; private set; }

        public ManureSaleViewModel()
        {
            Date = DateTime.Now;

            IsEditMode = true;
            CreationMode = true;

            SaveButtonClicked = new Command(SaveButtonCreateHousingItem);
            ResetButtonClicked = new Command(ResetButtonPressed);
            DeleteButtonClicked = new Command(DeleteItem);
            EditButtonClicked = new Command(EditItem);
            IsEditMode = true;
            IsCreationMode = !EditExistingMode;

            ManureSaleTitleTranslation = ManureSale;
            DateTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(DateTranslation), User.UserLang) + " *";

            VolumeSoldTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(VolumeSoldTranslation), User.UserLang);
            SoldToTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(SoldToTranslation), User.UserLang);
            OtherSoldToTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(OtherSoldToTranslation), User.UserLang);
            OtherUnitTypeTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(OtherUnitTypeTranslation), User.UserLang);

            ManureAmountRecievedTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(ManureAmountRecievedTranslation), User.UserLang) + " *";
            AnyOtherPaymentTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(AnyOtherPaymentTranslation), User.UserLang);
            //PaymentTypeTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(PaymentTypeTranslation), User.UserLang);
            //PaymentValueTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(PaymentValueTranslation), User.UserLang) + " *";
            TransportationCostTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(TransportationCostTranslation), User.UserLang) + " *";
            OtherCostTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(OtherCostTranslation), User.UserLang);
            CommentTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(CommentTranslation), User.UserLang);

            SaveTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(SaveTranslation), User.UserLang);
            ResetTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(ResetTranslation), User.UserLang);
            EditTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(EditTranslation), User.UserLang);
            DeleteTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(DeleteTranslation), User.UserLang);

            PickerUnitTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(PickerUnitTranslation), User.UserLang);
            PickerPaymentTypeTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(PickerPaymentTypeTranslation), User.UserLang);
            PickerSoldToTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(PickerSoldToTranslation), User.UserLang);
        }

        public void populatewithData(ManureSaleItem item)
        {
            isEditMode = true;
            CreationMode = false;
            EditExistingMode = !CreationMode;

            _itemForEditing = item;

            Date = item.Date;
            VolumeSold = item.VolumeSold;
            VolumeUnitType = item.VolumeUnitType;
            SoldTo = item.SoldTo;
            OtherSoldTo = item.OtherSoldTo;
            OtherUnitType = item.OtherUnitType;
            AmountRecieved = item.AmountRecieved;
            //PaymentType = item.PaymentType;
            //PaymentValue = item.PaymentValue;
            TransportationCost = item.TransportationCost;
            OtherCosts = item.OtherCosts;
            Comment = item.Comment;
        }

        public void SetPickers()
        {
            if (EditExistingMode)
            {
                SelectedVolumeUnitType = VolumeUnitTypeListOfOptions.Where(x => x.TranslationRowKey == _itemForEditing.VolumeUnitType).FirstOrDefault();
                SelectedSoldTo = SoldToListOfOptions.Where(x => x.TranslationRowKey == _itemForEditing.SoldTo).FirstOrDefault();
                //SelectedPaymentType = PaymentTypeListOfOptions.Where(x => x.TranslationRowKey == _itemForEditing.PaymentType).FirstOrDefault();
            }
        }

        private async void SaveButtonCreateHousingItem(object obj)
        {
            var valid = ValidateSave();

            if (!string.IsNullOrWhiteSpace(valid))
            {
                await Application.Current.MainPage.DisplayAlert(Error, valid, OK);
                return;
            }

            if (_itemForEditing != null)
            {

                _itemForEditing.Date = Date;
                _itemForEditing.VolumeSold = VolumeSold;
                _itemForEditing.VolumeUnitType = SelectedVolumeUnitType != null ? SelectedVolumeUnitType.TranslationRowKey : null;
                _itemForEditing.SoldTo = SelectedSoldTo != null ? SelectedSoldTo.TranslationRowKey : null;
                _itemForEditing.OtherSoldTo = OtherSoldTo;
                _itemForEditing.OtherUnitType = OtherUnitType;
                _itemForEditing.AmountRecieved = (double)AmountRecieved;
                //_itemForEditing.PaymentType = SelectedPaymentType != null ? SelectedPaymentType.TranslationRowKey : null;
                //_itemForEditing.PaymentValue = (double)PaymentValue;
                _itemForEditing.TransportationCost = (double)TransportationCost;
                _itemForEditing.OtherCosts = OtherCosts == null ? 0 : (double)OtherCosts;
                _itemForEditing.Comment = Comment;
                _itemForEditing.LastModified = DateTime.UtcNow;

                await repo.UpdateManureSaleItem(_itemForEditing);
                await Application.Current.MainPage.DisplayAlert(Updated, LogicHelper.GetTranslationFromStore(TranslationStore, "ManureUpdated", User.UserLang), OK);
                await Shell.Current.Navigation.PopAsync();
            }
            else
            {
                var newManureSale = new ManureSaleItem
                {
                    Date = Date,
                    VolumeSold = VolumeSold,
                    VolumeUnitType = SelectedVolumeUnitType != null ? SelectedVolumeUnitType.TranslationRowKey : null,
                    SoldTo = SelectedSoldTo != null ? SelectedSoldTo.TranslationRowKey : null,
                    OtherSoldTo = OtherSoldTo,
                    OtherUnitType = OtherUnitType,
                    AmountRecieved = (double)AmountRecieved,
                    //PaymentType = SelectedPaymentType != null ? SelectedPaymentType.TranslationRowKey : null,
                    //PaymentValue = PaymentValue,
                    TransportationCost = (double)TransportationCost,
                    OtherCosts = OtherCosts == null ? 0 : (double)OtherCosts,
                    Comment = Comment,
                    LastModified = DateTime.UtcNow,
                    CreatedBy = User.UserName,
                    PartitionKey = Constants.PartitionKeyManureSaleItem,
                };

                try
                {
                    await repo.AddSingleManureSaleItem(newManureSale);
                    await Application.Current.MainPage.DisplayAlert(Created, LogicHelper.GetTranslationFromStore(TranslationStore, "ManureSaved", User.UserLang), OK);
                    await Shell.Current.Navigation.PopAsync();
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert(Error, ex.InnerException.Message, OK);
                }
            }
        }

        private async void DeleteItem(object obj)
        {
            if (EditExistingMode)
            {
                var confirmDelete = await Application.Current.MainPage.DisplayAlert(DeleteConfirmation, DeleteVerify, OK, Cancel);
                if (confirmDelete)
                {
                    repo.DeleteManureSaleItem(_itemForEditing);
                    await Shell.Current.Navigation.PopAsync();
                }
            }
        }

        private async void EditItem(object obj)
        {
            IsEditMode = !IsEditMode;
        }

        private async void ResetButtonPressed(object obj)
        {
            ClearFormVariables();
        }

        private void ClearFormVariables()
        {
            VolumeSold = null;
            VolumeUnitType = null;
            SelectedVolumeUnitType = null;
            SelectedSoldTo = null;
            OtherSoldTo = null;
            AmountRecieved = null;
            PaymentValue = null;
            SelectedPaymentType = null;
            TransportationCost = null;
            OtherCosts = null;
            Comment = null;
        }

        public async Task PopulateDataDowns()
        {
            var VolumeUnitTypeControlData = await repo.GetControlData(Constants.VOLUMEUNITTYPE);
            var SoldToControlData = await repo.GetControlData(Constants.SOLDTOTYPE);
            var PaymentTypeControlData = await repo.GetControlData(Constants.OTHERPAYMENTTYPE);

            VolumeUnitTypeListOfOptions = LogicHelper.CreatePickerToolOption(VolumeUnitTypeControlData, User.UserLang);
            SoldToListOfOptions = LogicHelper.CreatePickerToolOption(SoldToControlData, User.UserLang);
            PaymentTypeListOfOptions = LogicHelper.CreatePickerToolOption(PaymentTypeControlData, User.UserLang);

            if (!IsEditMode)
            {
                selectedVolumeUnitType = VolumeUnitTypeListOfOptions.Where(x => x.TranslationRowKey == _itemForEditing.VolumeUnitType).FirstOrDefault();
                selectedSoldTo = SoldToListOfOptions.Where(x => x.TranslationRowKey == _itemForEditing.SoldTo).FirstOrDefault();
                //selectedPaymentType = PaymentTypeListOfOptions.Where(x => x.TranslationRowKey == _itemForEditing.PaymentType).FirstOrDefault();
            }
        }

        private string ValidateSave()
        {
            try
            {
                StringBuilder returnString = new StringBuilder();
                if (Date == null) returnString.AppendLine(LogicHelper.GetTranslationFromStore(TranslationStore, Constants.NoDate, User.UserLang));
                if (AmountRecieved == null) returnString.AppendLine(LogicHelper.GetTranslationFromStore(TranslationStore, Constants.NoAmountRecevied, User.UserLang));
                if (TransportationCost == null) returnString.AppendLine(LogicHelper.GetTranslationFromStore(TranslationStore, "NoTransportationCost", User.UserLang));
                if (PaymentType != null  && PaymentValue == null) returnString.AppendLine(LogicHelper.GetTranslationFromStore(TranslationStore,Constants.NoPaymentValue , User.UserLang));
                //if (OtherCosts == null) returnString.AppendLine("Other Cost Not Provided");


                if (SelectedSoldTo != null && SelectedSoldTo.TranslationRowKey == Constants.OTHER)
                {
                    if (string.IsNullOrWhiteSpace(OtherSoldTo)) returnString.AppendLine(LogicHelper.GetTranslationFromStore(TranslationStore, Constants.NoOtherSoldTo , User.UserLang));
                }

                return returnString.ToString();
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
