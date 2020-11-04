using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stag
{
    public partial class Main_Page : Form
    {
        private int numBitsUse = 1; 
        private Bitmap bitMap; 
        
        public Main_Page()
        {
            InitializeComponent();
            pic_image.SizeMode = PictureBoxSizeMode.StretchImage; 
        }

        private void btn_openFile_Click(object sender, EventArgs e)
        {
            //open a file for import
            var fileContent = string.Empty;
            var filePath = string.Empty; 
            const string msgStart = "!*"; 
            const string msgEnd = "*!"; 

            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                fileDialog.InitialDirectory = "c:\\";
                fileDialog.Filter = "Image Files (*.jpg *.png)|*.jpg | All files (*.*)|*.*";
                fileDialog.FilterIndex = 2;
                fileDialog.RestoreDirectory = true; 

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        filePath = fileDialog.FileName;
                        txtBx_filePath.Text = filePath;
                        using (var bmpStream = System.IO.File.Open(filePath, System.IO.FileMode.Open))
                        {
                            Image image = Image.FromStream(bmpStream);
                            bitMap = new Bitmap(image);
                            pic_image.Image = image;
                        }
                        pnl_msg.Visible = true; 
                    }catch (Exception ex)
                    {
                        MessageBox msgBox;
                        string message = "An error occurred while opening the file: " + ex.Message;
                        MessageBox.Show(message); 
                    }

                    lbl_maxMsgSizeVal.Text = calculateMaxMsgSize(bitMap).ToString(); 
                }
            }
        }

        private long calculateMaxMsgSize(Bitmap bitMap)
        {
            int totalNumPixels = bitMap.Width * bitMap.Height;
            int numPixelsNeeded = 8 / numBitsUse;
            int msgSize = totalNumPixels / numPixelsNeeded; 
            return msgSize - 4; //subtract 4 in order to allow for start and end of message flags  
        }

        private byte convertCharToByte(char target)
        {
            byte charByte = Convert.ToByte(target);
            return charByte; 
        }

        private void rTxt_imageMsg_TextChanged(object sender, EventArgs e)
        {
            lbl_currChars.Text = rTxt_imageMsg.Text.Length.ToString(); 
        }
    }
}
