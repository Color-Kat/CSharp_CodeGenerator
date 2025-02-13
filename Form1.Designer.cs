namespace CodeGenerator
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
            generateCode = new Button();
            label1 = new Label();
            TemplateDialog = new OpenFileDialog();
            MetadataDialog = new OpenFileDialog();
            SuspendLayout();
            // 
            // generateCode
            // 
            generateCode.Location = new Point(283, 195);
            generateCode.Name = "generateCode";
            generateCode.Size = new Size(150, 46);
            generateCode.TabIndex = 0;
            generateCode.Text = "Generate Program";
            generateCode.UseVisualStyleBackColor = true;
            generateCode.Click += generateCode_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(145, 27);
            label1.Name = "label1";
            label1.Size = new Size(459, 128);
            label1.TabIndex = 1;
            label1.Text = "Select template file (CodeTemplate.txt) \r\nand Metadata file (metadata.txt) \r\nto generate C# code (GeneratedCode.cs). \r\nThen it must be compiled using MSBuild";
            // 
            // TemplateDialog
            // 
            TemplateDialog.FileName = "openFileDialog1";
            // 
            // MetadataDialog
            // 
            MetadataDialog.FileName = "openFileDialog2";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Controls.Add(generateCode);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button generateCode;
        private Label label1;
        private OpenFileDialog TemplateDialog;
        private OpenFileDialog MetadataDialog;
    }
}
