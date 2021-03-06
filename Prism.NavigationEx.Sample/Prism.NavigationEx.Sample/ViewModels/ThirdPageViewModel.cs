﻿using System;
using System.Threading.Tasks;
using Prism.Navigation;
using Reactive.Bindings;
using Prism.Services;
using Prism.Events;

namespace Prism.NavigationEx.Sample.ViewModels
{
    public class ThirdPageViewModel : NavigationViewModel<string, string>
    {
        public ReactivePropertySlim<string> Text { get; } = new ReactivePropertySlim<string>();
        public AsyncReactiveCommand OkCommand { get; } = new AsyncReactiveCommand();
        public AsyncReactiveCommand CancelCommand { get; } = new AsyncReactiveCommand();
        public AsyncReactiveCommand GoBackToMainPageCommand { get; } = new AsyncReactiveCommand();

        private readonly IPageDialogService _pageDialogService;

        public ThirdPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IEventAggregator eventAggregator) : base(navigationService)
        {
            _pageDialogService = pageDialogService;

            OkCommand.Subscribe(async () => await GoBackAsync(Text.Value, canNavigate: () => pageDialogService.DisplayAlertAsync("Are you sure?", Text.Value, "Yes", "No")));
            CancelCommand.Subscribe(async () => await GoBackAsync());
            GoBackToMainPageCommand.Subscribe(async () => await GoBackToRootAsync());
        }

        public override void Prepare(string parameter)
        {
            Text.Value = parameter;
        }
    }
}
