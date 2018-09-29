﻿using System;
using System.Threading.Tasks;
using Prism.Navigation;

namespace Prism.NavigationEx
{
    public static class NavigationViewModelExtensions
    {
        internal static void PrepareIfNeeded<TParameter>(this INavigationViewModel<TParameter> self, NavigationParameters parameters)
        {
            if (parameters.GetNavigationMode() == NavigationMode.New)
            {
                if (parameters.TryGetValue<string>(NavigationParameterKey.ParameterId, out var id))
                {
                    if (parameters.TryGetValue<TParameter>(id, out var parameter))
                    {
                        self.Prepare(parameter);
                    }
                }
            }
        }
    }
}