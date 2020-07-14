namespace GroupTool
{
    partial class Form1
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
            this.tbAddCheck = new System.Windows.Forms.TextBox();
            this.cklTodo = new System.Windows.Forms.CheckedListBox();
            this.btAdd = new System.Windows.Forms.Button();
            this.btDelete = new System.Windows.Forms.Button();
            this.cklLongTerm = new System.Windows.Forms.CheckedListBox();
            this.tbAddLT = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btClose = new System.Windows.Forms.Button();
            this.btRemoveLT = new System.Windows.Forms.Button();
            this.btAddLT = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbAddCheck
            // 
            this.tbAddCheck.Location = new System.Drawing.Point(8, 25);
            this.tbAddCheck.Name = "tbAddCheck";
            this.tbAddCheck.Size = new System.Drawing.Size(253, 20);
            this.tbAddCheck.TabIndex = 0;
            this.tbAddCheck.TextChanged += new System.EventHandler(this.tbAddCheck_TextChanged);
            // 
            // cklTodo
            // 
            this.cklTodo.CheckOnClick = true;
            this.cklTodo.FormattingEnabled = true;
            this.cklTodo.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.cklTodo.Location = new System.Drawing.Point(8, 51);
            this.cklTodo.Name = "cklTodo";
            this.cklTodo.Size = new System.Drawing.Size(253, 154);
            this.cklTodo.TabIndex = 1;
            this.cklTodo.ThreeDCheckBoxes = true;
            this.cklTodo.SelectedIndexChanged += new System.EventHandler(this.cklTodo_SelectedIndexChanged);
            // 
            // btAdd
            // 
            this.btAdd.Location = new System.Drawing.Point(267, 51);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(56, 26);
            this.btAdd.TabIndex = 2;
            this.btAdd.Text = "Add";
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // btDelete
            // 
            this.btDelete.Location = new System.Drawing.Point(267, 84);
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(56, 26);
            this.btDelete.TabIndex = 3;
            this.btDelete.Text = "Remove";
            this.btDelete.UseVisualStyleBackColor = true;
            this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
            // 
            // cklLongTerm
            // 
            this.cklLongTerm.CheckOnClick = true;
            this.cklLongTerm.FormattingEnabled = true;
            this.cklLongTerm.Location = new System.Drawing.Point(12, 260);
            this.cklLongTerm.Name = "cklLongTerm";
            this.cklLongTerm.Size = new System.Drawing.Size(249, 199);
            this.cklLongTerm.TabIndex = 6;
            // 
            // tbAddLT
            // 
            this.tbAddLT.Location = new System.Drawing.Point(12, 234);
            this.tbAddLT.Name = "tbAddLT";
            this.tbAddLT.Size = new System.Drawing.Size(249, 20);
            this.tbAddLT.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Todo List:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 215);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "LongTerm Goals";
            // 
            // btClose
            // 
            this.btClose.Location = new System.Drawing.Point(267, 436);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(56, 23);
            this.btClose.TabIndex = 9;
            this.btClose.Text = "Close";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // btRemoveLT
            // 
            this.btRemoveLT.Location = new System.Drawing.Point(267, 295);
            this.btRemoveLT.Name = "btRemoveLT";
            this.btRemoveLT.Size = new System.Drawing.Size(56, 26);
            this.btRemoveLT.TabIndex = 11;
            this.btRemoveLT.Text = "Remove";
            this.btRemoveLT.UseVisualStyleBackColor = true;
            this.btRemoveLT.Click += new System.EventHandler(this.btRemoveLT_Click);
            // 
            // btAddLT
            // 
            this.btAddLT.Location = new System.Drawing.Point(267, 260);
            this.btAddLT.Name = "btAddLT";
            this.btAddLT.Size = new System.Drawing.Size(56, 26);
            this.btAddLT.TabIndex = 10;
            this.btAddLT.Text = "Add";
            this.btAddLT.UseVisualStyleBackColor = true;
            this.btAddLT.Click += new System.EventHandler(this.btAddLT_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 466);
            this.Controls.Add(this.btRemoveLT);
            this.Controls.Add(this.btAddLT);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cklLongTerm);
            this.Controls.Add(this.tbAddLT);
            this.Controls.Add(this.btDelete);
            this.Controls.Add(this.btAdd);
            this.Controls.Add(this.cklTodo);
            this.Controls.Add(this.tbAddCheck);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Form1";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbAddCheck;
        private System.Windows.Forms.CheckedListBox cklTodo;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Button btDelete;
        private System.Windows.Forms.CheckedListBox cklLongTerm;
        private System.Windows.Forms.TextBox tbAddLT;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btRemoveLT;
        private System.Windows.Forms.Button btAddLT;
    }
}