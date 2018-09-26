﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Prism.Navigation;

namespace Prism.NavigationEx
{
    public static class ConfirmNavigationViewModelExtensions
    {
        internal static async Task<bool> CanNavigateInternalAsync<TConfirmParameter>(this IConfirmNavigationViewModel<TConfirmParameter> self, INavigationParameters parameters)
        {
            var result = true;

            parameters.TryGetValue<TConfirmParameter>(NavigationParameterKey.ConfirmParameter, out var parameter);

            if (parameters.GetNavigationMode() == NavigationMode.Back)
            {
                result = await self.CanNavigateAtBackAsync(parameter).ConfigureAwait(false);
            }
            else
            {
                result = await self.CanNavigateAtNewAsync(parameter).ConfigureAwait(false);

                if (!result && parameters.TryGetValue<CancellationTokenSource>(NavigationParameterKey.CancellationTokenSource, out var cts))
                {
                    cts.Cancel();
                }
            }

            if (result && parameters.TryGetValue<Action<INavigationParameters>>(NavigationParameterKey.OnNavigatingFrom, out var onNavigatingFrom))
            {
                onNavigatingFrom(parameters);
            }

            return result;
        }

        public static Task<INavigationResult> NavigateWithConfirmAsync<TViewModel, TConfirmParameter>(this IConfirmNavigationViewModel<TConfirmParameter> self, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel
        {
            return self.NavigationService.NavigateWithConfirmAsync<TViewModel, TConfirmParameter>(confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult> NavigateWithConfirmAsync<TViewModel, TParameter, TConfirmParameter>(this IConfirmNavigationViewModel<TConfirmParameter> self, TParameter parameter, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel<TParameter>
        {
            return self.NavigationService.NavigateWithConfirmAsync<TViewModel, TParameter, TConfirmParameter>(parameter, confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult<TResult>> NavigateWithConfirmAsync<TViewModel, TResult, TConfirmParameter>(this IConfirmNavigationViewModel<TConfirmParameter> self, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModelResult<TResult>
        {
            return self.NavigationService.NavigateWithConfirmAsync<TViewModel, TResult, TConfirmParameter>(confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult<TResult>> NavigateWithConfirmAsync<TViewModel, TParameter, TResult, TConfirmParameter>(this IConfirmNavigationViewModel<TConfirmParameter> self, TParameter parameter, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true, bool wrapInNavigationPage = false, bool noHistory = false)
            where TViewModel : INavigationViewModel<TParameter, TResult>
        {
            return self.NavigationService.NavigateWithConfirmAsync<TViewModel, TParameter, TResult, TConfirmParameter>(parameter, confirmParameter, useModalNavigation, animated, wrapInNavigationPage, noHistory);
        }

        public static Task<INavigationResult> GoBackWithConfirmAsync<TConfirmParameter>(this IConfirmNavigationViewModel<TConfirmParameter> self, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true)
        {
            return self.NavigationService.GoBackWithConfirmAsync(self, confirmParameter, useModalNavigation, animated);
        }

        public static Task<INavigationResult> GoBackToRootWithConfirmAsync<TConfirmParameter>(this IConfirmNavigationViewModel<TConfirmParameter> self, TConfirmParameter confirmParameter)
        {
            return self.NavigationService.GoBackToRootWithConfirmAsync(self, confirmParameter);
        }

        public static Task<INavigationResult> GoBackWithConfirmAsync<TResult, TConfirmParameter>(this IConfirmNavigationViewModelResult<TResult, TConfirmParameter> self, TResult result, TConfirmParameter confirmParameter, bool? useModalNavigation = null, bool animated = true)
        {
            return self.NavigationService.GoBackWithConfirmAsync(self, result, confirmParameter, useModalNavigation, animated);
        }

        public static Task<INavigationResult> GoBackToRootWithConfirmAsync<TResult, TConfirmParameter>(this IConfirmNavigationViewModelResult<TResult, TConfirmParameter> self, TResult result, TConfirmParameter confirmParameter)
        {
            return self.NavigationService.GoBackToRootWithConfirmAsync(self, result, confirmParameter);
        }
    }
}
