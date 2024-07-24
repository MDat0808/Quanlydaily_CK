create database dbquanlydaily;
USE dbquanlydaily;

CREATE TABLE user (	`MaNguoiDung` INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    TaiKhoan VARCHAR(50) not null unique ,
    MatKhau VARCHAR(50) NOT NULL,
     `HoTen` VARCHAR(50) NOT NULL,
         `email` VARCHAR(100) NOT NULL,
         	DiaChi varchar(100) not null ,
	SDT varchar(20) not null ,
    QuyenTruyCap INT default 1 NOT NULL,
     CONSTRAINT fk_quyen FOREIGN KEY (QuyenTruyCap) REFERENCES quyentruycap(MaQuyen)
);

create table quyentruycap (
	MaQuyen INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Quyen varchar(20) default 'user' not null
);

create table loaimathang (
	`MaLoai` INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	TenLoai varchar(100) not null 
);

create table mathang (
	`MaMatHang` INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    SoLuong int not null,
	TenMatHang varchar(100) not null ,
    Gia float not null,
   MaLoaiMatHang int not null,
   CONSTRAINT fk_1
    FOREIGN KEY (MaLoaiMatHang) REFERENCES LoaiMatHang(MaLoai)
);

CREATE TABLE hoadon (
  MaHoaDon INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  MaNguoiDung INT NOT NULL,
  NgayLapHoaDon DATE NOT NULL ,
  TongTien FLOAT NOT NULL,
  CONSTRAINT fk_hoadon_nguoidung FOREIGN KEY (MaNguoiDung) REFERENCES user(MaNguoiDung)
);

create table chitiethoadon (
	  MaChiTietHD INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	MaHD int not null,
	TenMatHang	varchar(50) not null,
    SoLuong int not null,
    ThanhTien int not null,
      CONSTRAINT fk_hd
    FOREIGN KEY (MaHD) REFERENCES hoadon(MaHoaDon) 
);



INSERT INTO loaimathang (`MaLoai`, `TenLoai`) VALUES ('1', 'Bia');
INSERT INTO `loaimathang` (`MaLoai`, `TenLoai`) VALUES ('2', 'Nước ngọt');


INSERT INTO `mathang` (`MaMatHang`, `SoLuong`, `TenMatHang`, `Gia`, `MaLoaiMatHang`) VALUES ('1', '99', 'Bia 333', '10000', '1');
INSERT INTO `mathang` (`MaMatHang`, `SoLuong`, `TenMatHang`, `Gia`, `MaLoaiMatHang`) VALUES ('2', '100', 'Bia Việt', '12000', '1');
INSERT INTO `mathang` (`MaMatHang`, `SoLuong`, `TenMatHang`, `Gia`, `MaLoaiMatHang`) VALUES ('3', '98', 'Sting', '9000', '2');
INSERT INTO `mathang` (`MaMatHang`, `SoLuong`, `TenMatHang`, `Gia`, `MaLoaiMatHang`) VALUES ('4', '90', 'Pepsi', '11000', '2');
INSERT INTO `user` (`MaNguoiDung`, `TaiKhoan`, `MatKhau`, `HoTen`, `email`, `DiaChi`, `SDT`, `QuyenTruyCap`) VALUES ('1', 'admin', 'admin', 'Minh Đạt', 'admin@test.com', 'Tân Bình', '09000009', 2);
INSERT INTO `user` (`MaNguoiDung`, `TaiKhoan`, `MatKhau`, `HoTen`, `email`, `DiaChi`, `SDT`, `QuyenTruyCap`) VALUES ('2', 'user1', '1234', 'Nguyễn Văn A', 'user@test.com', 'Bình Thuận', '09009099', 1);
select TenMatHang,sum(SoLuong) as SoLuong , sum(ThanhTien) as TongTien 
from chitiethoadon cthd join hoadon hd on cthd.MaHD = hd.MaHoaDon
where day(hd.ngayLapHoaDon) = DAY(NOW())	
 group by cthd.tenmathang ;