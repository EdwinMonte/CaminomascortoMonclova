namespace CaminomascortoMonclova
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            cmbCityoforigin = new ComboBox();
            cmbDestinationcity = new ComboBox();
            btnCalculateShortest = new Button();
            txtresult = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            btnCalculateLongest = new Button();
            panelMap = new Panel();
            SuspendLayout();
            // 
            // cmbCityoforigin
            // 
            cmbCityoforigin.FormattingEnabled = true;
            cmbCityoforigin.Location = new Point(12, 45);
            cmbCityoforigin.Name = "cmbCityoforigin";
            cmbCityoforigin.Size = new Size(247, 28);
            cmbCityoforigin.TabIndex = 0;
            // 
            // cmbDestinationcity
            // 
            cmbDestinationcity.FormattingEnabled = true;
            cmbDestinationcity.Location = new Point(12, 150);
            cmbDestinationcity.Name = "cmbDestinationcity";
            cmbDestinationcity.Size = new Size(247, 28);
            cmbDestinationcity.TabIndex = 1;
            // 
            // btnCalculateShortest
            // 
            btnCalculateShortest.Location = new Point(34, 203);
            btnCalculateShortest.Name = "btnCalculateShortest";
            btnCalculateShortest.Size = new Size(183, 63);
            btnCalculateShortest.TabIndex = 2;
            btnCalculateShortest.Text = "Calculate Shortest";
            btnCalculateShortest.UseVisualStyleBackColor = true;
            // 
            // txtresult
            // 
            txtresult.Location = new Point(12, 464);
            txtresult.Multiline = true;
            txtresult.Name = "txtresult";
            txtresult.Size = new Size(275, 72);
            txtresult.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(52, 19);
            label1.Name = "label1";
            label1.Size = new Size(95, 20);
            label1.TabIndex = 5;
            label1.Text = "City of origin";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(52, 115);
            label2.Name = "label2";
            label2.Size = new Size(112, 20);
            label2.TabIndex = 6;
            label2.Text = "Destination city";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(79, 441);
            label3.Name = "label3";
            label3.Size = new Size(85, 20);
            label3.TabIndex = 7;
            label3.Text = "Description";
            // 
            // btnCalculateLongest
            // 
            btnCalculateLongest.Location = new Point(34, 272);
            btnCalculateLongest.Name = "btnCalculateLongest";
            btnCalculateLongest.Size = new Size(183, 58);
            btnCalculateLongest.TabIndex = 8;
            btnCalculateLongest.Text = "Calculate Longest";
            btnCalculateLongest.UseVisualStyleBackColor = true;
            // 
            // panelMap
            // 
            panelMap.Location = new Point(293, 45);
            panelMap.Name = "panelMap";
            panelMap.Size = new Size(903, 769);
            panelMap.TabIndex = 9;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1225, 838);
            Controls.Add(panelMap);
            Controls.Add(btnCalculateLongest);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtresult);
            Controls.Add(btnCalculateShortest);
            Controls.Add(cmbDestinationcity);
            Controls.Add(cmbCityoforigin);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbCityoforigin;
        private ComboBox cmbDestinationcity;
        private Button btnCalculateShortest;
        private TextBox txtresult;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button btnCalculateLongest;
        private Panel panelMap;
    }
}
