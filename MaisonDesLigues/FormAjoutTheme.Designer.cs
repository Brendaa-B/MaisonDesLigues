namespace MaisonDesLigues
{
    partial class FormAjoutTheme
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
            this.BtnAjoutTheme = new System.Windows.Forms.Button();
            this.libelleTheme = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.numeroTheme = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.comboAtelierTheme = new System.Windows.Forms.ComboBox();
            this.BtnAnuler = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numeroTheme)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnAjoutTheme
            // 
            this.BtnAjoutTheme.Location = new System.Drawing.Point(247, 241);
            this.BtnAjoutTheme.Name = "BtnAjoutTheme";
            this.BtnAjoutTheme.Size = new System.Drawing.Size(75, 23);
            this.BtnAjoutTheme.TabIndex = 25;
            this.BtnAjoutTheme.Text = "Ajouter";
            this.BtnAjoutTheme.UseVisualStyleBackColor = true;
            this.BtnAjoutTheme.Click += new System.EventHandler(this.BtnAjoutTheme_Click);
            // 
            // libelleTheme
            // 
            this.libelleTheme.Location = new System.Drawing.Point(335, 85);
            this.libelleTheme.Name = "libelleTheme";
            this.libelleTheme.Size = new System.Drawing.Size(100, 20);
            this.libelleTheme.TabIndex = 24;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(164, 92);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 13);
            this.label9.TabIndex = 23;
            this.label9.Text = "Libellé";
            // 
            // numeroTheme
            // 
            this.numeroTheme.Location = new System.Drawing.Point(335, 175);
            this.numeroTheme.Name = "numeroTheme";
            this.numeroTheme.Size = new System.Drawing.Size(120, 20);
            this.numeroTheme.TabIndex = 22;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(164, 182);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "Numéro";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(164, 140);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Atelier";
            // 
            // comboAtelierTheme
            // 
            this.comboAtelierTheme.FormattingEnabled = true;
            this.comboAtelierTheme.Location = new System.Drawing.Point(335, 132);
            this.comboAtelierTheme.Name = "comboAtelierTheme";
            this.comboAtelierTheme.Size = new System.Drawing.Size(121, 21);
            this.comboAtelierTheme.TabIndex = 19;
            this.comboAtelierTheme.SelectedIndexChanged += new System.EventHandler(this.comboAtelierTheme_SelectedIndexChanged);
            // 
            // BtnAnuler
            // 
            this.BtnAnuler.Location = new System.Drawing.Point(328, 241);
            this.BtnAnuler.Name = "BtnAnuler";
            this.BtnAnuler.Size = new System.Drawing.Size(75, 23);
            this.BtnAnuler.TabIndex = 26;
            this.BtnAnuler.Text = "Annuler";
            this.BtnAnuler.UseVisualStyleBackColor = true;
            this.BtnAnuler.Click += new System.EventHandler(this.BtnAnuler_Click);
            // 
            // FormAjoutTheme
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 348);
            this.Controls.Add(this.BtnAnuler);
            this.Controls.Add(this.BtnAjoutTheme);
            this.Controls.Add(this.libelleTheme);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.numeroTheme);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.comboAtelierTheme);
            this.Name = "FormAjoutTheme";
            this.Text = "FormAjoutTheme";
            this.Load += new System.EventHandler(this.FormAjoutTheme_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numeroTheme)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnAjoutTheme;
        private System.Windows.Forms.TextBox libelleTheme;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numeroTheme;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboAtelierTheme;
        private System.Windows.Forms.Button BtnAnuler;
    }
}