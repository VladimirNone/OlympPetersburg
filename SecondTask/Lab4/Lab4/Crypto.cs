using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Lab4
{
    public class Crypto
    {
        private RSACryptoServiceProvider RsaService { get; set; }
        public string FileName { get; set; }

        public void CreateNewKey(string keyName)
        {
            RsaService = new RSACryptoServiceProvider(2048);
            KeyContainer.WriteKeyToConteiner(RsaService.ToXmlString(true), keyName, isPrivate: true, isSignature: false);
        }

        public void SavePublicKey(string keyName)
        {
            if (RsaService == null)
                throw new Exception("Публичный ключ отсутсвует");

            KeyContainer.WriteKeyToConteiner(RsaService.ToXmlString(false), keyName, isPrivate: false, isSignature: false);
        }

        public void LoadKey(string keyName, bool isPrivate)
        {
            RsaService = new RSACryptoServiceProvider(2048);
            try
            {
                RsaService.FromXmlString(KeyContainer.ReadKeyFromContainer(keyName, isPrivate, isSignature: false));
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task EncryptUsingAes()
        {
            if (!File.Exists(FileName))
                throw new Exception("Указанный файл не существует");

            const int BUFFER_SIZE = 8192;
            byte[] buffer = new byte[BUFFER_SIZE];

            using var reader = File.OpenRead(FileName);
            var pathToFileName = Path.GetFileNameWithoutExtension(FileName) + "Encrypt" + Path.GetExtension(FileName);
            using (FileStream fileStream = new(pathToFileName, FileMode.OpenOrCreate))
            {
                using (Aes aes = Aes.Create())
                {
                    aes.GenerateKey();

                    var iv = aes.IV;
                    var ivLength = BitConverter.GetBytes(iv.Length);
                    fileStream.Write(ivLength, 0, ivLength.Length);
                    fileStream.Write(iv, 0, iv.Length);

                    var encryptedSymKey = EncryptUsingRsa(aes.Key);
                    var encryptedSymKeyLength = BitConverter.GetBytes(encryptedSymKey.Length);
                    fileStream.Write(encryptedSymKeyLength, 0, encryptedSymKeyLength.Length);
                    fileStream.Write(encryptedSymKey, 0, encryptedSymKey.Length);

                    using (CryptoStream cryptoStream = new(fileStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        using StreamWriter encryptWriter = new(cryptoStream);

                        int count;
                        while ((count = reader.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            await cryptoStream.WriteAsync(buffer, 0, count);
                        }
                    }
                }
            }
        }

        public async Task DecryptUsingAes()
        {
            if (!File.Exists(FileName))
                throw new Exception("Указанный файл не существует");

            var pathToFileName = FileName.Replace("Encrypt", "Decrypt");
            using var outStream = File.OpenWrite(pathToFileName);

            using (FileStream fileStream = new(FileName, FileMode.Open))
            {
                using (Aes aes = Aes.Create())
                {
                    var buffer = new byte[4];

                    fileStream.Read(buffer, 0, buffer.Length);
                    var ivLength = BitConverter.ToInt32(buffer);

                    buffer = new byte[ivLength];

                    fileStream.Read(buffer, 0, buffer.Length);
                    var iv = buffer;

                    buffer = new byte[4];

                    fileStream.Read(buffer, 0, buffer.Length);
                    var encryptedSymKeyLength = BitConverter.ToInt32(buffer);

                    buffer = new byte[encryptedSymKeyLength];

                    fileStream.Read(buffer, 0, buffer.Length);
                    var encryptedSymKey = buffer;

                    byte[] decryptedSymKey;

                    decryptedSymKey = DecryptUsingRsa(encryptedSymKey);

                    using (CryptoStream cryptoStream = new(fileStream, aes.CreateDecryptor(decryptedSymKey, iv), CryptoStreamMode.Read))
                    {
                        int count;
                        while ((count = cryptoStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            await outStream.WriteAsync(buffer, 0, count);
                        }
                    }
                }
            }
        }

        public byte[] EncryptUsingRsa(byte[] data)
        {
            var ecnrypted = RsaService.Encrypt(data, RSAEncryptionPadding.Pkcs1);

            //KeyContainer.WriteKeyToConteiner(rsaService.ToXmlString(true), FileName);

            return ecnrypted;
        }

        public byte[] DecryptUsingRsa(byte[] data)
        {
            //rsaService.FromXmlString(KeyContainer.ReadKeyFromContainer(FileName));

            try
            {
                return RsaService.Decrypt(data, RSAEncryptionPadding.Pkcs1);
            }
            catch
            {
                throw new Exception("Расшифровка невозможна. Возможные причины: поврежден ключ, неверный ключ, использован публичный ключ");
            }
        }
    }
}
