Imports DTO
Imports MySql.Data.MySqlClient
Imports System.Text
Imports System.Collections
Imports System.Configuration
Public Class DALDanhSachBenhNhan
    'Khởi tạo các biến cần thiết để kết nối với database
    Shared conn As New MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("ketnoicosodulieu").ConnectionString)
    Shared Command As New MySqlCommand

    Public Shared Function DAL_LayDanhSachBenhNhan() As DataTable
        Dim dt As New DataTable

        Return dt
    End Function
    Public Shared Function DAL_timkiembenhnhantrongdanhsach(temphoten As String, temploaibenh As String, strtungay As String, strdenngay As String) As DataTable
        Dim dt As New DataTable

        Return dt
    End Function
    Public Shared Function DAL_LayThongTinBenhNhands(temphoten As String, tempngaykham As String) As DataTable
        Dim dt As New DataTable

        Return dt
    End Function
End Class
