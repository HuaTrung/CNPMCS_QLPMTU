Imports DTO
Imports MySql.Data.MySqlClient
Imports System.Text
Imports System.Collections
Public Class DALHoaDon
    'Khởi tạo các biến cần thiết để kết nối với database
    Shared conn As New MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("ketnoicosodulieu").ConnectionString)
    Shared Command As New MySqlCommand
    Public Shared Function DAL_TaiHoaDon() As DataTable
        Dim dt As New DataTable

        Return dt
    End Function
    Public Shared Function DAL_TimKiemHoaDon(mahoadon As String, mabenhnhan As String, hoten As String, strtungay As String, strdenngay As String) As DataTable
        Dim dt As New DataTable

        Return dt
    End Function

    Public Shared Function DAL_TaiHoaDonChoPhieuHoaDon(mpkb As String) As DataTable
        Dim dt As New DataTable

        Return dt
    End Function
    Public Shared Function DAL_LayGiaTriTienKham() As String
        Dim tienkham As String = "0"

        Return tienkham
    End Function
    Public Shared Sub DAL_LuuHoaDonXuongBoNho(mpkb As String)


    End Sub
    Public Shared Sub DAL_KhoiTaoHoaDon(mpkb As String, tienkham As Double)

    End Sub
    Public Shared Function DAL_TinhTongCongHoaDon(mpkb As String) As String
        Dim tongcong As String = ""

        Return tongcong
    End Function
    Public Shared Function DAL_LayThongTinBenhNhan(pkbHoTen As String, pkbCMND As String, pkbGioiTinh As String) As DataTable
        Dim dt As New DataTable

        Return dt
    End Function

End Class
