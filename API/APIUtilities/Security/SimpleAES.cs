using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace API.Utilities.Security
{


    public class SimpleAES
    {

        public string EncryptString(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public string DecryptString(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
    }

   
    

    //public class SimpleAES
    //{
    //    private static byte[] key = { 123, 217, 19, 11, 24, 26, 85, 45, 114, 184, 27, 162, 37, 112, 222, 209, 241, 24, 175, 144, 173, 53, 196, 29, 24, 26, 17, 218, 131, 236, 53, 209 };
    //    private static byte[] vector = { 146, 64, 191, 111, 23, 3, 113, 119, 231, 121, 221, 112, 79, 32, 114, 156 };
    //    private ICryptoTransform encryptor, decryptor;
    //    private UTF8Encoding encoder;

    //    public SimpleAES()
    //    {
    //        RijndaelManaged rm = new RijndaelManaged();
    //        encryptor = rm.CreateEncryptor(key, vector);
    //        decryptor = rm.CreateDecryptor(key, vector);
    //        encoder = new UTF8Encoding();
    //    }

    //    public string Encrypt(string unencrypted)
    //    {
    //        return Convert.ToBase64String(Encrypt(encoder.GetBytes(unencrypted)));
    //    }

    //    public string Decrypt(string encrypted)
    //    {
    //        return encoder.GetString(Decrypt(Convert.FromBase64String(encrypted)));
    //    }

    //    public string EncryptToUrl(string unencrypted)
    //    {
             
    //        return HttpUtility.UrlEncode(Encrypt(unencrypted));
    //    }

    //    public string DecryptFromUrl(string encrypted)
    //    {
    //        return Decrypt(HttpUtility.UrlDecode(encrypted));
    //    }

    //    public byte[] Encrypt(byte[] buffer)
    //    {
    //        return Transform(buffer, encryptor);
    //    }

    //    public byte[] Decrypt(byte[] buffer)
    //    {
    //        return Transform(buffer, decryptor);
    //    }

    //    protected byte[] Transform(byte[] buffer, ICryptoTransform transform)
    //    {
    //        MemoryStream stream = new MemoryStream();
    //        using (CryptoStream cs = new CryptoStream(stream, transform, CryptoStreamMode.Write))
    //        {
    //            cs.Write(buffer, 0, buffer.Length);
    //        }
    //        return stream.ToArray();
    //    }
    //}

    //public class SimpleAES
    //{
    //    // Change these keys
    //    //private byte[] Key = { 123, 217, 19, 11, 24, 26, 85, 45, 114, 184, 27, 162, 37, 112, 222, 209, 241, 24, 175, 144, 173, 53, 196, 29, 24, 26, 17, 218, 131, 236, 53, 209 };
    //    //private byte[] Vector = { 146, 64, 191, 111, 23, 3, 113, 119, 231, 121, 25,21, 112, 79, 32, 114, 156 };

    //    private byte[] Key = { 123, 234, 124, 245, 125, 134, 126, 145, 116, 169, 243, 111, 222, 156, 118, 251, 234, 38, 58, 178, 189, 167, 190, 101, 201, 212, 223, 159, 157, 153, 151, 152 };
    //    private byte[] Vector = { 154, 158, 156, 152, 23, 34, 45, 56, 67, 78, 89, 90, 1, 12, 112, 110 };

    //    private ICryptoTransform EncryptorTransform, DecryptorTransform;
    //    private System.Text.UTF8Encoding UTFEncoder;

    //    public SimpleAES()
    //    {
    //        //This is our encryption method
    //        RijndaelManaged rm = new RijndaelManaged();

    //        //Create an encryptor and a decryptor using our encryption method, key, and vector.
    //        EncryptorTransform = rm.CreateEncryptor(this.Key, this.Vector);
    //        DecryptorTransform = rm.CreateDecryptor(this.Key, this.Vector);

    //        //Used to translate bytes to text and vice versa
    //        UTFEncoder = new System.Text.UTF8Encoding();
    //    }

    //    /// -------------- Two Utility Methods (not used but may be useful) -----------
    //    /// Generates an encryption key.
    //    static public byte[] GenerateEncryptionKey()
    //    {
    //        //Generate a Key.
    //        RijndaelManaged rm = new RijndaelManaged();
    //        rm.GenerateKey();
    //        return rm.Key;
    //    }

    //    /// Generates a unique encryption vector
    //    static public byte[] GenerateEncryptionVector()
    //    {
    //        //Generate a Vector
    //        RijndaelManaged rm = new RijndaelManaged();
    //        rm.GenerateIV();
    //        return rm.IV;
    //    }


    //    /// ----------- The commonly used methods ------------------------------    
    //    /// Encrypt some text and return a string suitable for passing in a URL.
    //    public string EncryptToString(string TextValue)
    //    {
    //        return ByteArrToString(Encrypt(TextValue));
    //    }

    //    /// Encrypt some text and return an encrypted byte array.
    //    public byte[] Encrypt(string TextValue)
    //    {
    //        //Translates our text value into a byte array.
    //        Byte[] bytes = UTFEncoder.GetBytes(TextValue);

    //        //Used to stream the data in and out of the CryptoStream.
    //        MemoryStream memoryStream = new MemoryStream();

    //        /*
    //         * We will have to write the unencrypted bytes to the stream,
    //         * then read the encrypted result back from the stream.
    //         */
    //        #region Write the decrypted value to the encryption stream
    //        CryptoStream cs = new CryptoStream(memoryStream, EncryptorTransform, CryptoStreamMode.Write);
    //        cs.Write(bytes, 0, bytes.Length);
    //        cs.FlushFinalBlock();
    //        #endregion

    //        #region Read encrypted value back out of the stream
    //        memoryStream.Position = 0;
    //        byte[] encrypted = new byte[memoryStream.Length];
    //        memoryStream.Read(encrypted, 0, encrypted.Length);
    //        #endregion

    //        //Clean up.
    //        cs.Close();
    //        memoryStream.Close();

    //        return encrypted;
    //    }

    //    /// The other side: Decryption methods
    //    public string DecryptString(string EncryptedString)
    //    {
    //        return Decrypt(StrToByteArray(EncryptedString));
    //    }

    //    /// Decryption when working with byte arrays.    
    //    public string Decrypt(byte[] EncryptedValue)
    //    {
    //        #region Write the encrypted value to the decryption stream
    //        MemoryStream encryptedStream = new MemoryStream();
    //        CryptoStream decryptStream = new CryptoStream(encryptedStream, DecryptorTransform, CryptoStreamMode.Write);
    //        decryptStream.Write(EncryptedValue, 0, EncryptedValue.Length);
    //        decryptStream.FlushFinalBlock();
    //        #endregion

    //        #region Read the decrypted value from the stream.
    //        encryptedStream.Position = 0;
    //        Byte[] decryptedBytes = new Byte[encryptedStream.Length];
    //        encryptedStream.Read(decryptedBytes, 0, decryptedBytes.Length);
    //        encryptedStream.Close();
    //        #endregion
    //        return UTFEncoder.GetString(decryptedBytes);
    //    }

    //    /// Convert a string to a byte array.  NOTE: Normally we'd create a Byte Array from a string using an ASCII encoding (like so).
    //    //      System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
    //    //      return encoding.GetBytes(str);
    //    // However, this results in character values that cannot be passed in a URL.  So, instead, I just
    //    // lay out all of the byte values in a long string of numbers (three per - must pad numbers less than 100).
    //    public byte[] StrToByteArray(string str)
    //    {
    //        if (str.Length == 0)
    //            throw new Exception("Invalid string value in StrToByteArray");

    //        byte val;
    //        byte[] byteArr = new byte[str.Length / 3];
    //        int i = 0;
    //        int j = 0;
    //        do
    //        {
    //            val = byte.Parse(str.Substring(i, 3));
    //            byteArr[j++] = val;
    //            i += 3;
    //        }
    //        while (i < str.Length);
    //        return byteArr;
    //    }

    //    // Same comment as above.  Normally the conversion would use an ASCII encoding in the other direction:
    //    //      System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
    //    //      return enc.GetString(byteArr);    
    //    public string ByteArrToString(byte[] byteArr)
    //    {
    //        byte val;
    //        string tempStr = "";
    //        for (int i = 0; i <= byteArr.GetUpperBound(0); i++)
    //        {
    //            val = byteArr[i];
    //            if (val < (byte)10)
    //                tempStr += "00" + val.ToString();
    //            else if (val < (byte)100)
    //                tempStr += "0" + val.ToString();
    //            else
    //                tempStr += val.ToString();
    //        }
    //        return tempStr;
    //    }
    //}

    //public class SimpleAES
    //{
    //    private byte[] Key = { 123, 234, 124, 245, 125, 134, 126, 145, 116, 169, 243, 111, 222, 156, 118, 251, 234, 38, 58, 178, 189, 167, 190, 101, 201, 212, 223, 159, 157, 153, 151, 152 };
    //    private byte[] Vector = { 154, 158, 156, 152, 23, 34, 45, 56, 67, 78, 89, 90, 1, 12, 112, 110 };
    //    private ICryptoTransform EncryptorTransform, DecryptorTransform;
    //    private System.Text.UTF8Encoding UTFEncoder;

    //    public SimpleAES()
    //    {
    //        //This is our encryption method
    //        RijndaelManaged rm = new RijndaelManaged();
    //        //Create an encryptor and a decryptor using our encryption method, key, and vector.
    //        EncryptorTransform = rm.CreateEncryptor(this.Key, this.Vector);
    //        DecryptorTransform = rm.CreateDecryptor(this.Key, this.Vector);
    //        //Used to translate bytes to text and vice versa
    //        UTFEncoder = new System.Text.UTF8Encoding();
    //    }

    //    /// Generates an encryption key.
    //    static public byte[] GenerateEncryptionKey()
    //    {
    //        //Generate a Key.
    //        RijndaelManaged rm = new RijndaelManaged();
    //        rm.GenerateKey();
    //        return rm.Key;
    //    }

    //    /// Sets an encryption key.
    //    public void SetEncryptionKey()
    //    {
    //        //Generate a Key.
    //        Key = GenerateEncryptionKey();
    //    }

    //    /// Generates a unique encryption vector
    //    static public byte[] GenerateEncryptionVector()
    //    {
    //        //Generate a Vector
    //        RijndaelManaged rm = new RijndaelManaged();
    //        rm.GenerateIV();
    //        return rm.IV;
    //    }

    //    /// Sets an encryption key.
    //    public void SetEncryptionVector()
    //    {
    //        //Generate a Key.
    //        Vector = GenerateEncryptionVector();
    //    }

    //    /// ----------- The commonly used methods ------------------------------    
    //    /// Encrypt some text and return a string suitable for passing in a URL.
    //    public string EncryptToString(string TextValue)
    //    {
    //        return ByteArrToString(Encrypt(TextValue));
    //    }

    //    /// Encrypt some text and return an encrypted byte array.
    //    public byte[] Encrypt(string TextValue)
    //    {
    //        //Translates our text value into a byte array.
    //        Byte[] bytes = UTFEncoder.GetBytes(TextValue);
    //        //Used to stream the data in and out of the CryptoStream.
    //        MemoryStream memoryStream = new MemoryStream();
    //        /*
    //         * We will have to write the unencrypted bytes to the stream,
    //         * then read the encrypted result back from the stream.
    //         */
    //        #region Write the decrypted value to the encryption stream
    //        CryptoStream cs = new CryptoStream(memoryStream, EncryptorTransform, CryptoStreamMode.Write);
    //        cs.Write(bytes, 0, bytes.Length);
    //        cs.FlushFinalBlock();
    //        #endregion

    //        #region Read encrypted value back out of the stream
    //        memoryStream.Position = 0;
    //        byte[] encrypted = new byte[memoryStream.Length];
    //        memoryStream.Read(encrypted, 0, encrypted.Length);
    //        #endregion

    //        //Clean up.
    //        cs.Close();
    //        memoryStream.Close();
    //        return encrypted;
    //    }

    //    /// The other side: Decryption methods
    //    public string DecryptString(string EncryptedString)
    //    {
    //        return Decrypt(StrToByteArray(EncryptedString));
    //    }

    //    /// Decryption when working with byte arrays.    
    //    public string Decrypt(byte[] EncryptedValue)
    //    {
    //        #region Write the encrypted value to the decryption stream

    //        MemoryStream encryptedStream = new MemoryStream();
    //        CryptoStream decryptStream = new CryptoStream(encryptedStream, DecryptorTransform, CryptoStreamMode.Write);
    //        decryptStream.Write(EncryptedValue, 0, EncryptedValue.Length);
    //        decryptStream.FlushFinalBlock();
    //        #endregion

    //        #region Read the decrypted value from the stream.
    //        encryptedStream.Position = 0;
    //        Byte[] decryptedBytes = new Byte[encryptedStream.Length];
    //        encryptedStream.Read(decryptedBytes, 0, decryptedBytes.Length);
    //        encryptedStream.Close();
    //        #endregion
    //        return UTFEncoder.GetString(decryptedBytes);
    //    }

    //    /// Convert a string to a byte array.  NOTE: Normally we'd create a Byte Array from a string using an ASCII encoding (like so).
    //    //      System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
    //    //      return encoding.GetBytes(str);
    //    // However, this results in character values that cannot be passed in a URL.  So, instead, I just
    //    // lay out all of the byte values in a long string of numbers (three per - must pad numbers less than 100).
    //    public byte[] StrToByteArray(string str)
    //    {

    //        if (str.Length == 0)
    //            throw new Exception("Invalid string value in StrToByteArray");
    //        //byte[] byteArr = null;

    //        //byteArr = UTFEncoder.GetBytes(str);
    //        //return byteArr;



    //        byte val;
    //        byte[] byteArr = new byte[str.Length / 3];
    //        int i = 0;
    //        int j = 0;

    //        try
    //        {
    //            do
    //            {
    //                val = Convert.ToByte(str.Substring(i, 3));
    //                byteArr[j++] = val;
    //                i += 3;
    //            }
    //            while (i < str.Length);
    //            return byteArr;
    //        }
    //        catch (Exception ex)
    //        { }
    //        return byteArr;

    //    }

    //    // Same comment as above.  Normally the conversion would use an ASCII encoding in the other direction:
    //    //      System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
    //    //      return enc.GetString(byteArr);    
    //    public string ByteArrToString(byte[] byteArr)
    //    {
    //        byte val;
    //        string tempStr = "";
    //        for (int i = 0; i <= byteArr.GetUpperBound(0); i++)
    //        {
    //            val = byteArr[i];
    //            if (val < (byte)10)
    //                tempStr += "00" + val.ToString();
    //            else if (val < (byte)100)
    //                tempStr += "0" + val.ToString();
    //            else
    //                tempStr += val.ToString();
    //        }
    //        return tempStr;
    //    }
    //}
}
