namespace maimaiAddress
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            place = new ComboBox();
            dataGridView1 = new DataGridView();
            search = new Button();
            panel1 = new Panel();
            gameChoose = new ComboBox();
            showText = new Label();
            mallCount = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // place
            // 
            place.DisplayMember = "所有";
            place.FormattingEnabled = true;
            place.Items.AddRange(new object[] { "所有", "北京市", "天津市", "河北省", "山西省", "内蒙古自治区", "辽宁省", "吉林省", "黑龙江省", "上海市", "江苏省", "浙江省", "安徽省", "福建省", "江西省", "山东省", "河南省", "湖北省", "湖南省", "广东省", "广西壮族自治区", "海南省", "重庆市", "四川省", "贵州省", "云南省", "西藏自治区", "陕西省", "甘肃省", "青海省", "宁夏回族自治区", "新疆维吾尔自治区" });
            place.Location = new Point(170, 2);
            place.Name = "place";
            place.Size = new Size(121, 25);
            place.TabIndex = 0;
            place.Text = "所有";
            place.ValueMember = "所有";
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = SystemColors.Window;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 30);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(800, 420);
            dataGridView1.TabIndex = 1;
            dataGridView1.RowPostPaint += dataGridView1_RowPostPaint;
            // 
            // search
            // 
            search.Location = new Point(349, 2);
            search.Name = "search";
            search.Size = new Size(75, 25);
            search.TabIndex = 2;
            search.Text = "查询";
            search.UseVisualStyleBackColor = true;
            search.Click += button1_Click;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Window;
            panel1.Controls.Add(gameChoose);
            panel1.Controls.Add(showText);
            panel1.Controls.Add(mallCount);
            panel1.Controls.Add(place);
            panel1.Controls.Add(search);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 30);
            panel1.TabIndex = 3;
            // 
            // gameChoose
            // 
            gameChoose.DisplayMember = "maimai でらっくす";
            gameChoose.FormattingEnabled = true;
            gameChoose.Items.AddRange(new object[] { "maimai でらっくす", "chunithm" });
            gameChoose.Location = new Point(12, 3);
            gameChoose.Name = "gameChoose";
            gameChoose.Size = new Size(136, 25);
            gameChoose.TabIndex = 4;
            gameChoose.Text = "maimai でらっくす";
            gameChoose.ValueMember = "maimai でらっくす";
            // 
            // showText
            // 
            showText.AutoSize = true;
            showText.Location = new Point(530, 6);
            showText.Name = "showText";
            showText.Size = new Size(92, 17);
            showText.TabIndex = 3;
            showText.Text = "当地店铺数量：";
            // 
            // mallCount
            // 
            mallCount.AutoSize = true;
            mallCount.Location = new Point(619, 6);
            mallCount.Name = "mallCount";
            mallCount.Size = new Size(40, 17);
            mallCount.TabIndex = 3;
            mallCount.Text = "count";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dataGridView1);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "舞萌DX/中二节奏机台查询";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ComboBox place;
        private DataGridView dataGridView1;
        private Button search;
        private Panel panel1;
        private Label showText;
        private Label mallCount;
        private ComboBox gameChoose;
    }
}
