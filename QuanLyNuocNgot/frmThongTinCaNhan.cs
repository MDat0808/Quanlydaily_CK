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

namespace QuanLyNuocNgot
{
    public partial class frmThongTinCaNhan : Form
    {
        private int userId;
        private HanldeData db = new HanldeData();
        public frmThongTinCaNhan(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            txtSDT.KeyPress += txtSDT_KeyPress_1;
            txtSDT.TextChanged += txtSDT_TextChanged_1;
            dgvMatHangDaMua.DataSource = db.getMatHangDaMua(userId);

        }

        private void frmThongTinCaNhan_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable information = db.getUserById(userId);
                DisplayInfo(information);
            }
            catch
            {
                MessageBox.Show("Bạn chưa có thông tin tài khoản");
            }
        }

        private void DisplayInfo(DataTable source)
        {
            this.txtTenDangNhap.Text = source.Rows[0]["TaiKhoan"].ToString();
            this.txtMatKhau.Text = source.Rows[0]["MatKhau"].ToString();
            this.txtHoVaTen.Text = source.Rows[0]["HoTen"].ToString();
            this.txtDiaChi.Text = source.Rows[0]["DiaChi"].ToString();
            this.txtEmail.Text = source.Rows[0]["Email"].ToString();
            this.txtSDT.Text = source.Rows[0]["SDT"].ToString();
        }

        private void txtTenDangNhap_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            frmChangePass frmChangePass = new frmChangePass(userId);
            frmChangePass.ShowDialog();
        }

        private void btnThayDoi_Click(object sender, EventArgs e)
        {
            string ten = this.txtHoVaTen.Text;
            string diachi = this.txtDiaChi.Text;
            string email = this.txtEmail.Text;
            string sdt = this.txtSDT.Text;

            DialogResult dr = MessageBox.Show("Bạn có chắc muốn cập nhật thông tin cá nhân không?", "Lưu", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                string sql = $"Update user set HoTen = '{ten}', DiaChi = '{diachi}', email = '{email}', sdt = '{sdt}'  where MaNGuoiDung = '{userId}'";
                int check = db.Sql(sql);
                if (check >= 0)
                {
                    MessageBox.Show("Lưu thành công thành công.");

                }
                else
                {
                    MessageBox.Show("Lưu thất bại.");
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Validate SDT
    
        private void txtSDT_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

     

        private void txtSDT_TextChanged_1(object sender, EventArgs e)
        {
            if (txtSDT.Text.Length > 8)
            {
                txtSDT.Text = txtSDT.Text.Substring(0, 8);
                txtSDT.SelectionStart = txtSDT.Text.Length;
            }
        }

        private void btnMatHangDaMua_Click(object sender, EventArgs e)
        {
            dgvMatHangDaMua.DataSource = db.getMatHangDaMua(userId);
        }

        private void btnMatHangMuaHomNay_Click(object sender, EventArgs e)
        {
            dgvMatHangDaMua.DataSource = db.getMatHangDaMuaTrongNgay(userId);
        }
    }
}
