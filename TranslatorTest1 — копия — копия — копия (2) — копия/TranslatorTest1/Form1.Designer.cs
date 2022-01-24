namespace TranslatorTest1
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonTranslate = new System.Windows.Forms.Button();
            this.button_LoadFromFile = new System.Windows.Forms.Button();
            this.label_LoadFileName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox_Pascal = new System.Windows.Forms.RichTextBox();
            this.button_SaveAlg = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_Alg = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonTranslate
            // 
            this.buttonTranslate.Location = new System.Drawing.Point(-4, 623);
            this.buttonTranslate.Name = "buttonTranslate";
            this.buttonTranslate.Size = new System.Drawing.Size(428, 33);
            this.buttonTranslate.TabIndex = 3;
            this.buttonTranslate.Text = "ПЕРЕВЕСТИ КОД";
            this.buttonTranslate.UseVisualStyleBackColor = true;
            this.buttonTranslate.Click += new System.EventHandler(this.buttonTranslate_Click);
            // 
            // button_LoadFromFile
            // 
            this.button_LoadFromFile.Location = new System.Drawing.Point(12, 79);
            this.button_LoadFromFile.Name = "button_LoadFromFile";
            this.button_LoadFromFile.Size = new System.Drawing.Size(400, 26);
            this.button_LoadFromFile.TabIndex = 4;
            this.button_LoadFromFile.Text = "Загрузить код из файла";
            this.button_LoadFromFile.UseVisualStyleBackColor = true;
            this.button_LoadFromFile.Click += new System.EventHandler(this.button_LoadFromFile_Click);
            // 
            // label_LoadFileName
            // 
            this.label_LoadFileName.AutoSize = true;
            this.label_LoadFileName.Location = new System.Drawing.Point(14, 63);
            this.label_LoadFileName.Name = "label_LoadFileName";
            this.label_LoadFileName.Size = new System.Drawing.Size(39, 13);
            this.label_LoadFileName.TabIndex = 5;
            this.label_LoadFileName.Text = "Файл:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(12, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(188, 25);
            this.label2.TabIndex = 6;
            this.label2.Text = "Алгоритмический";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.textBox_Pascal);
            this.panel1.Controls.Add(this.button_SaveAlg);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(418, 74);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(834, 542);
            this.panel1.TabIndex = 9;
            // 
            // textBox_Pascal
            // 
            this.textBox_Pascal.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.textBox_Pascal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_Pascal.Location = new System.Drawing.Point(13, 63);
            this.textBox_Pascal.Name = "textBox_Pascal";
            this.textBox_Pascal.Size = new System.Drawing.Size(805, 539);
            this.textBox_Pascal.TabIndex = 13;
            this.textBox_Pascal.Text = "";
            this.textBox_Pascal.TextChanged += new System.EventHandler(this.textBox_Alg_TextChanged);
            // 
            // button_SaveAlg
            // 
            this.button_SaveAlg.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button_SaveAlg.Location = new System.Drawing.Point(242, 3);
            this.button_SaveAlg.Name = "button_SaveAlg";
            this.button_SaveAlg.Size = new System.Drawing.Size(400, 26);
            this.button_SaveAlg.TabIndex = 10;
            this.button_SaveAlg.Text = "Сохранить код в файл";
            this.button_SaveAlg.UseVisualStyleBackColor = true;
            this.button_SaveAlg.Click += new System.EventHandler(this.button_SaveAlg_Click);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(8, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 25);
            this.label3.TabIndex = 11;
            this.label3.Text = "Паскаль";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(307, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(614, 31);
            this.label1.TabIndex = 10;
            this.label1.Text = "Транслятор Алгоритмический язык → Паскаль ";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // textBox_Alg
            // 
            this.textBox_Alg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_Alg.Location = new System.Drawing.Point(12, 144);
            this.textBox_Alg.Name = "textBox_Alg";
            this.textBox_Alg.Size = new System.Drawing.Size(400, 472);
            this.textBox_Alg.TabIndex = 11;
            this.textBox_Alg.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 667);
            this.Controls.Add(this.textBox_Alg);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label_LoadFileName);
            this.Controls.Add(this.button_LoadFromFile);
            this.Controls.Add(this.buttonTranslate);
            this.Name = "Form1";
            this.Text = "Транслятор ";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonTranslate;
        private System.Windows.Forms.Button button_LoadFromFile;
        private System.Windows.Forms.Label label_LoadFileName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox textBox_Alg;
        private System.Windows.Forms.RichTextBox textBox_Pascal;
        private System.Windows.Forms.Button button_SaveAlg;
    }
}

