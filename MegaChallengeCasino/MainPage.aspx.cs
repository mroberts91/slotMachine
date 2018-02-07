using System;
using System.Web.UI;

namespace MegaChallengeCasino
{
	// Gernerates 3 random reel images.
	// Player enters bet
	// Player money label is always shown and updated after each pull
	// Result lable of the previous spin

	public partial class MainPage : System.Web.UI.Page
	{
		// Globals
		Random random = new Random();
		string[] images = new string[] {"Bar", "Cherry", "Clover", "Watermellon", "Bell", "Diamond",
										"Strawberry", "Orange", "HorseShoe", "Lemon", "Plum", "Seven"};


		protected void Page_Load(object sender, EventArgs e)
		{
			textBoxPlaceBet.Focus();
			if (!Page.IsPostBack)
			{
				ViewState.Add("Cherry", 0);
				ViewState.Add("Seven", 0);
				ViewState.Add("Bar", 0);
				ViewState.Add("PlayerMoney", 100.0);
				labelMoney.Text = $" ${(double)ViewState["PlayerMoney"]}";
				DisplayReelImages();
			}
			
		}

		protected void buttonPullLever_Click(object sender, EventArgs e)
		{
			double playerBet;
			double totalWinnings = 0;

			// Display the Reels
			DisplayReelImages();
			
			// Get the numeric value of the players bet
			playerBet = (double.TryParse(textBoxPlaceBet.Text.Trim(), out double playerInput)) ? playerInput : 0;

			// Calculate the winnings or losses.
			CalculateSpinOutcome(playerBet, totalWinnings);
			labelMoney.Text = $" ${(double)ViewState["PlayerMoney"]}";
			ViewState["Cherry"] = 0;
			ViewState["Seven"] = 0;
			ViewState["Bar"] = 0;

			if ((double)ViewState["PlayerMoney"] <= 0)
			{
				buttonPullLever.Enabled = false;
				labelResult.Text = "You are out of money, Please try again later!";
			}
		}


// Helper methods for Reel Image Generation and Display
		private void DisplayReelImages()
		{
			// Setting each reel image URL to URL chosen at random
			imageReelOne.ImageUrl = GenerateImageURL();
			imageReelTwo.ImageUrl = GenerateImageURL();
			imageReelThree.ImageUrl = GenerateImageURL();
		}
		private string GenerateImageURL()
		{
			// Construc a URL bases on the random images function returned value.
			string imageURL = $"Resources/{SelectRandomImages()}.png";
			return imageURL;
		}
		private string SelectRandomImages()
		{
			// Selecting on of the images at random from the images array
			// If page is post back, call the counter functions for the winning images
			string image = images[random.Next(11)];
			if (Page.IsPostBack)
			{
				CherryCount(image);
				SevenCount(image);
				BarCount(image);
			}
			return image;
		}
		
// Helper methods for determining if any of the winning reels are set.
		private void CherryCount(string image)
		{
			// Determine if any of the reels were set to the "Cherry" Image
			if (image == "Cherry")
			{
				int totalCherries = (int)ViewState["Cherry"];
				totalCherries += 1;
				ViewState["Cherry"] = totalCherries;
			}
		}
		private void SevenCount(string image)
		{
			// Determine if any of the reels are set to the "Seven"
			if (image == "Seven") 
			{ 
				int totalSevens = (int)ViewState["Seven"];
				totalSevens += 1;
				ViewState["Seven"] = totalSevens;
			}
		}
		private void BarCount(string image)
		{
			// Determine if any of the reels were set to the "Bar" image
			if (image == "Bar")
			{
				int totalBars = (int)ViewState["Bar"];
				totalBars += 1;
				ViewState["Bar"] = totalBars;
			}
		}

// Calculations and Displays
		private void CalculateSpinOutcome(double playerBet, double totalWinnings)
		{
			// Determening the multiplier of the spin if any of the winning reels are present.
			totalWinnings = ((int)ViewState["Cherry"] == 1) ? playerBet * 2 : totalWinnings;
			totalWinnings = ((int)ViewState["Cherry"] == 2) ? playerBet * 3 : totalWinnings;
			totalWinnings = ((int)ViewState["Cherry"] == 3) ? playerBet * 4 : totalWinnings;
			totalWinnings = ((int)ViewState["Seven"] == 3) ? playerBet * 100 : totalWinnings;
			totalWinnings = ((int)ViewState["Bar"] > 0) ? 0 : totalWinnings;

			// If no winning reels are present, subract the bet from the winnings, so winnings are now loss.
			totalWinnings = (totalWinnings == 0) ? totalWinnings - playerBet : totalWinnings;
			
			DisplayOutcome(totalWinnings);
		}
		private void DisplayOutcome(double totalWinnings)
		{
			// Displays the total winnings or loses of the user
			if (totalWinnings > 0)
			{
				ViewState["PlayerMoney"] = (double)ViewState["PlayerMoney"] + totalWinnings;
				labelResult.Text = $"Congratulations you won {totalWinnings.ToString("C")}!";
			}
			else
			{
				// Multipying by -1 so that negative value does not appear in parens
				totalWinnings = totalWinnings * -1;
				ViewState["PlayerMoney"] = (double)ViewState["PlayerMoney"] - totalWinnings;
				labelResult.Text = $"Unlucky... you lost {totalWinnings.ToString("C")}!";
			}
		}


		// Input error message, but has never been used thus far
		private void InputError()
		{
			labelResult.Text = "Error -- Invalid bet entry. Please verify entry for numeric value!";
		}
	}
}
 