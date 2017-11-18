using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

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

	}

	public class MenuResponse
	{
		IEnumerable<ItemResponse> drinks;
	}

	public class ItemResponse
	{
		string content;
		int id;
		int price;
		int size;
		int xpGain;
	}

	public class OrderResponse
	{
		int id;
		bool isClaimed;
		bool isApproved;
		bool isFinished;
		string fromUser;
		ItemResponse item;
		string status;
	}

	public class UserResponse
	{
		int totalXp;
		int usableXp;
		string username;
	}

	// TODO Item, Order class
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

		public async Task<UserResponse> GetUser(string name)
		{
			var json = await client.GetStringAsync($"users?username={name}");

			return await Task.Run(() => JsonConvert.DeserializeObject<UserResponse>(json));
		}
		
		public async Task<MenuResponse> GetMenu(User user)
		{
			var json = await client.GetStringAsync($"menu?username={user.Name}");

			return await Task.Run(() => JsonConvert.DeserializeObject<MenuResponse>(json));
		}

		public async Task CreateOrder(User user, int item)
		{
			var serializedItem = JsonConvert.SerializeObject(new { username = user.Name, item = item });

			var response = await client.PostAsync($"orders/new",
				new StringContent(serializedItem, Encoding.UTF8, "application/json"));

			if (!response.IsSuccessStatusCode)
				throw new Exception("Creating order was not successful");
		}

		public async Task<IEnumerable<OrderResponse>> GetOrders(User user, OrderFilter filter)
		{
			var json = await client.GetStringAsync($"orders?filter={filter.GetFilter()}&username={user.Name}");

			return await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<OrderResponse>>(json));
		}

		public async Task OrderAction(User user, int order, string action)
		{
			var serializedItem = JsonConvert.SerializeObject(new { username = user.Name, order = order });

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

		public async Task BuySkill(User user, int skill)
		{
			var serializedItem = JsonConvert.SerializeObject(new { username = user.Name, skill = skill });

			var response = await client.PostAsync($"skills",
				new StringContent(serializedItem, Encoding.UTF8, "application/json"));

			if (!response.IsSuccessStatusCode)
				throw new Exception("Buying skill was not successful");
		}

		public async Task<IEnumerable<LeaderboardEntryResponse>> GetLeaderboard()
		{
			var json = await client.GetStringAsync($"leaderboard");

			return await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<LeaderboardEntryResponse>>(json));
		}
	}
}
