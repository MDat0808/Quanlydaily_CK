using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Security.Principal;
using System.Collections;
using System.Security.Cryptography;

namespace QuanLyNuocNgot.Model
{
    internal class HanldeData
    {
        public MySqlConnection connection;

        public string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

      
        public void KetNoi()
        {
            if (connection == null)
            {
                connection = new MySqlConnection();
                connection.ConnectionString = $"server=localhost;port=3306;user=root;password=080804Dat;database=dbquanlydaily";
            }
            if (connection.State == System.Data.ConnectionState.Closed ||
                connection.State == System.Data.ConnectionState.Broken)
            {


                connection.Open();
            }
        }
        public void NgatKetNoi()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }
        // Thực hiện câu lệnh sql
        public int Sql(string SQL)
        {
            int rowsAffected = -1;

            try
            {
                KetNoi();
                MySqlCommand command = new MySqlCommand();
                command.Connection = connection;
                command.CommandText = SQL;
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error updating data: " + ex.Message);
            }
            finally
            {
                NgatKetNoi();
            }

            return rowsAffected;
        }

        public int dangky(string SQL)
        {
            int rowsAffected = -1;

            try
            {
                KetNoi();
                MySqlCommand command = new MySqlCommand();
                command.Connection = connection;
                command.CommandText = SQL;
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Tài khoản đã tồn tại.");
            }
            finally
            {
                NgatKetNoi();
            }

            return rowsAffected;
        }

        // Lấy id
        public int getMaNguoiDung(string account, string pass)
        {
            int result = -1;
            try
            {
                KetNoi();
                MySqlCommand command = new MySqlCommand();
                command.Connection = connection;
                string SQL =  $"SELECT MaNguoiDung FROM user WHERE TaiKhoan = '{account}' and MatKhau = '{pass}'";

                command.CommandText = SQL;
                object user = command.ExecuteScalar();
                if (user != null)
                {
                    int id = int.Parse(user.ToString());
                    return id;

                }
                else
                {
                    return -1;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error updating data: " + ex.Message);
            }
            finally
            {
                NgatKetNoi();
            }

            return result;
        }


        public int Login(string account , string pass)
        {
            int result = -1;
            string role;
            try
            {
                KetNoi();
                MySqlCommand command = new MySqlCommand();
                command.Connection = connection;
                string SQL = $"SELECT Quyen FROM user u join QuyenTruyCap q on q.MaQuyen = u.QuyenTruyCap  WHERE TaiKhoan = '{account}' and MatKhau = '{pass}'";
                command.CommandText = SQL;
                object user = command.ExecuteScalar();
                if (user != null)
                {
                    role = user.ToString();
                    if (role == "admin")
                    {
                        return 1;
                    }
                    else 
                    {
                        return 0;
                    }

                }
                else
                {
                    return -1;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error updating data: " + ex.Message);
            }
            finally
            {
                NgatKetNoi();
            }

            return result;
        }

     
        public DataTable getUserById(int id)
        {
            DataTable dt = new DataTable();
            try
            {
                KetNoi();

                string SQL = $"Select * from user where MaNguoiDung = '{id}'";
                MySqlDataAdapter adapter = new MySqlDataAdapter(SQL, connection);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi lấy dữ liệu: " + ex.Message);
            }
            finally
            {
                NgatKetNoi();
            }
            return dt;
        }
        // Kiểm tra mật khẩu cũ đúng hay không
        public string getMatKhauById(int userId)
        {
            try
            {
                KetNoi();
                MySqlCommand command = new MySqlCommand();
                command.Connection = connection;
                string sql = $"Select MatKhau from user where MaNguoiDung = '{userId}'";

                command.CommandText = sql;
                object user = command.ExecuteScalar();
                if (user != null)
                {

                    return user.ToString();

                }

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error updating data: " + ex.Message);
            }
            finally
            {
                NgatKetNoi();
            }

            return null;
        }

        public DataTable getUsers()
        {
            DataTable dt = new DataTable();
            try
            {
                KetNoi();

                string SQL = $"Select * from user ";
                MySqlDataAdapter adapter = new MySqlDataAdapter(SQL, connection);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi lấy dữ liệu: " + ex.Message);
            }
            finally
            {
                NgatKetNoi();
            }
            return dt;
        }


        
        // Hanlde data MatHang

        public DataTable getMatHangs()
        {
            DataTable dt = new DataTable();
            try
            {
                KetNoi();

                string SQL = $"Select MaMatHang , TenMatHang, SoLuong, Gia from MatHang ";
                MySqlDataAdapter adapter = new MySqlDataAdapter(SQL, connection);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi lấy dữ liệu: " + ex.Message);
            }
            finally
            {
                NgatKetNoi();
            }
            return dt;
        }

        public DataTable getLoaiMatHang()
        {
            DataTable dt = new DataTable();
            try
            {
                KetNoi();

                string SQL = $"Select * from loaimathang ";
                MySqlCommand cmd = new MySqlCommand(SQL, connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader); 
                reader.Close(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lấy dữ liệu: " + ex.Message);
            }
            finally
            {
                NgatKetNoi();
            }
            return dt;
        }

        public DataTable getMatHangTheoMaMH(string maMH)
        {
            DataTable dt = new DataTable();
            try
            {
                KetNoi();

                string SQL = $"Select * from mathang  where MaMatHang = '{maMH}'";
                MySqlCommand cmd = new MySqlCommand(SQL, connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lấy dữ liệu: " + ex.Message);
            }
            finally
            {
                NgatKetNoi();
            }
            return dt;
        }


        public DataTable getMatHangTheoMaLoai(string maLoai)
        {
            DataTable dt = new DataTable();
            try
            {
                KetNoi();

                string SQL = $"Select * from mathang where MaLoaiMatHang = '{maLoai}' ";
                MySqlCommand cmd = new MySqlCommand(SQL, connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lấy dữ liệu: " + ex.Message);
            }
            finally
            {
                NgatKetNoi();
            }
            return dt;
        }

        public DataTable getMatHangTheoTenLoai(string loai)
        {
            DataTable dt = new DataTable();
            try
            {
                KetNoi();

                string SQL = $"Select mh.* from mathang mh join loaimathang l on MaLoai = mh.MaLoaiMatHang where l.TenLoai = '{loai}' ";
                MySqlCommand cmd = new MySqlCommand(SQL, connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lấy dữ liệu: " + ex.Message);
            }
            finally
            {
                NgatKetNoi();
            }
            return dt;
        }

        public DataTable getMatHangDaMua(int userId)
        {
            DataTable dt = new DataTable();
            try
            {
                KetNoi();

                string SQL = $"select TenMatHang,sum(SoLuong) as SoLuong , sum(ThanhTien) as TongTien from chitiethoadon cthd join hoadon hd on cthd.MaHD = hd.MaHoaDon join user u on u.MaNguoiDung = hd.MaNguoiDung where u.MaNguoiDung = '{userId}'  group by cthd.tenmathang ";
                MySqlCommand cmd = new MySqlCommand(SQL, connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lấy dữ liệu: " + ex.Message);
            }
            finally
            {
                NgatKetNoi();
            }
            return dt;
        }

        public DataTable getMatHangDaMuaTrongNgay(int userId)
        {
            DataTable dt = new DataTable();
            try
            {
                KetNoi();

                string SQL = $"select TenMatHang,sum(SoLuong) as SoLuong , sum(ThanhTien) as TongTien from chitiethoadon cthd join hoadon hd on cthd.MaHD = hd.MaHoaDon join user u on u.MaNguoiDung = hd.MaNguoiDung where u.MaNguoiDung = '{userId}' and Day(hd.ngayLapHoaDon) = Day(now()) group by cthd.tenmathang  ";
                MySqlCommand cmd = new MySqlCommand(SQL, connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lấy dữ liệu: " + ex.Message);
            }
            finally
            {
                NgatKetNoi();
            }
            return dt;
        }


        // Hanlde data HoaDon

        public int createHoaDon(string SQL)
        {
            int rowsAffected = -1;
            int newId;
            try
            {
                KetNoi();
                MySqlCommand command = new MySqlCommand();
                command.Connection = connection;
                command.CommandText = SQL;
                rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0) { 
                    newId = (int)command.LastInsertedId;
                    return newId;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error updating data: " + ex.Message);
            }
            finally
            {
                NgatKetNoi();
            }

            return -1;
        }


        public DataTable getHoaDons()
        {
            DataTable dt = new DataTable();
            try
            {
                KetNoi();

                string SQL = $"Select * from hoadon ";
                MySqlDataAdapter adapter = new MySqlDataAdapter(SQL, connection);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi lấy dữ liệu: " + ex.Message);
            }
            finally
            {
                NgatKetNoi();
            }
            return dt;
        }

        public DataTable getHoaDonTheoMaHD(string maHD)
        {
            DataTable dt = new DataTable();
            try
            {
                KetNoi();

                string SQL = $"Select * from hoadon where MaHoaDon = '{maHD}' ";
                MySqlDataAdapter adapter = new MySqlDataAdapter(SQL, connection);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi lấy dữ liệu: " + ex.Message);
            }
            finally
            {
                NgatKetNoi();
            }
            return dt;
        }

        public DataTable getChiTietDoanhThu(int ngay, int thang, int nam)
        {
            DataTable dt = new DataTable();
            string sql;
            try
            {
                KetNoi();
                if (nam <= 0)
                {
                    sql = $"select TenMatHang,sum(SoLuong) as SoLuong , sum(ThanhTien) as TongTien from chitiethoadon cthd join hoadon hd on cthd.MaHD = hd.MaHoaDon where day(hd.ngayLapHoaDon) = '{ngay}' and Month(hd.NgayLapHoaDon) = '{thang}' group by cthd.tenmathang";
                }
                else if (thang <= 0)
                {
                    sql = $"select TenMatHang,sum(SoLuong) as SoLuong , sum(ThanhTien) as TongTien from chitiethoadon cthd join hoadon hd on cthd.MaHD = hd.MaHoaDon where Year(hd.ngayLapHoaDon) = '{nam}' group by cthd.tenmathang";

                }
                else if (ngay <= 0)
                {
                    sql = $"select TenMatHang,sum(SoLuong) as SoLuong , sum(ThanhTien) as TongTien from chitiethoadon cthd join hoadon hd on cthd.MaHD = hd.MaHoaDon where Year(hd.ngayLapHoaDon) = '{nam}'  and Month(hd.ngayLapHoaDon) = '{thang}' group by cthd.tenmathang";
                }
                else
                {
                    sql = $"select TenMatHang,sum(SoLuong) as SoLuong , sum(ThanhTien) as TongTien from chitiethoadon cthd join hoadon hd on cthd.MaHD = hd.MaHoaDon where hd.ngayLapHoaDon = '{nam:0000}-{thang:00}-{ngay:00}' group by cthd.tenmathang";
                }

                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, connection);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi lấy dữ liệu: " + ex.Message);
            }
            finally
            {
                NgatKetNoi();
            }
            return dt;
        }

    }
}
