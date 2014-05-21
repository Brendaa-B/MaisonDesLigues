using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BaseDeDonnees;

namespace MaisonDesLigues
{
    public partial class FormAjoutTheme : Form
    {
        private BaseDeDonnees.Bdd UneConnexion = new Bdd("mdl", "mdl");
        public FormAjoutTheme()
        {
            InitializeComponent();
        }

       
        private void BtnAnuler_Click(object sender, EventArgs e)
        {
            try
            {
            
                (new FrmPrincipale()).Show(this);
                this.Hide();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void comboAtelierTheme_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void FormAjoutTheme_Load(object sender, EventArgs e)
        {
            if (UneConnexion.ObtenirDonnesOracle("atelier").Rows.Count > 0)
            {
                comboAtelierTheme.DataSource = UneConnexion.ObtenirDonnesOracle("ATELIER");
                comboAtelierTheme.DisplayMember = "LIBELLEATELIER";
                comboAtelierTheme.ValueMember = "ID";
                comboAtelierTheme.SelectedValue = "ID";
                comboAtelierTheme.Enabled = true;
            }
            else
            {
                MessageBox.Show("Il existe aucun atelier");
                FrmPrincipale form = new FrmPrincipale();
                form.Show();
                this.Hide();
            }
        }

        private void BtnAjoutTheme_Click(object sender, EventArgs e)
        {
            UneConnexion.ajoutTheme(Convert.ToInt32(this.comboAtelierTheme.SelectedValue), Convert.ToInt32(this.numeroTheme.Value), Convert.ToString(this.libelleTheme.Text));
        }



        
    }
}
