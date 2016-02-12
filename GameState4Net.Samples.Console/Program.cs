using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Grapevine.Server;
using System.Threading;
using GalaSoft.MvvmLight.Ioc;
using GameState4Net;
using GameState4Net.Common;
using GameState4Net.CSGO;

namespace TestServer {
    class Program {
        static void Main(string[] args) {
            var server = new GameStateServer();

            GameStateFrame oldFrame = null;
            server.RegisterGameStateCallback(frame => { 
                Console.WriteLine("Received gamestate");

                var player = frame.GetComponent<PlayerComponent>();

                Console.WriteLine("New game state");
                if(player != null)
                {
                    
                    Console.WriteLine(player.Name);

                    var weapons = player.GetComponent<PlayerWeaponsComponent>();
                    if(weapons != null)
                    {
                        Console.WriteLine("Number of weapons: " + weapons.Weapons.Count);

                        // Get the active weapon from the oldFrame
                        if(oldFrame != null)
                        {
                            var oldPlayer = oldFrame.GetComponent<PlayerComponent>();
                            if(oldPlayer != null) {
                                var oldWeapons = oldPlayer.GetComponent<PlayerWeaponsComponent>();
                                if(oldWeapons != null)
                                {
                                    var oldActiveWeapon = oldWeapons.Weapons.FirstOrDefault(w => w.State == WeaponState.Active);
                                    if(oldActiveWeapon != null)
                                    {
                                        Console.WriteLine("Old active weapon: " + oldActiveWeapon.Name);
                                    }
                                }
                            }
                        }

                        var activeWeapon = weapons.Weapons.FirstOrDefault(w => w.State == WeaponState.Active);
                        
                        if(activeWeapon != null)
                        {
                            Console.WriteLine("Active weapon: " + activeWeapon.Name);
                            if(activeWeapon.AmmoClip.HasValue && activeWeapon.AmmoClipMax.HasValue)
                            {
                                Console.WriteLine(activeWeapon.AmmoClip <= 0.25 * activeWeapon.AmmoClipMax ? "WARNING: LOW AMMO" : "");
                            }
                        }
                    }
                }
                Console.WriteLine("");

                oldFrame = frame;
            });

            server.Start();
            
            Console.WriteLine("Server started");


            while (server.IsListening) {
                Thread.Sleep(500);
            }
        }
    }
}
