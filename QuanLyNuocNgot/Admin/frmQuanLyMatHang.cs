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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QuanLyNuocNgot.Admin
{
    public partial class frmQuanLyMatHang : Form
    {
        private HanldeData db = new HanldeData();

        public frmQuanLyMatHang()
        {
            InitializeComponent();
            LoadMatHang();
            cbbLoaiMH.DataSource = db.getLoaiMatHang();
            cbbLoaiMH.DisplayMember = "TenLoai";
            cbbLoaiMH.ValueMember = "MaLoai";
        }

       

        private void LoadMatHang()
        {
            dgvMatHang.DataSource = db.getMatHangs();
            btnLuu.Enabled = false;

        }

        private void ResetVariable()
        {

            txtTenMH.Text = "";
            txtGia.Text = "";
            txtSoLuong.Text = "";
            btnLuu.Enabled = false;
        }
        private void dgvMatHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {

                string sql = $"insert into mathang (TenMatHang,  SoLuong,  Gia, MaLoaiMatHang ) values ('{txtTenMH.Text}', '{txtSoLuong.Text}', '{txtGia.Text}', '{cbbLoaiMH.SelectedValue.ToString()}') ";
                int check = db.Sql(sql);
                if (check >= 0)
                {
                    MessageBox.Show("Tạo mới mặt hàng thành công.");
                    ResetVariable();
                    LoadMatHang();
                }
                else
                {
                    MessageBox.Show("Tạo mới mặt hàng  thất bại.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int r = this.dgvMatHang.CurrentCell.RowIndex;
            string maMH = this.dgvMatHang.Rows[r].Cells[0].Value.ToString();
            DialogResult dr = MessageBox.Show("Bạn có chắc muốn xóa mặt hàng này không?", "Xóa mặt hàng", MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                string sql = $"Delete from mathang where MaMatHang = '{maMH}'";
                int check = db.Sql(sql);
                if (check >= 0)
                {
                    MessageBox.Show("Xóa thành công.");
                    LoadMatHang();

                }
                else
                {
                    MessageBox.Show("Xóa thất bại thử lại sau.");
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            int r = dgvMatHang.CurrentCell.RowIndex;
            if (dgvMatHang.SelectedRows.Count < 1)
            {
                MessageBox.Show("Vui lòng chọn mặt hàng cần sửa.");
            }
            else
            {
                btnThem.Enabled = false;
                btnLuu.Enabled = true;
                txtSoLuong.Text = dgvMatHang.Rows[r].Cells["SoLuong"].Value.ToString();
                txtTenMH.Text = dgvMatHang.Rows[r].Cells["TenMatHang"].Value.ToString();
                txtGia.Text = dgvMatHang.Rows[r].Cells["Gia"].Value.ToString();
                cbbLoaiMH.Enabled = false;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            int r = dgvMatHang.CurrentCell.RowIndex;
            btnThem.Enabled = true;
            string maMH = dgvMatHang.Rows[r].Cells["MaMatHang"].Value.ToString();
            string sql = $"update mathang set TenMatHang ='{txtTenMH.Text}', SoLuong = '{txtSoLuong.Text}', Gia = '{txtGia.Text}' where MaMatHang = '{maMH}' ";
            int check = db.Sql(sql);
            if (check >= 0)
            {
                MessageBox.Show("Cập nhật dữ liệu thành công.");
                LoadMatHang();
            }
            else
            {
                MessageBox.Show("Cập nhật dữ liệu thất bại.");
            }
            ResetVariable();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadMatHang() ;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dgvMatHang.DataSource = db.getMatHangTheoTenLoai("Bia");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dgvMatHang.DataSource = db.getMatHangTheoTenLoai("Nước ngọt");

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtMaMH.Text))
            {
                MessageBox.Show("Vui lòng nhập mã hóa đơn.");
            }
            else
            {
                DataTable data = db.getMatHangTheoMaMH(txtMaMH.Text);
                if (data.Rows.Count <= 0)
                {
                    MessageBox.Show("Mã mặt hàng không hợp lệ.");
                }
                else
                {
                    dgvMatHang.DataSource = data;
                }
                txtMaMH.Text = "";
            }
          
        }
    }
}
