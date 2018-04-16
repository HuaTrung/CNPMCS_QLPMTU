Imports DTO
Imports MySql.Data.MySqlClient
Imports System.Text
Imports System.Collections
Public Class DALThuoc
    'Khởi tạo các biến cần thiết để kết nối với database
    Shared conn As New MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("ketnoicosodulieu").ConnectionString)
    Shared Command As New MySqlCommand
    Public Shared Function DAL_TaiDuLieuThuoc() As DataTable
        Dim dt As New DataTable

        Return dt
    End Function
    Public Shared Function DAL_TimKiemThuoctrongDanhSachThuoc(text As String) As DataTable
        Dim dt As New DataTable

        Return dt
    End Function

End Class
