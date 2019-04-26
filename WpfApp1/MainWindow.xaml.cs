using BlowFishApps.Blowfish;
using Org.BouncyCastle.Crypto.Engines;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private int send1 = 0;
        private int send2 = 0;
        string TextEncrypt1;
        string TextDecrypt1;
        string TextEncrypt2;
        string TextDecrypt2;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Se clicado no botão, retorna isso e.e");
        }

        private void ButtonPopUpLogout_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e) // Button hide menu
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e) // button close menu
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)// Button Show/Hide second chat
        {
            if(send2 >= 1)
            {
                send2 = 0;
                Not22.Badge = "";
                NewMessage2.Visibility = Visibility.Collapsed;
            }

            if (PanelChat2.Visibility == Visibility.Collapsed)
                PanelChat2.Visibility = Visibility.Visible;

            else
                PanelChat2.Visibility = Visibility.Collapsed;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(send1 >= 1)
            {
                send1 = 0;
                Not2.Badge = "";
                NewMessage1.Visibility = Visibility.Collapsed;
            }

            if (PanelChat1.Visibility == Visibility.Collapsed)
                PanelChat1.Visibility = Visibility.Visible;

            else
                PanelChat1.Visibility = Visibility.Collapsed;
        }

        private void ButtonOpenMenu2_Click(object sender, RoutedEventArgs e) // button hide second menu
        {
            ButtonOpenMenu2.Visibility = Visibility.Collapsed;
            ButtonCloseMenu2.Visibility = Visibility.Visible;
        }

        private void ButtonCloseMenu2_Click(object sender, RoutedEventArgs e) // button close second menu
        {
            ButtonOpenMenu2.Visibility = Visibility.Visible;
            ButtonCloseMenu2.Visibility = Visibility.Collapsed;
        }

        BCEngine bce = new BCEngine(new BlowfishEngine());

        private void ButtonEnvi1_Click(object sender, RoutedEventArgs e) // button send chat 1
        {
            if (Chat1.Text != "")      
            {
                send2++;

                if (CheckBoxDecript2.IsChecked == true) // se o checkbox do Lucas estiver marcado, envia o texto normal
                {
                    TextEncrypt1 = bce.Encrypt(Chat1.Text, ChaveEncript.Text);

                    Not22.Badge = send2;
                    FirstChat.Text += "Douglas: " + Chat1.Text + "\n";
                    Chat1.Text = "";

                    SendText();

                    NewMessage2.Visibility = Visibility.Visible;
                    Not222.Badge = send2;
                    TimerNot2();

                    if (PanelChat2.Visibility == Visibility.Visible)
                        Not22.Badge = "";
                }

                else // se não, envia o texto criptografado pro douglas
                {
                    if (!string.IsNullOrEmpty(ChaveEncript.Text))
                    {
                        TextEncrypt1 = bce.Encrypt(Chat1.Text, ChaveEncript.Text);

                        FirstChat.Text += "Douglas: " + Chat1.Text + "\n";
                        TextSecond.Text += "Douglas: " + TextEncrypt1 + "\n";
                        Chat1.Text = "";
                        Not22.Badge = send2;
                        Not222.Badge = send2;
                    }
                    else
                    {
                        FirstChat.Text += "Douglas: " + Chat1.Text + "\n"; //linha que vai criptografar o texto
                        TextSecond.Text += "Douglas: " + Chat1.Text + "\n";
                        Chat1.Text = "";
                        Not22.Badge = send2;
                        Not222.Badge = send2;
                    }

                    if (PanelChat2.Visibility == Visibility.Visible)
                        Not22.Badge = "";

                    NewMessage2.Visibility = Visibility.Visible;
                    Not222.Badge = send2;
                    TimerNot2();
                }
            }
        }

        private void Send2_Click(object sender, RoutedEventArgs e) //  button send chat 2
        {
            if (Chat2.Text != "")
            {
                send1++;

                if (CheckBoxDecript1.IsChecked == true) // se o decript do usuario Douglas estiver marcado, aparece o chat normal
                {
                    TextEncrypt2 = bce.Encrypt(Chat2.Text, ChaveEncript2.Text);

                    Not2.Badge = send1;
                    TextSecond.Text += "Lucas: " + Chat2.Text + "\n";
                    Chat2.Text = "";

                    SendTextSecond();

                    NewMessage1.Visibility = Visibility.Visible;
                    Not1.Badge = send1;
                    TimerNot1();

                    if (PanelChat1.Visibility == Visibility.Visible)
                        Not2.Badge = "";
                }
                else // se não, envia o texto encriptado
                {
                    if (!string.IsNullOrEmpty(ChaveEncript2.Text))
                    {
                        TextEncrypt2 = bce.Encrypt(Chat2.Text, ChaveEncript2.Text);

                        Not2.Badge = send1;
                        Not1.Badge = send1;
                        FirstChat.Text += "Lucas: " + TextEncrypt2 + "\n"; //linha que vai criptografar o texto
                        TextSecond.Text += "Lucas: " + Chat2.Text + "\n";
                        Chat2.Text = "";
                    }

                    else
                    {
                        FirstChat.Text += "Lucas: " + Chat2.Text + "\n"; //linha que vai criptografar o texto
                        TextSecond.Text += "Lucas: " + Chat2.Text + "\n";
                        Chat2.Text = "";
                        Not2.Badge = send1;
                    }

                    NewMessage1.Visibility = Visibility.Visible;
                    Not1.Badge = send1;
                    TimerNot1();
                }
            }
        }

        private async Task SendText() // Dec chat 1
        {
            await Task.Delay(10);

            try
            {
                TextDecrypt1 = bce.Decrypt(TextEncrypt1, ChaveDecript2.Text);
                TextSecond.Text += "Douglas: " + TextDecrypt1 + "\n";
            }
            catch
            {
                TextSecond.Text += "Douglas: Chave Incorreta!\n";
            }
        }

        private async Task SendTextSecond() // Dec chat 2
        {
            await Task.Delay(10);

            try
            {
                TextDecrypt2 = bce.Decrypt(TextEncrypt2, ChaveDecript1.Text);
                FirstChat.Text += "Lucas: " + TextDecrypt2 + "\n";
            }
            catch
            {
                FirstChat.Text += "Lucas: Chave Incorreta!\n";
            }
        }

        private async Task TimerNot2()
        {
            await Task.Delay(2000);
            NewMessage2.Visibility = Visibility.Collapsed;
        }

        private async Task TimerNot1()
        {
            await Task.Delay(2000);
            NewMessage1.Visibility = Visibility.Collapsed;
        }     
    }
}
