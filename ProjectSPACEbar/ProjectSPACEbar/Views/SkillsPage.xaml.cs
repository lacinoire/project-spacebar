using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProjectSPACEbar.Views
{
    public partial class SkillsPage : ContentPage
    {
        ObservableCollection<Skill> avaliableSkills;

        public SkillsPage()
        {
            InitializeComponent();
            Initialize();
        }

        async Task Initialize()
        {
            avaliableSkills = new ObservableCollection<Skill>(await App.DataStore.GetSkills(App.CurrentUser, SkillsFilter.Available));
            SkillsList.ItemsSource = avaliableSkills;
            SkillsList.ItemSelected += OnItemSelected;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args) {
            var boughtSkill = args.SelectedItem as Skill;
            if (boughtSkill == null || App.CurrentUser.CurrentXP < boughtSkill.XPcost)
            {
                return;
            }
            await App.DataStore.BuySkill(App.CurrentUser, boughtSkill);
            // TODO Update changed Data
            SkillsList.SelectedItem = null;
        }
    }
}
