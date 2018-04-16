Imports DTO
Imports DAL
Imports DAL.DALDanhSachKhamBenh
Imports DAL.DALHoaDon
Imports System.Text.RegularExpressions
Public Class BLLHoaDon
    Public Shared Sub BLL_KhoiTaoHoaDon(mpkb As String, tienkham As Double)
        DAL_KhoiTaoHoaDon(mpkb, tienkham)
    End Sub
    Public Shared Function BLL_TaiHoaDon() As DataTable
        Return DAL_TaiHoaDon()
    End Function
    Public Shared Function BLL_TimKiemHoaDon(mahoadon As String, mabenhnhan As String, hoten As String, strtungay As String, strdenngay As String) As DataTable
        Return DAL_TimKiemHoaDon(mahoadon, mabenhnhan, hoten, strtungay, strdenngay)
    End Function
    Public Shared Function BLL_TaiHoaDonChoPhieuHoaDon(mpkb As String) As DataTable
        Return DAL_TaiHoaDonChoPhieuHoaDon(mpkb)
    End Function
    Public Shared Function BLL_LayGiaTriTienKham() As String
        Return DAL_LayGiaTriTienKham()
    End Function
    Public Shared Sub BLL_LuuHoaDonXuongBoNho(mpkb As String)
        DAL_LuuHoaDonXuongBoNho(mpkb)
    End Sub
    Public Shared Function BLL_TinhTongCongHoaDon(mpkb As String) As String
        Return DAL_TinhTongCongHoaDon(mpkb)
    End Function
    Public Shared Function BLL_LayThongTinBenhNhan(pkbHoTen As String, pkbCMND As String, pkbGioiTinh As String) As DataTable
        Return DAL_LayThongTinBenhNhan(pkbHoTen, pkbCMND, pkbGioiTinh)
    End Function
End Class
