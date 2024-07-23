using QuanLyNuocNgot.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNuocNgot.Admin
{
    public partial class frmThongKeDoanhSo : Form
    {
        private HanldeData db = new HanldeData();
        
        public frmThongKeDoanhSo()
        {
            InitializeComponent();
            LoadNgayThangNam();
            LoadDoanhThu();
        }

        private void LoadDoanhThu()
        {
            DateTime now = DateTime.Now;

            int day = now.Day;
            int currentDay = day;
            int currentMonth = now.Month;
            int currentYear = now.Year;
            dataGridView1.DataSource = db.getChiTietDoanhThu(currentDay, currentMonth , currentYear);

            double totalTongTien = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["TongTien"].Value != null)
                {
                    totalTongTien += Convert.ToDouble(row.Cells["TongTien"].Value);
                }
            }

            txtTotal.Text = totalTongTien.ToString() + "VNĐ";
        }

        private void LoadNgayThangNam()
        {
            this.cbbNam.Items.Add("");
            this.cbbThang.Items.Add("");
            this.cbbNgay.Items.Add("");
            for (int i = 2024; i <= 2025; i++)
            {
                this.cbbNam.Items.Add(i);
            }

            for (int i = 1; i <= 12; i++)
            {
                this.cbbThang.Items.Add(i);
            }

            for (int i = 1; i <= 31; i++)
            {
                this.cbbNgay.Items.Add(i);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void frmThongKeDoanhSo_Load(object sender, EventArgs e)
        {

        }

        private void btnThayDoi_Click(object sender, EventArgs e)
        {
            int ngay = cbbNgay.Text == "" ? 0 : int.Parse(cbbNgay.Text);
            int thang = cbbThang.Text == "" ? 0: int.Parse(cbbThang.Text);
            int nam = cbbNam.Text == "" ? 0 : int.Parse (cbbNam.Text);

            dataGridView1.DataSource = db.getChiTietDoanhThu(ngay, thang, nam);

            double totalTongTien = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["TongTien"].Value != null)
                {
                    totalTongTien += Convert.ToDouble(row.Cells["TongTien"].Value);
                }
            }

            txtTotal.Text = totalTongTien.ToString() + "VNĐ";
        }
    }
}
