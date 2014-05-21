using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Configuration;
using System.Collections.ObjectModel;
using ComposantNuite;
using BaseDeDonnees;

namespace MaisonDesLigues
{
    public partial class FrmPrincipale : Form
    {

        /// <summary>
        /// constructeur du formulaire
        /// </summary>
        public FrmPrincipale()
        {
            InitializeComponent();
        }
        private Bdd UneConnexion;
        private String TitreApplication;
        private String IdStatutSelectionne = "";
        /// <summary>
        /// création et ouverture d'une connexion vers la base de données sur le chargement du formulaire
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmPrincipale_Load(object sender, EventArgs e)
        {
            UneConnexion = ((FrmLogin)Owner).UneConnexion;
            TitreApplication = ((FrmLogin)Owner).TitreApplication;
            this.Text = TitreApplication;
        }
        /// <summary>
        /// gestion de l'événement click du bouton quitter.
        /// Demande de confirmation avant de quitetr l'application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmdQuitter_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Voulez-vous quitter l'application ?", ConfigurationManager.AppSettings["TitreApplication"], MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                UneConnexion.FermerConnexion();
                Application.Exit();
            }
        }

        private void RadTypeParticipant_Changed(object sender, EventArgs e)
        {
            switch (((RadioButton)sender).Name)
            {
                case "RadBenevole":
                    this.GererInscriptionBenevole();
                    break;
                case "RadLicencie":
                    this.GererInscriptionLicencie();
                    break;
                case "RadIntervenant":
                    this.GererInscriptionIntervenant();
                    break;
                default:
                    throw new Exception("Erreur interne à l'application");
            }
        }

        /// <summary>     
        /// procédure permettant d'afficher l'interface de saisie du complément d'inscription d'un intervenant.
        /// </summary>
        private void GererInscriptionIntervenant()
        {

            GrpBenevole.Visible = false;
            GrpLicencie.Visible = false;
            GrpIntervenant.Visible = true;
            PanFonctionIntervenant.Visible = true;
            GrpIntervenant.Left = 23;
            GrpIntervenant.Top = 264;
            Utilitaire.CreerDesControles(this, UneConnexion, "VSTATUT01", "Rad_", PanFonctionIntervenant, "RadioButton", this.rdbStatutIntervenant_StateChanged);
            Utilitaire.RemplirComboBox(UneConnexion, CmbAtelierIntervenant, "VATELIER01");

            CmbAtelierIntervenant.Text = "Choisir";

        }

         /// <summary>     
        /// procédure permettant d'afficher l'interface de saisie du complément d'inscription d'un licencié.
        /// </summary>
        private void GererInscriptionLicencie()
        {
            GrpLicencie.Visible = true;
            GrpBenevole.Visible = false;
            GrpIntervenant.Visible = false;
            GrpLicencie.Left = 23;
            GrpLicencie.Top = 264;
            PanelAtelierLicencie.Visible = true;
            Utilitaire.RemplirComboBox(UneConnexion, CmbQualite, "VQUALITE01");
            CmbQualite.Text = "Choisir";
            Utilitaire.CreerDesControles(this, UneConnexion, "VATELIER01", "ChkAtelierLicencie_", PanelAtelierLicencie, "CheckBox", this.rdbStatutIntervenant_StateChanged);
            foreach (Control UnControle in PanelAtelierLicencie.Controls)
            {
                if (UnControle.GetType().Name == "CheckBox")
                {
                    CheckBox UneCheckBox = (CheckBox)UnControle;
                    UneCheckBox.CheckedChanged += new System.EventHandler(this.ChkAtelierLicencie_CheckedChanged);
                }
            }
            BtnEnregistreLicencie.Enabled = VerifBtnEnregistreLicencie();
        }
        /// <summary>   
        /// New
        /// procédure permettant d'afficher l'interface de saisie des disponibilités des bénévoles.
        /// </summary>
        private void GererInscriptionBenevole()
        {

            GrpBenevole.Visible = true;
            GrpBenevole.Left = 23;
            GrpBenevole.Top = 264;
            GrpIntervenant.Visible = false;
            GrpLicencie.Visible = false;

            Utilitaire.CreerDesControles(this, UneConnexion, "VDATEBENEVOLAT01", "ChkDateB_", PanelDispoBenevole, "CheckBox", this.rdbStatutIntervenant_StateChanged);
            // on va tester si le controle à placer est de type CheckBox afin de lui placer un événement checked_changed
            // Ceci afin de désactiver les boutons si aucune case à cocher du container n'est cochée
            foreach (Control UnControle in PanelDispoBenevole.Controls)
            {
                if (UnControle.GetType().Name == "CheckBox")
                {
                    CheckBox UneCheckBox = (CheckBox)UnControle;
                    UneCheckBox.CheckedChanged += new System.EventHandler(this.ChkDateBenevole_CheckedChanged);
                }
            }


        }
        /// <summary>
        /// permet d'appeler la méthode VerifBtnEnregistreIntervenant qui déterminera le statu du bouton BtnEnregistrerIntervenant
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdbStatutIntervenant_StateChanged(object sender, EventArgs e)
        {
            // stocke dans un membre de niveau form l'identifiant du statut sélectionné (voir règle de nommage des noms des controles : prefixe_Id)
            this.IdStatutSelectionne = ((RadioButton)sender).Name.Split('_')[1];
            BtnEnregistrerIntervenant.Enabled = VerifBtnEnregistreIntervenant();
        }
        /// <summary>
        /// Permet d'intercepter le click sur le bouton d'enregistrement d'un bénévole.
        /// Cetteméthode va appeler la méthode InscrireBenevole de la Bdd, après avoir mis en forme certains paramètres à envoyer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEnregistreBenevole_Click(object sender, EventArgs e)
        {
            Collection<Int16> IdDatesSelectionnees = new Collection<Int16>();
            Int64? NumeroLicence;
            if (TxtLicenceBenevole.MaskCompleted)
            {
                NumeroLicence = System.Convert.ToInt64(TxtLicenceBenevole.Text);
            }
            else
            {
                NumeroLicence = null;
            }


            foreach (Control UnControle in PanelDispoBenevole.Controls)
            {
                if (UnControle.GetType().Name == "CheckBox" && ((CheckBox)UnControle).Checked)
                {
                    /* Un name de controle est toujours formé come ceci : xxx_Id où id représente l'id dans la table
                     * Donc on splite la chaine et on récupére le deuxième élément qui correspond à l'id de l'élément sélectionné.
                     * on rajoute cet id dans la collection des id des dates sélectionnées
                    */
                    IdDatesSelectionnees.Add(System.Convert.ToInt16((UnControle.Name.Split('_'))[1]));
                }
            }
            UneConnexion.InscrireBenevole(TxtNom.Text, TxtPrenom.Text, TxtAdr1.Text, TxtAdr2.Text != "" ? TxtAdr2.Text : null, TxtCp.Text, TxtVille.Text, txtTel.MaskCompleted ? txtTel.Text : null, TxtMail.Text != "" ? TxtMail.Text : null, System.Convert.ToDateTime(TxtDateNaissance.Text), NumeroLicence, IdDatesSelectionnees);
            //Pour chaque Control c dans le ComboBox, on supprime le text.
            Utilitaire.ViderGroupBox(GrpIdentite,false);
            Utilitaire.ViderGroupBox(GrpTypeParticipant,false);
            Utilitaire.ViderGroupBox(GrpIntervenant,true);
            Utilitaire.ViderGroupBox(GrpBenevole,true);
        }
           
        /// <summary>
        /// Cetet méthode teste les données saisies afin d'activer ou désactiver le bouton d'enregistrement d'un bénévole
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkDateBenevole_CheckedChanged(object sender, EventArgs e)
        {
            BtnEnregistreBenevole.Enabled = (TxtLicenceBenevole.Text == "" || TxtLicenceBenevole.MaskCompleted) && TxtDateNaissance.MaskCompleted && Utilitaire.CompteChecked(PanelDispoBenevole) > 0;
        }
        /// <summary>
        /// New
        /// Cetet méthode teste les données saisies afin d'activer ou désactiver le bouton d'enregistrement d'un licencié
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkAtelierLicencie_CheckedChanged(object sender, EventArgs e)
        {
            BtnEnregistreLicencie.Enabled = VerifBtnEnregistreLicencie();
        }
        /// <summary>
        /// Méthode qui permet d'afficher ou masquer le controle panel permettant la saisie des nuités d'un intervenant.
        /// S'il faut rendre visible le panel, on teste si les nuités possibles ont été chargés dans ce panel. Si non, on les charges 
        /// On charge ici autant de contrôles ResaNuit qu'il y a de nuits possibles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RdbNuiteIntervenant_CheckedChanged(object sender, EventArgs e)
        {
            Utilitaire.AfficheNuitee(sender, e, PanNuiteIntervenant, UneConnexion, "RdbNuiteIntervenantOui");
            BtnEnregistrerIntervenant.Enabled = VerifBtnEnregistreIntervenant();
        }

        /// <summary>
        /// Cette procédure va appeler la procédure .... qui aura pour but d'enregistrer les éléments 
        /// de l'inscription d'un intervenant, avec éventuellment les nuités à prendre en compte        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEnregistrerIntervenant_Click(object sender, EventArgs e)
        {
            try
            {
                if (RdbNuiteIntervenantOui.Checked)
                {
                    // inscription avec les nuitées
                    Collection<Int16> NuitsSelectionnes = new Collection<Int16>();
                    Collection<String> HotelsSelectionnes = new Collection<String>();
                    Collection<String> CategoriesSelectionnees = new Collection<string>();
                    foreach (Control UnControle in PanNuiteIntervenant.Controls)
                    {
                        if (UnControle.GetType().Name == "ResaNuite" && ((ResaNuite)UnControle).GetNuitSelectionnee())
                        {
                            // la nuité a été cochée, il faut donc envoyer l'hotel et la type de chambre à la procédure de la base qui va enregistrer le contenu hébergement 
                            //ContenuUnHebergement UnContenuUnHebergement= new ContenuUnHebergement();
                            CategoriesSelectionnees.Add(((ResaNuite)UnControle).GetTypeChambreSelectionnee());
                            HotelsSelectionnes.Add(((ResaNuite)UnControle).GetHotelSelectionne());
                            NuitsSelectionnes.Add(((ResaNuite)UnControle).IdNuite);
                         }

                    }
                    if (NuitsSelectionnes.Count == 0)
                    {
                        MessageBox.Show("Si vous avez sélectionné que l'intervenant avait des nuités\n il faut qu'au moins une nuit soit sélectionnée");
                    }
                    else
                    {
                        UneConnexion.InscrireIntervenant(TxtNom.Text, TxtPrenom.Text, TxtAdr1.Text, TxtAdr2.Text != "" ? TxtAdr2.Text : null, TxtCp.Text, TxtVille.Text, txtTel.MaskCompleted ? txtTel.Text : null, TxtMail.Text != "" ? TxtMail.Text : null, System.Convert.ToInt16(CmbAtelierIntervenant.SelectedValue), this.IdStatutSelectionne, CategoriesSelectionnees, HotelsSelectionnes, NuitsSelectionnes);
                        MessageBox.Show("Inscription intervenant effectuée");
                        Utilitaire.ViderGroupBox(GrpIdentite, false);
                        Utilitaire.ViderGroupBox(GrpTypeParticipant, false);
                        Utilitaire.ViderGroupBox(GrpIntervenant, true);
                        Utilitaire.ViderGroupBox(GrpBenevole, true);
                    }
                }
                else
                { // inscription sans les nuitées
                    UneConnexion.InscrireIntervenant(TxtNom.Text, TxtPrenom.Text, TxtAdr1.Text, TxtAdr2.Text != "" ? TxtAdr2.Text : null, TxtCp.Text, TxtVille.Text, txtTel.MaskCompleted ? txtTel.Text : null, TxtMail.Text != "" ? TxtMail.Text : null, System.Convert.ToInt16(CmbAtelierIntervenant.SelectedValue), this.IdStatutSelectionne);
                    MessageBox.Show("Inscription intervenant effectuée");
                    Utilitaire.ViderGroupBox(GrpIdentite, false);
                    Utilitaire.ViderGroupBox(GrpTypeParticipant, false);
                    Utilitaire.ViderGroupBox(GrpIntervenant, true);
                    Utilitaire.ViderGroupBox(GrpBenevole, true);
                    
                }

                
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        /// <summary>
        /// Méthode privée testant le contrôle combo et la variable IdStatutSelectionne qui contient une valeur
        /// Cette méthode permetra ensuite de définir l'état du bouton BtnEnregistrerIntervenant
        /// </summary>
        /// <returns></returns>
        private Boolean VerifBtnEnregistreIntervenant()
        {
            return CmbAtelierIntervenant.Text !="Choisir" && this.IdStatutSelectionne.Length > 0;
        }

        private void CmbAtelierIntervenant_TextChanged(object sender, EventArgs e)
        {
            BtnEnregistrerIntervenant.Enabled = VerifBtnEnregistreIntervenant();
        }

        private Boolean VerifBtnEnregistreLicencie()
        {
            return (TxtLicenceLicencie.Text == "" || TxtLicenceLicencie.MaskCompleted) && CmbQualite.Text != "Choisir" && Utilitaire.CompteChecked(PanelAtelierLicencie) <6;
        }

        private void CmbQualite_SelectedIndexChanged(object sender, EventArgs e)
        {
            BtnEnregistreLicencie.Enabled = VerifBtnEnregistreLicencie();
        }

        private void RdbNuiteLicencie_CheckedChanged(object sender, EventArgs e)
        {
            Utilitaire.AfficheNuitee(sender, e, PanNuiteLicencie, UneConnexion, "RdbNuiteLicencieOui");
            BtnEnregistreLicencie.Enabled = VerifBtnEnregistreLicencie();
        }

        /// <summary>
        /// Cette procédure va appeler la procédure .... qui aura pour but d'enregistrer les éléments 
        /// de l'inscription d'un licencié, avec éventuellment les nuités à prendre en compte        
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEnregistreLicencie_Click(object sender, EventArgs e)
        {
            try
            {
                Int16 NbCheque = 1;
                Double MontantCheque1 = System.Convert.ToDouble(TxtMontantChequeAccomp.Text);
                Double MontantCheque2 = 0;
                Double MontantTotalCheque1 = 100;
                Double MontantTotalCheque2 = 0;
                //si il y a du texte dans le Montant du cheque 2, on enregistre qu'il y a 2 cheque
                if (TxtMontantCheque2.Text != "   ,")
                {
                    NbCheque = 2;
                    MontantCheque2 = System.Convert.ToDouble(TxtMontantCheque2.Text);
                }

                Int16 i = 1;
                //on recupere tout les ateliers dans la collection AteliersSelectionnes
                Collection<Int16> AteliersSelectionnes = new Collection<Int16>();
                foreach (CheckBox c in PanelAtelierLicencie.Controls)
                {
                    if (c.Checked)
                    {
                        AteliersSelectionnes.Add(i);
                    }
                    i++;
                }
                i = 1;
                String Inscription = "Insc";
                Collection<Int16> AccompagnantsSelectionnes = new Collection<Int16>();
                //on récupère les accompagnants dans la collection AccompagnantsSelectionnes
                foreach (CheckBox c in PanLicencie.Controls)
                {
                    if (c.Checked)
                    {
                        AccompagnantsSelectionnes.Add(i);
                        Inscription = "Tout";
                        if (NbCheque == 2)
                        {
                            MontantTotalCheque2 += 35;
                        }
                        else
                        {
                            MontantTotalCheque1 += 35;
                        }
                        
                    }
                    i++;
                }
                String MonTypeChambre = "";
                String MonHotel = "";
                //ici on gère le prix des nuitées
                foreach (Control UnControle in PanNuiteLicencie.Controls)
                {
                    if (UnControle.GetType().Name == "ResaNuite" && ((ResaNuite)UnControle).GetNuitSelectionnee())
                    {
                        MonTypeChambre = ((ResaNuite)UnControle).GetTypeChambreSelectionnee();
                        MonHotel = ((ResaNuite)UnControle).GetHotelSelectionne();
                        if (MonTypeChambre == "S")
                        {
                            if (MonHotel == "IBIS")
                            {
                                MontantTotalCheque1 += 61.20;
                            }
                            else
                            {
                                MontantTotalCheque1 += 112;
                            }
                        }
                        else
                        {
                            if (MonHotel == "IBIS")
                            {
                                MontantTotalCheque1 += 62.20;
                            }
                            else
                            {
                                MontantTotalCheque1 += 122;
                            }
                        }
                    }
                    

                }
                if ((MontantCheque1 == MontantTotalCheque1) && (MontantCheque2 == MontantTotalCheque2))
                {
                    if (RdbNuiteLicencieOui.Checked)
                    {
                        // inscription avec les nuitées
                        Collection<Int16> NuitsSelectionnes = new Collection<Int16>();
                        Collection<String> HotelsSelectionnes = new Collection<String>();
                        Collection<String> CategoriesSelectionnees = new Collection<string>();
                        foreach (Control UnControle in PanNuiteLicencie.Controls)
                        {
                            if (UnControle.GetType().Name == "ResaNuite" && ((ResaNuite)UnControle).GetNuitSelectionnee())
                            {
                                // la nuité a été cochée, il faut donc envoyer l'hotel et la type de chambre à la procédure de la base qui va enregistrer le contenu hébergement 
                                //ContenuUnHebergement UnContenuUnHebergement= new ContenuUnHebergement();
                                CategoriesSelectionnees.Add(((ResaNuite)UnControle).GetTypeChambreSelectionnee());
                                HotelsSelectionnes.Add(((ResaNuite)UnControle).GetHotelSelectionne());
                                NuitsSelectionnes.Add(((ResaNuite)UnControle).IdNuite);
                            }

                        }

                        if (NuitsSelectionnes.Count == 0)
                        {
                            MessageBox.Show("Si vous avez sélectionné que le licencié avait des nuités\n il faut qu'au moins une nuit soit sélectionnée");
                        }
                        else
                        {
                            UneConnexion.InscrireLicencie(TxtNom.Text, TxtPrenom.Text, TxtAdr1.Text, TxtAdr2.Text != "" ? TxtAdr2.Text : null, TxtCp.Text, TxtVille.Text, txtTel.MaskCompleted ? txtTel.Text : null, TxtMail.Text != "" ? TxtMail.Text : null, System.Convert.ToInt16(CmbQualite.SelectedValue), System.Convert.ToInt64(TxtLicenceLicencie.Text), AteliersSelectionnes, CategoriesSelectionnees, HotelsSelectionnes, NuitsSelectionnes, System.Convert.ToInt64(TxtNumChequeInsc.Text), MontantCheque1, AccompagnantsSelectionnes, Inscription);
                            if (NbCheque == 2)
                            {
                                UneConnexion.EnregistrerPaiement(MontantCheque2, System.Convert.ToInt64(TxtNumChequeInsc2.Text), System.Convert.ToInt64(TxtLicenceLicencie.Text), "Acco");
                            }
                                MessageBox.Show("Inscription Licencié effectuée");
                            Utilitaire.ViderGroupBox(GrpIdentite, false);
                            Utilitaire.ViderGroupBox(GrpTypeParticipant, false);
                            Utilitaire.ViderGroupBox(GrpIntervenant, true);
                            Utilitaire.ViderGroupBox(GrpLicencie, true);
                            Utilitaire.ViderGroupBox(PanLicencie, true);
                            Utilitaire.ViderGroupBox(PanelAtelierLicencie, true);
                        }
                    }
                    else
                    {
                        // inscription sans les nuitées
                        UneConnexion.InscrireLicencie(TxtNom.Text, TxtPrenom.Text, TxtAdr1.Text, TxtAdr2.Text != "" ? TxtAdr2.Text : null, TxtCp.Text, TxtVille.Text, txtTel.MaskCompleted ? txtTel.Text : null, TxtMail.Text != "" ? TxtMail.Text : null, System.Convert.ToInt16(CmbQualite.SelectedValue), System.Convert.ToInt64(TxtLicenceLicencie.Text), AteliersSelectionnes, System.Convert.ToInt64(TxtNumChequeInsc.Text), MontantCheque1, AccompagnantsSelectionnes, Inscription);
                        if (NbCheque == 2)
                        {
                            UneConnexion.EnregistrerPaiement(MontantCheque2, System.Convert.ToInt64(TxtNumChequeInsc2.Text), System.Convert.ToInt64(TxtLicenceLicencie.Text), "Acco");
                        }
                        MessageBox.Show("Inscription Licencié effectuée");
                        Utilitaire.ViderGroupBox(GrpIdentite, false);
                        Utilitaire.ViderGroupBox(GrpTypeParticipant, false);
                        Utilitaire.ViderGroupBox(GrpIntervenant, true);
                        Utilitaire.ViderGroupBox(GrpLicencie, true);
                        Utilitaire.ViderGroupBox(PanLicencie, true);
                        Utilitaire.ViderGroupBox(PanelAtelierLicencie, true);
                    }
                }
                else
                {
                    MessageBox.Show("Montant incorrect ! Mail envoyé.");
                    //EMAIL ICI
                }
                }
                
            
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void ChkDateBenevole_CheckedChanged(object sender, KeyEventArgs e)
        //{

        //}
    }
}
