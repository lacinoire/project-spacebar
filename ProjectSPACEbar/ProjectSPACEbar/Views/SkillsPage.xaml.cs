using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace ProjectSPACEbar.Views
{
    public partial class SkillsPage : ContentPage
    {
        ObservableCollection<Skill> avaliableSkills;

        public SkillsPage()
        {
            InitializeComponent();
            avaliableSkills = new ObservableCollection<Skill>();
            avaliableSkills.Add(new Skill
            {
                Name = "5% of Beer",
                XPcost = 50,
            });
            avaliableSkills.Add(new Skill
            {
                Name = "1l Cocktails",
                XPcost = 235,
            });
            SkillsList.ItemsSource = avaliableSkills;
            SkillsList.ItemSelected += OnItemSelected;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args) {
            var boughtSkill = args.SelectedItem as Skill;
            if (boughtSkill == null || App.CurrentUser.CurrentXP < boughtSkill.XPcost)
            {
                return;
            }
            App.CurrentUser.CurrentXP = App.CurrentUser.CurrentXP - boughtSkill.XPcost;
            App.CurrentUser.Skills.Add(boughtSkill);
            avaliableSkills.Remove(boughtSkill);
            SkillsList.SelectedItem = null;
        }
    }
}
