﻿using PigTool.Helpers;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PigTool.ViewModels.DataViewModels;

namespace PigTool.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MembershipPage : ContentPage
    {
        private MembershipViewModel _viewModel;
        private bool IsRendered = false;

        public MembershipPage()
        {
            BindingContext = _viewModel = new MembershipViewModel();
            InitializeComponent();
        }

        public MembershipPage(MembershipItem MI)
        {
            BindingContext = _viewModel = new MembershipViewModel();
            _viewModel.populatewithData(MI);
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            if (!IsRendered)
            {
                await _viewModel.PopulateDataDowns();

                PopulateTheTable();

                _viewModel.SetPickers();

                base.OnAppearing();

                IsRendered = true;
            }
        }

        private void PopulateTheTable()
        {
            var FullTableSection = new TableSection();

            if (_viewModel.EditExistingMode)
            {
                var buttonCellTop = new ViewCell();
                var buttonStackTop = FormattedElementsHelper.ButtonCommandStack(
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
                buttonCellTop.View = buttonStackTop;
                FullTableSection.Add(buttonCellTop);
            }

            //Date
            var DateCell = new ViewCell();
            var stack = FormattedElementsHelper.TableRowStack();
            stack.Children.Add(FormattedElementsHelper.FormDataLabel(nameof(_viewModel.DateTranslation)));
            stack.Children.Add(FormattedElementsHelper.FormDatePicker(nameof(_viewModel.Date), nameof(_viewModel.IsEditMode)));
            DateCell.View = stack;
            FullTableSection.Add(DateCell);

            // Duration
            var DurationCell = new ViewCell();
            var DurationVerticalStack = FormattedElementsHelper.TableRowStack(stackOrientation: StackOrientation.Vertical);

            var DurationLabel = FormattedElementsHelper.DataLabel(nameof(_viewModel.MembershipDurationTranslation));
            DurationVerticalStack.Children.Add(DurationLabel);

            //DurationStart
            var DurationStartCell = new ViewCell();
            var DurationStartStack = FormattedElementsHelper.TableRowStack();
            DurationStartStack.Children.Add(FormattedElementsHelper.FormDataLabel(nameof(_viewModel.StartTranslation)));
            DurationStartStack.Children.Add(FormattedElementsHelper.FormDatePicker(nameof(_viewModel.DurationStart), nameof(_viewModel.IsEditMode)));
            DurationStartCell.View = DurationStartStack;

            //DurationFinish
            var DurationFinishCell = new ViewCell();
            var DurationFinishStack = FormattedElementsHelper.TableRowStack();
            DurationFinishStack.Children.Add(FormattedElementsHelper.FormDataLabel(nameof(_viewModel.FinishTranslation)));
            DurationFinishStack.Children.Add(FormattedElementsHelper.FormDatePicker(
                nameof(_viewModel.DurationFinish), 
                nameof(_viewModel.IsEditMode),
                nameof(_viewModel.DurationStart)));
            DurationFinishCell.View = DurationFinishStack;

            DurationVerticalStack.Children.Add(DurationStartStack);
            DurationVerticalStack.Children.Add(DurationFinishStack);
            DurationCell.View = DurationVerticalStack;
            FullTableSection.Add(DurationCell);

            //Total Cost
            var TotalCostCell = new ViewCell();
            var TotalCostStack = FormattedElementsHelper.TableRowStack();
            TotalCostStack.Children.Add(FormattedElementsHelper.FormDataLabel(nameof(_viewModel.TotalCostTranslation)));
            TotalCostStack.Children.Add(FormattedElementsHelper.FormNumericEntry(nameof(_viewModel.TotalCosts), nameof(_viewModel.IsEditMode), null));
            TotalCostCell.View = TotalCostStack;
            FullTableSection.Add(TotalCostCell);

            //Membership
            var MembershipCell = new ViewCell();
            var MembershipVerticalStack = FormattedElementsHelper.TableRowStack(stackOrientation: StackOrientation.Vertical);
            MembershipVerticalStack.Padding = 0;
            var MembershipTypeStack = FormattedElementsHelper.TableRowGrid();
            FormattedElementsHelper.AddGridValue(
                MembershipTypeStack,
                FormattedElementsHelper.FormDataLabel(nameof(_viewModel.MembershipTypeTranslation)),
                GridPostion.TwoLeft);
            FormattedElementsHelper.AddGridValue(
                MembershipTypeStack,
                FormattedElementsHelper.FormPickerEntry(
                    nameof(_viewModel.MembershipTypeListOfOptions),
                    nameof(PickerToolHelper.TranslatedValue),
                    nameof(_viewModel.SelectedMembershipType),
                    nameof(_viewModel.IsEditMode),
                    _viewModel.SelectedMembershipType,
                    _viewModel.PickerMembershipTypeTranslation
                    ),
                GridPostion.TwoRight);
            var OtherMembershipType = FormattedElementsHelper.TableRowGrid(nameof(_viewModel.DisplayOtherMembershipType), true);
            FormattedElementsHelper.AddGridValue(
                OtherMembershipType,
                FormattedElementsHelper.FormDataLabel(nameof(_viewModel.OtherMembershipTypeTranslation)),
                GridPostion.TwoLeft);
            FormattedElementsHelper.AddGridValue(
                OtherMembershipType,
                FormattedElementsHelper.FormTextEntry(nameof(_viewModel.OtherMembershipType), nameof(_viewModel.IsEditMode)),
                GridPostion.TwoRight);
            MembershipVerticalStack.Children.Add(MembershipTypeStack);
            MembershipVerticalStack.Children.Add(OtherMembershipType);
            MembershipCell.View = MembershipVerticalStack;
            FullTableSection.Add(MembershipCell);

            //Time Period
            /*
            var TimePeriodCell = new ViewCell();

            var UnitVerticalStack = FormattedElementsHelper.TableRowStack(stackOrientation: StackOrientation.Vertical);
            UnitVerticalStack.Padding = 0;

            var TimePeriodUnitTypeStack = FormattedElementsHelper.TableRowStack();
            TimePeriodUnitTypeStack.Children.Add(FormattedElementsHelper.FormDataLabel(nameof(_viewModel.TimePeriodTranslation)));

            var InputContainer = FormattedElementsHelper.TableRowStack();
            InputContainer.Padding = 0;
            InputContainer.Children.Add(FormattedElementsHelper.FormNumericEntry(nameof(_viewModel.TimePeriod), nameof(_viewModel.IsEditMode), null));
            InputContainer.Children.Add(FormattedElementsHelper.FormPickerEntry(
                nameof(_viewModel.TimePeriodUnitListOfOptions),
                nameof(PickerToolHelper.TranslatedValue),
                nameof(_viewModel.SelectedTimePeriodUnit),
                nameof(_viewModel.IsEditMode),
                _viewModel.SelectedTimePeriodUnit,
                _viewModel.PickerUnitTranslation
                )
                );

            TimePeriodUnitTypeStack.Children.Add(InputContainer);
            
            UnitVerticalStack.Children.Add(TimePeriodUnitTypeStack);
            TimePeriodCell.View = UnitVerticalStack;
            FullTableSection.Add(TimePeriodCell);*/

            //Any Other Cost
            var OtherCostCell = new ViewCell();
            var OtherCostsStack = FormattedElementsHelper.TableRowStack();
            OtherCostsStack.Children.Add(FormattedElementsHelper.FormDataLabel(nameof(_viewModel.OtherCostTranslation)));
            OtherCostsStack.Children.Add(FormattedElementsHelper.FormNumericEntry(nameof(_viewModel.OtherCosts), nameof(_viewModel.IsEditMode), null));
            OtherCostCell.View = OtherCostsStack;
            FullTableSection.Add(OtherCostCell);

            //Comment
            var commentCell = new ViewCell();
            var CommentStack = FormattedElementsHelper.TableRowGrid();
            FormattedElementsHelper.AddGridValue(
                CommentStack,
                FormattedElementsHelper.FormDataLabel(nameof(_viewModel.CommentTranslation)),
                GridPostion.TwoLeft);
            FormattedElementsHelper.AddGridValue(
                CommentStack,
                FormattedElementsHelper.FormEditorEntry(nameof(_viewModel.Comment), nameof(_viewModel.IsEditMode), heightRequest: 100),
                GridPostion.TwoRight);
            commentCell.View = CommentStack;
            FullTableSection.Add(commentCell);


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


            MembershipTableView.Root.Add(FullTableSection);
            
        }
    }
}