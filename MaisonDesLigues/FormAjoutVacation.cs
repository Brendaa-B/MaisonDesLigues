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
    public partial class FormAjoutVacation : Form
    {

        private BaseDeDonnees.Bdd UneConnexion = new Bdd("mdl", "mdl");

        public FormAjoutVacation()
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

        private void FormAjoutVacation_Load(object sender, EventArgs e)
        {
            
            if (UneConnexion.ObtenirDonnesOracle("atelier").Rows.Count > 0)
            {
                comboAtelier.DataSource = UneConnexion.ObtenirDonnesOracle("ATELIER");
                comboAtelier.DisplayMember = "LIBELLEATELIER";
                comboAtelier.ValueMember = "ID";
                comboAtelier.SelectedValue = "ID";
                comboAtelier.Enabled = true;
            }
            else
            {
                MessageBox.Show("Il existe aucun atelier");
                FrmPrincipale form = new FrmPrincipale();
                form.Show();
                this.Hide();
            }
            
        }

        private void BtnAjoutVacation_Click(object sender, EventArgs e)
        {
            
            /**Lors du clique bouton, les date et heure de vacation sont convertis en chaîne de caractères afins de les passer en paramètres
             * de la méthode ajoutvacation**/
            DateTime dateHeureDebut; ;
            DateTime dateHeureFin;
            dateHeureDebut = new DateTime(this.dateTimeJour.Value.Year, this.dateTimeJour.Value.Month, this.dateTimeJour.Value.Day, Convert.ToInt32(this.heureDeb.Value), Convert.ToInt32(this.minDeb.Value),00);
            dateHeureFin = new DateTime(this.dateTimeJour.Value.Year, this.dateTimeJour.Value.Month, this.dateTimeJour.Value.Day, Convert.ToInt32(this.heureFin.Value), Convert.ToInt32(this.minFin.Value), 00);

            if (this.dateTimeJour.Value != null && this.comboAtelier.SelectedValue != null && this.choixNumero.Value != 0)
            {
                string dDebut = Convert.ToString(dateHeureDebut.Day) + "/" + Convert.ToString(dateHeureDebut.Month) + "/" +
                    Convert.ToString(dateHeureDebut.Year) + " " + Convert.ToString(dateHeureDebut.Hour) + ":" +
                    Convert.ToString(dateHeureDebut.Minute) + ":00";


                string dFin = Convert.ToString(dateHeureFin.Day) + "/" + Convert.ToString(dateHeureFin.Month) + "/" +
                    Convert.ToString(dateHeureFin.Year) + " " + Convert.ToString(dateHeureFin.Hour) + ":" +
                    Convert.ToString(dateHeureFin.Minute) + ":00";

                UneConnexion.ajoutVacation(Convert.ToInt32(this.comboAtelier.SelectedValue), Convert.ToInt32(this.choixNumero.Value), dDebut, dFin);
            }
            else
            {
                MessageBox.Show("Merci de renseigner tout les champs");
            }
        }

        
    }
}
