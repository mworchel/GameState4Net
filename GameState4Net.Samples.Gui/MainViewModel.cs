using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;
using System.Windows.Media;
using GameState4Net;
using GameState4Net.CSGO;
using GameState4Net.Common;

namespace WpfTest
{
    /// <summary>
    /// Messy app, hacked it together :x it does not work anymore if you are in T or CT team since the bomb timer event will be 
    /// fired with a (probably) randomized delay
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private const double BombTimer = 40.00;

        private GameStateServer server;
        private DateTime? bombPlantedTime = null;
        private object bombPlantedTimeMutex = new object();
        private bool bombIsPlanted = false;

        private bool providerComponentIsActive = false;
        private string providerName = null;

        private bool playerComponentIsActive = false;
        private string playerName = null;

        private bool playerStateComponentIsActive = false;
        private PlayerStateComponent playerStateComponent = null;

        private bool mapComponentIsActive = false;
        private string mapName = null;

        private bool weaponComponentIsActive = false;
        private Weapon activeWeapon = null;

        public MainViewModel()
        {
            if (!IsInDesignMode)
            {
                server = new GameStateServer();
                server.RegisterGameStateCallback(GameStateCallback);
                server.Start();
            } else
            {
                ProviderComponentIsActive = true;
                ProviderName = "Counter Strike - Global Offensive";

                PlayerComponentIsActive = true;
                PlayerName = "Lexington";

                MapComponentIsActive = true;
                MapName = "de_dust2";
            }
        }

        public override void Cleanup()
        {
            base.Cleanup();

            server.Stop();
            server.Dispose();
        }

        private void GameStateCallback(GameStateFrame frame)
        {
            try
            {
                var provider = frame.GetComponent<ProviderComponent>();
                if (provider != null)
                {
                    ProviderComponentIsActive = true;
                    ProviderName = provider.Name;
                }

                var player = frame.GetComponent<PlayerComponent>();
                if(player != null)
                {
                    PlayerComponentIsActive = true;
                    PlayerName = player.Name;

                    var weapons = player.GetComponent<PlayerWeaponsComponent>();
                    if (weapons != null)
                    {
                        ActiveWeapon = weapons.Weapons.FirstOrDefault(w => w.State == WeaponState.Active || w.State == WeaponState.Reloading);
                        WeaponComponentIsActive = ActiveWeapon != null && ActiveWeapon.AmmoClip.HasValue && ActiveWeapon.AmmoClipMax.HasValue;
                        RaisePropertyChangedOnUiThread(() => LowAmmo);
                    }

                    var state = player.GetComponent<PlayerStateComponent>();
                    if(state != null)
                    {
                        PlayerStateComponentIsActive = true;
                        PlayerStateComponent = state;
                    }
                }

                var round = frame.GetComponent<RoundComponent>();
                if (round != null)
                {
                    switch (round.Bomb)
                    {
                        case BombStatus.Planted:
                            if (!bombPlantedTime.HasValue)
                            {
                                lock (bombPlantedTimeMutex)
                                {
                                    bombPlantedTime = DateTime.Now;
                                    Task.Run((Action)UpdateBombTimer);
                                }
                            }
                            bombIsPlanted = true;
                            RaisePropertyChangedOnUiThread(() => BombIsPlanted);
                            break;
                        default:
                            lock (bombPlantedTimeMutex)
                            {
                                bombPlantedTime = null;
                            }
                            bombIsPlanted = false;
                            RaisePropertyChangedOnUiThread(() => BombIsPlanted);
                            UpdateUiThread();
                            break;
                    }
                }
            }
            catch (Exception e)
            {
            }
        }

        private void UpdateBombTimer()
        {
            while (bombPlantedTime.HasValue)
            {
                UpdateUiThread();
                Thread.Sleep(50);
            }
        }
        private void UpdateUiThread()
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                RaisePropertyChanged(() => BombRemainingTime);
                RaisePropertyChanged(() => AlertBrush);
            }));
        }

        private void RaisePropertyChangedOnUiThread<T>(System.Linq.Expressions.Expression<Func<T>> expression)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                RaisePropertyChanged(expression);
            }));
        }

        private double CalculateRemainingTime()
        {
            var time = 0.00;

            lock (bombPlantedTimeMutex)
            {
                if (bombPlantedTime.HasValue)
                {
                    time = Math.Max(BombTimer - (DateTime.Now - bombPlantedTime.Value).TotalSeconds, 0.00);
                }
            }

            return time;
        }

        public string BombRemainingTime
        {
            get
            {
                var time = CalculateRemainingTime();

                return time.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
            }
        }
        public Brush AlertBrush
        {
            get
            {
                double remainingTime = CalculateRemainingTime();
                double gb = 1.0 - (BombTimer - remainingTime) / BombTimer;
                if (gb == 0.00)
                {
                    gb = 1.00;
                }

                return new SolidColorBrush(new Color() { R = 255, G = (byte)(gb * 255), B = (byte)(gb * 255), A = 255 });
            }
        }
        public bool BombIsPlanted
        {
            get
            {
                return bombIsPlanted;
            }

        }

        public bool ProviderComponentIsActive
        {
            get { return providerComponentIsActive; }
            private set { providerComponentIsActive = value; RaisePropertyChangedOnUiThread(() => ProviderComponentIsActive); }
        }
        public string ProviderName
        {
            get
            {
                return providerName;
            }
            private set
            {
                providerName = value;
                RaisePropertyChangedOnUiThread(() => ProviderName);
            }
        }

        public bool PlayerComponentIsActive
        {
            get { return playerComponentIsActive; }
            private set { playerComponentIsActive = value; RaisePropertyChangedOnUiThread(() => PlayerComponentIsActive); }
        }
        public string PlayerName
        {
            get
            {
                return playerName;
            }
            private set
            {
                playerName = value;
                RaisePropertyChangedOnUiThread(() => PlayerName);
            }
        }

        public bool PlayerStateComponentIsActive
        {
            get { return playerStateComponentIsActive; }
            private set { playerStateComponentIsActive = value; RaisePropertyChangedOnUiThread(() => PlayerStateComponentIsActive); }
        }
        public PlayerStateComponent PlayerStateComponent
        {
            get
            {
                return playerStateComponent;
            }
            private set
            {
                playerStateComponent = value;
                RaisePropertyChangedOnUiThread(() => PlayerStateComponent);
            }
        }

        public bool MapComponentIsActive
        {
            get { return mapComponentIsActive; }
            set { mapComponentIsActive = value; RaisePropertyChangedOnUiThread(() => MapComponentIsActive); }
        }
        public string MapName
        {
            get
            {
                return mapName;
            }
            set
            {
                mapName = value;
                RaisePropertyChangedOnUiThread(() => MapName);
            }
        }

        public bool WeaponComponentIsActive
        {
            get { return weaponComponentIsActive; }
            set { weaponComponentIsActive = value; RaisePropertyChangedOnUiThread(() => WeaponComponentIsActive); }
        }
        public Weapon ActiveWeapon
        {
            get
            {
                return activeWeapon;
            }
            set
            {
                activeWeapon = value;
                RaisePropertyChangedOnUiThread(() => ActiveWeapon);
            }
        }
        public bool LowAmmo
        {
            get {
                if (WeaponComponentIsActive)
                {
                    return ActiveWeapon.AmmoClip <= ActiveWeapon.AmmoClipMax * 0.25;
                } else
                {
                    return false;
                }
            }
        }
    }
}
