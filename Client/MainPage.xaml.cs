using System;
using System.Diagnostics;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Client
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        DatagramSocket socket = new DatagramSocket();
        public MainPage()
        {
            this.InitializeComponent();

            btnForward.AddHandler(PointerPressedEvent, new PointerEventHandler(btnForward_PointerPressed), true);
            btnForward.AddHandler(PointerReleasedEvent, new PointerEventHandler(btn_PointerReleased), true);

            btnRightForward.AddHandler(PointerPressedEvent, new PointerEventHandler(btnRightForward_PointerPressed), true);
            btnRightForward.AddHandler(PointerReleasedEvent, new PointerEventHandler(btn_PointerReleased), true);

            btnLeftForward.AddHandler(PointerPressedEvent, new PointerEventHandler(btnLeftFoward_PointerPressed), true);
            btnLeftForward.AddHandler(PointerReleasedEvent, new PointerEventHandler(btn_PointerReleased), true);

            btnBackward.AddHandler(PointerPressedEvent, new PointerEventHandler(btnBackward_PointerPressed), true);
            btnBackward.AddHandler(PointerReleasedEvent, new PointerEventHandler(btn_PointerReleased), true);

            btnRightBackward.AddHandler(PointerPressedEvent, new PointerEventHandler(btnRightBackward_PointerPressed), true);
            btnRightBackward.AddHandler(PointerReleasedEvent, new PointerEventHandler(btn_PointerReleased), true);

            btnLeftBackward.AddHandler(PointerPressedEvent, new PointerEventHandler(btnLeftBackward_PointerPressed), true);
            btnLeftBackward.AddHandler(PointerReleasedEvent, new PointerEventHandler(btn_PointerReleased), true);
        }


        DataWriter writer;
        private async void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtIPAddress.Text))
            {
                return;
            }

            await socket.ConnectAsync(new Windows.Networking.HostName(txtIPAddress.Text.Trim()), "8888");
            writer = new DataWriter(socket.OutputStream);

            Send("create");
        }

        private async void Send(string message)
        {
            try
            {
                writer.WriteString(message);

                //commit and send the data through the OutputStream
                await writer.StoreAsync();
            }
            catch (Exception ex)
            {
                writer = new DataWriter(socket.OutputStream);
                Debug.Write(ex.Message);
            }
            finally
            {

            }

        }

        private void btn_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            Send("stop");
        }

        private void btnForward_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Send("forward");
        }

        private void btnBackward_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Send("backward");
        }

        private void btnRightForward_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Send("turnright");
        }

        private void btnLeftFoward_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Send("turnleft");
        }

        private void btnLeftBackward_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Send("backleft");
        }

        private void btnRightBackward_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Send("backright");
        }
    }
}
