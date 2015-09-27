using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.Gpio;

namespace Charley.SmartCar.Engine
{
    public enum enmWheel
    {
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight
    }

    public enum Direction
    {
        None,
        Forward,
        Backward
    }

    public sealed class Wheel
    {
        private static GpioController controller = GpioController.GetDefault();

        private GpioPin _highPin;
        private GpioPin _lowPin;

        public enmWheel Name { get; set; }
        public float Speed { get; set; }

        public Wheel(enmWheel name, int highPin, int lowPin)
        {
            Name = name;

            _highPin = controller.OpenPin(highPin);
            _lowPin = controller.OpenPin(lowPin);

            _highPin.SetDriveMode(GpioPinDriveMode.Output);
            _lowPin.SetDriveMode(GpioPinDriveMode.Output);
        }

        public void Trigger(Direction direction, float speed)
        {
            switch (direction)
            {
                case Direction.Forward:
                    _highPin.Write(GpioPinValue.High);
                    _lowPin.Write(GpioPinValue.Low);
                    break;
                case Direction.Backward:
                    _highPin.Write(GpioPinValue.Low);
                    _lowPin.Write(GpioPinValue.High);
                    break;
                case Direction.None:
                default:
                    _highPin.Write(GpioPinValue.Low);
                    _lowPin.Write(GpioPinValue.Low);
                    break;
            }
        }
    }
}
