namespace SimpleRTSPPlayerWinforms
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
            this.videoControl1 = new SimpleRTSPPlayerWinforms.VideoControl();
            this.btnPlay = new System.Windows.Forms.Button();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // videoControl1
            // 
            this.videoControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.videoControl1.Location = new System.Drawing.Point(37, 12);
            this.videoControl1.Name = "videoControl1";
            this.videoControl1.Size = new System.Drawing.Size(375, 221);
            this.videoControl1.TabIndex = 0;
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(37, 275);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(75, 23);
            this.btnPlay.TabIndex = 1;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(37, 249);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(375, 20);
            this.txtUrl.TabIndex = 2;
            this.txtUrl.Text = "rtsp://170.93.143.139/rtplive/470011e600ef003a004ee33696235daa";
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(119, 275);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 311);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.videoControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private VideoControl videoControl1;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Button btnStop;
    }
}

