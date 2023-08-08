using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace RepositoryLayer.Services
{
    public class EncryptionDecryption
    {
        
            private static readonly byte[] EncryptionKey; // Use a static field to store the encryption key

            // Static constructor to generate the encryption key
           static EncryptionDecryption()
            {
                EncryptionKey = Generate256BitKey();
            }

            public static byte[] Generate256BitKey()
            {
                using (var aes = new AesCryptoServiceProvider())
                {
                    aes.KeySize = 256; // Use 256-bit key
                    aes.GenerateKey();
                    return aes.Key;
                }
            }

            public static string EncryptPassword(string plainText)
            {
                byte[] encrypted;
                using (Aes aes = Aes.Create())
                {
                    aes.Key = EncryptionKey;
                    aes.GenerateIV();
                    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter writer = new StreamWriter(cryptoStream))
                            {
                                writer.Write(plainText);
                            }
                            encrypted = memoryStream.ToArray();
                        }
                    }
                }
                return Convert.ToBase64String(encrypted);
            }
        public static string DecryptPassword(string encryptedText, byte[] encryptionKey, byte[] iv)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
            string decryptedText;

            using (Aes aes = Aes.Create())
            {
                aes.Key = encryptionKey;
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(encryptedBytes))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader reader = new StreamReader(cryptoStream))
                        {
                            decryptedText = reader.ReadToEnd();
                        }
                    }
                }
            }

            return decryptedText;
        }

        /* public static string DecryptPassword(string encryptedText)
         {
             try
             {

                 byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
                 byte[] decryptedBytes;
                 byte[] iv = new byte[16];
                 byte[] encryptedData = new byte[encryptedBytes.Length - 16];

                 Array.Copy(encryptedBytes, iv, 16);
                 Array.Copy(encryptedBytes, 16, encryptedData, 0, encryptedBytes.Length - 16);

                 using (Aes aes = Aes.Create())
                 {
                     aes.Key = EncryptionKey;
                     aes.IV = encryptedBytes.Take(16).ToArray();
                     ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                     using (MemoryStream memoryStream = new MemoryStream(encryptedBytes.Skip(16).ToArray()))
                     {
                         using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                         {
                             using (StreamReader reader = new StreamReader(cryptoStream))
                             {
                                 // Read the decrypted data as a string
                                 return reader.ReadToEnd();
                             }
                         }
                     }
                 }
             }
             catch (FormatException ex)
             {

                 Console.WriteLine(ex.Message);
                 return null; // Or handle the error in an appropriate way
             }*/
        /* catch (Exception ex)
         {

             Console.WriteLine(ex.Message);
             return null; // Or handle the error in an appropriate way
         }*/
    }
        }


        /* public static  string EncryptPassword (string password)
         {
             byte[] StorePassword = ASCIIEncoding.ASCII.GetBytes (password);
             string EncryptedPassword = Convert.ToBase64String (StorePassword);
             return EncryptedPassword;

         }
         public static string DecryptPassword(string password)
         {
             byte[] EncryptedPassword = Convert.FromBase64String(password);
             string DecryptedPassword = ASCIIEncoding.ASCII.GetString(EncryptedPassword);
             return DecryptedPassword;

         }*/
    

