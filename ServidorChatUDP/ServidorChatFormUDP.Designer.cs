namespace ServidorChatUDP
{
    partial class ServidorChatFormUDP
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
            this.mostrarTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // mostrarTextBox
            // 
            this.mostrarTextBox.Location = new System.Drawing.Point(32, 22);
            this.mostrarTextBox.Multiline = true;
            this.mostrarTextBox.Name = "mostrarTextBox";
            this.mostrarTextBox.Size = new System.Drawing.Size(731, 398);
            this.mostrarTextBox.TabIndex = 1;
            // 
            // ServidorChatFormUDP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.mostrarTextBox);
            this.Name = "ServidorChatFormUDP";
            this.Text = "Chat Servidor (UDP)";
            this.Load += new System.EventHandler(this.ServidorChatFormUDP_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox mostrarTextBox;
    }
}