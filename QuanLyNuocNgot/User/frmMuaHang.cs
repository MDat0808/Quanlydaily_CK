using QuanLyNuocNgot.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNuocNgot.User
{
    public partial class frmMuaHang : Form
    {
        private int userId;
        private HanldeData db = new HanldeData();
        public frmMuaHang(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            listView1.View = View.Details;
            listView1.Columns.Add("TenMatHang", 80);
            listView1.Columns.Add("SoLuong", 80);
            listView1.Columns.Add("TongTien", 100);
        }

        public void ResetListView()
        {
            listView1.Items.Clear(); // Remove all items
            listView1.Columns.Clear(); // Remove all columns

            listView1.View = View.Details;
            listView1.Columns.Add("TenMatHang", 80);
            listView1.Columns.Add("SoLuong", 80);
            listView1.Columns.Add("TongTien", 100);
        }
        private void LoadForm()
        {
            this.dgvMatHang.DataSource = db.getMatHangs();
            comboBox1.DataSource = db.getLoaiMatHang();
            comboBox1.DisplayMember = "TenLoai"; 
            comboBox1.ValueMember = "MaLoai";
        }

     
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string maLoai =  comboBox1.SelectedValue.ToString();
            dgvMatHang.DataSource = db.getMatHangTheoMaLoai(maLoai);
        }

        private void frmMuaHang_Load(object sender, EventArgs e)
        {
            LoadForm();
         
        }

        private void btnChiTietSach_Click(object sender, EventArgs e)
        {
            double total = 0;
            bool isInsert = true;
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
            foreach (ListViewItem item in listView1.Items)
            {
                    double currentPrice = double.Parse(item.SubItems[2].Text);
                    total += currentPrice;
              
            }

            if (total > 0)
            {
                DialogResult dr = MessageBox.Show($"Tổng tiền cần thanh toán {total} ", "Thanh toán", MessageBoxButtons.YesNo,
              MessageBoxIcon.None);
                if (dr == DialogResult.Yes)
                {
                    string sql = $"insert into hoadon (MaNguoiDung,TongTien, NgayLapHoaDon) values('{userId}', '{total}', '{currentDate}' )";
                
                    int newId = db.createHoaDon(sql);
                    if (newId >= 0)
                    {
                        foreach (ListViewItem item in listView1.Items)
                        {
                            double currentPrice = double.Parse(item.SubItems[2].Text);
                            int currentQuantity = int.Parse(item.SubItems[1].Text);
                            string tenMh = item.Text;
                            string insertCTHD = $"insert into chitiethoadon (MaHD,TenMatHang, SoLuong, ThanhTien) values ('{newId}', '{tenMh}', '{currentQuantity}', '{currentPrice}') ";
                            int isCheck = db.Sql(insertCTHD);
                            if (isCheck >= 0)
                            {
                                continue;
                            } else
                            {
                                isInsert = false;
                            }

                        }

                        if (isInsert)
                        {
                            MessageBox.Show("Thanh toán thành công." + newId);
                            ResetListView();
                            
                        } else
                        {
                            MessageBox.Show("Mua thất bại, lỗi thêm dữ liệu.");
                        }


                    }
                    else
                    {
                        MessageBox.Show("Mua thất bại.");
                    }
                }
                
            } else
            {
                MessageBox.Show("Bạn chưa chọn mặt hàng nào.");
            }


        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int r = dgvMatHang.CurrentCell.RowIndex;
            double sum = 0;
            if (dgvMatHang.SelectedRows.Count < 1)
            {
                MessageBox.Show("Vui lòng chọn mặt hàng cần thêm.");
            }
            else
            {
                string tenMH = dgvMatHang.Rows[r].Cells["TenMatHang"].Value.ToString();
                double giaMH = double.Parse(dgvMatHang.Rows[r].Cells["Gia"].Value.ToString());
                bool itemExists = false;

                foreach (ListViewItem item in listView1.Items)
                {
                    if (item.Text == tenMH)
                    {
                        int currentQuantity = int.Parse(item.SubItems[1].Text);
                        double currentPrice = double.Parse(item.SubItems[2].Text);
                        item.SubItems[1].Text = (currentQuantity + 1).ToString();
                        item.SubItems[2].Text = (currentPrice + giaMH).ToString();
                        itemExists = true;
                        break;
                    }
                }

                if (!itemExists)
                {
                    ListViewItem newItem = new ListViewItem(tenMH);
                    newItem.SubItems.Add("1");
                    newItem.SubItems.Add(giaMH.ToString());
                    listView1.Items.Add(newItem);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                listView1.Items.Remove(listView1.SelectedItems[0]);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn mặt hàng cần xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadForm();
        }
    }
}
