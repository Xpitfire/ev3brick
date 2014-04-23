namespace PrgSps2Gr1.Control
{
    partial class SimControlGui
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
            this.buttonEscapeReleased = new System.Windows.Forms.Button();
            this.buttonEnterReleased = new System.Windows.Forms.Button();
            this.buttonReachedEdge = new System.Windows.Forms.Button();
            this.lblEvents = new System.Windows.Forms.Label();
            this.txtBoxSpinScanner = new System.Windows.Forms.TextBox();
            this.lblDeviceTrigger = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBoxDisplayLog = new System.Windows.Forms.TextBox();
            this.lblDisplayLog = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonEscapeReleased
            // 
            this.buttonEscapeReleased.Location = new System.Drawing.Point(49, 59);
            this.buttonEscapeReleased.Name = "buttonEscapeReleased";
            this.buttonEscapeReleased.Size = new System.Drawing.Size(148, 41);
            this.buttonEscapeReleased.TabIndex = 0;
            this.buttonEscapeReleased.Text = "EscapeReleased";
            this.buttonEscapeReleased.UseVisualStyleBackColor = true;
            this.buttonEscapeReleased.Click += new System.EventHandler(this.buttonEscapeReleased_Click);
            // 
            // buttonEnterReleased
            // 
            this.buttonEnterReleased.Location = new System.Drawing.Point(49, 106);
            this.buttonEnterReleased.Name = "buttonEnterReleased";
            this.buttonEnterReleased.Size = new System.Drawing.Size(148, 41);
            this.buttonEnterReleased.TabIndex = 1;
            this.buttonEnterReleased.Text = "EnterReleased";
            this.buttonEnterReleased.UseVisualStyleBackColor = true;
            this.buttonEnterReleased.Click += new System.EventHandler(this.buttonEnterReleased_Click);
            // 
            // buttonReachedEdge
            // 
            this.buttonReachedEdge.Location = new System.Drawing.Point(49, 153);
            this.buttonReachedEdge.Name = "buttonReachedEdge";
            this.buttonReachedEdge.Size = new System.Drawing.Size(148, 41);
            this.buttonReachedEdge.TabIndex = 2;
            this.buttonReachedEdge.Text = "ReachedEdge";
            this.buttonReachedEdge.UseVisualStyleBackColor = true;
            this.buttonReachedEdge.Click += new System.EventHandler(this.buttonReachedEdge_Click);
            // 
            // lblEvents
            // 
            this.lblEvents.AutoSize = true;
            this.lblEvents.Location = new System.Drawing.Point(46, 27);
            this.lblEvents.Name = "lblEvents";
            this.lblEvents.Size = new System.Drawing.Size(81, 17);
            this.lblEvents.TabIndex = 3;
            this.lblEvents.Text = "EV3 Events";
            // 
            // txtBoxSpinScanner
            // 
            this.txtBoxSpinScanner.Location = new System.Drawing.Point(535, 59);
            this.txtBoxSpinScanner.Name = "txtBoxSpinScanner";
            this.txtBoxSpinScanner.ReadOnly = true;
            this.txtBoxSpinScanner.Size = new System.Drawing.Size(203, 22);
            this.txtBoxSpinScanner.TabIndex = 4;
            // 
            // lblDeviceTrigger
            // 
            this.lblDeviceTrigger.AutoSize = true;
            this.lblDeviceTrigger.Location = new System.Drawing.Point(532, 27);
            this.lblDeviceTrigger.Name = "lblDeviceTrigger";
            this.lblDeviceTrigger.Size = new System.Drawing.Size(138, 17);
            this.lblDeviceTrigger.TabIndex = 5;
            this.lblDeviceTrigger.Text = "EV3 Device Triggers";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(440, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "SpinScanner";
            // 
            // txtBoxDisplayLog
            // 
            this.txtBoxDisplayLog.Location = new System.Drawing.Point(49, 376);
            this.txtBoxDisplayLog.Name = "txtBoxDisplayLog";
            this.txtBoxDisplayLog.ReadOnly = true;
            this.txtBoxDisplayLog.Size = new System.Drawing.Size(337, 22);
            this.txtBoxDisplayLog.TabIndex = 7;
            // 
            // lblDisplayLog
            // 
            this.lblDisplayLog.AutoSize = true;
            this.lblDisplayLog.Location = new System.Drawing.Point(46, 341);
            this.lblDisplayLog.Name = "lblDisplayLog";
            this.lblDisplayLog.Size = new System.Drawing.Size(112, 17);
            this.lblDisplayLog.TabIndex = 8;
            this.lblDisplayLog.Text = "EV3 Display Log";
            // 
            // SimControlGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 465);
            this.Controls.Add(this.lblDisplayLog);
            this.Controls.Add(this.txtBoxDisplayLog);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblDeviceTrigger);
            this.Controls.Add(this.txtBoxSpinScanner);
            this.Controls.Add(this.lblEvents);
            this.Controls.Add(this.buttonReachedEdge);
            this.Controls.Add(this.buttonEnterReleased);
            this.Controls.Add(this.buttonEscapeReleased);
            this.Name = "SimControlGui";
            this.Text = "SimControlGui";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonEscapeReleased;
        private System.Windows.Forms.Button buttonEnterReleased;
        private System.Windows.Forms.Button buttonReachedEdge;
        private System.Windows.Forms.Label lblEvents;
        private System.Windows.Forms.TextBox txtBoxSpinScanner;
        private System.Windows.Forms.Label lblDeviceTrigger;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBoxDisplayLog;
        private System.Windows.Forms.Label lblDisplayLog;
    }
}