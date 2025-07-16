using System.Security.Policy;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Text.Json;

namespace maimaiAddress
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private List<Info> infoMai;
        private List<Info> infoChi;

        /// <summary>
        /// 简称转全称，json中为简称
        /// </summary>
        private readonly Dictionary<string, string> provinceMap = new Dictionary<string, string>
        {
            ["北京市"] = "北京",
            ["天津市"] = "天津",
            ["河北省"] = "河北",
            ["山西省"] = "山西",
            ["内蒙古自治区"] = "内蒙古",
            ["辽宁省"] = "辽宁",
            ["吉林省"] = "吉林",
            ["黑龙江省"] = "黑龙江",
            ["上海市"] = "上海",
            ["江苏省"] = "江苏",
            ["浙江省"] = "浙江",
            ["安徽省"] = "安徽",
            ["福建省"] = "福建",
            ["江西省"] = "江西",
            ["山东省"] = "山东",
            ["河南省"] = "河南",
            ["湖北省"] = "湖北",
            ["湖南省"] = "湖南",
            ["广东省"] = "广东",
            ["广西壮族自治区"] = "广西",
            ["海南省"] = "海南",
            ["重庆市"] = "重庆",
            ["四川省"] = "四川",
            ["贵州省"] = "贵州",
            ["云南省"] = "云南",
            ["西藏自治区"] = "西藏",
            ["陕西省"] = "陕西",
            ["甘肃省"] = "甘肃",
            ["青海省"] = "青海",
            ["宁夏回族自治区"] = "宁夏",
            ["新疆维吾尔自治区"] = "新疆",
        };

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            // 获取当前行号（从 1 开始）
            string rowIndex = (e.RowIndex + 1).ToString();

            // 获取绘制范围的矩形（行头区域）
            Rectangle rowHeaderBounds = new Rectangle(
                e.RowBounds.Left,
                e.RowBounds.Top,
                dataGridView1.RowHeadersWidth,
                e.RowBounds.Height);

            // 居中绘制行号
            using (StringFormat stringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            using (Brush brush = new SolidBrush(dataGridView1.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString(rowIndex, dataGridView1.Font, brush, rowHeaderBounds, stringFormat);
            }
        }

        /// <summary>
        /// 从华立获取数据
        /// </summary>
        /// <returns></returns>
        private async Task getInfomation()
        {
            // URL
            string urlMai = "https://wc.wahlap.net/maidx/rest/location";
            string urlChi = "https://wc.wahlap.net/chunithm/rest/location";

            try
            {
                using HttpClient client = new HttpClient();
                var gamePlaceMai = await client.GetStringAsync(urlMai);
                var gamePlaceChi = await client.GetStringAsync(urlChi);
                List<Info>? maiData = JsonSerializer.Deserialize<List<Info>>(gamePlaceMai);
                List<Info>? chiData = JsonSerializer.Deserialize<List<Info>>(gamePlaceChi);
                if (maiData != null)
                    infoMai = maiData;
                else Thread.Sleep(1000);

                if (chiData != null)
                    infoChi = chiData;
                else Thread.Sleep(1000);

            }
            catch (Exception) { MessageBox.Show("未到获取信息！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        /// <summary>
        /// 用于生成表格和表格中的内容
        /// </summary>
        /// <param name="data">传入表格中的数据源</param>
        private void makeTable(List<Info> data)
        {
            if (data == null) { 
                // 防止加载图形化界面报错
                MessageBox.Show("请稍等", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); 
            }
            else
            {
                // 展示列表显示的内容
                var filtered = data.Select(x => new InfoDisplay
                {
                    //省份 = x.province,
                    店铺名称 = x.arcadeName,
                    地址 = x.address,
                    机台数量 = x.machineCount
                }).ToList();

                dataGridView1.DataSource = filtered;

                // 自动调整列宽和行高
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

                mallCount.Text = dataGridView1.Rows.Count.ToString();
            }
        }

        private void main()
        {
            string selectedProvince = place.Text;

            //if (info != null)
            {
                if (place.Text == "所有")
                {
                    if (gameChoose.Text == "maimai でらっくす")
                        makeTable(infoMai);
                    else makeTable(infoChi);
                }
                else if (provinceMap.TryGetValue(selectedProvince, out string shortName))
                {
                    if (gameChoose.Text == "maimai でらっくす")
                    {
                        var filtered = infoMai.Where(x => x.province == shortName).ToList();
                        makeTable(filtered);
                    }
                    else
                    {
                        var filtered = infoChi.Where(x => x.province == shortName).ToList();
                        makeTable(filtered);
                    }
                }
                else
                {
                    MessageBox.Show("未知的省份选择：" + selectedProvince);
                }
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show($"{place.SelectedIndex = 0}");
            main();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bool flag = true;
            _ = getInfomation();
            while (flag) {
                if (infoMai != null && infoChi != null)
                {
                    search.PerformClick();
                    flag = false;
                }
                else { 
                    Thread.Sleep(1000);
                    main();
                }
            }
        }

    }
}
