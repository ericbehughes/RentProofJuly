using System.Threading.Tasks;
using System.Collections.Generic;

namespace TCheck.Droid
{
	public static class BackgroundCheck_List_Data
	{
		public static async Task<List<BackgroundCheck>> GetAllCrewAsync()
		{
			return await Task.Factory.StartNew(() => GetAllCrew());
		}

		public static List<BackgroundCheck> GetAllCrew()
		{
			var crewList = new List<BackgroundCheck>();

			const int numberOfPermutations = 1;
			for(int i = 0; i < numberOfPermutations; i++)
			{
				crewList.Add(new BackgroundCheck()
					{
						Name = "Eric Hughes",
						Rank = "Co-Founder",
						Posting = "RentProof Inc",
						Position = "UI/UX",
						TenantScore = "Human",
						PhotoResourceId = Resource.Drawable.ericAngelList,
						Biography = "I have been able to reach the top 1% for total overall points on the teamtreehouse.com leaderboard. Their leaderboard system is based on points earned for coding challenges and has roughly 283,681 total students.",

					});

				crewList.Add(new BackgroundCheck()
					{
						Name = "Denis Ouspenski",
						Rank = "Co-Founder",
						Posting = "RentProof Inc",
						Position = "Azure Mobile Services",
						TenantScore = "Human",
						PhotoResourceId = Resource.Drawable.denisAngelList,
						Biography = "My biggest accomplishment is getting 3rd place in the 2014 Quebec Entrepreneurship contest, technology & innovation category, with a past project.\n\nThis year, I have also secured an orange belt in karate. This is a proud moment for me due to persevering in a sport long enough to reach the next level for the first time in my life.\n"
					});

				crewList.Add(new BackgroundCheck()
					{
						Name = "Eric Hughes",
						Rank = "Co-Founder",
						Posting = "RentProof Inc",
						Position = "UI/UX",
						TenantScore = "Human",
						PhotoResourceId = Resource.Drawable.ericAngelList,
						Biography = " have been able to reach the top 1% for total overall points on the teamtreehouse.com leaderboard. Their leaderboard system is based on points earned for coding challenges and has roughly 283,681 total students.",

					});

				crewList.Add(new BackgroundCheck()
					{
						Name = "Denis Ouspenski",
						Rank = "Co-Founder",
						Posting = "RentProof Inc",
						Position = "Azure Mobile Services",
						TenantScore = "Human",
						PhotoResourceId = Resource.Drawable.denisAngelList,
						Biography = "My biggest accomplishment is getting 3rd place in the 2014 Quebec Entrepreneurship contest, technology & innovation category, with a past project.\n\nThis year, I have also secured an orange belt in karate. This is a proud moment for me due to persevering in a sport long enough to reach the next level for the first time in my life.\n"
					});
				crewList.Add(new BackgroundCheck()
					{
						Name = "Eric Hughes",
						Rank = "Co-Founder",
						Posting = "RentProof Inc",
						Position = "UI/UX",
						TenantScore = "Human",
						PhotoResourceId = Resource.Drawable.ericAngelList,
						Biography = " have been able to reach the top 1% for total overall points on the teamtreehouse.com leaderboard. Their leaderboard system is based on points earned for coding challenges and has roughly 283,681 total students.",

					});

				crewList.Add(new BackgroundCheck()
					{
						Name = "Denis Ouspenski",
						Rank = "Co-Founder",
						Posting = "RentProof Inc",
						Position = "Azure Mobile Services",
						TenantScore = "Human",
						PhotoResourceId = Resource.Drawable.denisAngelList,
						Biography = "My biggest accomplishment is getting 3rd place in the 2014 Quebec Entrepreneurship contest, technology & innovation category, with a past project.\n\nThis year, I have also secured an orange belt in karate. This is a proud moment for me due to persevering in a sport long enough to reach the next level for the first time in my life.\n"
					});
				
			}

			return crewList;
		}
	}
}

