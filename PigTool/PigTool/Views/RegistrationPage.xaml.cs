﻿using PigTool.Helpers;
using PigTool.ViewModels.DataViewModels;
using Shared;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PigTool.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        private RegistrationViewModel _viewModel;
        private bool IsRendered = false;
        private UserLangSettings lang;
        private string countryTranslationRowKey;


        public RegistrationPage(string AccessToken, string email, UserLangSettings lang, string countryTranslationRowKey)
        {
            InitializeComponent();
            this.lang = lang;
            this.countryTranslationRowKey = countryTranslationRowKey;
            BindingContext = _viewModel = new RegistrationViewModel(lang, countryTranslationRowKey, true);
            _viewModel.accessToken = AccessToken;
            _viewModel.registeredEmail = email;
            RegistrationTitleLabel.Text = _viewModel.RegistrationTitleTranslation;
        }

        public RegistrationPage(MobileUser user,UserLangSettings lang, string countryTranslationRowKey)
        {
            InitializeComponent();
            this.lang = lang;
            this.countryTranslationRowKey = countryTranslationRowKey;
            BindingContext = _viewModel = new RegistrationViewModel(lang, countryTranslationRowKey, true);
            _viewModel.populatewithData(user);
            _viewModel.accessToken = user.AuthorisedToken;
            _viewModel.registeredEmail = user.AuthorisedEmail;
            RegistrationTitleLabel.Text = _viewModel.RegistrationTitleTranslation;
        }

        /*
        public RegistrationPage(MobileUser UI, bool newUser)
        {
            BindingContext = _viewModel = new RegistrationViewModel(newUser);
            _viewModel.populatewithData(UI);
            InitializeComponent();
        }*/

        protected async override void OnAppearing()
        {
            if (!IsRendered)
            {
                await _viewModel.PopulateDataDowns();

                PopulateTheTable();

                _viewModel.SetPickers();

                base.OnAppearing();

                IsRendered = true;

                _viewModel.ShowSuccess = (async (obj) =>
                {
                    await Shell.Current.GoToAsync("//RegistrationSuccessful");
                });
            }
        }

        private void PopulateTheTable()
        {
            var FullTableSection = new TableSection();
            /*
            // UserName
            var UserNameCell = new ViewCell();
            var UserNameStack = FormattedElementsHelper.TableRowStack();
            UserNameStack.Children.Add(FormattedElementsHelper.FormDataLabel(nameof(_viewModel.UserNameTranslation)));
            UserNameStack.Children.Add(FormattedElementsHelper.FormTextEntry(nameof(_viewModel.UserName), nameof(_viewModel.IsEditMode), null));
            UserNameCell.View = UserNameStack;
            FullTableSection.Add(UserNameCell);*/

            // Name
            var NameCell = new ViewCell();
            var NameStack = FormattedElementsHelper.TableRowStack();
            NameStack.Children.Add(FormattedElementsHelper.FormDataLabel(nameof(_viewModel.NameTranslation)));
            NameStack.Children.Add(FormattedElementsHelper.FormTextEntry(nameof(_viewModel.Name), nameof(_viewModel.IsEditMode), null));
            NameCell.View = NameStack;
            FullTableSection.Add(NameCell);


            //Gender
            var GenderCell = new ViewCell();
            var GenderVerticalStack = FormattedElementsHelper.TableRowStack(stackOrientation: StackOrientation.Vertical);
            GenderVerticalStack.Padding = 0;
            var GenderStack = FormattedElementsHelper.TableRowStack();
            GenderStack.Children.Add(FormattedElementsHelper.FormDataLabel(nameof(_viewModel.GenderTranslation)));
            GenderStack.Children.Add(FormattedElementsHelper.FormPickerEntry(
                nameof(_viewModel.GenderListOfOptions),
                nameof(PickerToolHelper.TranslatedValue),
                nameof(_viewModel.SelectedGender),
                nameof(_viewModel.IsEditMode),
                _viewModel.SelectedGender,
                _viewModel.PickerGenderTranslation
                )
                );
            GenderVerticalStack.Children.Add(GenderStack);
            GenderCell.View = GenderVerticalStack;
            FullTableSection.Add(GenderCell);

            // Email
            var EmailCell = new ViewCell();
            var EmailStack = FormattedElementsHelper.TableRowStack();
            EmailStack.Children.Add(FormattedElementsHelper.FormDataLabel(nameof(_viewModel.EmailTranslation)));
            EmailStack.Children.Add(FormattedElementsHelper.FormTextEntry(nameof(_viewModel.Email), nameof(_viewModel.IsEditMode), null));
            EmailCell.View = EmailStack;
            FullTableSection.Add(EmailCell);

            // PhoneNumber
            var PhoneNumberCell = new ViewCell();
            var PhoneNumberStack = FormattedElementsHelper.TableRowStack();
            PhoneNumberStack.Children.Add(FormattedElementsHelper.FormDataLabel(nameof(_viewModel.PhoneNumberTranslation)));
            PhoneNumberStack.Children.Add(FormattedElementsHelper.FormTextEntry(nameof(_viewModel.PhoneNumber), nameof(_viewModel.IsEditMode), null));
            PhoneNumberCell.View = PhoneNumberStack;
            FullTableSection.Add(PhoneNumberCell);


            // Province
            if (countryTranslationRowKey == Constants.VIETNAMTYPE || countryTranslationRowKey == Constants.RWANDATYPE)
            {
                var ProvinceCell = new ViewCell();
                var ProvinceStack = FormattedElementsHelper.TableRowStack();
                ProvinceStack.Children.Add(FormattedElementsHelper.FormDataLabel(nameof(_viewModel.ProvinceTranslation)));
                ProvinceStack.Children.Add(FormattedElementsHelper.FormTextEntry(nameof(_viewModel.Province), nameof(_viewModel.IsEditMode), null));
                ProvinceCell.View = ProvinceStack;
                FullTableSection.Add(ProvinceCell);
            }

            var DistrictCell = new ViewCell();
            var DistrictStack = FormattedElementsHelper.TableRowStack();
            DistrictStack.Children.Add(FormattedElementsHelper.FormDataLabel(nameof(_viewModel.DistrictTranslation)));
            DistrictStack.Children.Add(FormattedElementsHelper.FormTextEntry(nameof(_viewModel.District), nameof(_viewModel.IsEditMode), null));
            DistrictCell.View = DistrictStack;
            FullTableSection.Add(DistrictCell);

            if (countryTranslationRowKey == Constants.UGANDATYPE)
            {
                
                var CountyCell = new ViewCell();
                var CountyStack = FormattedElementsHelper.TableRowStack();
                CountyStack.Children.Add(FormattedElementsHelper.FormDataLabel(nameof(_viewModel.CountyTranslation)));
                CountyStack.Children.Add(FormattedElementsHelper.FormTextEntry(nameof(_viewModel.County), nameof(_viewModel.IsEditMode), null));
                CountyCell.View = CountyStack;
                FullTableSection.Add(CountyCell);

                var SubCountyCell = new ViewCell();
                var SubCountyStack = FormattedElementsHelper.TableRowStack();
                SubCountyStack.Children.Add(FormattedElementsHelper.FormDataLabel(nameof(_viewModel.SubCountyTranslation)));
                SubCountyStack.Children.Add(FormattedElementsHelper.FormTextEntry(nameof(_viewModel.SubCounty), nameof(_viewModel.IsEditMode), null));
                SubCountyCell.View = SubCountyStack;
                FullTableSection.Add(SubCountyCell);

                // Parish
                var ParishCell = new ViewCell();
                var ParishStack = FormattedElementsHelper.TableRowStack();
                ParishStack.Children.Add(FormattedElementsHelper.FormDataLabel(nameof(_viewModel.ParishTranslation)));
                ParishStack.Children.Add(FormattedElementsHelper.FormTextEntry(nameof(_viewModel.Parish), nameof(_viewModel.IsEditMode), null));
                ParishCell.View = ParishStack;
                FullTableSection.Add(ParishCell);
            }


            // Commune
            if (countryTranslationRowKey == Constants.VIETNAMTYPE)
            {
                var CommuneCell = new ViewCell();
                var CommuneStack = FormattedElementsHelper.TableRowStack();
                CommuneStack.Children.Add(FormattedElementsHelper.FormDataLabel(nameof(_viewModel.CommuneTranslation)));
                CommuneStack.Children.Add(FormattedElementsHelper.FormTextEntry(nameof(_viewModel.Commune), nameof(_viewModel.IsEditMode), null));
                CommuneCell.View = CommuneStack;
                FullTableSection.Add(CommuneCell);
            }


            if (countryTranslationRowKey == Constants.RWANDATYPE)
            {
                // Sector
                var SectorCell = new ViewCell();
                var SectorStack = FormattedElementsHelper.TableRowStack();
                SectorStack.Children.Add(FormattedElementsHelper.FormDataLabel(nameof(_viewModel.SectorTranslation)));
                SectorStack.Children.Add(FormattedElementsHelper.FormTextEntry(nameof(_viewModel.Sector), nameof(_viewModel.IsEditMode), null));
                SectorCell.View = SectorStack;
                FullTableSection.Add(SectorCell);

                // Cell
                var CellCell = new ViewCell();
                var CellStack = FormattedElementsHelper.TableRowStack();
                CellStack.Children.Add(FormattedElementsHelper.FormDataLabel(nameof(_viewModel.CellTranslation)));
                CellStack.Children.Add(FormattedElementsHelper.FormTextEntry(nameof(_viewModel.Cell), nameof(_viewModel.IsEditMode), null));
                CellCell.View = CellStack;
                FullTableSection.Add(CellCell);
            }

            // Village
            var VillageCell = new ViewCell();
            var VillageStack = FormattedElementsHelper.TableRowStack();
            VillageStack.Children.Add(FormattedElementsHelper.FormDataLabel(nameof(_viewModel.VillageTranslation)));
            VillageStack.Children.Add(FormattedElementsHelper.FormTextEntry(nameof(_viewModel.Village), nameof(_viewModel.IsEditMode), null));
            VillageCell.View = VillageStack;
            FullTableSection.Add(VillageCell);

            //Button Commands
            var buttonCell = new ViewCell();
            var buttonStack = FormattedElementsHelper.ButtonCommandStack(
                 ResetCommandBinding: nameof(_viewModel.ResetButtonClicked),
                 EditCommandBinding: nameof(_viewModel.EditButtonClicked),
                 DeleteCommandBinding: nameof(_viewModel.DeleteButtonClicked),
                 SaveCommandBinding: nameof(_viewModel.SaveButtonClicked),
                 EditModeBinding: nameof(_viewModel.IsEditMode),
                 ExistingModeBinding: nameof(_viewModel.EditExistingMode),
                 ResetText: _viewModel.ResetTranslation,
                 SaveText: _viewModel.SaveTranslation,
                 EditText: _viewModel.EditTranslation,
                 DeleteText: _viewModel.DeleteTranslation
                 );
            buttonCell.View = buttonStack;
            FullTableSection.Add(buttonCell);


            RegistrationTableView.Root.Add(FullTableSection);

        }
    }
}