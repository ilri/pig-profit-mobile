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
    public class HealthCareViewModel : LoggedInViewModel, INotifyPropertyChanged
    {
        bool isEditMode, isCreationMode;
        private bool editExistingMode;
        private DateTime date;
        private string healthCareType;
        private string otherHealthCareType;
        private double? healthCareCost;
        private string provider;
        private string otherProvider;
        private double? medicineCost;
        private string medicineType;
        private string otherMedicineType;
        private string purchasedFrom;
        private string otherPurchasedFrom;
        private double? transportationCost;
        private double? otherCosts;
        private string comment;
        List<PickerToolHelper> healthCareTypeListOfOptions;
        List<PickerToolHelper> providerListOfOptions;
        List<PickerToolHelper> medicineTypeListOfOptions;
        List<PickerToolHelper> purchasedFromListOfOptions;
        HealthCareItem _itemForEditing;

        //Button Clicks
        public Command SaveButtonClicked { get; }
        public Command ResetButtonClicked { get; }
        public Command DeleteButtonClicked { get; }
        public Command EditButtonClicked { get; }

        #region translations
        public string HealthCareTitleTranslation { get; set; }
        public string DateTranslation { get; set; }

        public string HealthCareTypeTranslation { get; set; }
        public string OtherHealthCareTypeTranslation { get; set; }
        public string ProviderTranslation { get; set; }
        public string OtherProviderTranslation { get; set; }
        public string MedicineTypeTranslation { get; set; }
        public string OtherMedicineTypeTranslation { get; set; }
        public string PurchasedFromTranslation { get; set; }
        public string OtherPurchasedFromTranslation { get; set; }

        public string HealthCareCostTranslation { get; set; }
        public string MedicineCostTranslation { get; set; }

        public string TotalCostTranslation { get; set; }
        public string TransportationCostTranslation { get; set; }
        public string OtherCostTranslation { get; set; }
        public string CommentTranslation { get; set; }

        public string SaveTranslation { get; set; }
        public string ResetTranslation { get; set; }
        public string EditTranslation { get; set; }
        public string DeleteTranslation { get; set; }

        public string PickerMedicineTypeTranslation { get; set; }
        public string PickerHealthCareTypeTranslation { get; set; }
        public string PickerProviderTranslation { get; set; }
        public string PickerPurchasedFromTranslation { get; set; }
        #endregion

        #region Health care item fields
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
        public string HealthCareType
        {
            get => healthCareType;
            set
            {
                if (value != healthCareType)
                {
                    healthCareType = value;
                    OnPropertyChanged(nameof(HealthCareType));
                }
            }
        }
        public string OtherHealthCareType
        {
            get => otherHealthCareType;
            set
            {
                if (value != otherHealthCareType)
                {
                    otherHealthCareType = value;
                    OnPropertyChanged(nameof(OtherHealthCareType));
                }
            }
        }
        public double? HealthCareCost
        {
            get => healthCareCost;
            set
            {
                if (value != healthCareCost)
                {
                    healthCareCost = value;
                    OnPropertyChanged(nameof(HealthCareCost));
                }
            }
        }
        public string Provider
        {
            get => provider;
            set
            {
                if (value != provider)
                {
                    provider = value;
                    OnPropertyChanged(nameof(Provider));
                }
            }
        }
        public string OtherProvider
        {
            get => otherProvider;
            set
            {
                if (value != otherProvider)
                {
                    otherProvider = value;
                    OnPropertyChanged(nameof(OtherProvider));
                }
            }
        }
        public double? MedicineCost
        {
            get => medicineCost;
            set
            {
                if (value != medicineCost)
                {
                    medicineCost = value;
                    OnPropertyChanged(nameof(MedicineCost));
                }
            }
        }
        public string MedicineType
        {
            get => medicineType;
            set
            {
                if (value != medicineType)
                {
                    medicineType = value;
                    OnPropertyChanged(nameof(MedicineType));
                }
            }
        }
        public string OtherMedicineType
        {
            get => otherMedicineType;
            set
            {
                if (value != otherMedicineType)
                {
                    otherMedicineType = value;
                    OnPropertyChanged(nameof(OtherMedicineType));
                }
            }
        }
        public string PurchasedFrom
        {
            get => purchasedFrom;
            set
            {
                if (value != purchasedFrom)
                {
                    purchasedFrom = value;
                    OnPropertyChanged(nameof(PurchasedFrom));
                }
            }
        }
        public string OtherPurchasedFrom
        {
            get => otherPurchasedFrom;
            set
            {
                if (value != otherPurchasedFrom)
                {
                    otherPurchasedFrom = value;
                    OnPropertyChanged(nameof(OtherPurchasedFrom));
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
        public List<PickerToolHelper> HealthCareTypeListOfOptions
        {
            get { return healthCareTypeListOfOptions; }
            set
            {
                healthCareTypeListOfOptions = value;
                OnPropertyChanged(nameof(HealthCareTypeListOfOptions));
            }
        }

        public List<PickerToolHelper> ProviderListOfOptions
        {
            get { return providerListOfOptions; }
            set
            {
                providerListOfOptions = value;
                OnPropertyChanged(nameof(ProviderListOfOptions));
            }
        }

        public List<PickerToolHelper> MedicineTypeListOfOptions
        {
            get { return medicineTypeListOfOptions; }
            set
            {
                medicineTypeListOfOptions = value;
                OnPropertyChanged(nameof(MedicineTypeListOfOptions));
            }
        }

        public List<PickerToolHelper> PurchasedFromListOfOptions
        {
            get { return purchasedFromListOfOptions; }
            set
            {
                purchasedFromListOfOptions = value;
                OnPropertyChanged(nameof(PurchasedFromListOfOptions));
            }
        }

        private PickerToolHelper selectedHealthCareType;

        public PickerToolHelper SelectedHealthCareType
        {
            get { return selectedHealthCareType; }
            set
            {
                if (selectedHealthCareType != value)
                {
                    DisplayOtherHealthCareType = value?.TranslationRowKey == Constants.OTHER;
                    selectedHealthCareType = value;
                    OnPropertyChanged(nameof(SelectedHealthCareType));
                }
            }
        }

        private PickerToolHelper selectedProvider;

        public PickerToolHelper SelectedProvider
        {
            get { return selectedProvider; }
            set
            {
                if (selectedProvider != value)
                {
                    DisplayOtherProvider = value?.TranslationRowKey == Constants.OTHER;
                    selectedProvider = value;
                    OnPropertyChanged(nameof(SelectedProvider));
                }
            }
        }

        private PickerToolHelper selectedMedicineType;

        public PickerToolHelper SelectedMedicineType
        {
            get { return selectedMedicineType; }
            set
            {
                if (selectedMedicineType != value)
                {
                    DisplayOtherMedicineType = value?.TranslationRowKey == Constants.OTHER;
                    selectedMedicineType = value;
                    OnPropertyChanged(nameof(SelectedMedicineType));
                }
            }
        }

        private PickerToolHelper selectedPurchasedFrom;

        public PickerToolHelper SelectedPurchasedFrom
        {
            get { return selectedPurchasedFrom; }
            set
            {
                if (selectedPurchasedFrom != value)
                {
                    DisplayOtherPurchasedFrom = value?.TranslationRowKey == Constants.OTHER;
                    selectedPurchasedFrom = value;
                    OnPropertyChanged(nameof(SelectedPurchasedFrom));
                }
            }
        }
        #endregion

        #region Hidden Fields
        private bool displayOtherHealthCareType;
        private bool displayOtherProvider;
        private bool displayOtherMedicineType;
        private bool displayOtherPurchasedFrom;

        public bool DisplayOtherHealthCareType
        {
            get => displayOtherHealthCareType;
            set
            {
                if (displayOtherHealthCareType != value)
                {
                    displayOtherHealthCareType = value;
                    OnPropertyChanged(nameof(DisplayOtherHealthCareType));
                }
            }
        }
        public bool DisplayOtherProvider
        {
            get => displayOtherProvider;
            set
            {
                if (displayOtherProvider != value)
                {
                    displayOtherProvider = value;
                    OnPropertyChanged(nameof(DisplayOtherProvider));
                }
            }
        }
        public bool DisplayOtherMedicineType
        {
            get => displayOtherMedicineType;
            set
            {
                if (displayOtherMedicineType != value)
                {
                    displayOtherMedicineType = value;
                    OnPropertyChanged(nameof(DisplayOtherMedicineType));
                }
            }
        }
        public bool DisplayOtherPurchasedFrom
        {
            get => displayOtherPurchasedFrom;
            set
            {
                if (displayOtherPurchasedFrom != value)
                {
                    displayOtherPurchasedFrom = value;
                    OnPropertyChanged(nameof(DisplayOtherPurchasedFrom));
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

        public HealthCareViewModel()
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

            HealthCareTitleTranslation = Healthcare;
            DateTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(DateTranslation), User.UserLang) + " *";

            HealthCareTypeTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(HealthCareTypeTranslation), User.UserLang);
            OtherHealthCareTypeTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(OtherHealthCareTypeTranslation), User.UserLang);
            ProviderTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(ProviderTranslation), User.UserLang);
            OtherProviderTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(OtherProviderTranslation), User.UserLang);
            MedicineTypeTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(MedicineTypeTranslation), User.UserLang);
            OtherMedicineTypeTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(OtherMedicineTypeTranslation), User.UserLang);
            PurchasedFromTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(PurchasedFromTranslation), User.UserLang);
            OtherPurchasedFromTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(OtherPurchasedFromTranslation), User.UserLang);

            HealthCareCostTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(HealthCareCostTranslation), User.UserLang) + " *";
            MedicineCostTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(MedicineCostTranslation), User.UserLang) + " *";

            TransportationCostTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(TransportationCostTranslation), User.UserLang) + " *";
            OtherCostTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(OtherCostTranslation), User.UserLang);
            CommentTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(CommentTranslation), User.UserLang);

            SaveTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(SaveTranslation), User.UserLang);
            ResetTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(ResetTranslation), User.UserLang);
            EditTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(EditTranslation), User.UserLang);
            DeleteTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(DeleteTranslation), User.UserLang);

            PickerHealthCareTypeTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(PickerHealthCareTypeTranslation), User.UserLang);
            PickerPurchasedFromTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(PickerPurchasedFromTranslation), User.UserLang);
            PickerProviderTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(PickerProviderTranslation), User.UserLang);
            PickerMedicineTypeTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(PickerMedicineTypeTranslation), User.UserLang);
        }

        public void populatewithData(HealthCareItem item)
        {
            isEditMode = true;
            CreationMode = false;
            EditExistingMode = !CreationMode;

            _itemForEditing = item;

            Date = item.Date;
            HealthCareType = item.HealthCareType;
            OtherHealthCareType = item.OtherHealthCareType;
            HealthCareCost = item.HealthCareCost;
            Provider = item.Provider;
            OtherProvider = item.OtherProvider;
            MedicineCost = item.MedicineCost;
            MedicineType = item.MedicineType;
            OtherMedicineType = item.OtherMedicineType;
            PurchasedFrom = item.PurchasedFrom;
            OtherPurchasedFrom = item.OtherPurchasedFrom;
            TransportationCost = item.TransportationCost;
            OtherCosts = item.OtherCosts;
            Comment = item.Comment;
        }

        public void SetPickers()
        {
            if (EditExistingMode)
            {
                SelectedHealthCareType = HealthCareTypeListOfOptions.Where(x => x.TranslationRowKey == _itemForEditing.HealthCareType).FirstOrDefault();
                SelectedProvider = ProviderListOfOptions.Where(x => x.TranslationRowKey == _itemForEditing.Provider).FirstOrDefault();
                SelectedMedicineType = MedicineTypeListOfOptions.Where(x => x.TranslationRowKey == _itemForEditing.MedicineType).FirstOrDefault();
                SelectedPurchasedFrom = PurchasedFromListOfOptions.Where(x => x.TranslationRowKey == _itemForEditing.PurchasedFrom).FirstOrDefault();
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

                _itemForEditing.HealthCareType = SelectedHealthCareType != null ? SelectedHealthCareType.TranslationRowKey : null;
                _itemForEditing.OtherHealthCareType = OtherHealthCareType;
                _itemForEditing.HealthCareCost = (double)HealthCareCost;
                _itemForEditing.Provider = SelectedProvider != null ? SelectedProvider.TranslationRowKey : null;
                _itemForEditing.OtherProvider = OtherProvider;
                _itemForEditing.MedicineCost = (double)MedicineCost;
                _itemForEditing.MedicineType = SelectedMedicineType != null ? SelectedMedicineType.TranslationRowKey : null;
                _itemForEditing.OtherMedicineType = OtherMedicineType;
                _itemForEditing.PurchasedFrom = SelectedPurchasedFrom != null ? SelectedPurchasedFrom.TranslationRowKey : null;
                _itemForEditing.OtherPurchasedFrom = OtherPurchasedFrom;
                //_itemForEditing.CreatedBy = User.AuthorisedUserName;
                _itemForEditing.TransportationCost = (double)TransportationCost;
                _itemForEditing.OtherCosts = OtherCosts == null ? 0 : (double)OtherCosts;
                _itemForEditing.Comment = Comment;
                _itemForEditing.LastModified = DateTime.UtcNow;

                await repo.UpdateHealthCareItem(_itemForEditing);
                await Application.Current.MainPage.DisplayAlert(Updated, LogicHelper.GetTranslationFromStore(TranslationStore, "HealthUpdated", User.UserLang), OK);
                await Shell.Current.Navigation.PopAsync();
            }
            else
            {


                try
                {
                    var newHealthCare = new HealthCareItem();
                    newHealthCare = new HealthCareItem
                    {
                        Date = Date,
                        HealthCareType = SelectedHealthCareType != null ? SelectedHealthCareType.TranslationRowKey : null,
                        OtherHealthCareType = OtherHealthCareType,
                        HealthCareCost = (double)HealthCareCost,
                        Provider = SelectedProvider != null ? SelectedProvider.TranslationRowKey : null,
                        OtherProvider = OtherProvider,
                        MedicineCost = (double)MedicineCost,
                        MedicineType = SelectedMedicineType != null ? SelectedMedicineType.TranslationRowKey : null,
                        OtherMedicineType = OtherMedicineType,
                        PurchasedFrom = SelectedPurchasedFrom != null ? SelectedPurchasedFrom.TranslationRowKey : null,
                        OtherPurchasedFrom = OtherPurchasedFrom,
                        TransportationCost = (double)TransportationCost,
                        OtherCosts = OtherCosts == null ? 0 : (double)OtherCosts,
                        Comment = Comment,
                        LastModified = DateTime.UtcNow,
                        CreatedBy = User.UserName,
                        PartitionKey = Constants.PartitionKeyHealthCareItem,
                    };
                    await repo.AddSingleHealthCareItem(newHealthCare);
                    await Application.Current.MainPage.DisplayAlert(Created, LogicHelper.GetTranslationFromStore(TranslationStore, "HealthSaved", User.UserLang), OK);
                    await Shell.Current.Navigation.PopAsync();
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                    {
                        await Application.Current.MainPage.DisplayAlert(Error, ex.InnerException.Message, OK);
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert(Error, ex.Message, OK);
                    }
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
                    repo.DeleteHealthCareItem(_itemForEditing);
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
            SelectedHealthCareType = null;
            OtherHealthCareType = null;
            HealthCareCost = null;
            SelectedProvider = null;
            OtherProvider = null;
            MedicineCost = null;
            SelectedMedicineType = null;
            OtherMedicineType = null;
            SelectedPurchasedFrom = null;
            OtherPurchasedFrom = null;
            TransportationCost = null;
            OtherCosts = null;
            Comment = null;
        }

        public async Task PopulateDataDowns()
        {
            var HealthCareTypeControlData = await repo.GetControlData(Constants.HEALTHCARETYPE);
            var ProviderControlData = await repo.GetControlData(Constants.HEALTHSERVICEPROVIDER);
            var MedicineTypeControlData = await repo.GetControlData(Constants.HEALTHMEDICETYPE);
            var PurchasedFromControlData = await repo.GetControlData(Constants.HEALTHPURCHASEFROMTYPE);

            HealthCareTypeListOfOptions = LogicHelper.CreatePickerToolOption(HealthCareTypeControlData, User.UserLang);
            ProviderListOfOptions = LogicHelper.CreatePickerToolOption(ProviderControlData, User.UserLang);
            MedicineTypeListOfOptions = LogicHelper.CreatePickerToolOption(MedicineTypeControlData, User.UserLang);
            PurchasedFromListOfOptions = LogicHelper.CreatePickerToolOption(PurchasedFromControlData, User.UserLang);

            if (!IsEditMode)
            {
                SelectedHealthCareType = HealthCareTypeListOfOptions.Where(x => x.TranslationRowKey == _itemForEditing.HealthCareType).FirstOrDefault();
                SelectedProvider = ProviderListOfOptions.Where(x => x.TranslationRowKey == _itemForEditing.Provider).FirstOrDefault();
                SelectedMedicineType = MedicineTypeListOfOptions.Where(x => x.TranslationRowKey == _itemForEditing.MedicineType).FirstOrDefault();
                SelectedPurchasedFrom = PurchasedFromListOfOptions.Where(x => x.TranslationRowKey == _itemForEditing.PurchasedFrom).FirstOrDefault();
            }
        }

        private string ValidateSave()
        {
            try
            {
                StringBuilder returnString = new StringBuilder();
                if (Date == null) returnString.AppendLine(LogicHelper.GetTranslationFromStore(TranslationStore, Constants.NoDate, User.UserLang));
                if (HealthCareCost == null) returnString.AppendLine(LogicHelper.GetTranslationFromStore(TranslationStore, Constants.NoHealthCareCost, User.UserLang));
                if (MedicineCost == null) returnString.AppendLine(LogicHelper.GetTranslationFromStore(TranslationStore, Constants.NoMedicineCost, User.UserLang));
                if (TransportationCost == null) returnString.AppendLine(LogicHelper.GetTranslationFromStore(TranslationStore, "NoTransportationCost", User.UserLang));
                //if (OtherCosts == null) returnString.AppendLine("Other Cost Not Provided");


                if (SelectedHealthCareType != null && SelectedHealthCareType.TranslationRowKey == Constants.OTHER)
                {
                    if (string.IsNullOrWhiteSpace(OtherHealthCareType)) returnString.AppendLine(LogicHelper.GetTranslationFromStore(TranslationStore,Constants.NoOtherHealthCareYpe, User.UserLang)); 
                }

                if (SelectedProvider != null && SelectedProvider.TranslationRowKey == Constants.OTHER)
                {
                    if (string.IsNullOrWhiteSpace(OtherProvider)) returnString.AppendLine(LogicHelper.GetTranslationFromStore(TranslationStore,Constants.NoOtherProvider, User.UserLang)); 
                }

                if (SelectedMedicineType != null && SelectedMedicineType.TranslationRowKey == Constants.OTHER)
                { 
                    if (string.IsNullOrWhiteSpace(OtherMedicineType)) returnString.AppendLine(LogicHelper.GetTranslationFromStore(TranslationStore, Constants.NoOtherMediceType, User.UserLang)); 
                }

                if (SelectedPurchasedFrom != null && SelectedPurchasedFrom.TranslationRowKey == Constants.OTHER)
                {
                    if (string.IsNullOrWhiteSpace(OtherPurchasedFrom)) returnString.AppendLine(LogicHelper.GetTranslationFromStore(TranslationStore, Constants.NoOtherPurcFrom, User.UserLang)); 
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
