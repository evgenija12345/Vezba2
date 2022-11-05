using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography; //*
namespace Vezba2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string Kluc;
        string IV;
        byte[] encrypted;
        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            try
            {
                Kluc = textBox2.Text;
                IV = textBox3.Text;
                string VnesenString = textBox1.Text;
                byte[] plaintextbytes = System.Text.ASCIIEncoding.ASCII.GetBytes(VnesenString);
                AesCryptoServiceProvider aes= new AesCryptoServiceProvider();
                aes.KeySize = 128;
                aes.BlockSize = 128;
                aes.Key = System.Text.ASCIIEncoding.ASCII.GetBytes(Kluc);
                aes.IV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV);
                aes.Padding = PaddingMode.PKCS7;
                aes.Mode = CipherMode.CBC;
                ICryptoTransform crypto = aes.CreateEncryptor(aes.Key, aes.IV);
                encrypted = crypto.TransformFinalBlock(plaintextbytes, 0, plaintextbytes.Length);
                textBox4.Text = BitConverter.ToString(encrypted);
                textBox5.Text = String.Empty;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            try
            {
                AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
                aes.KeySize = 128;
                aes.BlockSize = 128;
                aes.Key = System.Text.ASCIIEncoding.ASCII.GetBytes(Kluc);
                aes.IV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV);
                aes.Padding = PaddingMode.PKCS7;
                aes.Mode = CipherMode.CBC;
                ICryptoTransform crypto = aes.CreateDecryptor(aes.Key, aes.IV);
                byte[] secret = crypto.TransformFinalBlock(encrypted, 0, encrypted.Length);
                textBox5.Text = System.Text.ASCIIEncoding.ASCII.GetString(secret);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
