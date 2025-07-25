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
        /// 酒各廬畠各��json嶄葎酒各
        /// </summary>
        private readonly Dictionary<string, string> provinceMap = new Dictionary<string, string>
        {
            ["臼奨偏"] = "臼奨",
            ["爺薯偏"] = "爺薯",
            ["采臼福"] = "采臼",
            ["表廉福"] = "表廉",
            ["坪檀硬徭嵶曝"] = "坪檀硬",
            ["蘇逓福"] = "蘇逓",
            ["耳爽福"] = "耳爽",
            ["菜霜臭福"] = "菜霜臭",
            ["貧今偏"] = "貧今",
            ["臭釦福"] = "臭釦",
            ["寃臭福"] = "寃臭",
            ["芦師福"] = "芦師",
            ["牽秀福"] = "牽秀",
            ["臭廉福"] = "臭廉",
            ["表叫福"] = "表叫",
            ["采掴福"] = "采掴",
            ["刷臼福"] = "刷臼",
            ["刷掴福"] = "刷掴",
            ["鴻叫福"] = "鴻叫",
            ["鴻廉彝怛徭嵶曝"] = "鴻廉",
            ["今掴福"] = "今掴",
            ["嶷伯偏"] = "嶷伯",
            ["膨寒福"] = "膨寒",
            ["酷巒福"] = "酷巒",
            ["堝掴福"] = "堝掴",
            ["廉茄徭嵶曝"] = "廉茄",
            ["病廉福"] = "病廉",
            ["己摩福"] = "己摩",
            ["楳今福"] = "楳今",
            ["逓歪指怛徭嵶曝"] = "逓歪",
            ["仟舟略令櫛徭嵶曝"] = "仟舟",
        };

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            // 資函輝念佩催�┫� 1 蝕兵��
            string rowIndex = (e.RowIndex + 1).ToString();

            // 資函紙崙袈律議裳侘��佩遊曝囃��
            Rectangle rowHeaderBounds = new Rectangle(
                e.RowBounds.Left,
                e.RowBounds.Top,
                dataGridView1.RowHeadersWidth,
                e.RowBounds.Height);

            // 肖嶄紙崙佩催
            using (StringFormat stringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            using (Brush brush = new SolidBrush(dataGridView1.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString(rowIndex, dataGridView1.Font, brush, rowHeaderBounds, stringFormat);
            }
        }

        /// <summary>
        /// 貫鯖羨資函方象
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
            catch (Exception) { MessageBox.Show("隆欺資函佚連��", "少御", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        /// <summary>
        /// 喘噐伏撹燕鯉才燕鯉嶄議坪否
        /// </summary>
        /// <param name="data">勧秘燕鯉嶄議方象坿</param>
        private void makeTable(List<Info> data)
        {
            if (data == null) { 
                // 契峭紗墮夕侘晒順中烏危
                MessageBox.Show("萩不吉", "戻幣", MessageBoxButtons.OK, MessageBoxIcon.Information); 
            }
            else
            {
                // 婢幣双燕�塋承陳敞�
                var filtered = data.Select(x => new InfoDisplay
                {
                    //福芸 = x.province,
                    糾凸兆各 = x.arcadeName,
                    仇峽 = x.address,
                    字岬方楚 = x.machineCount
                }).ToList();

                dataGridView1.DataSource = filtered;

                // 徭強距屁双錐才佩互
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
                if (place.Text == "侭嗤")
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
                    MessageBox.Show("隆岑議福芸僉夲��" + selectedProvince);
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
