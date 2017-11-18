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

	public class SkillResponse
	{

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
			var serializedItem = JsonConvert.SerializeObject(new { username = name });

			var response = await client.PostAsync($"users",
				new StringContent(serializedItem, Encoding.UTF8, "application/json"));

			if (!response.IsSuccessStatusCode)
				throw new Exception("Creating user was not successful");
		}

		public async Task<User> GetUser(string name)
		{
			var json = await client.GetStringAsync($"users?username={name}");

			var response = await Task.Run(() => JsonConvert.DeserializeObject<UserResponse>(json));
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
			var serializedItem = JsonConvert.SerializeObject(new { username = user.Name, item = item.id });

			var response = await client.PostAsync($"orders/new",
				new StringContent(serializedItem, Encoding.UTF8, "application/json"));

			if (!response.IsSuccessStatusCode)
				throw new Exception("Creating order was not successful");
		}

		public async Task<IEnumerable<Order>> GetOrders(User user, OrderFilter filter)
		{
			var json = await client.GetStringAsync($"orders?filter={filter.GetFilter()}&username={user.Name}");

			var response = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<OrderResponse>>(json));
			return response.Select(o => o.ToOrder());
		}

		public async Task OrderAction(User user, Order order, string action)
		{
			var serializedItem = JsonConvert.SerializeObject(new { username = user.Name, order = order.Id });

			var response = await client.PostAsync($"orders/{action}",
				new StringContent(serializedItem, Encoding.UTF8, "application/json"));

			if (!response.IsSuccessStatusCode)
				throw new Exception("Changing order was not successful");
		}

		public async Task<IEnumerable<SkillResponse>> GetSkills(User user)
		{
			var json = await client.GetStringAsync($"skills?username={user.Name}");

			return await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<SkillResponse>>(json));
		}

		public async Task BuySkill(User user, Skill skill)
		{
			var serializedItem = JsonConvert.SerializeObject(new { username = user.Name, skill = skill.Id });

			var response = await client.PostAsync($"skills",
				new StringContent(serializedItem, Encoding.UTF8, "application/json"));

			if (!response.IsSuccessStatusCode)
				throw new Exception("Buying skill was not successful");
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
