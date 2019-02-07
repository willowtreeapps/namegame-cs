using System;
using Newtonsoft.Json;

namespace WillowTree.NameGame.Core.Models
{
	public class Profile
    {
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("slug")]
		public string Slug { get; set; }

		[JsonProperty("jobTitle")]
		public string JobTitle { get; set; }

		[JsonProperty("firstName")]
		public string FirstName { get; set; }

		[JsonProperty("lastName")]
		public string LastName { get; set; }

		[JsonProperty("headshot")]
		public Headshot Headshot { get; set; }

		public string FullName
		{
			get { return $"{FirstName} {LastName}"; }
		}
    }
}
