namespace MaisonDesLigues
{
    partial class FormAjoutVacation
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
            this.choixNumero = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.BtnAjoutVacation = new System.Windows.Forms.Button();
            this.minFin = new System.Windows.Forms.NumericUpDown();
            this.heureFin = new System.Windows.Forms.NumericUpDown();
            this.minDeb = new System.Windows.Forms.NumericUpDown();
            this.heureDeb = new System.Windows.Forms.NumericUpDown();
            this.comboAtelier = new System.Windows.Forms.ComboBox();
            this.BtnAnuler = new System.Windows.Forms.Button();
            this.dateTimeJour = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.choixNumero)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minFin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.heureFin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minDeb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.heureDeb)).BeginInit();
            this.SuspendLayout();
            // 
            // choixNumero
            // 
            this.choixNumero.Location = new System.Drawing.Point(310, 140);
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
            this.choixNumero.TabIndex = 22;
            this.choixNumero.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(139, 240);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Heure de début";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(139, 266);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Heure de fin";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(139, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(147, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Numero (Compris entre 1 et 5)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(139, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Atelier";
            // 
            // BtnAjoutVacation
            // 
            this.BtnAjoutVacation.Location = new System.Drawing.Point(211, 303);
            this.BtnAjoutVacation.Name = "BtnAjoutVacation";
            this.BtnAjoutVacation.Size = new System.Drawing.Size(75, 23);
            this.BtnAjoutVacation.TabIndex = 17;
            this.BtnAjoutVacation.Text = "Ajouter";
            this.BtnAjoutVacation.UseVisualStyleBackColor = true;
            this.BtnAjoutVacation.Click += new System.EventHandler(this.BtnAjoutVacation_Click);
            // 
            // minFin
            // 
            this.minFin.Location = new System.Drawing.Point(349, 259);
            this.minFin.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.minFin.Name = "minFin";
            this.minFin.Size = new System.Drawing.Size(33, 20);
            this.minFin.TabIndex = 16;
            // 
            // heureFin
            // 
            this.heureFin.Location = new System.Drawing.Point(310, 259);
            this.heureFin.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.heureFin.Name = "heureFin";
            this.heureFin.Size = new System.Drawing.Size(33, 20);
            this.heureFin.TabIndex = 15;
            // 
            // minDeb
            // 
            this.minDeb.Location = new System.Drawing.Point(349, 233);
            this.minDeb.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.minDeb.Name = "minDeb";
            this.minDeb.Size = new System.Drawing.Size(33, 20);
            this.minDeb.TabIndex = 14;
            // 
            // heureDeb
            // 
            this.heureDeb.Location = new System.Drawing.Point(310, 233);
            this.heureDeb.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.heureDeb.Name = "heureDeb";
            this.heureDeb.Size = new System.Drawing.Size(33, 20);
            this.heureDeb.TabIndex = 13;
            // 
            // comboAtelier
            // 
            this.comboAtelier.FormattingEnabled = true;
            this.comboAtelier.Location = new System.Drawing.Point(310, 88);
            this.comboAtelier.Name = "comboAtelier";
            this.comboAtelier.Size = new System.Drawing.Size(121, 21);
            this.comboAtelier.TabIndex = 12;
            // 
            // BtnAnuler
            // 
            this.BtnAnuler.Location = new System.Drawing.Point(292, 303);
            this.BtnAnuler.Name = "BtnAnuler";
            this.BtnAnuler.Size = new System.Drawing.Size(75, 23);
            this.BtnAnuler.TabIndex = 27;
            this.BtnAnuler.Text = "Annuler";
            this.BtnAnuler.UseVisualStyleBackColor = true;
            this.BtnAnuler.Click += new System.EventHandler(this.BtnAnuler_Click);
            // 
            // dateTimeJour
            // 
            this.dateTimeJour.Location = new System.Drawing.Point(310, 194);
            this.dateTimeJour.Name = "dateTimeJour";
            this.dateTimeJour.Size = new System.Drawing.Size(200, 20);
            this.dateTimeJour.TabIndex = 28;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(139, 201);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 29;
            this.label1.Text = "Jour";
            // 
            // FormAjoutVacation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 378);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimeJour);
            this.Controls.Add(this.BtnAnuler);
            this.Controls.Add(this.choixNumero);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.BtnAjoutVacation);
            this.Controls.Add(this.minFin);
            this.Controls.Add(this.heureFin);
            this.Controls.Add(this.minDeb);
            this.Controls.Add(this.heureDeb);
            this.Controls.Add(this.comboAtelier);
            this.Name = "FormAjoutVacation";
            this.Text = "FormAjoutVacation";
            this.Load += new System.EventHandler(this.FormAjoutVacation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.choixNumero)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minFin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.heureFin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minDeb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.heureDeb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown choixNumero;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BtnAjoutVacation;
        private System.Windows.Forms.NumericUpDown minFin;
        private System.Windows.Forms.NumericUpDown heureFin;
        private System.Windows.Forms.NumericUpDown minDeb;
        private System.Windows.Forms.NumericUpDown heureDeb;
        private System.Windows.Forms.ComboBox comboAtelier;
        private System.Windows.Forms.Button BtnAnuler;
        private System.Windows.Forms.DateTimePicker dateTimeJour;
        private System.Windows.Forms.Label label1;
    }
}