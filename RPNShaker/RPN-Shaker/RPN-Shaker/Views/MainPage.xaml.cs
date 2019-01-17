using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeviceMotion.Plugin;
using DeviceMotion.Plugin.Abstractions;
using Xamarin.Forms;

namespace RPN_Shaker.Views
{
    public partial class MainPage : ContentPage
    {
        #region Fields
        bool? _proceed = null;
        bool _hasUpdated;
        double _lastX, _lastY, _lastZ;
        double _shakeThreshold;
        DateTime _lastUpdate;
        #endregion

        public MainPage()
        {
            InitializeComponent();

            _shakeThreshold = 10;

            CrossDeviceMotion.Current.Start(MotionSensorType.Accelerometer, MotionSensorDelay.Ui);
            CrossDeviceMotion.Current.SensorValueChanged += HandleSensorValueChanged;
        }

        #region Methods
        private void HandleSensorValueChanged(object sender, SensorValueChangedEventArgs e)
        {

            if (e.SensorType == MotionSensorType.Accelerometer)
            {
                var x = Math.Abs(Math.Round(((MotionVector)e.Value).X, 2));
                var y = Math.Abs(Math.Round(((MotionVector)e.Value).Y, 2));
                var z = Math.Abs(Math.Round(((MotionVector)e.Value).Z, 2));
                var currentTime = DateTime.Now;

                if (_hasUpdated == false)
                {
                    _hasUpdated = true;
                    _lastUpdate = currentTime;
                }
                else
                {
                    _lastUpdate = currentTime;

                    var totalMovementDistance = Math.Abs(Math.Round(x + y + z - _lastX - _lastY - _lastZ, 2));
                    if (totalMovementDistance >= _shakeThreshold)
                    {
                        HandleShake();
                    }
                }

                _lastX = x;
                _lastY = y;
                _lastZ = z;
            }
        }

        private void HandleShake()
        {
            CrossDeviceMotion.Current.Stop(MotionSensorType.Accelerometer);

            Device.BeginInvokeOnMainThread(async () =>
            {
                _proceed = await DisplayAlert("Alert", "Do you want to proceed?", "Yes", "No");

                if (_proceed == false)
                {
                    _proceed = null;
                    CrossDeviceMotion.Current.Start(MotionSensorType.Accelerometer, MotionSensorDelay.Ui);
                }
                else if (_proceed == true)
                {
                    await Navigation.PushAsync(new ResoultPage());
                }
            });
        }

        protected override void OnAppearing()
        {
            CrossDeviceMotion.Current.Start(MotionSensorType.Accelerometer, MotionSensorDelay.Ui);
            base.OnAppearing();
        }
        #endregion
    }
}
