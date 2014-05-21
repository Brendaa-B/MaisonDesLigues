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
    public partial class FormAjoutAtelier : Form
    {
        private BaseDeDonnees.Bdd UneConnexion = new Bdd("mdl", "mdl");

        public FormAjoutAtelier()
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

        private void BtnAjoutAtelier_Click(object sender, EventArgs e)
        {
            if (this.libelleAtelier.Text.Length > 0 && Convert.ToInt32(this.nbPlaces.Value) > 0)
            {
                UneConnexion.ajoutAtelier(Convert.ToString(this.libelleAtelier.Text), Convert.ToInt32(this.nbPlaces.Value));

            }
            else
            {
                MessageBox.Show("Merci de renseigner tout les champs");
            }
        }

        private void FormAjoutAtelier_Load(object sender, EventArgs e)
        {

        }
    }
}
