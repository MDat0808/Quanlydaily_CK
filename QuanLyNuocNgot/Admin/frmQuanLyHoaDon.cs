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
    public partial class frmQuanLyHoaDon : Form
    {
        private HanldeData db = new HanldeData();
        public frmQuanLyHoaDon()
        {
            InitializeComponent();
            LoadHoaDon();
            btnLuu.Enabled = false;
        }

        private void LoadHoaDon()
        {
            dgvHoaDon.DataSource = db.getHoaDons();

        }

        private void ResetVariable()
        {
            txtGia.Text = "";
            txtNgayLap.Text = "";
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtMaHoaDon.Text))
            {
                MessageBox.Show("Vui lòng nhập mã hóa đơn.");
            }
            else
            {
                DataTable data = db.getHoaDonTheoMaHD(txtMaHoaDon.Text);
                if (data.Rows.Count <= 0)
                {
                    MessageBox.Show("Mã hóa đơn không hợp lệ.");
                }
                else
                {
                    dgvHoaDon.DataSource = data;
                }
                txtMaHoaDon.Text = "";
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvHoaDon.SelectedRows.Count < 1)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần sửa.");
            }
            else
            {
                btnLuu.Enabled = true;
                int r = this.dgvHoaDon.CurrentCell.RowIndex;
                string maHD = this.dgvHoaDon.Rows[r].Cells[0].Value.ToString();
                double gia = double.Parse(this.dgvHoaDon.Rows[r].Cells["TongTien"].Value.ToString());
                DateTime ngayLapHD = (DateTime)(this.dgvHoaDon.Rows[r].Cells["NgayLapHoaDon"].Value);
                string date = ngayLapHD.ToString("yyyy-MM-dd");
                txtGia.Text = gia.ToString();
                txtNgayLap.Text = date;

            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            int r = this.dgvHoaDon.CurrentCell.RowIndex;
            string maHD = this.dgvHoaDon.Rows[r].Cells[0].Value.ToString();
            string sql = $"Update hoadon set NgayLapHoaDon = '{txtNgayLap.Text}', TongTien = '{txtGia.Text}' where MaHoaDon = '{maHD}' ";
            int check = db.Sql(sql);
            if (check >= 0)
            {
                MessageBox.Show("Cập nhật hóa đơn thành công.");
                LoadHoaDon();
            }
            else
            {
                MessageBox.Show("Cập nhật hóa đơn thất bại.");

            }
            ResetVariable();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvHoaDon.SelectedRows.Count < 1)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần xóa.");
            }
            else
            {
                int r = this.dgvHoaDon.CurrentCell.RowIndex;
                string maHD = this.dgvHoaDon.Rows[r].Cells[0].Value.ToString();
                DialogResult dr = MessageBox.Show("Bạn có chắc muốn xóa hóa đơn này không?", "Xóa hóa đơn", MessageBoxButtons.YesNo,
              MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    string sql = $"Delete from hoadon where MaHoaDon = '{maHD}'";
                    int check = db.Sql(sql);
                    if (check >= 0)
                    {
                        MessageBox.Show("Xóa thành công.");
                        LoadHoaDon();

                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại thử lại sau.");
                    }
                }

            }
        }
    }
}
