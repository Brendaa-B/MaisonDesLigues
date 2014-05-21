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
    public partial class FormModifiVacation : Form
    {
        private BaseDeDonnees.Bdd UneConnexion = new Bdd("mdl", "mdl");

        public FormModifiVacation()
        {
            InitializeComponent();
        }

        private void FormModifiVacation_Load(object sender, EventArgs e)
        {
            comboAtelier.DataSource = UneConnexion.ObtenirIdAtelierVacation();
            comboAtelier.DisplayMember = "LIBELLEATELIER";
            comboAtelier.ValueMember = "ID";
            comboAtelier.Enabled = true;
        }

        private void comboAtelier_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        private void BtnAjoutVacation_Click(object sender, EventArgs e)
        {   /**Lors du click, les dates et heures de vacations sont convertis en chaîne de caracteres afin de les passer en parametres de la mathode modif vacation.**/
            DateTime dateHeureDebut; ;
            DateTime dateHeureFin;
            dateHeureDebut = new DateTime(this.dateTimeJour.Value.Year, this.dateTimeJour.Value.Month, this.dateTimeJour.Value.Day, Convert.ToInt32(this.heureDeb.Value), Convert.ToInt32(this.minDeb.Value), 00);
            dateHeureFin = new DateTime(this.dateTimeJour.Value.Year, this.dateTimeJour.Value.Month, this.dateTimeJour.Value.Day, Convert.ToInt32(this.heureFin.Value), Convert.ToInt32(this.minFin.Value), 00);

            string dDebut = Convert.ToString(dateHeureDebut.Day) + "/" + Convert.ToString(dateHeureDebut.Month) + "/" +
                Convert.ToString(dateHeureDebut.Year) + " " + Convert.ToString(dateHeureDebut.Hour) + ":" +
                Convert.ToString(dateHeureDebut.Minute) + ":00";


            string dFin = Convert.ToString(dateHeureFin.Day) + "/" + Convert.ToString(dateHeureFin.Month) + "/" +
                Convert.ToString(dateHeureFin.Year) + " " + Convert.ToString(dateHeureFin.Hour) + ":" +
                Convert.ToString(dateHeureFin.Minute) + ":00";

            UneConnexion.modifVacation(Convert.ToInt32(this.comboAtelier.SelectedValue),Convert.ToInt32(this.choixNumero.Value), dDebut, dFin);
            
        }
    }
}
