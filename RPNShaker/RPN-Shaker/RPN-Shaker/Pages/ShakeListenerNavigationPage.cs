using System;
using System.Collections.Generic;
using System.Text;
using DeviceMotion.Plugin;
using DeviceMotion.Plugin.Abstractions;
using Xamarin.Forms;

namespace RPN_Shaker.Pages
{
    public class ShakeListenerNavigationPage : NavigationPage
    {
        #region Constant Fields
        const int _shakeDetectionTimeLapse = 250;
        readonly double _shakeThreshold;
        #endregion

        #region Fields
        bool _hasUpdated;
        DateTime _lastUpdate;
        double _lastX, _lastY, _lastZ;
        #endregion

        #region Constructors
        public ShakeListenerNavigationPage(Page root) : base(root)
        {

            _shakeThreshold = 100000;

            CrossDeviceMotion.Current.Start(MotionSensorType.Accelerometer, MotionSensorDelay.Default);
            CrossDeviceMotion.Current.SensorValueChanged += HandleSensorValueChanged;
        }
        #endregion

        #region Methods
        void HandleSensorValueChanged(object sender, SensorValueChangedEventArgs e)
        {
            if (e.SensorType == MotionSensorType.Accelerometer)
            {
                double x = ((MotionVector)e.Value).X;
                double y = ((MotionVector)e.Value).Y;
                double z = ((MotionVector)e.Value).Z;

                var currentTime = DateTime.Now;

                if (_hasUpdated == false)
                {
                    _hasUpdated = true;
                    _lastUpdate = currentTime;
                }
                else
                {
                    var hasMinimumTimeElapsed = (currentTime - _lastUpdate).TotalMilliseconds > _shakeDetectionTimeLapse;

                    if (!hasMinimumTimeElapsed)
                        return;

                    _lastUpdate = currentTime;

                    var timeSinceLastShakeInMilliseconds = (currentTime - _lastUpdate).TotalMilliseconds;
                    var totalMovementDistance = x + y + z - _lastX - _lastY - _lastZ;
                    var shakeSpeed = Math.Abs(totalMovementDistance) / timeSinceLastShakeInMilliseconds * 10;

                    if (shakeSpeed > _shakeThreshold)
                        HandleShake();
                }

                _lastX = x;
                _lastY = y;
                _lastZ = z;
            }
        }

        void HandleShake()
        {
            Device.BeginInvokeOnMainThread(async () => await DisplayAlert("Shake Detected", "You shook your device!", "Ok"));
        }
        #endregion
    }
}