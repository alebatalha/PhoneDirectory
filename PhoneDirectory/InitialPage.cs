using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhoneDirectory
{
    public partial class InitialPage : Form
    {
        public InitialPage()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                panel1.Enabled = true;
                App.PhoneBook.AddPhoneBookRow(App.PhoneBook.NewPhoneBookRow());
                phoneBookBindingSource.MoveLast();
                txtPhoneNumber.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                App.PhoneBook.RejectChanges();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            panel1.Enabled = true;
            txtPhoneNumber.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            phoneBookBindingSource.ResetBindings(false);
            panel1.Enabled=false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                phoneBookBindingSource.EndEdit();
                App.PhoneBook.AcceptChanges();
                App.PhoneBook.WriteXml(string.Format("{0}//data.dat", Application.StartupPath));
                panel1.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                App.PhoneBook.RejectChanges();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this record?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                phoneBookBindingSource.RemoveCurrent();
                try
                {
                    App.PhoneBook.AcceptChanges();
                    App.PhoneBook.WriteXml(string.Format("{0}//data.dat", Application.StartupPath));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    App.PhoneBook.RejectChanges();
                }
            }

        static AppData db;
        protected static AppData App
        {
            get 
            { 
                if(db == null)
                    db= new AppData();
               return db; 
            }
        }

        private void InitialPage_Load(object sender, EventArgs e)
        {
            string fileName = string.Format("{0}//data.dat", Application.StartupPath);
            if (File.Exists(fileName))
                App.PhoneBook.ReadXml(fileName);
            phoneBookBindingSource.DataSource = App.PhoneBook;
            panel1.Enabled = false;                        
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
