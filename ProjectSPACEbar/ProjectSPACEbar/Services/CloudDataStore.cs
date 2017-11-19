using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using System.Linq;

namespace ProjectSPACEbar
{
	public enum OrderFilter
	{
		Own,
		Open,
		Claimed,
	}

	public static class OrderFilterHelper
	{
		public static string GetFilter(this OrderFilter f)
		{
			switch (f)
			{
				case OrderFilter.Own: return "own";
				case OrderFilter.Open: return "open";
				case OrderFilter.Claimed: return "claimed";
			}
			throw new Exception("Unhandled filter");
		}
	}

	public enum SkillsFilter
	{
		All,
		Bought,
		Available,
		Basic,
	}

	public static class SkillsFilterHelper
	{
		public static string GetFilter(this SkillsFilter f)
		{
			switch (f)
			{
				case SkillsFilter.All: return "all";
				case SkillsFilter.Bought: return "bought";
				case SkillsFilter.Available: return "available";
				case SkillsFilter.Basic: return "basic";
			}
			throw new Exception("Unhandled filter");
		}
	}

	public class SkillResponse
	{
		public uint id;
		public string name;
		public uint xpCost;
		public List<uint> nextSkills;

		public Skill ToSkill()
		{
			return new Skill
			{
				Id = id,
				Name = name,
				XPcost = xpCost,
			};
		}
	}

	public class LeaderboardEntryResponse
	{
		public string username;
		public uint totalXp;
		public uint usableXp;

		public User ToUser()
		{
			return new User
			{
				EarnedXP = totalXp,
				CurrentXP = usableXp,
				Name = username,
			};
		}
	}

	public class MenuResponse
	{
		public List<ItemResponse> drinks;
	}

	public class ItemResponse
	{
		public string content;
		public uint id;
		/// <summary>
		/// In cent
		/// </summary>
		public uint price;
		/// <summary>
		/// In milliliter
		/// </summary>
		public uint size;
		public uint xpGain;

		public MenuItem ToMenuItem()
		{
			return new MenuItem
			{
				id = id,
				name = content,
				xp = xpGain,
				cost = price / 100m,
				size = size / 1000m,
			};
		}
	}

	public class OrderResponse
	{
		public uint id;
		public bool isClaimed;
		public bool isApproved;
		public bool isFinished;
		public string fromUser;
		public string assignee;
		public ItemResponse item;

		public Order ToOrder()
		{
            return new Order
            {
                Id = id,
                IsClaimed = isClaimed,
                IsApproved = isApproved,
                IsFinished = isFinished,
                MenuItem = item.ToMenuItem(),
                CreatorName = fromUser,
                AssigneeName = assignee,
			};
		}
	}

	public class UserResponse
	{
		public uint totalXp;
		public uint usableXp;
		public string username;
	}
	
	public class CloudDataStore
    {
        HttpClient client;

        public CloudDataStore()
        {
			client = new HttpClient
			{
				BaseAddress = new Uri($"{App.BackendUrl}/")
			};
		}

		public async Task RegisterUser(string name)
		{
			App.Logger.Info("Registering user {0}", name);
			var serializedItem = JsonConvert.SerializeObject(new { username = name });

			var response = await client.PostAsync($"users",
				new StringContent(serializedItem, Encoding.UTF8, "application/json"));

			App.Logger.Info("Registering answer: {0}", response.Content);

			if (!response.IsSuccessStatusCode)
				throw new Exception(string.Format("Creating user was not successful ({0}): {1}",
					response.StatusCode, response.Content));
		}

		public async Task<User> GetUser(string name)
		{
			App.Logger.Info("Get user info for {0}", name);
			var json = await client.GetStringAsync($"users?username={name}");

			var response = await Task.Run(() => JsonConvert.DeserializeObject<UserResponse>(json));

			App.Logger.Info("Got user");
			return new User
			{
				EarnedXP = response.totalXp,
				CurrentXP = response.usableXp,
				Name = response.username,
			};
		}
		
		public async Task<Menu> GetMenu(User user)
		{
			var json = await client.GetStringAsync($"menu?username={user.Name}");

			var response = await Task.Run(() => JsonConvert.DeserializeObject<MenuResponse>(json));
			var menu = new Menu();
			menu.Items.AddRange(response.drinks.Select(d => d.ToMenuItem()));
			return menu;
		}

		public async Task CreateOrder(User user, MenuItem item)
		{
			App.Logger.Info("Create order for {0}", user.Name);
			var serializedItem = JsonConvert.SerializeObject(new { username = user.Name, item = item.id });

			var response = await client.PostAsync($"orders/new",
				new StringContent(serializedItem, Encoding.UTF8, "application/json"));

			App.Logger.Info("Create order got {0}", response.Content);

			if (!response.IsSuccessStatusCode)
				throw new Exception(string.Format("Creating order was not successful ({0}): {1}",
					response.StatusCode, response.Content));
		}

		public async Task<IEnumerable<Order>> GetOrders(User user, OrderFilter filter)
		{
			var json = await client.GetStringAsync($"orders?filter={filter.GetFilter()}&username={user.Name}");

			var response = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<OrderResponse>>(json));
			return response.Select(o => o.ToOrder());
		}

		public async Task OrderAction(User user, Order order, string action)
		{
			App.Logger.Info("{0} does {1} on order", user.Name, action);
			var serializedItem = JsonConvert.SerializeObject(new { username = user.Name, order = order.Id });

			var response = await client.PostAsync($"orders/{action}",
				new StringContent(serializedItem, Encoding.UTF8, "application/json"));

			App.Logger.Info("Create order got {0}", response.Content);

			if (!response.IsSuccessStatusCode)
				throw new Exception(string.Format("Changing order was not successful ({0}): {1}",
					response.StatusCode, response.Content));
		}

		/// <summary>
		/// Returns skills without children.
		/// </summary>
		/// <param name="user">Current user</param>
		/// <param name="skills">Filter returned skills</param>
		/// <returns>Skills without the children list.</returns>
		public async Task<IEnumerable<Skill>> GetSkills(User user, SkillsFilter skills)
		{
			var json = await client.GetStringAsync($"skills?username={user.Name}&filter={skills.GetFilter()}");

			var response = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<SkillResponse>>(json));

			return response.Select(s => s.ToSkill());
		}

		public async Task<SkillGraph> GetSkillGraph(User user)
		{
			var json = await client.GetStringAsync($"skills?username={user.Name}&filter={SkillsFilter.All.GetFilter()}");

			var response = await Task.Run(() => JsonConvert.DeserializeObject<List<SkillResponse>>(json));

			var dict = response.Select(r => r.ToSkill()).ToDictionary(s => s.Id);

			// Set children relation
			foreach (var r in response)
			{
				var cur = dict[r.id];
				foreach (var id in r.nextSkills)
					cur.Children.Add(dict[id]);
			}

			var graph = new SkillGraph();
			graph.All.AddRange(dict.Values.ToList());
			// Well, not needed so far
			throw new NotImplementedException();
			return graph;
		}

		public async Task BuySkill(User user, Skill skill)
		{
			App.Logger.Info("Buy skill {0} for {1}", skill.Name, user.Name);
			var serializedItem = JsonConvert.SerializeObject(new { username = user.Name, skill = skill.Id });

			var response = await client.PostAsync($"skills",
				new StringContent(serializedItem, Encoding.UTF8, "application/json"));

			App.Logger.Info("Buying skill got {0}", response.Content);

			if (!response.IsSuccessStatusCode)
				throw new Exception(string.Format("Buying skill was not successful ({0}): {1}",
					response.StatusCode, response.Content));
		}

		public async Task<Leaderboard> GetLeaderboard()
		{
			var json = await client.GetStringAsync($"leaderboard");

			var response = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<LeaderboardEntryResponse>>(json));

			var leaderboard = new Leaderboard();
			leaderboard.Users.AddRange(response.Select(e => e.ToUser()));
			return leaderboard;
		}
	}
}
