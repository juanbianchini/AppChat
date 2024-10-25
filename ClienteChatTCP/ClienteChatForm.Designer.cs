namespace ClienteChatTCP
{
    partial class ClienteChatForm
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
            this.entradaTextBox = new System.Windows.Forms.TextBox();
            this.mostrarTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // entradaTextBox
            // 
            this.entradaTextBox.Location = new System.Drawing.Point(36, 22);
            this.entradaTextBox.Name = "entradaTextBox";
            this.entradaTextBox.Size = new System.Drawing.Size(717, 22);
            this.entradaTextBox.TabIndex = 0;
            this.entradaTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.entradaTextBox_KeyDown);
            // 
            // mostrarTextBox
            // 
            this.mostrarTextBox.Location = new System.Drawing.Point(36, 65);
            this.mostrarTextBox.Multiline = true;
            this.mostrarTextBox.Name = "mostrarTextBox";
            this.mostrarTextBox.Size = new System.Drawing.Size(717, 357);
            this.mostrarTextBox.TabIndex = 1;
            // 
            // ClienteChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.mostrarTextBox);
            this.Controls.Add(this.entradaTextBox);
            this.Name = "ClienteChatForm";
            this.Text = "Chat Cliente (TCP)";
            this.Load += new System.EventHandler(this.ClienteChatForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox entradaTextBox;
        private System.Windows.Forms.TextBox mostrarTextBox;
    }
}