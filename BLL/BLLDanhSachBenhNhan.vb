Imports DTO
Imports DAL
Imports DAL.DALDanhSachKhamBenh
Imports DAL.DALHoaDon
Imports DAL.DALKhamBenh
Imports DAL.DALDanhSachBenhNhan
Imports System.Text.RegularExpressions
Public Class BLLDanhSachBenhNhan
    Public Shared Function BLL_LayDanhSachBenhNhan() As DataTable
        Return DAL_LayDanhSachBenhNhan()
    End Function
    Public Shared Function BLL_timkiembenhnhantrongdanhsach(temphoten As String, temploaibenh As String, tempTuNgay As String, tempDenNgay As String) As DataTable
        Return DAL_timkiembenhnhantrongdanhsach(temphoten, temploaibenh, tempTuNgay, tempDenNgay)
    End Function
    Public Shared Function BLL_LayThongTinBenhNhands(temphoten As String, tempngaykham As String) As DataTable
        Return DAL_LayThongTinBenhNhands(temphoten, tempngaykham)
    End Function

End Class
