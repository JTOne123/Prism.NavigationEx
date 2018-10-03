﻿using System;
using Prism.Navigation;

namespace Prism.NavigationEx
{
    public class Tab : ITab
    {
        protected string _name;
        protected bool _wrapInNavigationPage;

        public Tab(string name, bool wrapInNavigationPage)
        {
            _name = name;
            _wrapInNavigationPage = wrapInNavigationPage;
        }

        public virtual string GetPath(ref NavigationParameters parameters)
        {
            return $"{(_wrapInNavigationPage ? NavigationNameProvider.DefaultNavigationPageName + "|" : "")}{_name}";
        }
    }

    public class Tab<TViewModel> : ITab where TViewModel : INavigationViewModel
    {
        protected bool _wrapInNavigationPage;

        public Tab(bool wrapInNavigationPage)
        {
            _wrapInNavigationPage = wrapInNavigationPage;
        }

        public virtual string GetPath(ref NavigationParameters parameters)
        {
            var name = NavigationNameProvider.GetNavigationName(typeof(TViewModel));
            return $"{(_wrapInNavigationPage ? NavigationNameProvider.DefaultNavigationPageName + "|" : "")}{name}";
        }
    }

    public class Tab<TViewModel, TParameter> : Tab<TViewModel> where TViewModel : INavigationViewModel<TParameter>
    {
        protected TParameter _parameter;

        public Tab(TParameter parameter, bool wrapInNavigationPage) : base(wrapInNavigationPage)
        {
            _parameter = parameter;
        }

        public override string GetPath(ref NavigationParameters parameters)
        {
            if (parameters == null)
            {
                parameters = new NavigationParameters();
            }

            var parameter = base.GetPath(ref parameters);

            var parameterId = Guid.NewGuid().ToString();
            parameter += $"?{NavigationParameterKey.ParameterId}={parameterId}";
            parameters.Add(parameterId, _parameter);

            return parameter;
        }
    }
}
