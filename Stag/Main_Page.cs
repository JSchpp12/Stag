using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
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
            Image currentImage; 

            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                fileDialog.InitialDirectory = "c:\\";
                fileDialog.Filter = "Image Files (*.jpg)|*.jpg | All files (*.*)|*.*";
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
                            currentImage = Image.FromStream(bmpStream);
                            var currentImageBitMap = new Bitmap(currentImage);
                            pic_orgImage.Image = currentImage;
                            sBitMap = new stag_Bitmap(currentImageBitMap, numBitsUse);
                        }

                        if (sBitMap.TestForMessage())
                        {
                            var confirmResult = MessageBox.Show("A message has been detected in the image that was opened.", "Would you like to try and decode the message?",  MessageBoxButtons.YesNo); 
                            if (confirmResult == DialogResult.Yes)
                            {
                                startDecodeAsync(currentImage); 
                            }
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
            startEmbedAsync();
            
        }
        
        private void rTxt_imageMsg_TextChanged(object sender, EventArgs e)
        {
            lbl_currChars.Text = rTxt_imageMsg.Text.Length.ToString();
        }

        private async Task startEmbedAsync()
        {
            sBitMap.Message = rTxt_imageMsg.Text;
            Debug.WriteLine("Beginning Encode"); 
            await Task.Run(sBitMap.embedMessageAsync);
            rTxt_imageMsg.Enabled = true;
            Debug.WriteLine("Displaying modified image");
            pic_modifiedImage.Image = sBitMap.newBitmap;
        }

        //decode the message contained in the message and set the message to the message box
        private async Task startDecodeAsync(Image image)
        {
            Bitmap modifiedBitmap = new Bitmap(image); 
            stag_Bitmap decodeTest = new stag_Bitmap(modifiedBitmap);
            try
            {
                await Task.Run(decodeTest.decodeMessageAsync);
                rTxt_imageMsg.Text = decodeTest.Message; 
            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save Location";
            saveFileDialog.CheckPathExists = true;
            saveFileDialog.RestoreDirectory = true; 
            saveFileDialog.Filter = "Image Files (*.bmp)|*.bmp";
            if(pic_modifiedImage.Image != null)
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var name = saveFileDialog.FileName;
                    stag_Bitmap testMap = new stag_Bitmap(sBitMap.newBitmap);
                    testMap.TestForMessage(); 
                    sBitMap.newBitmap.Save(name, ImageFormat.Bmp); 
                }
            }
            else
            {
                //show alert that there is no file to save
            }
        }

        private void saveImage(Bitmap image, string name)
        {

            ImageCodecInfo codecInfo;
            System.Drawing.Imaging.Encoder encoder;
            EncoderParameter encoderParameter;
            EncoderParameters encoderParameters;

            encoder = System.Drawing.Imaging.Encoder.Quality; 
            codecInfo = GetEncoderInfo("image/jpeg");
            encoderParameters = new EncoderParameters(1);

            encoderParameter = new EncoderParameter(encoder, 100L);
            encoderParameters.Param[0] = encoderParameter;
            image.Save(name, codecInfo, encoderParameters); 
        }

        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var options = new Options(this.numBitsUse);
            if (options.ShowDialog(this) == DialogResult.OK)
            {
                numBitsUse = options.numBits;
                sBitMap.setNumBits(numBitsUse); 
            }
        }
    }
}
