using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProjectSPACEbar.Views
{
    public partial class SkillsPage : ContentPage
    {
        List<Skill> availiableSkills;

        public SkillsPage()
        {
            InitializeComponent();
			App.OrdersChanged += async () => await Initialize();
            Initialize();
        }

        async Task Initialize()
        {
            availiableSkills = (await App.DataStore.GetSkills(App.CurrentUser, SkillsFilter.Available)).ToList();
            SkillsList.ItemsSource = availiableSkills;
            SkillsList.ItemSelected += OnItemSelected;
			OnPropertyChanged(nameof(availiableSkills));
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args) {
            var boughtSkill = args.SelectedItem as Skill;
            if (boughtSkill == null || App.CurrentUser.CurrentXP < boughtSkill.XPcost)
            {
                return;
            }
            await App.DataStore.BuySkill(App.CurrentUser, boughtSkill);
			App.NotifyAll();
            SkillsList.SelectedItem = null;
        }
    }
}
