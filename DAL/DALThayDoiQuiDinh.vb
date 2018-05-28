Imports DTO
Imports MySql.Data.MySqlClient
Imports System.Text
Imports System.Collections
Public Class DALThayDoiQuiDinh
    'Khởi tạo các biến cần thiết để kết nối với database
    Shared conn As New MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("ketnoicosodulieu").ConnectionString)
    Shared Command As New MySqlCommand
    Public Shared Function DAL_chinhsuaquidinh(_soluongbenhnhankham As String, _soluongloaibenh As String, _soluongthuoc As String, _soluongdonvi As String, _soluongcachdung As String) As Integer
        Dim READER As MySqlDataReader
        Try
            conn.Open()
            Dim Query As String
            Query = "call ThayDoiQuiDinh('" & _soluongbenhnhankham & "','" & _soluongloaibenh & "','" & _soluongthuoc & "','" & _soluongdonvi & "','" & _soluongcachdung & "')"
            Command = New MySqlCommand(Query, conn)
            READER = Command.ExecuteReader
            conn.Close()
        Catch ex As MySqlException
            Return -1
        Finally
            conn.Dispose()
        End Try
        Return 1
    End Function
    Public Shared Function DAL_laythamso() As DataTable
        Dim dt As New DataTable
        Dim SDA As New MySqlDataAdapter
        Try
            conn.Open()
            Command = New MySqlCommand("select SoBenhNhanToiDa,SoLuongLoaiBenh,SoLuongThuoc,SoLuongDonViTinh,SoLuongCachDung from thamso limit 1", conn)
            SDA = New MySqlDataAdapter(Command)
            SDA.Fill(dt)
            conn.Close()
        Catch ex As MySqlException
        Finally
            conn.Dispose()
        End Try
        Return dt
    End Function
End Class
