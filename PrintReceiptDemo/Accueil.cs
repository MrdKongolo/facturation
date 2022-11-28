using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrintReceiptDemo
{
    public partial class Accueil : Form
    {
        int ordre = 1;
        double total = 0;

        public Accueil()
        {
            InitializeComponent();
        }
        //Codage du bouton Ajouter
        private void btnAjouter_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtArticle.Text) && !string.IsNullOrEmpty(txtPrix.Text))
                {
                    Receipt obj = new Receipt() { Id = ordre++, NomArticle = txtArticle.Text, Prix = Convert.ToDouble(txtPrix.Text), Quantite = Convert.ToInt32(txtQte.Text) };
                    total += obj.Prix * obj.Quantite;

                    receiptBindingSource.Add(obj);
                    receiptBindingSource.MoveLast();
                     
                    txtArticle.Text = string.Empty;
                    txtQte.Text = string.Empty;
                    txtPrix.Text = string.Empty;
                    txtTotal.Text = string.Format("{0}$", total);
                }
            }
            catch (Exception erreur)
            {
                MessageBox.Show(erreur.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ordre--;
            }
        }

        private void Accueil_Load(object sender, EventArgs e)
        {
            receiptBindingSource.DataSource = new List<Receipt>(); // Init empty list
            txtClient.Focus();
        }

        private void bntEnlever_Click(object sender, EventArgs e)
        {
            Receipt obj = receiptBindingSource.Current as Receipt;
            if (obj != null)
            {
                total -= obj.Quantite * obj.Prix;
                txtTotal.Text = string.Format("{0}$",total);
            }
            receiptBindingSource.RemoveCurrent();
        }
        //Codage du bouton Imprimer
        private void btnImprimer_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtClient.Text))
                {
                    MessageBox.Show("Veuillez entrer le nom du client s'il vous plaît!", "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtClient.Focus();                    
                }

                using (frmImpr frm = new frmImpr(receiptBindingSource.DataSource as List<Receipt>, string.Format("{0:0.00}$", total), string.Format("{0:0.00}$", 
                    txtCash.Text), string.Format("{0:0.00}$", Convert.ToDouble(txtCash.Text) - total), DateTime.Now.ToString(), 
                    string.Format("{0}", txtClient.Text), string.Format("{0:0.00}$", total*0.16)))
                {                   
                    frm.ShowDialog();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
