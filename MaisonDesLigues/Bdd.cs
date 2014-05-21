using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.DataAccess.Client;
using System.Configuration;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;  // bibliothèque pour les expressions régulières
using MaisonDesLigues;



namespace BaseDeDonnees
{
    class Bdd
    {
        //
        // propriétés membres
        //
        private OracleConnection CnOracle;
        private OracleCommand UneOracleCommand;
        private OracleDataAdapter UnOracleDataAdapter;
        private DataTable UneDataTable;
        private OracleTransaction UneOracleTransaction;
        //
        // méthodes
        //
        /// <summary>
        /// constructeur de la connexion
        /// </summary>
        /// <param name="UnLogin">login utilisateur</param>
        /// <param name="UnPwd">mot de passe utilisateur</param>
        public Bdd(String UnLogin, String UnPwd)
        {
            try
            {
                /// <remarks>on commence par récupérer dans CnString les informations contenues dans le fichier app.config
                /// pour la connectionString de nom StrConnMdl
                /// </remarks>
                ConnectionStringSettings CnString = ConfigurationManager.ConnectionStrings["StrConnMdl"];
                ///<remarks>
                /// on va remplacer dans la chaine de connexion les paramètres par le login et le pwd saisis
                ///dans les zones de texte. Pour ça on va utiliser la méthode Format de la classe String.                /// 
                /// </remarks>
                CnOracle = new OracleConnection(string.Format(CnString.ConnectionString, UnLogin, UnPwd));
                CnOracle.Open();
            }
            catch (OracleException Oex)
            {
                throw new Exception("Erreur à la connexion" + Oex.Message);
            }
        }
        /// <summary>
        /// Méthode permettant de fermer la connexion
        /// </summary>
        public void FermerConnexion()
        {
            this.CnOracle.Close();
        }
        /// <summary>
        /// méthode permettant de renvoyer un message d'erreur provenant de la bd
        /// après l'avoir formatté. On ne renvoie que le message, sans code erreur
        /// </summary>
        /// <param name="unMessage">message à formater</param>
        /// <returns>message formaté à afficher dans l'application</returns>
        private String GetMessageOracle(String unMessage)
        {
            String[] message = Regex.Split(unMessage, "ORA-");
            return (Regex.Split(message[1], ":"))[1];
        }
        /// <summary>
        /// permet de récupérer le contenu d'une table ou d'une vue. 
        /// </summary>
        /// <param name="UneTableOuVue"> nom de la table ou la vue dont on veut récupérer le contenu</param>
        /// <returns>un objet de type datatable contenant les données récupérées</returns>
        public DataTable ObtenirDonnesOracle(String UneTableOuVue)
        {
            string Sql = "select * from " + UneTableOuVue;
            this.UneOracleCommand = new OracleCommand(Sql, CnOracle);
            UnOracleDataAdapter = new OracleDataAdapter();
            UnOracleDataAdapter.SelectCommand = this.UneOracleCommand;
            UneDataTable = new DataTable();
            UnOracleDataAdapter.Fill(UneDataTable);
            return UneDataTable;
        }
        /// <summary>
        /// méthode privée permettant de valoriser les paramètres d'un objet commmand communs aux licenciés, bénévoles et intervenants
        /// </summary>
        /// <param name="Cmd">nom de l'objet command concerné par les paramètres</param>
        /// <param name="pNom">nom du participant</param>
        /// <param name="pPrenom">prénom du participant</param>
        /// <param name="pAdresse1">adresse1 du participant</param>
        /// <param name="pAdresse2">adresse2 du participant</param>
        /// <param name="pCp">cp du participant</param>
        /// <param name="pVille">ville du participant</param>
        /// <param name="pTel">téléphone du participant</param>
        /// <param name="pMail">mail du participant</param>
        private void ParamCommunsNouveauxParticipants(OracleCommand Cmd, String pNom, String pPrenom, String pAdresse1, String pAdresse2, String pCp, String pVille, String pTel, String pMail)
        {
            Cmd.Parameters.Add("pNom", OracleDbType.Varchar2, ParameterDirection.Input).Value = pNom;
            Cmd.Parameters.Add("pPrenom", OracleDbType.Varchar2, ParameterDirection.Input).Value = pPrenom;
            Cmd.Parameters.Add("pAdr1", OracleDbType.Varchar2, ParameterDirection.Input).Value = pAdresse1;
            Cmd.Parameters.Add("pAdr2", OracleDbType.Varchar2, ParameterDirection.Input).Value = pAdresse2;
            Cmd.Parameters.Add("pCp", OracleDbType.Varchar2, ParameterDirection.Input).Value = pCp;
            Cmd.Parameters.Add("pVille", OracleDbType.Varchar2, ParameterDirection.Input).Value = pVille;
            Cmd.Parameters.Add("pTel", OracleDbType.Varchar2, ParameterDirection.Input).Value = pTel;
            Cmd.Parameters.Add("pMail", OracleDbType.Varchar2, ParameterDirection.Input).Value = pMail;
        }
        /// <summary>
        /// procédure qui va se charger d'invoquer la procédure stockée qui ira inscrire un participant de type bénévole
        /// </summary>
        /// <param name="Cmd">nom de l'objet command concerné par les paramètres</param>
        /// <param name="pNom">nom du participant</param>
        /// <param name="pPrenom">prénom du participant</param>
        /// <param name="pAdresse1">adresse1 du participant</param>
        /// <param name="pAdresse2">adresse2 du participant</param>
        /// <param name="pCp">cp du participant</param>
        /// <param name="pVille">ville du participant</param>
        /// <param name="pTel">téléphone du participant</param>
        /// <param name="pMail">mail du participant</param>
        /// <param name="pDateNaissance">mail du bénévole</param>
        /// <param name="pNumeroLicence">numéro de licence du bénévole ou null</param>
        /// <param name="pDateBenevolat">collection des id des dates où le bénévole sera présent</param>
        public void InscrireBenevole(String pNom, String pPrenom, String pAdresse1, String pAdresse2, String pCp, String pVille, String pTel, String pMail, DateTime pDateNaissance, Int64? pNumeroLicence, Collection<Int16> pDateBenevolat)
        {
            try
            {
                UneOracleCommand = new OracleCommand("pckparticipant.nouveaubenevole", CnOracle);
                UneOracleCommand.CommandType = CommandType.StoredProcedure;
                this.ParamCommunsNouveauxParticipants(UneOracleCommand, pNom, pPrenom, pAdresse1, pAdresse2, pCp, pVille, pTel, pMail);
                UneOracleCommand.Parameters.Add("pDateNaiss", OracleDbType.Date, ParameterDirection.Input).Value = pDateNaissance;
                UneOracleCommand.Parameters.Add("pLicence", OracleDbType.Int64, ParameterDirection.Input).Value = pNumeroLicence;
                //UneOracleCommand.Parameters.Add("pLesDates", OracleDbType.Array, ParameterDirection.Input).Value = pDateBenevolat;
                OracleParameter pLesDates = new OracleParameter();
                pLesDates.ParameterName = "pLesDates";
                pLesDates.OracleDbType = OracleDbType.Int16;
                pLesDates.CollectionType = OracleCollectionType.PLSQLAssociativeArray;

                pLesDates.Value = pDateBenevolat.ToArray();
                pLesDates.Size = pDateBenevolat.Count;
                UneOracleCommand.Parameters.Add(pLesDates);
                UneOracleCommand.ExecuteNonQuery();
                MessageBox.Show("Inscription bénévole effectuée");
                
            }
            catch (OracleException Oex)
            {
                MessageBox.Show("Erreur Oracle \n" + Oex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Autre Erreur  \n" + ex.Message);
            }

        }
        /// <summary>
        /// méthode privée permettant de valoriser les paramètres d'un objet commmand spécifiques intervenants
        /// </summary>
        /// <param name="Cmd"> nom de l'objet command concerné par les paramètres</param>
        /// <param name="pIdAtelier"> Id de l'atelier où interviendra l'intervenant</param>
        /// <param name="pIdStatut">statut de l'intervenant pour l'atelier : animateur ou intervenant</param>
        private void ParamsSpecifiquesIntervenant(OracleCommand Cmd,Int16 pIdAtelier, String pIdStatut)
        {
            Cmd.Parameters.Add("pIdAtelier", OracleDbType.Int16, ParameterDirection.Input).Value = pIdAtelier;
            Cmd.Parameters.Add("pIdStatut", OracleDbType.Char, ParameterDirection.Input).Value = pIdStatut;
        }
        /// <summary>
        /// Procédure publique qui va appeler la procédure stockée permettant d'inscrire un nouvel intervenant sans nuité
        /// </summary>
        /// <param name="Cmd">nom de l'objet command concerné par les paramètres</param>
        /// <param name="pNom">nom du participant</param>
        /// <param name="pPrenom">prénom du participant</param>
        /// <param name="pAdresse1">adresse1 du participant</param>
        /// <param name="pAdresse2">adresse2 du participant</param>
        /// <param name="pCp">cp du participant</param>
        /// <param name="pVille">ville du participant</param>
        /// <param name="pTel">téléphone du participant</param>
        /// <param name="pMail">mail du participant</param>
        /// <param name="pIdAtelier"> Id de l'atelier où interviendra l'intervenant</param>
        /// <param name="pIdStatut">statut de l'intervenant pour l'atelier : animateur ou intervenant</param>
        public void InscrireIntervenant(String pNom, String pPrenom, String pAdresse1, String pAdresse2, String pCp, String pVille, String pTel, String pMail, Int16 pIdAtelier, String pIdStatut)
        {
            /// <remarks>
            /// procédure qui va créer :
            /// 1- un enregistrement dans la table participant
            /// 2- un enregistrement dans la table intervenant 
            ///  en cas d'erreur Oracle, appel à la méthode GetMessageOracle dont le rôle est d'extraire uniquement le message renvoyé
            /// par une procédure ou un trigger Oracle
            /// </remarks>
            /// 
            String MessageErreur = "";
            try
            {
                UneOracleCommand = new OracleCommand("pckparticipant.nouvelintervenant", CnOracle);
                UneOracleCommand.CommandType = CommandType.StoredProcedure;
                // début de la transaction Oracle il vaut mieyx gérer les transactions dans l'applicatif que dans la bd dans les procédures stockées.
                UneOracleTransaction = this.CnOracle.BeginTransaction();
                // on appelle la procédure ParamCommunsNouveauxParticipants pour charger les paramètres communs aux intervenants
                this.ParamCommunsNouveauxParticipants(UneOracleCommand, pNom, pPrenom, pAdresse1, pAdresse2, pCp, pVille, pTel, pMail);
                // on appelle la procédure ParamsCommunsIntervenant pour charger les paramètres communs aux intervenants
                this.ParamsSpecifiquesIntervenant(UneOracleCommand, pIdAtelier, pIdStatut);        
                //execution
                UneOracleCommand.ExecuteNonQuery();
                // fin de la transaction. Si on arrive à ce point, c'est qu'aucune exception n'a été levée
                UneOracleTransaction.Commit();
            }
            catch (OracleException Oex)
            {
                MessageErreur = "Erreur Oracle \n" + this.GetMessageOracle(Oex.Message);
             }
            catch (Exception ex)
            {

                MessageErreur = "Autre Erreur, les informations n'ont pas été correctement saisies";
            }
            finally
            {
                if (MessageErreur.Length > 0)
                {
                    // annulation de la transaction
                    UneOracleTransaction.Rollback();
                    // Déclenchement de l'exception
                    throw new Exception(MessageErreur);
                }
            }
        }
        /// <summary>
        /// Procédure publique qui va appeler la procédure stockée permettant d'inscrire un nouvel intervenant qui aura des nuités
        /// </summary>
        /// <param name="Cmd">nom de l'objet command concerné par les paramètres</param>
        /// <param name="pNom">nom du participant</param>
        /// <param name="pPrenom">prénom du participant</param>
        /// <param name="pAdresse1">adresse1 du participant</param>
        /// <param name="pAdresse2">adresse2 du participant</param>
        /// <param name="pCp">cp du participant</param>
        /// <param name="pVille">ville du participant</param>
        /// <param name="pTel">téléphone du participant</param>
        /// <param name="pMail">mail du participant</param>
        /// <param name="pIdAtelier"> Id de l'atelier où interviendra l'intervenant</param>
        /// <param name="pIdStatut">statut de l'intervenant pour l'atelier : animateur ou intervenant</param>
        /// <param name="pLesCategories">tableau contenant la catégorie de chambre pour chaque nuité à réserver</param>
        /// <param name="pLesHotels">tableau contenant l'hôtel pour chaque nuité à réserver</param>
        /// <param name="pLesNuits">tableau contenant l'id de la date d'arrivée pour chaque nuité à réserver</param>
        public void InscrireIntervenant(String pNom, String pPrenom, String pAdresse1, String pAdresse2, String pCp, String pVille, String pTel, String pMail, Int16 pIdAtelier, String pIdStatut, Collection<string> pLesCategories, Collection<string> pLesHotels, Collection<Int16> pLesNuits)
        {
            /// <remarks>
            /// procédure qui va  :
            /// 1- faire appel à la procédure 
            /// un enregistrement dans la table participant
            /// 2- un enregistrement dans la table intervenant 
            /// 3- un à 2 enregistrements dans la table CONTENUHEBERGEMENT
            /// 
            /// en cas d'erreur Oracle, appel à la méthode GetMessageOracle dont le rôle est d'extraire uniquement le message renvoyé
            /// par une procédure ou un trigger Oracle
            /// </remarks>
            /// 
            String MessageErreur="";
            try
            {                
                // pckparticipant.nouvelintervenant est une procédure surchargée
                UneOracleCommand = new OracleCommand("pckparticipant.nouvelintervenant", CnOracle);
                UneOracleCommand.CommandType = CommandType.StoredProcedure;
                // début de la transaction Oracle : il vaut mieyx gérer les transactions dans l'applicatif que dans la bd.
                UneOracleTransaction = this.CnOracle.BeginTransaction();
                this.ParamCommunsNouveauxParticipants(UneOracleCommand, pNom, pPrenom, pAdresse1, pAdresse2, pCp, pVille, pTel, pMail);
                this.ParamsSpecifiquesIntervenant(UneOracleCommand, pIdAtelier, pIdStatut);

                //On va créer ici les paramètres spécifiques à l'inscription d'un intervenant qui réserve des nuits d'hôtel.
                // Paramètre qui stocke les catégories sélectionnées
                OracleParameter pOraLescategories = new OracleParameter();
                pOraLescategories.ParameterName = "pLesCategories";
                pOraLescategories.OracleDbType = OracleDbType.Char;
                pOraLescategories.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
                pOraLescategories.Value = pLesCategories.ToArray();
                pOraLescategories.Size = pLesCategories.Count;
                UneOracleCommand.Parameters.Add(pOraLescategories);
               
                // Paramètre qui stocke les hotels sélectionnées
                OracleParameter pOraLesHotels = new OracleParameter();
                pOraLesHotels.ParameterName = "pLesHotels";
                pOraLesHotels.OracleDbType = OracleDbType.Char;
                pOraLesHotels.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
                pOraLesHotels.Value = pLesHotels.ToArray();
                pOraLesHotels.Size = pLesHotels.Count;
                UneOracleCommand.Parameters.Add(pOraLesHotels);
                
                // Paramètres qui stocke les nuits sélectionnées
                OracleParameter pOraLesNuits = new OracleParameter();
                pOraLesNuits.ParameterName = "pLesNuits";
                pOraLesNuits.OracleDbType = OracleDbType.Int16;
                pOraLesNuits.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
                pOraLesNuits.Value = pLesNuits.ToArray();
                pOraLesNuits.Size = pLesNuits.Count;
                UneOracleCommand.Parameters.Add(pOraLesNuits);
                //execution
                UneOracleCommand.ExecuteNonQuery();
                // fin de la transaction. Si on arrive à ce point, c'est qu'aucune exception n'a été levée
                UneOracleTransaction.Commit();
               
            }
            catch (OracleException Oex)
            {
                //MessageErreur="Erreur Oracle \n" + this.GetMessageOracle(Oex.Message);
                MessageBox.Show(Oex.Message);
            }
            catch (Exception ex)
            {
                
                MessageErreur= "Autre Erreur, les informations n'ont pas été correctement saisies";
            }
            finally
            {
                if (MessageErreur.Length > 0)
                {
                    // annulation de la transaction
                    UneOracleTransaction.Rollback();
                    // Déclenchement de l'exception
                    throw new Exception(MessageErreur);
                }             
            }
        }
        /// <summary>
        /// fonction permettant de construire un dictionnaire dont l'id est l'id d'une nuité et le contenu une date
        /// sous la la forme : lundi 7 janvier 2013        /// 
        /// </summary>
        /// <returns>un dictionnaire dont l'id est l'id d'une nuité et le contenu une date</returns>
        public Dictionary<Int16, String> ObtenirDatesNuites()
        {
            Dictionary<Int16, String> LesDatesARetourner = new Dictionary<Int16, String>();
            DataTable LesDatesNuites = this.ObtenirDonnesOracle("VDATENUITE01");
            foreach (DataRow UneLigne in LesDatesNuites.Rows)
            {
                LesDatesARetourner.Add(System.Convert.ToInt16(UneLigne["id"]), UneLigne["libelle"].ToString());
            }
            return LesDatesARetourner;

        }

        public Dictionary<Int16, String> ObtenirAtelier()
        {
            Dictionary<Int16, String> LesAteliersARetourner = new Dictionary<Int16, String>();
            DataTable LesAteliers = this.ObtenirDonnesOracle("VDATENUITE01");
            foreach (DataRow UneLigne in LesAteliers.Rows)
            {
                LesAteliersARetourner.Add(System.Convert.ToInt16(UneLigne["id"]), UneLigne["libelleatelier"].ToString());
            }
            return LesAteliersARetourner;

        }

        /// <summary>
        /// Procédure publique qui va appeler la procédure stockée permettant d'inscrire un nouvel licencié avec nuité
        /// </summary>
        /// <param name="Cmd">nom de l'objet command concerné par les paramètres</param>
        /// <param name="pNom">nom du participant</param>
        /// <param name="pPrenom">prénom du participant</param>
        /// <param name="pAdresse1">adresse1 du participant</param>
        /// <param name="pAdresse2">adresse2 du participant</param>
        /// <param name="pCp">cp du participant</param>
        /// <param name="pVille">ville du participant</param>
        /// <param name="pTel">téléphone du participant</param>
        /// <param name="pMail">mail du participant</param>
        /// <param name="pIdQualite">qualité du licencié</param>
        /// <param name="pNumeroLicence">numéro de licence du licencié</param>
        /// <param name="pLesAteliers">collection des ateliers du licencié</param>
        /// <param name="pNumCheque">numéro de cheque du licencié</param>
        /// <param name="pMontantCheque">montant du cheque du licencié</param>
        /// <param name="pLesAccompagnants">collection des accompagnants du licencié</param>
        /// <param name="pInscription">type du paiement du licencié</param>
        public void InscrireLicencie(String pNom, String pPrenom, String pAdresse1, String pAdresse2, String pCp, String pVille, String pTel, String pMail, Int16 pIdQualite, Int64 pNumeroLicence, Collection<Int16> pLesAteliers, Int64 pNumCheque, Double pMontantCheque, Collection<Int16> pLesAccompagnants, String pInscription)
        {
            String MessageErreur = "";
            try
            {
                // pckparticipant.nouvelintervenant est une procédure surchargée
                UneOracleCommand = new OracleCommand("pckparticipant.nouveaulicencie", CnOracle);
                UneOracleCommand.CommandType = CommandType.StoredProcedure;
                // début de la transaction Oracle : il vaut mieyx gérer les transactions dans l'applicatif que dans la bd.
                UneOracleTransaction = this.CnOracle.BeginTransaction();
                this.ParamCommunsNouveauxParticipants(UneOracleCommand, pNom, pPrenom, pAdresse1, pAdresse2, pCp, pVille, pTel, pMail);
                this.ParamsSpecifiquesLicencie(UneOracleCommand, pIdQualite, pNumeroLicence, pNumCheque, pMontantCheque, pInscription);
                // si aucun atelier, on rempli la collection d'un atelier à 0 afin de ne rien faire dans la base
                if (pLesAteliers.Count == 0)
                {
                    pLesAteliers.Add(0);
                }
                // Paramètre qui stocke les ateliers sélectionnées
                OracleParameter pOraLesAteliers = new OracleParameter();
                pOraLesAteliers.ParameterName = "pLesAteliers";
                pOraLesAteliers.OracleDbType = OracleDbType.Int16;
                pOraLesAteliers.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
                pOraLesAteliers.Value = pLesAteliers.ToArray();
                pOraLesAteliers.Size = pLesAteliers.Count;
                UneOracleCommand.Parameters.Add(pOraLesAteliers);
                // si aucun accompagnant, on rempli la collection d'un accompagnant à 0 afin de ne rien faire dans la base
                if (pLesAccompagnants.Count == 0)
                {
                    pLesAccompagnants.Add(0);
                }
                // Paramètres qui stocke les accompagnants sélectionnées
                OracleParameter pOraLesAccompagnants = new OracleParameter();
                pOraLesAccompagnants.ParameterName = "plesaccompagnants";
                pOraLesAccompagnants.OracleDbType = OracleDbType.Int16;
                pOraLesAccompagnants.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
                pOraLesAccompagnants.Value = pLesAccompagnants.ToArray();
                pOraLesAccompagnants.Size = pLesAccompagnants.Count;
                UneOracleCommand.Parameters.Add(pOraLesAccompagnants);
                //execution
                UneOracleCommand.ExecuteNonQuery();
                // fin de la transaction. Si on arrive à ce point, c'est qu'aucune exception n'a été levée
                UneOracleTransaction.Commit();
            }
            catch (OracleException Oex)
            {
                MessageErreur = "Erreur Oracle \n" + this.GetMessageOracle(Oex.Message);
            }
            catch (Exception ex)
            {

                MessageErreur = "Autre Erreur, les informations n'ont pas été correctement saisies";
            }
            finally
            {
                if (MessageErreur.Length > 0)
                {
                    // annulation de la transaction
                    UneOracleTransaction.Rollback();
                    // Déclenchement de l'exception
                    throw new Exception(MessageErreur);
                }
            }
        }
        /// <summary>
        /// Procédure publique qui va appeler la procédure stockée permettant d'inscrire un nouvel intervenant sans nuité
        /// </summary>
        /// <param name="Cmd">nom de l'objet command concerné par les paramètres</param>
        /// <param name="pNom">nom du participant</param>
        /// <param name="pPrenom">prénom du participant</param>
        /// <param name="pAdresse1">adresse1 du participant</param>
        /// <param name="pAdresse2">adresse2 du participant</param>
        /// <param name="pCp">cp du participant</param>
        /// <param name="pVille">ville du participant</param>
        /// <param name="pTel">téléphone du participant</param>
        /// <param name="pMail">mail du participant</param>
        /// <param name="pIdQualite">qualité du licencié</param>
        /// <param name="pNumeroLicence">numéro de licence du licencié</param>
        /// <param name="pLesAteliers">collection des ateliers du licencié</param>
        /// <param name="pLesCategories">collection des catégories de l'hotel du licencié</param>
        /// <param name="pLesHotels">collection des hotels du licencié</param>
        /// <param name="pLesNuits">collection des nuits du licencié</param>
        /// <param name="pNumCheque">numéro du cheque du licencié</param>
        /// <param name="pMontantCheque">montant du cheque du licencié</param>
        /// <param name="pLesAccompagnants">collection des accompagnants du licencié</param>
        /// <param name="pInscription">type du paiement du licencié</param>
        public void InscrireLicencie(String pNom, String pPrenom, String pAdresse1, String pAdresse2, String pCp, String pVille, String pTel, String pMail, Int16 pIdQualite, Int64 pNumeroLicence, Collection<Int16> pLesAteliers, Collection<string> pLesCategories, Collection<string> pLesHotels, Collection<Int16> pLesNuits, Int64 pNumCheque, Double pMontantCheque, Collection<Int16> pLesAccompagnants, String pInscription)
        {
            String MessageErreur = "";
            try
            {
                // pckparticipant.nouvelintervenant est une procédure surchargée
                UneOracleCommand = new OracleCommand("pckparticipant.nouveaulicencie", CnOracle);
                UneOracleCommand.CommandType = CommandType.StoredProcedure;
                // début de la transaction Oracle : il vaut mieyx gérer les transactions dans l'applicatif que dans la bd.
                UneOracleTransaction = this.CnOracle.BeginTransaction();
                this.ParamCommunsNouveauxParticipants(UneOracleCommand, pNom, pPrenom, pAdresse1, pAdresse2, pCp, pVille, pTel, pMail);
                this.ParamsSpecifiquesLicencie(UneOracleCommand, pIdQualite, pNumeroLicence, pNumCheque, pMontantCheque, pInscription);
                if (pLesAteliers.Count == 0)
                {
                    pLesAteliers.Add(0);
                }
                // Paramètre qui stocke les ateliers sélectionnées
                OracleParameter pOraLesAteliers = new OracleParameter();
                pOraLesAteliers.ParameterName = "pLesAteliers";
                pOraLesAteliers.OracleDbType = OracleDbType.Int16;
                pOraLesAteliers.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
                pOraLesAteliers.Value = pLesAteliers.ToArray();
                pOraLesAteliers.Size = pLesAteliers.Count;
                UneOracleCommand.Parameters.Add(pOraLesAteliers);
                //On va créer ici les paramètres spécifiques à l'inscription d'un intervenant qui réserve des nuits d'hôtel.
                // Paramètre qui stocke les catégories sélectionnées
                OracleParameter pOraLescategories = new OracleParameter();
                pOraLescategories.ParameterName = "pLesCategories";
                pOraLescategories.OracleDbType = OracleDbType.Char;
                pOraLescategories.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
                pOraLescategories.Value = pLesCategories.ToArray();
                pOraLescategories.Size = pLesCategories.Count;
                UneOracleCommand.Parameters.Add(pOraLescategories);
                // Paramètre qui stocke les hotels sélectionnées
                OracleParameter pOraLesHotels = new OracleParameter();
                pOraLesHotels.ParameterName = "pLesHotels";
                pOraLesHotels.OracleDbType = OracleDbType.Char;
                pOraLesHotels.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
                pOraLesHotels.Value = pLesHotels.ToArray();
                pOraLesHotels.Size = pLesHotels.Count;
                UneOracleCommand.Parameters.Add(pOraLesHotels);
                // Paramètres qui stocke les nuits sélectionnées
                OracleParameter pOraLesNuits = new OracleParameter();
                pOraLesNuits.ParameterName = "pLesNuits";
                pOraLesNuits.OracleDbType = OracleDbType.Int16;
                pOraLesNuits.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
                pOraLesNuits.Value = pLesNuits.ToArray();
                pOraLesNuits.Size = pLesNuits.Count;
                UneOracleCommand.Parameters.Add(pOraLesNuits);
                if (pLesAccompagnants.Count == 0)
                {
                    pLesAccompagnants.Add(0);
                }
                // Paramètres qui stocke les accompagnants sélectionnées
                OracleParameter pOraLesAccompagnants = new OracleParameter();
                pOraLesAccompagnants.ParameterName = "plesaccompagnants";
                pOraLesAccompagnants.OracleDbType = OracleDbType.Int16;
                pOraLesAccompagnants.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
                pOraLesAccompagnants.Value = pLesAccompagnants.ToArray();
                pOraLesAccompagnants.Size = pLesAccompagnants.Count;
                UneOracleCommand.Parameters.Add(pOraLesAccompagnants);
                //execution
                UneOracleCommand.ExecuteNonQuery();
                // fin de la transaction. Si on arrive à ce point, c'est qu'aucune exception n'a été levée
                UneOracleTransaction.Commit();
            }
            catch (OracleException Oex)
            {
                //MessageErreur="Erreur Oracle \n" + this.GetMessageOracle(Oex.Message);
                MessageBox.Show(Oex.Message);
            }
            catch (Exception ex)
            {

                MessageErreur = "Autre Erreur, les informations n'ont pas été correctement saisies";
            }
            finally
            {
                if (MessageErreur.Length > 0)
                {
                    // annulation de la transaction
                    UneOracleTransaction.Rollback();
                    // Déclenchement de l'exception
                    throw new Exception(MessageErreur);
                }
            }
        }

        /// <summary>
        /// méthode privée permettant de valoriser les paramètres d'un objet commmand spécifiques licenciés
        /// </summary>
        /// <param name="Cmd"> nom de l'objet command concerné par les paramètres</param>
        /// <param name="pIdQualite">qualité du licencié</param>
        /// <param name="pNumeroLicence">numéro de licence du licencié</param>
        /// <param name="pNumCheque">numéro du cheque du licencié</param>
        /// <param name="pMontantCheque">montant du cheque du licencié</param>
        /// <param name="pTypePaiement">type de paiement du licencié</param>
        private void ParamsSpecifiquesLicencie(OracleCommand Cmd, Int16 pIdQualite, Int64 pNumeroLicence, Int64 pNumCheque, Double pMontantCheque, String pTypePaiement)
        {
            Cmd.Parameters.Add("pNumeroLicence", OracleDbType.Int64, ParameterDirection.Input).Value = pNumeroLicence;
            Cmd.Parameters.Add("pIdQualite", OracleDbType.Int16, ParameterDirection.Input).Value = pIdQualite;
            Cmd.Parameters.Add("pNumCheque", OracleDbType.Int64, ParameterDirection.Input).Value = pNumCheque;
            Cmd.Parameters.Add("pMontantCheque", OracleDbType.Double, ParameterDirection.Input).Value = pMontantCheque;
            Cmd.Parameters.Add("pTypePaiement", OracleDbType.Char, ParameterDirection.Input).Value = pTypePaiement;
        }

        /// <summary>
        /// méthode privée permettant de valoriser les paramètres d'un objet command spécifiques licenciés
        /// </summary>
        /// <param name="Cmd">nom de l'objet command concerné par les paramètres</param>
        /// <param name="pMontantCheque">Numero du cheque du licencie</param>
        /// <param name="pNumeroCheque">Numero du cheque du licencie</param>
        /// <param name="pTypePaiement">Type de paiement du licencie</param>
        /// <param name="pIdLicencie">id du licencie</param>
        private void ParamsSpecifiquePaiement(OracleCommand Cmd, Double pMontantCheque, Int64 pNumeroCheque, String pTypePaiement, Int64 pIdLicencie)
        {
            Cmd.Parameters.Add("pLicencie", OracleDbType.Int64, ParameterDirection.Input).Value = pIdLicencie;
            Cmd.Parameters.Add("pNumCheque", OracleDbType.Int64, ParameterDirection.Input).Value = pNumeroCheque;
            Cmd.Parameters.Add("pMontantCheque", OracleDbType.Double, ParameterDirection.Input).Value = pMontantCheque;
            Cmd.Parameters.Add("pTypePaiement", OracleDbType.Char, ParameterDirection.Input).Value = pTypePaiement;
        }

        /// <summary>
        /// Fonction qui permet d'enregistrer un paiement
        /// </summary>
        /// <param name="pMontantCheque">Numero du cheque du licencie</param>
        /// <param name="pNumeroCheque">Numero du cheque du licencie</param>
        /// <param name="pNumeroLicencie">Numero de licence du licencie</param>
        /// <param name="pTypePaiement">Type de paiement du licencie</param>
        public void EnregistrerPaiement(Double pMontantCheque, Int64 pNumeroCheque, Int64 pNumeroLicencie, String pTypePaiement)
        {
            Int64 MonIdLicencie = 0;
            UneOracleCommand = new OracleCommand("Select Idlicencie from licencie where numerolicence =" + pNumeroLicencie, CnOracle);
            OracleDataReader rdr = UneOracleCommand.ExecuteReader();
            while (rdr.Read())
            {
                MonIdLicencie = System.Convert.ToInt64(rdr[0]);
            }
            UneOracleCommand = new OracleCommand("pckparticipant.ENREGISTREPAIEMENT", CnOracle);
            UneOracleCommand.CommandType = CommandType.StoredProcedure;
            // début de la transaction Oracle : il vaut mieyx gérer les transactions dans l'applicatif que dans la bd.
            UneOracleTransaction = this.CnOracle.BeginTransaction();
            this.ParamsSpecifiquePaiement(UneOracleCommand, pMontantCheque, pNumeroCheque, pTypePaiement, MonIdLicencie);
            UneOracleCommand.ExecuteNonQuery();
            // fin de la transaction. Si on arrive à ce point, c'est qu'aucune exception n'a été levée
            UneOracleTransaction.Commit();
        }


        /// <summary>
        /// Fonction permettant d'ajouter un nouvel atelier
        /// </summary>
        /// <param name="Libelle">Libelle de l'atelier</param>
        /// <param name="NbPlacesMaxi">Nombre de places que peut accueillir l'atelier</param>
        public void ajoutAtelier(string Libelle, int NbPlacesMaxi)
        {

            UneOracleCommand = new OracleCommand("insertionatelier", CnOracle);
            UneOracleCommand.CommandType = CommandType.StoredProcedure;
            this.ParamNouvelAtelier(UneOracleCommand, Libelle, NbPlacesMaxi);
            UneOracleCommand.ExecuteNonQuery();
            MessageBox.Show("ajout atelier effectuée");
        }

        /// <summary>
        /// Fonction permettant d'ajouter un theme
        /// </summary>
        /// <param name="idAtelier">Id de l'atelier concerné par le nouveau theme</param>
        /// <param name="numero">Numéro du thème</param>
        /// <param name="libelleTheme">Libelle du thème</param>
        public void ajoutTheme(int idAtelier, int numero, string libelleTheme)
        {
            UneOracleCommand = new OracleCommand("insertiontheme", CnOracle);
            UneOracleCommand.CommandType = CommandType.StoredProcedure;
            this.ParamNouveauTheme(UneOracleCommand, idAtelier, numero, libelleTheme);
            UneOracleCommand.ExecuteNonQuery();
            MessageBox.Show("ajout theme effectuée");
        }

        /// <summary>
        /// Fonction permettant d'ajouter une vacation
        /// </summary>
        /// <param name="idAtelier">Id de l'atelier concernée</param>
        /// <param name="numero">numero</param>
        /// <param name="dateHeureDebut">Date et heure de début de la vacation</param>
        /// <param name="dateHeurefin">Date et heure de fin de la vacation</param>
        public void ajoutVacation(int idAtelier, int numero, string dateHeureDebut, string dateHeurefin)
        {
            UneOracleCommand = new OracleCommand("insertionvacation", CnOracle);
            UneOracleCommand.CommandType = CommandType.StoredProcedure;
            this.ParamNouvelVacation(UneOracleCommand, idAtelier, numero, dateHeureDebut, dateHeurefin);
            UneOracleCommand.ExecuteNonQuery();
            MessageBox.Show("ajout vacation effectuée");
        }

        /// <summary>
        /// Fonction permettant de modifier les dates et heures d'une vacation
        /// </summary>
        /// <param name="idAtelier">id de l'atelier concernée</param>
        /// <param name="dateHeureDebut">date et heure de début</param>
        /// <param name="dateHeureFin">date et heure de fin</param>
        public void modifVacation(int idAtelier, int numero, string dateHeureDebut, string dateHeureFin)
        {
            UneOracleCommand = new OracleCommand("modificationvacation", CnOracle);
            UneOracleCommand.CommandType = CommandType.StoredProcedure;
            this.ParamModifVacation(UneOracleCommand, idAtelier, dateHeureDebut, dateHeureFin, numero);
            UneOracleCommand.ExecuteNonQuery();
            MessageBox.Show("Modification de la vacation effectuée");
        }

        /// <summary>
        /// Pramètres necessaire pour une modification d'une vacation, convertis les types c# en types oracles
        /// </summary>
        private void ParamModifVacation(OracleCommand Cmd, int pIdAtelier, string pHeureDebut, string pHeureFin, int pNumero)
        {
            Cmd.Parameters.Add("pNumero", OracleDbType.Int64, ParameterDirection.Input).Value = pNumero;
            Cmd.Parameters.Add("pIdAtelier", OracleDbType.Int64, ParameterDirection.Input).Value = pIdAtelier;
            Cmd.Parameters.Add("pHeureDebut", OracleDbType.Varchar2, ParameterDirection.Input).Value = pHeureDebut;
            Cmd.Parameters.Add("pHeureFin", OracleDbType.Varchar2, ParameterDirection.Input).Value = pHeureFin;

        }

        /// <summary>
        /// Pramètres necessaire pour L'ajout d'un atelier, convertis les types c# en types oracles
        /// </summary>
        private void ParamNouvelAtelier(OracleCommand Cmd, String pLibelle, int pNbPlaces)
        {
            Cmd.Parameters.Add("pLibelle", OracleDbType.Varchar2, ParameterDirection.Input).Value = pLibelle;
            Cmd.Parameters.Add("pPrenom", OracleDbType.Int64, ParameterDirection.Input).Value = pNbPlaces;
        }

        /// <summary>
        /// Pramètres necessaire pour l'ajout d'un nouveau thème, convertis les types c# en types oracles
        /// </summary>
        private void ParamNouveauTheme(OracleCommand Cmd, int pIdAtelier, int pNumero, string pLibelleTheme)
        {
            Cmd.Parameters.Add("pIdAtelier", OracleDbType.Int32, ParameterDirection.Input).Value = pIdAtelier;
            Cmd.Parameters.Add("pNumero", OracleDbType.Int32, ParameterDirection.Input).Value = pNumero;
            Cmd.Parameters.Add("pLibelleTheme", OracleDbType.Varchar2, ParameterDirection.Input).Value = pLibelleTheme;
        }

        /// <summary>
        /// Pramètres necessaire pour l'ajout d'une nouvelle vacation, convertis les types c# en types oracles
        /// </summary>
        private void ParamNouvelVacation(OracleCommand Cmd, int pIdAtelier, int pNumero, string pHeureDebut, string pHeureFin)
        {
            Cmd.Parameters.Add("pIdAtelier", OracleDbType.Int64, ParameterDirection.Input).Value = pIdAtelier;
            Cmd.Parameters.Add("pNumero", OracleDbType.Int64, ParameterDirection.Input).Value = pNumero;
            Cmd.Parameters.Add("pHeureDebut", OracleDbType.Varchar2, ParameterDirection.Input).Value = pHeureDebut;
            Cmd.Parameters.Add("pHeureFin", OracleDbType.Varchar2, ParameterDirection.Input).Value = pHeureFin;
        }

        /// <summary>
        /// Fonction qui retourne tout les ateliers qui sont en relations avec des vacations
        /// </summary>
        /// <returns>une datatable représentant les ateliers concernés</returns>
        public DataTable ObtenirIdAtelierVacation()
        {
            string Sql = "select * from atelier inner join vacation on atelier.id = vacation.idatelier";
            this.UneOracleCommand = new OracleCommand(Sql, CnOracle);
            UnOracleDataAdapter = new OracleDataAdapter();
            UnOracleDataAdapter.SelectCommand = this.UneOracleCommand;
            UneDataTable = new DataTable();
            UnOracleDataAdapter.Fill(UneDataTable);
            return UneDataTable;
        }
    }
}
