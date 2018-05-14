Imports DTO
Imports MySql.Data.MySqlClient
Imports System.Text
Imports System.Collections
Public Class DALBaoCao
    'Khởi tạo các biến cần thiết để kết nối với database
    Shared conn As New MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("ketnoicosodulieu").ConnectionString)
    Shared Command As New MySqlCommand
    Public Shared Function DAL_BaoCaoDoanhThu(thang As String, nam As String) As DataTable
        Dim dt As New DataTable
        Dim SDA As New MySqlDataAdapter
        Try
            conn.Open()
            Command = New MySqlCommand("call LayBaoCaoDoanhThuTheoNgay('" & thang & "','" & nam & "')", conn)
            SDA = New MySqlDataAdapter(Command)
            SDA.Fill(dt)
            conn.Close()
        Catch ex As MySqlException

        Finally
            conn.Dispose()
        End Try
        Return dt
    End Function

    Public Shared Function DAL_BaoCaoSuDungThuoc(thang As String, nam As String) As DataTable
        Dim dt As New DataTable
        Dim SDA As New MySqlDataAdapter
        Try
            conn.Open()
            Command = New MySqlCommand("call LayBaoCaoSuDungThuoc('" & thang & "','" & nam & "')", conn)
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
