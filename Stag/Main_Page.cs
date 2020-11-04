using System;
using System.Collections;
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
        private int numOfPixelsNeeded = 0;  //number of pixels needed to encode one char
        private stag_Bitmap sBitMap; 

        public Main_Page()
        {
            InitializeComponent();
            pic_orgImage.SizeMode = PictureBoxSizeMode.StretchImage;
            pic_modifiedImage.SizeMode = PictureBoxSizeMode.StretchImage; 
        }

        private void btn_openFile_Click(object sender, EventArgs e)
        {
            //open a file for import
            var fileContent = string.Empty;
            var filePath = string.Empty;

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
                            var currentImageBitMap = new Bitmap(image);
                            pic_orgImage.Image = image;
                            sBitMap = new stag_Bitmap(currentImageBitMap, numBitsUse);
                        }
                        pnl_msg.Visible = true;
                        lbl_maxMsgSizeVal.Text = sBitMap.MaxMsgSize.ToString();
                    }
                    catch (Exception ex)
                    {
                        MessageBox msgBox;
                        string message = "An error occurred while opening the file: " + ex.Message;
                        MessageBox.Show(message); 
                    }
                }
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            rTxt_imageMsg.Enabled = false;
            startEmbed(); 
        }
        
        private void rTxt_imageMsg_TextChanged(object sender, EventArgs e)
        {
            lbl_currChars.Text = rTxt_imageMsg.Text.Length.ToString();
        }

        private async Task startEmbed()
        {
            sBitMap.Message = rTxt_imageMsg.Text; 
            await Task.Run(sBitMap.EmbedMessageAsync);
            rTxt_imageMsg.Enabled = true;
            pic_modifiedImage.Image = sBitMap.newBitmap; 
        }
    }
}
