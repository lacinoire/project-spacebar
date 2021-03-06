﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProjectSPACEbar
{
    public partial class ProfilePage : ContentPage
    {
        public User CurrentUser => App.CurrentUser;

        public ProfilePage()
        {
            InitializeComponent();
            App.AllChanged += async () =>
            {
                App.CurrentUser = await App.DataStore.GetUser(App.CurrentUser.Name);
                await Initialize();
                OnPropertyChanged(nameof(CurrentUser));
            };
            Initialize();
            BindingContext = CurrentUser;
        }

        async Task Initialize()
        {
            App.CurrentUser.Skills = new List<Skill>(await App.DataStore.GetSkills(App.CurrentUser, SkillsFilter.Bought));
            CurrentUser.NotifyPropertyChanged(nameof(CurrentUser.SkillsListText));
        }
    }
}
