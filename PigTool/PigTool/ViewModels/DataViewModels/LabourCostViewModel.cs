﻿using PigTool.Helpers;
using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PigTool.ViewModels.DataViewModels
{
    public class LabourCostViewModel : LoggedInViewModel, INotifyPropertyChanged
    {
        LabourCostItem _itemForEditing;

        private bool isEditMode;
        public bool IsEditMode
        {
            get => isEditMode;
            set
            {
                if (value != isEditMode)
                {
                    isEditMode = value;
                    OnPropertyChanged(nameof(IsEditMode));
                }

            }
        }

        #region Translations
        public string LabourTitleTranslation { get; set; }
        public string DateTranslation { get; set; }
        public string LabourTypeTranslation { get; set; }
        public string OtherLaboutTypeTranslation { get; set; }
        public string AmountPaidTranslation { get; set; }
        public string OtherCostsTranslation { get; set; }
        public string CommentTranslation { get; set; }
        public string SaveTranslation { get; set; }
        public string EditTranslation { get; set; }
        public string DeleteTranslation { get; set; }
        public string ResetTranslation { get; set; }
        public string PickerLabourTypeTranslation { get; set; }

        public string StartTranslation { get; set; }
        public string FinishTranslation { get; set; }
        public string LabourDurationTranslation { get; set; }
        #endregion


        #region FormValues
        private DateTime date, durationStart, durationFinish;

        public DateTime Date { get => date; set { if (date != value) { date = value; OnPropertyChanged(nameof(Date)); } } }
        public string LabourType
        {
            get => labourType; set
            {
                if (labourType != value)
                {
                    labourType = value;
                    OnPropertyChanged(nameof(LabourType));
                }
            }
        }
        public DateTime DurationStart
        {
            get => durationStart;
            set
            {
                if (durationStart != value)
                {
                    durationStart = value;
                    OnPropertyChanged(nameof(DurationStart));
                }
            }
        }
        public DateTime DurationFinish
        {
            get => durationFinish;
            set
            {
                if (durationFinish != value)
                {
                    durationFinish = value;
                    OnPropertyChanged(nameof(DurationFinish));
                }
            }
        }
        public string OtherLaboutType
        {
            get => otherLaboutType; set
            {
                if (otherLaboutType != value)
                {
                    otherLaboutType = value;
                    OnPropertyChanged(nameof(otherLaboutType));
                }
            }
        }
        public double? AmountPaid
        {
            get => amountPaid;
            set
            {
                if (amountPaid != value)
                {
                    amountPaid = value;
                    OnPropertyChanged(nameof(AmountPaid));
                }
            }
        }
        public double? OtherCosts
        {
            get => otherCosts; set
            {
                if (otherCosts != value)
                {
                    otherCosts = value;
                    OnPropertyChanged(nameof(OtherCosts));
                }
            }
        }
        public string Comment { get => comment; set { if (comment != value) { comment = value; OnPropertyChanged(nameof(Comment)); } } }

        private string labourType;
        private PickerToolHelper selectedLabourType;
        private bool displayOtherLabourType;
        private string otherLaboutType;
        private double? amountPaid;
        private double? otherCosts;
        private string comment;

        public PickerToolHelper SelectedLabourType
        {
            get => selectedLabourType;
            set
            {
                if (selectedLabourType != value)
                {
                    DisplayOtherLabourType = value?.TranslationRowKey == Constants.OTHER;
                    selectedLabourType = value;
                    //LabourType = value.TranslationRowKey; #caused bug,  do not think this line is needed
                    OnPropertyChanged(nameof(SelectedLabourType));
                }
            }
        }

        #endregion

        public bool DisplayOtherLabourType
        {
            get => displayOtherLabourType;
            set
            {
                if (displayOtherLabourType != value)
                {
                    displayOtherLabourType = value;
                    OnPropertyChanged(nameof(DisplayOtherLabourType));
                }
            }
        }

        private List<PickerToolHelper> labourTypeOptions;
        private bool editExistingMode;

        public List<PickerToolHelper> LabourTypeOptions
        {

            get { return labourTypeOptions; }
            set
            {
                labourTypeOptions = value;
                OnPropertyChanged(nameof(LabourTypeOptions));
            }
        }

        public bool EditExistingMode { get => editExistingMode; set { if (editExistingMode != value) { editExistingMode = value; OnPropertyChanged(nameof(EditExistingMode)); } } }

        public Command SaveButtonClicked { get; }
        public Command ResetButtonClicked { get; }
        public Command DeleteButtonClicked { get; }
        public Command EditButtonClicked { get; }
        public bool CreationMode { get; private set; }

        public LabourCostViewModel()
        {
            Date = DateTime.Now;
            DurationStart = DateTime.Now;
            DurationFinish = DateTime.Now;
            LabourType = null;
            OtherLaboutType = null;
            AmountPaid = null;
            OtherCosts = null;
            Comment = null;

            IsEditMode = true;
            CreationMode = true;

            SaveButtonClicked = new Command(SaveButtonCreateLabourCostItemAsync);
            ResetButtonClicked = new Command(ResetButtonPressed);
            DeleteButtonClicked = new Command(DeleteItem);
            EditButtonClicked = new Command(EditItemAsync);
            EditExistingMode = !CreationMode;

            LabourTitleTranslation = Labour;
            DateTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(DateTranslation), User.UserLang) + " *";
            LabourTypeTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(LabourTypeTranslation), User.UserLang);
            OtherLaboutTypeTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(OtherLaboutTypeTranslation), User.UserLang);
            AmountPaidTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(AmountPaidTranslation), User.UserLang) + " *";
            OtherCostsTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(OtherCostsTranslation), User.UserLang);
            CommentTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(CommentTranslation), User.UserLang);
            SaveTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(SaveTranslation), User.UserLang);
            ResetTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(ResetTranslation), User.UserLang);
            EditTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(EditTranslation), User.UserLang);
            DeleteTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(DeleteTranslation), User.UserLang);

            PickerLabourTypeTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(PickerLabourTypeTranslation), User.UserLang);

            StartTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(StartTranslation), User.UserLang) + " *";
            FinishTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(FinishTranslation), User.UserLang) + " *";
            LabourDurationTranslation = LogicHelper.GetTranslationFromStore(TranslationStore, nameof(LabourDurationTranslation), User.UserLang);

        }

        public void populatewithData(LabourCostItem item)
        {
            isEditMode = true;
            CreationMode = false;
            EditExistingMode = !CreationMode;

            _itemForEditing = item;

            Date = item.Date;
            LabourType = item.LabourType;
            OtherLaboutType = item.OtherLabourType;
            AmountPaid = item.AmountPaid;
            OtherCosts = item.OtherCost;
            Comment = item.Comment;
            DurationStart = item.DurationStart;
            DurationFinish = item.DurationFinish;
        }

        public void SetPickers()
        {
            if (EditExistingMode)
            {
                SelectedLabourType = labourTypeOptions.Where(x => x.TranslationRowKey == _itemForEditing.LabourType).FirstOrDefault();
            }
        }


        public async Task PopulateDataDowns()
        {
            LabourTypeOptions = LogicHelper.CreatePickerToolOption(await repo.GetControlData(Constants.LABOURTYPE), User.UserLang);


            if (!IsEditMode)
            {
                SelectedLabourType = LabourTypeOptions.Where(x => x.TranslationRowKey == _itemForEditing.LabourType).FirstOrDefault();
            }

        }


        private void EditItemAsync()
        {
            IsEditMode = !IsEditMode;
        }

        private async void DeleteItem(object obj)
        {
            if (EditExistingMode)
            {
                var confirmDelete = await Application.Current.MainPage.DisplayAlert(DeleteConfirmation, DeleteVerify, OK, Cancel);
                if (confirmDelete)
                {
                    //repo.DeleteHealthCaredItem(_itemForEditing);
                    await Shell.Current.Navigation.PopAsync();
                }
            }
        }

        private void ResetButtonPressed(object obj)
        {
            ClearFormVariables();
        }

        private void ClearFormVariables()
        {
            SelectedLabourType = null;
            AmountPaid = null;
            OtherCosts = null;
            OtherLaboutType = null;
            Comment = null;
            Date = DateTime.Today;
            DurationStart = DateTime.Today;
            DurationFinish = DateTime.Today;
        }

        private async void SaveButtonCreateLabourCostItemAsync(object obj)
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
                _itemForEditing.LabourType = SelectedLabourType != null ? SelectedLabourType.TranslationRowKey : null;
                _itemForEditing.OtherLabourType = OtherLaboutType;
                _itemForEditing.AmountPaid = (double)AmountPaid;
                _itemForEditing.OtherCost = OtherCosts == null ? 0 : (double)OtherCosts;
                _itemForEditing.Comment = Comment;
                _itemForEditing.LastModified = DateTime.UtcNow;
                _itemForEditing.DurationStart = DurationStart;
                _itemForEditing.DurationFinish = DurationFinish;

                await repo.UpdateLabourCostItem(_itemForEditing);
                await Application.Current.MainPage.DisplayAlert(Updated,  LogicHelper.GetTranslationFromStore(TranslationStore, "RecordLabourUpdated", User.UserLang), OK);
                await Shell.Current.Navigation.PopAsync();

            }
            else
            {
                try
                {
                    var newLabourCost = new LabourCostItem
                    {
                        Date = Date,
                        LabourType = SelectedLabourType != null ? SelectedLabourType.TranslationRowKey : null,
                        OtherLabourType = OtherLaboutType,
                        AmountPaid = (double)AmountPaid,
                        OtherCost = OtherCosts == null ? 0 : (double)OtherCosts,
                        Comment = Comment,
                        LastModified = DateTime.UtcNow,
                        CreatedBy = User.UserName,
                        PartitionKey = Constants.PartitionKeyLabourCostItem,
                        DurationStart = DurationStart,
                        DurationFinish = DurationFinish
                    };

                    await repo.AddSingleLabourCostItem(newLabourCost);
                    await Application.Current.MainPage.DisplayAlert(Created, LogicHelper.GetTranslationFromStore(TranslationStore, "RecordLabourCostSaved", User.UserLang), OK);
                    await Shell.Current.Navigation.PopAsync();
                }
                catch (Exception ex)
                {
                    if(ex.InnerException!= null) { 
                        await Application.Current.MainPage.DisplayAlert(Error, ex.InnerException.Message, OK);
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert(Error, ex.InnerException.Message, OK);
                    }

                }
            }

        }

        private string ValidateSave()
        {
            StringBuilder returnString = new StringBuilder();
            if (Date == null) returnString.AppendLine(LogicHelper.GetTranslationFromStore(TranslationStore, Constants.NoDate, User.UserLang));
            if (AmountPaid == null) returnString.AppendLine(LogicHelper.GetTranslationFromStore(TranslationStore, Constants.AmountPaidNotProvided, User.UserLang));
            //if (OtherCosts == null) returnString.AppendLine("Other Cost Not Provided");
            if (DurationStart == null) returnString.AppendLine(LogicHelper.GetTranslationFromStore(TranslationStore, Constants.NoDurationStart, User.UserLang));
            if (DurationFinish == null) returnString.AppendLine(LogicHelper.GetTranslationFromStore(TranslationStore, Constants.NoDurationEnd, User.UserLang));
            if (DurationFinish < DurationStart) returnString.AppendLine(LogicHelper.GetTranslationFromStore(TranslationStore, Constants.DurIsbefore, User.UserLang));

            if (selectedLabourType != null && selectedLabourType.TranslationRowKey == Constants.OTHER)
            {
                if (string.IsNullOrWhiteSpace(OtherLaboutType)) returnString.AppendLine(LogicHelper.GetTranslationFromStore(TranslationStore, Constants.NoOtherLabourType, User.UserLang));
            }

            return returnString.ToString();
        }
    }
}
