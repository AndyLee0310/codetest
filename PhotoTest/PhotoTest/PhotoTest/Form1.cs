using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.OleDb;
using System.IO;

namespace PhotoTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void photoBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.photoBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.database1DataSet);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet.photo' table. You can move, or remove it, as needed.
            this.photoTableAdapter.Fill(this.database1DataSet.photo);
            Read(null, null);
        }

        private void NewBtn_Click(object sender, EventArgs e)
        {
            Write(null, null);
            this.photoBindingSource.AddNew();
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            Write(null, null);
        }

        private void DelBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to delete this data?", "Question", MessageBoxButtons.YesNo) == DialogResult.Yes ){
                this.photoBindingSource.RemoveCurrent ();
                SaveBtn_Click(sender, e);
            }
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (photoPictureBox.Image is null)
            {
                MessageBox.Show("Photo File is not empty!!", "Note");
            }else{
                photoBindingNavigatorSaveItem_Click(sender, e);
                this.photoTableAdapter.Fill(this.database1DataSet.photo);
                Read(null, null);
            }
        }

        private void CelBtn_Click(object sender, EventArgs e)
        {
            this.photoTableAdapter.Fill(this.database1DataSet.photo);
            Read(null, null);
        }

        string imgname;
        OleDbDataAdapter diaimage;
        DataSet dsimage;

        private void LoadBtn_Click(object sender, EventArgs e)
        {
            try
            {
                FileDialog dlgimage = new OpenFileDialog();
                dlgimage.Filter = "Image file (*.jpg;*.png)|*.jpg;*.png| All file (*.*)|*.*";
                if(dlgimage.ShowDialog() == DialogResult.OK)
                {
                    imgname = dlgimage.FileName;
                    Bitmap newimg = new Bitmap(imgname);
                    photoPictureBox.Image = (Image)newimg; //directcast(newimg, Image);
                }
                dlgimage = null;
            }
            catch (ArgumentException ae){
                imgname = " ";
                MessageBox.Show(ae.Message .ToString());
            }
            catch (Exception ex){
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Write(object sender, EventArgs e)
        {
            NewBtn.Enabled = false;
            EditBtn.Enabled = false;
            DelBtn.Enabled = false;
            SaveBtn.Enabled = true;
            CelBtn.Enabled = true;
            panel1.Enabled = true;
            panel2.Enabled = false;

        }
        private void Read(object sender, EventArgs e)
        {
            NewBtn.Enabled = true;
            EditBtn.Enabled = true;
            DelBtn.Enabled = true;
            SaveBtn.Enabled = false;
            CelBtn.Enabled = false;
            panel1.Enabled = false;
            panel2.Enabled = true;

        }
    }
}
