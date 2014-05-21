namespace MaisonDesLigues
{
    partial class FormAjoutAtelier
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
            this.BtnAjoutAtelier = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nbPlaces = new System.Windows.Forms.NumericUpDown();
            this.libelleAtelier = new System.Windows.Forms.TextBox();
            this.BtnAnuler = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nbPlaces)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnAjoutAtelier
            // 
            this.BtnAjoutAtelier.Location = new System.Drawing.Point(247, 247);
            this.BtnAjoutAtelier.Name = "BtnAjoutAtelier";
            this.BtnAjoutAtelier.Size = new System.Drawing.Size(75, 23);
            this.BtnAjoutAtelier.TabIndex = 10;
            this.BtnAjoutAtelier.Text = "Ajouter";
            this.BtnAjoutAtelier.UseVisualStyleBackColor = true;
            this.BtnAjoutAtelier.Click += new System.EventHandler(this.BtnAjoutAtelier_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(160, 168);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Nombre de places maximum";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(160, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Libelle";
            // 
            // nbPlaces
            // 
            this.nbPlaces.Location = new System.Drawing.Point(333, 161);
            this.nbPlaces.Name = "nbPlaces";
            this.nbPlaces.Size = new System.Drawing.Size(120, 20);
            this.nbPlaces.TabIndex = 7;
            // 
            // libelleAtelier
            // 
            this.libelleAtelier.Location = new System.Drawing.Point(333, 95);
            this.libelleAtelier.Name = "libelleAtelier";
            this.libelleAtelier.Size = new System.Drawing.Size(100, 20);
            this.libelleAtelier.TabIndex = 6;
            // 
            // BtnAnuler
            // 
            this.BtnAnuler.Location = new System.Drawing.Point(328, 247);
            this.BtnAnuler.Name = "BtnAnuler";
            this.BtnAnuler.Size = new System.Drawing.Size(75, 23);
            this.BtnAnuler.TabIndex = 27;
            this.BtnAnuler.Text = "Annuler";
            this.BtnAnuler.UseVisualStyleBackColor = true;
            this.BtnAnuler.Click += new System.EventHandler(this.BtnAnuler_Click);
            // 
            // FormAjoutAtelier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 365);
            this.Controls.Add(this.BtnAnuler);
            this.Controls.Add(this.BtnAjoutAtelier);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nbPlaces);
            this.Controls.Add(this.libelleAtelier);
            this.Name = "FormAjoutAtelier";
            this.Text = "ForAjoutAtelier";
            this.Load += new System.EventHandler(this.FormAjoutAtelier_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nbPlaces)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnAjoutAtelier;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nbPlaces;
        private System.Windows.Forms.TextBox libelleAtelier;
        private System.Windows.Forms.Button BtnAnuler;
    }
}