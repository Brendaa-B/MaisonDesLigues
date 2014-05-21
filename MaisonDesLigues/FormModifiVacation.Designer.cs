namespace MaisonDesLigues
{
    partial class FormModifiVacation
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.minFin = new System.Windows.Forms.NumericUpDown();
            this.heureFin = new System.Windows.Forms.NumericUpDown();
            this.minDeb = new System.Windows.Forms.NumericUpDown();
            this.heureDeb = new System.Windows.Forms.NumericUpDown();
            this.dateTimeJour = new System.Windows.Forms.DateTimePicker();
            this.comboAtelier = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnAnuler = new System.Windows.Forms.Button();
            this.BtnAjoutVacation = new System.Windows.Forms.Button();
            this.choixNumero = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.minFin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.heureFin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minDeb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.heureDeb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.choixNumero)).BeginInit();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(184, 152);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 27;
            this.label6.Text = "Heure de début";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(184, 178);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 26;
            this.label5.Text = "Heure de fin";
            // 
            // minFin
            // 
            this.minFin.Location = new System.Drawing.Point(394, 171);
            this.minFin.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.minFin.Name = "minFin";
            this.minFin.Size = new System.Drawing.Size(33, 20);
            this.minFin.TabIndex = 25;
            // 
            // heureFin
            // 
            this.heureFin.Location = new System.Drawing.Point(355, 171);
            this.heureFin.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.heureFin.Name = "heureFin";
            this.heureFin.Size = new System.Drawing.Size(33, 20);
            this.heureFin.TabIndex = 24;
            // 
            // minDeb
            // 
            this.minDeb.Location = new System.Drawing.Point(394, 145);
            this.minDeb.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.minDeb.Name = "minDeb";
            this.minDeb.Size = new System.Drawing.Size(33, 20);
            this.minDeb.TabIndex = 23;
            // 
            // heureDeb
            // 
            this.heureDeb.Location = new System.Drawing.Point(355, 145);
            this.heureDeb.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.heureDeb.Name = "heureDeb";
            this.heureDeb.Size = new System.Drawing.Size(33, 20);
            this.heureDeb.TabIndex = 22;
            // 
            // dateTimeJour
            // 
            this.dateTimeJour.Location = new System.Drawing.Point(355, 105);
            this.dateTimeJour.Name = "dateTimeJour";
            this.dateTimeJour.Size = new System.Drawing.Size(200, 20);
            this.dateTimeJour.TabIndex = 29;
            // 
            // comboAtelier
            // 
            this.comboAtelier.FormattingEnabled = true;
            this.comboAtelier.Location = new System.Drawing.Point(355, 47);
            this.comboAtelier.Name = "comboAtelier";
            this.comboAtelier.Size = new System.Drawing.Size(121, 21);
            this.comboAtelier.TabIndex = 30;
            this.comboAtelier.SelectedIndexChanged += new System.EventHandler(this.comboAtelier_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(184, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 31;
            this.label3.Text = "Atelier";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(184, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "Jour";
            // 
            // BtnAnuler
            // 
            this.BtnAnuler.Location = new System.Drawing.Point(312, 236);
            this.BtnAnuler.Name = "BtnAnuler";
            this.BtnAnuler.Size = new System.Drawing.Size(75, 23);
            this.BtnAnuler.TabIndex = 34;
            this.BtnAnuler.Text = "Annuler";
            this.BtnAnuler.UseVisualStyleBackColor = true;
            this.BtnAnuler.Click += new System.EventHandler(this.BtnAnuler_Click);
            // 
            // BtnAjoutVacation
            // 
            this.BtnAjoutVacation.Location = new System.Drawing.Point(231, 236);
            this.BtnAjoutVacation.Name = "BtnAjoutVacation";
            this.BtnAjoutVacation.Size = new System.Drawing.Size(75, 23);
            this.BtnAjoutVacation.TabIndex = 33;
            this.BtnAjoutVacation.Text = "Ajouter";
            this.BtnAjoutVacation.UseVisualStyleBackColor = true;
            this.BtnAjoutVacation.Click += new System.EventHandler(this.BtnAjoutVacation_Click);
            // 
            // choixNumero
            // 
            this.choixNumero.Location = new System.Drawing.Point(355, 79);
            this.choixNumero.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.choixNumero.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.choixNumero.Name = "choixNumero";
            this.choixNumero.Size = new System.Drawing.Size(120, 20);
            this.choixNumero.TabIndex = 36;
            this.choixNumero.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(184, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(147, 13);
            this.label4.TabIndex = 35;
            this.label4.Text = "Numero (Compris entre 1 et 5)";
            // 
            // FormModifiVacation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 337);
            this.Controls.Add(this.choixNumero);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.BtnAnuler);
            this.Controls.Add(this.BtnAjoutVacation);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboAtelier);
            this.Controls.Add(this.dateTimeJour);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.minFin);
            this.Controls.Add(this.heureFin);
            this.Controls.Add(this.minDeb);
            this.Controls.Add(this.heureDeb);
            this.Name = "FormModifiVacation";
            this.Text = "FormModifiVacation";
            this.Load += new System.EventHandler(this.FormModifiVacation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.minFin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.heureFin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minDeb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.heureDeb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.choixNumero)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown minFin;
        private System.Windows.Forms.NumericUpDown heureFin;
        private System.Windows.Forms.NumericUpDown minDeb;
        private System.Windows.Forms.NumericUpDown heureDeb;
        private System.Windows.Forms.DateTimePicker dateTimeJour;
        private System.Windows.Forms.ComboBox comboAtelier;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnAnuler;
        private System.Windows.Forms.Button BtnAjoutVacation;
        private System.Windows.Forms.NumericUpDown choixNumero;
        private System.Windows.Forms.Label label4;
    }
}