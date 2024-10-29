namespace ServidorChatTCP
{
    partial class ServidorChatForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServidorChatForm));
            this.entradaTextBox = new System.Windows.Forms.TextBox();
            this.mostrarTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // entradaTextBox
            // 
            this.entradaTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.entradaTextBox.Location = new System.Drawing.Point(177, 60);
            this.entradaTextBox.Name = "entradaTextBox";
            this.entradaTextBox.Size = new System.Drawing.Size(823, 24);
            this.entradaTextBox.TabIndex = 0;
            this.entradaTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.entradaTextBox_KeyDown);
            // 
            // mostrarTextBox
            // 
            this.mostrarTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mostrarTextBox.Location = new System.Drawing.Point(41, 105);
            this.mostrarTextBox.Multiline = true;
            this.mostrarTextBox.Name = "mostrarTextBox";
            this.mostrarTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.mostrarTextBox.Size = new System.Drawing.Size(1310, 450);
            this.mostrarTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.1F);
            this.label1.Location = new System.Drawing.Point(37, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Escriba un mensaje:";
            // 
            // ServidorChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1382, 653);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mostrarTextBox);
            this.Controls.Add(this.entradaTextBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ServidorChatForm";
            this.Text = "Chat Servidor (TCP)";
            this.Load += new System.EventHandler(this.ServidorChatForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox entradaTextBox;
        private System.Windows.Forms.TextBox mostrarTextBox;
        private System.Windows.Forms.Label label1;
    }
}