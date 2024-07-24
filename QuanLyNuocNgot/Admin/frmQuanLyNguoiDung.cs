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
    public partial class frmQuanLyNguoiDung : Form
    {
        private Model.HanldeData db = new Model.HanldeData();
        public frmQuanLyNguoiDung()
        {
            InitializeComponent();
            LoadNguoiDung();
            txtSDT.KeyPress += txtSDT_KeyPress;
            txtSDT.TextChanged += txtSDT_TextChanged;
        }

        private void ResetVariable()
        {

            txtHoTen.Text = "";
            txtEmail.Text = "";
            txtDiaChi.Text = "";
            txtMatKhau.Text = "";
            txtQuyen.Text = "";
            txtTaiKhoan.Text = "";
            txtSDT.Text = "";
            btnLuu.Enabled = false;
            txtTaiKhoan.Enabled = true;
            txtMatKhau.Enabled = false;
        }

        private void LoadNguoiDung()
        {
            dgvUser.DataSource = db.getUsers();
            btnLuu.Enabled = false ;
        }

        private void frmQuanLyNguoiDung_Load(object sender, EventArgs e)
        {

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Vui lòng nhập mã người dùng");
            }
            else
            {
                DataTable data = db.getUserById(int.Parse(txtId.Text));
                if (data.Rows.Count <= 0)
                {
                    MessageBox.Show("Mã người dùng không hợp lệ.");
                }
                else
                {
                    dgvUser.DataSource = data;
                }
                txtId.Text = "";
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            int r = dgvUser.CurrentCell.RowIndex;
            if (dgvUser.SelectedRows.Count < 1)
            {
                MessageBox.Show("Vui lòng chọn người dùng cần sửa.");

            }
            else
            {
                txtHoTen.Text = dgvUser.Rows[r].Cells["HoTen"].Value.ToString();
                txtSDT.Text = dgvUser.Rows[r].Cells["sdt"].Value.ToString();
                txtDiaChi.Text = dgvUser.Rows[r].Cells["DiaChi"].Value.ToString();
                txtEmail.Text = dgvUser.Rows[r].Cells["email"].Value.ToString();
                txtMatKhau.Text = db.MD5Hash( dgvUser.Rows[r].Cells["MatKhau"].Value.ToString());
                txtQuyen.Text = dgvUser.Rows[r].Cells["QuyenTruyCap"].Value.ToString();
                txtTaiKhoan.Text = dgvUser.Rows[r].Cells["TaiKhoan"].Value.ToString();
                txtTaiKhoan.Enabled = false;
                txtMatKhau.Enabled = false;
                btnThem.Enabled = false;
                btnLuu.Enabled = true;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            int r = dgvUser.CurrentCell.RowIndex;
            if (txtMatKhau.Text.Length < 8) 
            {
                MessageBox.Show("Mật khẩu phải ít nhất 8 ký tự.");
            } else
            {
                string id = dgvUser.Rows[r].Cells["MaNguoiDung"].Value.ToString();
                string sql = $"update user set HoTen ='{txtHoTen.Text}', DiaChi = '{txtDiaChi.Text}', email = '{txtEmail.Text}', QuyenTruyCap = '{txtQuyen.Text}', SDT = '{txtSDT.Text}' where MaNguoiDung = '{id}' ";
                int check = db.Sql(sql);
                if (check >= 0)
                {
                    btnThem.Enabled = true;
                    MessageBox.Show("Cập nhật dữ liệu thành công.");
                    LoadNguoiDung();
                    ResetVariable();
                }
                else
                {
                    MessageBox.Show("Cập nhật dữ liệu thất bại.");
                }

            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtHoTen.Text) || string.IsNullOrEmpty(txtDiaChi.Text) || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtTaiKhoan.Text) || string.IsNullOrEmpty(txtMatKhau.Text) || string.IsNullOrEmpty(txtSDT.Text) )
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
                }
                 else
                {
                string sql = $"insert into user (HoTen, DiaChi, email, QuyenTruyCap, TaiKhoan, MatKhau , SDT) values ('{txtHoTen.Text}', '{txtDiaChi.Text}', '{txtEmail.Text}', '{txtQuyen.Text}', '{txtTaiKhoan.Text}', '{txtMatKhau.Text}' ,'{txtSDT.Text}') ";
                int check = db.Sql(sql);
                if (check >= 0)
                {
                    MessageBox.Show("Tạo mới người dùng thành công.");
                    ResetVariable();
                    LoadNguoiDung();
                }
                else
                {
                    MessageBox.Show("Tạo mới người dùng thất bại.");
                }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int r = this.dgvUser.CurrentCell.RowIndex;
            string id = this.dgvUser.Rows[r].Cells[0].Value.ToString();
            DialogResult dr = MessageBox.Show("Bạn có chắc muốn xóa người dùng này không?", "Xóa người dùng", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                string sql = $"Delete  from user  where MaNguoiDung = '{id}'";
                int check = db.Sql(sql);
                if (check >= 0)
                {
                    MessageBox.Show("Xóa thành công.");
                    LoadNguoiDung();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại thử lại sau.");
                }
            }
        }
        // Validate sdt
        private void txtSDT_TextChanged(object sender, EventArgs e)
        {
            if (txtSDT.Text.Length > 8)
            {
                txtSDT.Text = txtSDT.Text.Substring(0, 8);
                txtSDT.SelectionStart = txtSDT.Text.Length;
            }
        }

        private void txtSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; 
            }
        }
    }
}
