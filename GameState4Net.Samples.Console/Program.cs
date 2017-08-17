using System;
using System.Linq;
using System.Threading;
using GameState4Net;
using GameState4Net.CSGO;

namespace TestServer
{
	class Program
	{
		static void Main(string[] args)
		{
			var listener = new GameStateListener<CsGoGameStateFrame>();
			listener.AddGameStateCallback(Callback);

			listener.Start();
			Console.WriteLine("Listening...");
			while (listener.IsListening)
			{
				Thread.Sleep(500);
			}
		}

		static void Callback(CsGoGameStateFrame frame)
		{
			Console.WriteLine("Received gamestate");

			// Get the player
			var player = frame.Player;
			if(player == null) { return; }

			Console.WriteLine(player.Name);

			// Get the player's weapons
			var weapons = player.Weapons;
			if (weapons == null) { return; }

			Console.WriteLine("Number of weapons: " + weapons.Count());

			// Get the active weapon
			var activeWeapon = weapons.FirstOrDefault(w => w.State == WeaponState.Active);
			if(activeWeapon == null) { return; }

			Console.WriteLine("Active weapon: " + activeWeapon.Name);

			if(activeWeapon.AmmoClip.HasValue && activeWeapon.AmmoClipMax.HasValue)
			{
				Console.WriteLine(activeWeapon.AmmoClip <= 0.25 * activeWeapon.AmmoClipMax ? "WARNING: LOW AMMO" : "");
			}

			Console.WriteLine("\n");
		}
	}
}
