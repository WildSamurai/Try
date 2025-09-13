namespace Calculator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button24 = new System.Windows.Forms.Button();
            this.button23 = new System.Windows.Forms.Button();
            this.button22 = new System.Windows.Forms.Button();
            this.button21 = new System.Windows.Forms.Button();
            this.button19 = new System.Windows.Forms.Button();
            this.button18 = new System.Windows.Forms.Button();
            this.button17 = new System.Windows.Forms.Button();
            this.button16 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.TextResult = new System.Windows.Forms.Label();
            this.panel = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textHistory = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.button20 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(0, 4);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 57);
            this.button1.TabIndex = 0;
            this.button1.Text = "1/x";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.buttonOperationClick);
            this.button1.Paint += new System.Windows.Forms.PaintEventHandler(this.button24_Paint);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.button24);
            this.panel1.Controls.Add(this.button23);
            this.panel1.Controls.Add(this.button22);
            this.panel1.Controls.Add(this.button21);
            this.panel1.Controls.Add(this.button19);
            this.panel1.Controls.Add(this.button18);
            this.panel1.Controls.Add(this.button17);
            this.panel1.Controls.Add(this.button16);
            this.panel1.Controls.Add(this.button14);
            this.panel1.Controls.Add(this.button13);
            this.panel1.Controls.Add(this.button12);
            this.panel1.Controls.Add(this.button11);
            this.panel1.Controls.Add(this.button9);
            this.panel1.Controls.Add(this.button8);
            this.panel1.Controls.Add(this.button7);
            this.panel1.Controls.Add(this.button6);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Location = new System.Drawing.Point(2, 223);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(365, 326);
            this.panel1.TabIndex = 1;
            // 
            // button24
            // 
            this.button24.BackColor = System.Drawing.Color.Transparent;
            this.button24.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button24.Location = new System.Drawing.Point(276, 260);
            this.button24.Margin = new System.Windows.Forms.Padding(4);
            this.button24.Name = "button24";
            this.button24.Size = new System.Drawing.Size(84, 57);
            this.button24.TabIndex = 23;
            this.button24.Text = "=";
            this.button24.UseVisualStyleBackColor = false;
            this.button24.Click += new System.EventHandler(this.buttonResultClick);
            this.button24.Paint += new System.Windows.Forms.PaintEventHandler(this.button24_Paint);
            // 
            // button23
            // 
            this.button23.BackColor = System.Drawing.Color.Transparent;
            this.button23.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button23.Location = new System.Drawing.Point(276, 196);
            this.button23.Margin = new System.Windows.Forms.Padding(4);
            this.button23.Name = "button23";
            this.button23.Size = new System.Drawing.Size(84, 57);
            this.button23.TabIndex = 22;
            this.button23.Text = "+";
            this.button23.UseVisualStyleBackColor = false;
            this.button23.Click += new System.EventHandler(this.buttonOperationClick);
            this.button23.Paint += new System.Windows.Forms.PaintEventHandler(this.button24_Paint);
            // 
            // button22
            // 
            this.button22.BackColor = System.Drawing.Color.Transparent;
            this.button22.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button22.Location = new System.Drawing.Point(276, 132);
            this.button22.Margin = new System.Windows.Forms.Padding(4);
            this.button22.Name = "button22";
            this.button22.Size = new System.Drawing.Size(84, 57);
            this.button22.TabIndex = 21;
            this.button22.Text = "-";
            this.button22.UseVisualStyleBackColor = false;
            this.button22.Click += new System.EventHandler(this.buttonOperationClick);
            this.button22.Paint += new System.Windows.Forms.PaintEventHandler(this.button24_Paint);
            // 
            // button21
            // 
            this.button21.BackColor = System.Drawing.Color.Transparent;
            this.button21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button21.Location = new System.Drawing.Point(276, 68);
            this.button21.Margin = new System.Windows.Forms.Padding(4);
            this.button21.Name = "button21";
            this.button21.Size = new System.Drawing.Size(84, 57);
            this.button21.TabIndex = 20;
            this.button21.Text = "x";
            this.button21.UseVisualStyleBackColor = false;
            this.button21.Click += new System.EventHandler(this.buttonOperationClick);
            this.button21.Paint += new System.Windows.Forms.PaintEventHandler(this.button24_Paint);
            // 
            // button19
            // 
            this.button19.BackColor = System.Drawing.Color.Transparent;
            this.button19.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button19.Location = new System.Drawing.Point(184, 261);
            this.button19.Margin = new System.Windows.Forms.Padding(4);
            this.button19.Name = "button19";
            this.button19.Size = new System.Drawing.Size(84, 57);
            this.button19.TabIndex = 18;
            this.button19.Text = ",";
            this.button19.UseVisualStyleBackColor = false;
            this.button19.Click += new System.EventHandler(this.buttonNumberClick);
            this.button19.Paint += new System.Windows.Forms.PaintEventHandler(this.button24_Paint);
            // 
            // button18
            // 
            this.button18.BackColor = System.Drawing.Color.Transparent;
            this.button18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button18.Location = new System.Drawing.Point(184, 196);
            this.button18.Margin = new System.Windows.Forms.Padding(4);
            this.button18.Name = "button18";
            this.button18.Size = new System.Drawing.Size(84, 57);
            this.button18.TabIndex = 17;
            this.button18.Text = "3";
            this.button18.UseVisualStyleBackColor = false;
            this.button18.Click += new System.EventHandler(this.buttonNumberClick);
            this.button18.Paint += new System.Windows.Forms.PaintEventHandler(this.button24_Paint);
            // 
            // button17
            // 
            this.button17.BackColor = System.Drawing.Color.Transparent;
            this.button17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button17.Location = new System.Drawing.Point(184, 132);
            this.button17.Margin = new System.Windows.Forms.Padding(4);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(84, 57);
            this.button17.TabIndex = 16;
            this.button17.Text = "6";
            this.button17.UseVisualStyleBackColor = false;
            this.button17.Click += new System.EventHandler(this.buttonNumberClick);
            this.button17.Paint += new System.Windows.Forms.PaintEventHandler(this.button24_Paint);
            // 
            // button16
            // 
            this.button16.BackColor = System.Drawing.Color.Transparent;
            this.button16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button16.Location = new System.Drawing.Point(184, 68);
            this.button16.Margin = new System.Windows.Forms.Padding(4);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(84, 57);
            this.button16.TabIndex = 15;
            this.button16.Text = "9";
            this.button16.UseVisualStyleBackColor = false;
            this.button16.Click += new System.EventHandler(this.buttonNumberClick);
            this.button16.Paint += new System.Windows.Forms.PaintEventHandler(this.button24_Paint);
            // 
            // button14
            // 
            this.button14.BackColor = System.Drawing.Color.Transparent;
            this.button14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button14.Location = new System.Drawing.Point(92, 260);
            this.button14.Margin = new System.Windows.Forms.Padding(4);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(84, 57);
            this.button14.TabIndex = 13;
            this.button14.Text = "0";
            this.button14.UseVisualStyleBackColor = false;
            this.button14.Click += new System.EventHandler(this.buttonNumberClick);
            this.button14.Paint += new System.Windows.Forms.PaintEventHandler(this.button24_Paint);
            // 
            // button13
            // 
            this.button13.BackColor = System.Drawing.Color.Transparent;
            this.button13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button13.Location = new System.Drawing.Point(92, 196);
            this.button13.Margin = new System.Windows.Forms.Padding(4);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(84, 57);
            this.button13.TabIndex = 12;
            this.button13.Text = "2";
            this.button13.UseVisualStyleBackColor = false;
            this.button13.Click += new System.EventHandler(this.buttonNumberClick);
            this.button13.Paint += new System.Windows.Forms.PaintEventHandler(this.button24_Paint);
            // 
            // button12
            // 
            this.button12.BackColor = System.Drawing.Color.Transparent;
            this.button12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button12.Location = new System.Drawing.Point(92, 132);
            this.button12.Margin = new System.Windows.Forms.Padding(4);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(84, 57);
            this.button12.TabIndex = 11;
            this.button12.Text = "5";
            this.button12.UseVisualStyleBackColor = false;
            this.button12.Click += new System.EventHandler(this.buttonNumberClick);
            this.button12.Paint += new System.Windows.Forms.PaintEventHandler(this.button24_Paint);
            // 
            // button11
            // 
            this.button11.BackColor = System.Drawing.Color.Transparent;
            this.button11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button11.Location = new System.Drawing.Point(92, 68);
            this.button11.Margin = new System.Windows.Forms.Padding(4);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(84, 57);
            this.button11.TabIndex = 10;
            this.button11.Text = "8";
            this.button11.UseVisualStyleBackColor = false;
            this.button11.Click += new System.EventHandler(this.buttonNumberClick);
            this.button11.Paint += new System.Windows.Forms.PaintEventHandler(this.button24_Paint);
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.Color.Transparent;
            this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button9.Location = new System.Drawing.Point(0, 260);
            this.button9.Margin = new System.Windows.Forms.Padding(4);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(84, 57);
            this.button9.TabIndex = 8;
            this.button9.Text = "+/-";
            this.button9.UseVisualStyleBackColor = false;
            this.button9.Click += new System.EventHandler(this.buttonNumberClick);
            this.button9.Paint += new System.Windows.Forms.PaintEventHandler(this.button24_Paint);
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.Color.Transparent;
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button8.Location = new System.Drawing.Point(0, 196);
            this.button8.Margin = new System.Windows.Forms.Padding(4);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(84, 57);
            this.button8.TabIndex = 7;
            this.button8.Text = "1";
            this.button8.UseVisualStyleBackColor = false;
            this.button8.Click += new System.EventHandler(this.buttonNumberClick);
            this.button8.Paint += new System.Windows.Forms.PaintEventHandler(this.button24_Paint);
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.Transparent;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Location = new System.Drawing.Point(0, 132);
            this.button7.Margin = new System.Windows.Forms.Padding(4);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(84, 57);
            this.button7.TabIndex = 6;
            this.button7.Text = "4";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.buttonNumberClick);
            this.button7.Paint += new System.Windows.Forms.PaintEventHandler(this.button24_Paint);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.Transparent;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Location = new System.Drawing.Point(0, 68);
            this.button6.Margin = new System.Windows.Forms.Padding(4);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(84, 57);
            this.button6.TabIndex = 5;
            this.button6.Text = "7";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.buttonNumberClick);
            this.button6.Paint += new System.Windows.Forms.PaintEventHandler(this.button24_Paint);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Transparent;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Location = new System.Drawing.Point(276, 4);
            this.button4.Margin = new System.Windows.Forms.Padding(4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(84, 57);
            this.button4.TabIndex = 3;
            this.button4.Text = "/";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.buttonOperationClick);
            this.button4.Paint += new System.Windows.Forms.PaintEventHandler(this.button24_Paint);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Transparent;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Location = new System.Drawing.Point(184, 4);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(84, 57);
            this.button3.TabIndex = 2;
            this.button3.Text = "√";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.buttonOperationClick);
            this.button3.Paint += new System.Windows.Forms.PaintEventHandler(this.button24_Paint);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(92, 4);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(84, 57);
            this.button2.TabIndex = 1;
            this.button2.Text = "Х²";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.buttonOperationClick);
            this.button2.Paint += new System.Windows.Forms.PaintEventHandler(this.button24_Paint);
            // 
            // TextResult
            // 
            this.TextResult.AutoSize = true;
            this.TextResult.BackColor = System.Drawing.Color.Transparent;
            this.TextResult.Font = new System.Drawing.Font("Monotype Corsiva", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextResult.Location = new System.Drawing.Point(299, 93);
            this.TextResult.Name = "TextResult";
            this.TextResult.Size = new System.Drawing.Size(0, 24);
            this.TextResult.TabIndex = 0;
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.Color.Transparent;
            this.panel.Controls.Add(this.textHistory);
            this.panel.Location = new System.Drawing.Point(2, 26);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(363, 54);
            this.panel.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Location = new System.Drawing.Point(2, 86);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(363, 54);
            this.panel2.TabIndex = 4;
            // 
            // textHistory
            // 
            this.textHistory.AutoSize = true;
            this.textHistory.Font = new System.Drawing.Font("Monotype Corsiva", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textHistory.Location = new System.Drawing.Point(297, 14);
            this.textHistory.Name = "textHistory";
            this.textHistory.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.textHistory.Size = new System.Drawing.Size(0, 24);
            this.textHistory.TabIndex = 5;
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.Transparent;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Location = new System.Drawing.Point(2, 162);
            this.button5.Margin = new System.Windows.Forms.Padding(4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(84, 57);
            this.button5.TabIndex = 24;
            this.button5.Text = "%";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.buttonOperationClick);
            // 
            // button10
            // 
            this.button10.BackColor = System.Drawing.Color.Transparent;
            this.button10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button10.Location = new System.Drawing.Point(94, 162);
            this.button10.Margin = new System.Windows.Forms.Padding(4);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(84, 57);
            this.button10.TabIndex = 25;
            this.button10.Text = "CE";
            this.button10.UseVisualStyleBackColor = false;
            this.button10.Click += new System.EventHandler(this.buttonClear);
            // 
            // button15
            // 
            this.button15.BackColor = System.Drawing.Color.Transparent;
            this.button15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button15.Location = new System.Drawing.Point(186, 162);
            this.button15.Margin = new System.Windows.Forms.Padding(4);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(84, 57);
            this.button15.TabIndex = 26;
            this.button15.Text = "C";
            this.button15.UseVisualStyleBackColor = false;
            this.button15.Click += new System.EventHandler(this.buttonClear);
            // 
            // button20
            // 
            this.button20.BackColor = System.Drawing.Color.Transparent;
            this.button20.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button20.Location = new System.Drawing.Point(278, 162);
            this.button20.Margin = new System.Windows.Forms.Padding(4);
            this.button20.Name = "button20";
            this.button20.Size = new System.Drawing.Size(84, 57);
            this.button20.TabIndex = 27;
            this.button20.Text = "<-";
            this.button20.UseVisualStyleBackColor = false;
            this.button20.Click += new System.EventHandler(this.buttonResetNumber);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Purple;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(368, 542);
            this.Controls.Add(this.button20);
            this.Controls.Add(this.button15);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.TextResult);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button24;
        private System.Windows.Forms.Button button23;
        private System.Windows.Forms.Button button22;
        private System.Windows.Forms.Button button21;
        private System.Windows.Forms.Button button19;
        private System.Windows.Forms.Button button18;
        private System.Windows.Forms.Button button17;
        private System.Windows.Forms.Button button16;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Label TextResult;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Label textHistory;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.Button button20;
    }
}

