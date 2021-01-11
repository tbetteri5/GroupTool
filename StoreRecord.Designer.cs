namespace GroupTool
{
    partial class StoreRecord
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
            this.tbStoreRecDescription = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btStorRecSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lbStoreRecSubject = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbStoreRecDescription
            // 
            this.tbStoreRecDescription.Location = new System.Drawing.Point(31, 56);
            this.tbStoreRecDescription.Name = "tbStoreRecDescription";
            this.tbStoreRecDescription.Size = new System.Drawing.Size(277, 20);
            this.tbStoreRecDescription.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(174, 87);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(64, 26);
            this.button1.TabIndex = 1;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btStorRecSave
            // 
            this.btStorRecSave.Location = new System.Drawing.Point(244, 87);
            this.btStorRecSave.Name = "btStorRecSave";
            this.btStorRecSave.Size = new System.Drawing.Size(64, 26);
            this.btStorRecSave.TabIndex = 2;
            this.btStorRecSave.Text = "Save";
            this.btStorRecSave.UseVisualStyleBackColor = true;
            this.btStorRecSave.Click += new System.EventHandler(this.btStorRecSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Description:";
            // 
            // lbStoreRecSubject
            // 
            this.lbStoreRecSubject.AutoSize = true;
            this.lbStoreRecSubject.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStoreRecSubject.Location = new System.Drawing.Point(28, 9);
            this.lbStoreRecSubject.Name = "lbStoreRecSubject";
            this.lbStoreRecSubject.Size = new System.Drawing.Size(63, 20);
            this.lbStoreRecSubject.TabIndex = 4;
            this.lbStoreRecSubject.Text = "Subject";
            this.lbStoreRecSubject.Click += new System.EventHandler(this.lbStoreRecSubject_Click);
            // 
            // StoreRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 132);
            this.Controls.Add(this.lbStoreRecSubject);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btStorRecSave);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbStoreRecDescription);
            this.Name = "StoreRecord";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Store Record";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.StoreRecord_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbStoreRecDescription;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btStorRecSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbStoreRecSubject;
    }
}