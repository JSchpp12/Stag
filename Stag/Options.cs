using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stag
{
    public partial class Options : Form
    {
        public int numBits; 

        public Options(int inNumBits)
        {
            InitializeComponent();
            txt_numBits.Text = inNumBits.ToString(); 
        }

        private void btn_confirm_Click(object sender, EventArgs e)
        {
            try
            {
                this.numBits = Int32.Parse(txt_numBits.Text);
                if (numBits > 0 && numBits <= 8)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close(); 
                }
                else
                {
                    MessageBox.Show("Only numbers 1 through 8 are permitted"); 
                }
            }catch(Exception ex)
            {
                MessageBox.Show("Only numbers are permitted for the number of bits"); 
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; 
            this.Close(); 
        }
    }
}
