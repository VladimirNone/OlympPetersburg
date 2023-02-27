using System;
using System.IO;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Crypto crypto = new Crypto();

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void bEncrypt_Click(object sender, RoutedEventArgs e)
        {
            crypto.FileName = tbFilePath.Text;
            try
            {
                await crypto.EncryptUsingAes();
                tbNotifications.Text = "Файл был зашифрован";
            }
            catch(Exception ex)
            {
                tbNotifications.Text = ex.Message;
            }
        }

        private async void bDecrypt_Click(object sender, RoutedEventArgs e)
        {
            crypto.FileName = tbFilePath.Text;
            try
            {
                await crypto.DecryptUsingAes();
                tbNotifications.Text = "Файл был расшифрован";
            }
            catch(Exception ex)
            {
                tbNotifications.Text = ex.Message;
            }
        }

        private void bSavePublicKey_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbKeyName.Text))
            {
                try
                {
                    crypto.SavePublicKey(tbKeyName.Text);

                    tbNotifications.Text = "Публичный ключ был сохранен";
                }
                catch (Exception ex)
                {
                    tbNotifications.Text = ex.Message;
                }
            }
            else
            {
                tbNotifications.Text = "Вам необходимо дать название ключу.";
            }
        }

        private void bCreatePrivatecKey_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbKeyName.Text))
            {
                crypto.CreateNewKey(tbKeyName.Text);

                tbNotifications.Text = "Ключ был создан и сохранен";
            }
            else
            {
                tbNotifications.Text = "Вам необходимо дать название ключу.";
            }
        }

        private void bLoadPublicKey_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbKeyName.Text))
            {
                try
                {
                    crypto.LoadKey(tbKeyName.Text, false);

                    tbNotifications.Text = "Публичный ключ был загружен";
                }
                catch(Exception ex)
                {
                    tbNotifications.Text = ex.Message;
                }
            }
            else
            {
                tbNotifications.Text = "Вам необходимо дать название ключу. Возможно ключа с таким названием не существует.";
            }
        }

        private void bLoadPrivateKey_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbKeyName.Text))
            {
                try
                {
                    crypto.LoadKey(tbKeyName.Text, true);

                    tbNotifications.Text = "Приватный ключ был загружен";
                }
                catch (Exception ex)
                {
                    tbNotifications.Text = ex.Message;
                }
            }
            else
            {
                tbNotifications.Text = "Вам необходимо дать название ключу. Возможно ключа с таким названием не существует.";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var decrypted = File.OpenRead(tbFilePath.Text);
            var original = File.OpenRead("E:\\University\\OlympPetersburg\\SecondTask\\4лаба.docx");
            while(decrypted.Position != decrypted.Length)
            {
                if (decrypted.ReadByte() != original.ReadByte())
                    tbNotifications.Text = "ОШибка!";
            }
        }
    }
}
