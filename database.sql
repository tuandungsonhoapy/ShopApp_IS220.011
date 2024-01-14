select MaNhanVien, TenNhanVien, SDT, count(MaNhanVien) as SoLanSua
from NHANVIEN join NV_BT on NHANVIEN.MaNhanVien = NV_BT.MaNhanVien
group by MaNhanVien, TenNhanVien, SDT
having count(MaNhanVien) >= 100;