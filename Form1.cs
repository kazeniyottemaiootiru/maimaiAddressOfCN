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
        /// ���תȫ�ƣ�json��Ϊ���
        /// </summary>
        private readonly Dictionary<string, string> provinceMap = new Dictionary<string, string>
        {
            ["������"] = "����",
            ["�����"] = "���",
            ["�ӱ�ʡ"] = "�ӱ�",
            ["ɽ��ʡ"] = "ɽ��",
            ["���ɹ�������"] = "���ɹ�",
            ["����ʡ"] = "����",
            ["����ʡ"] = "����",
            ["������ʡ"] = "������",
            ["�Ϻ���"] = "�Ϻ�",
            ["����ʡ"] = "����",
            ["�㽭ʡ"] = "�㽭",
            ["����ʡ"] = "����",
            ["����ʡ"] = "����",
            ["����ʡ"] = "����",
            ["ɽ��ʡ"] = "ɽ��",
            ["����ʡ"] = "����",
            ["����ʡ"] = "����",
            ["����ʡ"] = "����",
            ["�㶫ʡ"] = "�㶫",
            ["����׳��������"] = "����",
            ["����ʡ"] = "����",
            ["������"] = "����",
            ["�Ĵ�ʡ"] = "�Ĵ�",
            ["����ʡ"] = "����",
            ["����ʡ"] = "����",
            ["����������"] = "����",
            ["����ʡ"] = "����",
            ["����ʡ"] = "����",
            ["�ຣʡ"] = "�ຣ",
            ["���Ļ���������"] = "����",
            ["�½�ά���������"] = "�½�",
        };

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            // ��ȡ��ǰ�кţ��� 1 ��ʼ��
            string rowIndex = (e.RowIndex + 1).ToString();

            // ��ȡ���Ʒ�Χ�ľ��Σ���ͷ����
            Rectangle rowHeaderBounds = new Rectangle(
                e.RowBounds.Left,
                e.RowBounds.Top,
                dataGridView1.RowHeadersWidth,
                e.RowBounds.Height);

            // ���л����к�
            using (StringFormat stringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            using (Brush brush = new SolidBrush(dataGridView1.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString(rowIndex, dataGridView1.Font, brush, rowHeaderBounds, stringFormat);
            }
        }

        /// <summary>
        /// �ӻ�����ȡ����
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
            catch (Exception) { MessageBox.Show("δ����ȡ��Ϣ��", "����", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        /// <summary>
        /// �������ɱ��ͱ���е�����
        /// </summary>
        /// <param name="data">�������е�����Դ</param>
        private void makeTable(List<Info> data)
        {
            if (data == null) { 
                // ��ֹ����ͼ�λ����汨��
                MessageBox.Show("���Ե�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information); 
            }
            else
            {
                // չʾ�б���ʾ������
                var filtered = data.Select(x => new InfoDisplay
                {
                    //ʡ�� = x.province,
                    �������� = x.arcadeName,
                    ��ַ = x.address,
                    ��̨���� = x.machineCount
                }).ToList();

                dataGridView1.DataSource = filtered;

                // �Զ������п���и�
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
                if (place.Text == "����")
                {
                    if (gameChoose.Text == "maimai �Ǥ�ä���")
                        makeTable(infoMai);
                    else makeTable(infoChi);
                }
                else if (provinceMap.TryGetValue(selectedProvince, out string shortName))
                {
                    if (gameChoose.Text == "maimai �Ǥ�ä���")
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
                    MessageBox.Show("δ֪��ʡ��ѡ��" + selectedProvince);
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
