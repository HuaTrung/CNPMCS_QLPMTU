Imports DTO
Imports MySql.Data.MySqlClient
Imports System.Text
Imports System.Collections
Public Class DALThayDoiQuiDinh
    'Khởi tạo các biến cần thiết để kết nối với database
    Shared conn As New MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("ketnoicosodulieu").ConnectionString)
    Shared Command As New MySqlCommand
    Public Shared Function DAL_chinhsuaquidinh(_soluongbenhnhankham As String, _soluongloaibenh As String, _soluongthuoc As String, _soluongdonvi As String, _soluongcachdung As String) As Integer

        Return 1
    End Function
    Public Shared Function DAL_laythamso() As DataTable
        Dim dt As New DataTable

        Return dt
    End Function
End Class
