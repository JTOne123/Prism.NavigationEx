﻿using System;
using System.Threading.Tasks;
using Prism.Navigation;
using Reactive.Bindings;
using System.Diagnostics;

namespace Prism.NavigationEx.Sample.ViewModels
{
    public class SecondPageViewModel : NavigationViewModelResult<string>
    {
        public ReactivePropertySlim<string> Text { get; } = new ReactivePropertySlim<string>();
        public AsyncReactiveCommand OkCommand { get; } = new AsyncReactiveCommand();
        public AsyncReactiveCommand CancelCommand { get; } = new AsyncReactiveCommand();
        public AsyncReactiveCommand GoToThirdPageCommand { get; } = new AsyncReactiveCommand();
        public AsyncReactiveCommand ReplaceToThirdPageCommand { get; } = new AsyncReactiveCommand();

        public SecondPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            OkCommand.Subscribe(() => GoBackAsync(Text.Value));
            CancelCommand.Subscribe(() => GoBackAsync());

            GoToThirdPageCommand.Subscribe(async () =>
            {
                var result = await NavigateAsync<ThirdPageViewModel, string, string>(Text.Value);
                if (result.Success)
                {
                    Text.Value = result.Data;
                }
            });

            ReplaceToThirdPageCommand.Subscribe(async () =>
            {
                await NavigateAsync<ThirdPageViewModel, string>(Text.Value, replaced: true, animated: false);
            });
        }
    }
}
