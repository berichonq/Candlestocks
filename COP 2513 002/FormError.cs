/*
 * Quinn Berichon
 * FormError Class
 * 4/18/2023
 */

using System;
using System.Windows.Forms;

namespace COP_2513_002
{
    public partial class FormError : Form
    {       
        /// <summary>
        /// Default constructor
        /// </summary>
        public FormError()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Closes the dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}