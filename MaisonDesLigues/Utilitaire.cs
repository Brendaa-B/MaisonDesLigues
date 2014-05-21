using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using System.Data;
using BaseDeDonnees;
using ComposantNuite;
using System.Reflection;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

// Ligne obligatoire pour que la classe Utilitaire soit
// visible par les tests unitaires
[assembly: InternalsVisibleTo("UnitTestMDL")]

namespace MaisonDesLigues
{
    internal abstract class Utilitaire
    {
        /// <summary>
        /// Cette méthode permet de renseigner les propriétés des contrôles à créer. C'est une partie commune aux 
        /// 3 types de participants : intervenant, licencié, bénévole
        /// </summary>
        /// <param name="UneForme">le formulaire concerné</param>  
        /// <param name="UnContainer">le panel ou le groupbox dans lequel on va placer les controles</param> 
        /// <param name="UnControleAPlacer"> le controle en cours de création</param>
        /// <param name="UnPrefixe">les noms des controles sont standard : NomControle_XX
        ///                                         ou XX estl'id de l'enregistrement récupéré dans la vue qui
        ///                                         sert de siurce de données</param> 
        /// <param name="UneLigne">un enregistrement de la vue, celle pour laquelle on crée le contrôle</param> 
        /// <param name="i"> Un compteur qui sert à positionner en hauteur le controle</param>   
        /// <param name="callback"> Le pointeur de fonction. En fait le pointeur sur la fonction qui réagira à l'événement déclencheur </param>
        private static void AffecterControle(Form UneForme, ScrollableControl UnContainer, ButtonBase UnControleAPlacer, String UnPrefixe, DataRow UneLigne, Int16 i, Action<object, EventArgs> callback)
        {
            UnControleAPlacer.Name = UnPrefixe + UneLigne[0];
            UnControleAPlacer.Width = 320;
            UnControleAPlacer.Text = UneLigne[1].ToString();
            UnControleAPlacer.Left = 13;
            UnControleAPlacer.Top = 5 +(10 * i);
            UnControleAPlacer.Visible = true;                  
            System.Type UnType = UneForme.GetType();
            //((UnType)UneForme).
            UnContainer.Controls.Add(UnControleAPlacer);
            
        }
        /// <summary>
        /// Créé une combobox dans un container avec le nom passé en paramètre
        /// </summary>
        /// <param name="UnContainer">panel ou groupbox</param> 
        /// <param name="unNom">nom de la groupbox à créer</param> 
        /// <param name="UnTop">positionnement haut dans le container  </param> 
        /// <param name="UnLeft">positionnement bas dans le container </param> 
        public static void CreerCombo(ScrollableControl UnContainer, String unNom, Int16 UnTop, Int16 UnLeft)
        {
            CheckBox UneCheckBox= new CheckBox();
            UneCheckBox.Name=unNom;
            UneCheckBox.Top=UnTop;
            UneCheckBox.Left=UnLeft;
            UneCheckBox.Visible=true;
            UnContainer.Controls.Add(UneCheckBox);
        }
        /// <summary>
        /// Cette méthode crée des controles de type chckbox ou radio button dans un controle de type panel.
        /// Elle va chercher les données dans la base de données et crée autant de controles (les uns au dessous des autres
        /// qu'il y a de lignes renvoyées par la base de données.
        /// </summary>
        /// <param name="UneForme">Le formulaire concerné</param> 
        /// <param name="UneConnexion">L'objet connexion à utiliser pour la connexion à la BD</param> 
        /// <param name="pUneTable">Le nom de la source de données qui va fournir les données. Il s'agit en fait d'une vue de type
        /// VXXXXOn ou XXXX représente le nom de la tabl à partir de laquelle la vue est créée. n représente un numéro de séquence</param>  
        /// <param name="pPrefixe">les noms des controles sont standard : NomControle_XX
        ///                                         ou XX estl'id de l'enregistrement récupéré dans la vue qui
        ///                                         sert de source de données</param>
        /// <param name="UnPanel">panel ou groupbox dans lequel on va créer les controles</param>
        /// <param name="unTypeControle">type de controle à créer : checkbox ou radiobutton</param>
        /// <param name="callback"> Le pointeur de fonction. En fait le pointeur sur la fonction qui réagira à l'événement déclencheur </param>
        public static void CreerDesControles(Form UneForme, Bdd UneConnexion, String pUneTable, String pPrefixe, ScrollableControl UnPanel, String unTypeControle, Action<object, EventArgs> callback)
        {
            DataTable UneTable = UneConnexion.ObtenirDonnesOracle(pUneTable);
            // on va récupérer les statuts dans un datatable puis on va parcourir les lignes(rows) de ce datatable pour 
            // construire dynamiquement les boutons radio pour le statut de l'intervenant dans son atelier


            Int16 i = 0;
            foreach (DataRow UneLigne in UneTable.Rows)
            {
                //object UnControle = Activator.CreateInstance(object unobjet, unTypeControle);
                //UnControle=Convert.ChangeType(UnControle, TypeC);
                  
                if (unTypeControle == "CheckBox")
                {
                    CheckBox UnControle = new CheckBox();
                    AffecterControle(UneForme, UnPanel, UnControle, pPrefixe, UneLigne, i++, callback);     
              
                }
                else if (unTypeControle == "RadioButton")
                {
                    RadioButton UnControle = new RadioButton();
                    AffecterControle(UneForme, UnPanel, UnControle, pPrefixe, UneLigne, i++, callback);
                    UnControle.CheckedChanged += new System.EventHandler(callback);
                }
                i++;
            }
            UnPanel.Height = 20 * i + 5;
        }
        /// <summary>
        /// méthode permettant de remplir une combobox à partir d'une source de données
        /// </summary>
        /// <param name="UneConnexion">L'objet connexion à utiliser pour la connexion à la BD</param>
        /// <param name="UneCombo"> La combobox que l'on doit remplir</param>
        /// <param name="UneSource">Le nom de la source de données qui va fournir les données. Il s'agit en fait d'une vue de type
        /// VXXXXOn ou XXXX représente le nom de la tabl à partir de laquelle la vue est créée. n représente un numéro de séquence</param>
        public static void RemplirComboBox(Bdd UneConnexion, ComboBox UneCombo, String UneSource)
        {

            UneCombo.DataSource = UneConnexion.ObtenirDonnesOracle(UneSource);
            UneCombo.DisplayMember = "libelle";
            UneCombo.ValueMember = "id";
        }
        /// <summary>
        /// Cette fonction va compter le nombre de controles types CheckBox qui sont cochées contenus dans la collection controls
        /// du container passé en paramètre
        /// </summary>
        /// <param name="UnControl"> le container sur lequel on va compter les controles de type checkbox qui sont checked</param>
        /// <returns>nombre  de checkbox cochées</returns>
        internal static int CompteChecked(ScrollableControl UnContainer)
        {
            Int16 i = 0;
            foreach (Control UnControle in UnContainer.Controls)
            {
                if (UnControle.GetType().Name == "CheckBox" && ((CheckBox)UnControle).Checked)
                {
                    i++;
                }
            }
            return i;
        }

        /// <summary>
        /// Fonction permettant de vider les groupbox
        /// </summary>
        /// <param name="UnControl">GroupBox concerné pour être vidé</param>
        /// <param name="pVisible">GroupBox visible pour les rendre invisinbles</param>
        public static void ViderGroupBox(Control UnControl, Boolean pVisible)
        {
            foreach (Control c in UnControl.Controls)
            {
                if (c is TextBox)
                {
                    ((TextBox)c).Clear();
                }
                else
                {
                    if (c is MaskedTextBox)
                    {
                        ((MaskedTextBox)c).Clear();
                    }
                    else
                    {
                        if (c is RadioButton)
                        {
                            ((RadioButton)c).Checked = false;
                        }
                        else
                        {
                            if (c is CheckBox)
                            {
                                ((CheckBox)c).Checked = false;
                            }
                        }
                    }
                }
            }
            if (pVisible)
            {
                UnControl.Visible = false;
            }
        }

        /// <summary>
        /// Fonction permettant d'afficher les nuitées
        /// disponibles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="UnPanel">Panel concerné pour afficher les nuitées</param>
        /// <param name="UneConnexion">Connexion à la base de données</param>
        /// <param name="NomButton">Nom du bouton</param>
        public static void AfficheNuitee(object sender, EventArgs e, Panel UnPanel, Bdd UneConnexion, String NomButton)
        {
            if (((RadioButton)sender).Name == NomButton)
            {
                UnPanel.Visible = true;
                if (UnPanel.Controls.Count == 0) // on charge les nuites possibles possibles et on les affiche
                {
                    //DataTable LesDateNuites = UneConnexion.ObtenirDonnesOracle("VDATENUITE01");
                    //foreach(Dat
                    Dictionary<Int16, String> LesNuites = UneConnexion.ObtenirDatesNuites();
                    int i = 0;
                    foreach (KeyValuePair<Int16, String> UneNuite in LesNuites)
                    {
                        ComposantNuite.ResaNuite unResaNuit = new ResaNuite(UneConnexion.ObtenirDonnesOracle("VHOTEL01"), (UneConnexion.ObtenirDonnesOracle("VCATEGORIECHAMBRE01")), UneNuite.Value, UneNuite.Key);
                        unResaNuit.Left = 5;
                        unResaNuit.Top = 5 + (24 * i++);
                        unResaNuit.Visible = true;
                        //unResaNuit.click += new System.EventHandler(ComposantNuite_StateChanged);
                        UnPanel.Controls.Add(unResaNuit);
                    }

                }

            }
            else
            {
                UnPanel.Visible = false;

            }
 
        }

        internal static void ViderGroupBox(GroupBox GrpTypeParticipant, int p)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///  Fonction permettant d'envoyer un mail 
        ///  de confirmation lors d'une incription.
        /// </summary>
        /// <param name="pDestinataire">Adresse mail du destinataire</param>
        /// <param name="pNom">Nom de l'inscrit </param>
        /// <param name="pPrenom">Prénom de l'inscrit</param>
        public static void EnvoyerMail(String pDestinataire, String pNom, String pPrenom)
        {
            try
            {
                if (Regex.IsMatch(pDestinataire, @"^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"))
                {
                    MailMessage mail = new MailMessage();
                    using (SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["SmtpServeur"]))
                    {
                        mail.From = new MailAddress(ConfigurationManager.AppSettings["SmtpFrom"], ConfigurationManager.AppSettings["TitreApplication"]);
                        mail.To.Add(pDestinataire);
                        mail.Subject = ConfigurationManager.AppSettings["SmtpSubject"];
                        mail.Body = "      Votre inscription au nom de " + pNom + " " + pPrenom + " a bien été prise en compte ce jour (" + DateTime.Now + ").\n\nCordialement, l'équipe de maison des ligues.";
                        smtp.Port = 587;
                        smtp.EnableSsl = true;
                        smtp.UseDefaultCredentials = false;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SmtpFrom"], ConfigurationManager.AppSettings["SmtpPwd"]);

                        smtp.Send(mail);

                        MessageBox.Show("Un mail de confirmation vous a bien été envoyé.");
                    }
                }
                else
                {
                    throw new Exception("L'adresse entrée est incorrecte, le mail de confirmation n'a pas pu être envoyé.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        ///  Fonction permettant d'envoyer un mail 
        ///  d'annulation d'incription.
        /// </summary>
        /// <param name="pDestinataire">Adresse mail du destinataire</param>
        /// <param name="pNom">Nom de l'inscrit</param>
        /// <param name="pPrenom">Prénom de l'inscrit</param>
        /// <param name="pMontantCorrect">Montant correct atendu du chèque</param>
        public static void EnvoyerMail(String pDestinataire, String pNom, String pPrenom, Double pMontantCorrect)
        {
            try
            {
                if (Regex.IsMatch(pDestinataire, @"^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"))
                {
                    MailMessage mail = new MailMessage();
                    using (SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["SmtpServeur"]))
                    {
                        mail.From = new MailAddress(ConfigurationManager.AppSettings["SmtpFrom"], ConfigurationManager.AppSettings["TitreApplication"]);
                        mail.To.Add(pDestinataire);
                        mail.Subject = ConfigurationManager.AppSettings["SmtpSubject"];
                        mail.Body = "      Votre inscription au nom de " + pNom + " " + pPrenom + " n'a pas pu être prise en compte ce jour (" + DateTime.Now + ")." +
                        "Elle pourra être enregistrée qu'à la réception du chèque comportant le montant correct étant de " + pMontantCorrect + ".\n\nCordialement, l'équipe de maison des ligues.";
                        smtp.Port = 587;
                        smtp.EnableSsl = true;
                        smtp.UseDefaultCredentials = false;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SmtpFrom"], ConfigurationManager.AppSettings["SmtpPwd"]);

                        smtp.Send(mail);

                        MessageBox.Show("Un mail vous a été envoyé car une erreur avec le montant des chèques a été détecté.");
                    }
                }
                else
                {
                    throw new Exception("L'adresse entrée est incorrecte, le mail de confirmation n'a pas pu être envoyé.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
}
