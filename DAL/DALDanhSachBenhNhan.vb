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
        Dim SDA As New MySqlDataAdapter
        Try
            conn.Open()
            Command = New MySqlCommand("call LayDanhSachBenhNhan()", conn)
            SDA = New MySqlDataAdapter(Command)
            SDA.Fill(dt)
            conn.Close()
        Catch ex As MySqlException
        Finally
            conn.Dispose()
        End Try
        Return dt
    End Function
    Public Shared Function DAL_timkiembenhnhantrongdanhsach(temphoten As String, temploaibenh As String, strtungay As String, strdenngay As String) As DataTable
        Dim dt As New DataTable
        Dim SDA As New MySqlDataAdapter
        If String.IsNullOrEmpty(strtungay) Then
            strtungay = "2017-04-01"
        End If
        Try
            conn.Open()
            Dim Query As String
            Query = "call timkiembenhnhantrongdanhsach('" & temphoten & "','" & temploaibenh & "','" & strtungay & "','" & strdenngay & "')"
            Command = New MySqlCommand(Query, conn)
            SDA = New MySqlDataAdapter(Command)
            SDA.Fill(dt)
            conn.Close()
        Catch ex As MySqlException
        Finally
            conn.Dispose()
        End Try
        Return dt
    End Function
    Public Shared Function DAL_LayThongTinBenhNhands(temphoten As String, tempngaykham As String) As DataTable
        Dim dt As New DataTable
        Dim SDA As New MySqlDataAdapter
        Try
            conn.Open()
            Dim Query As String
            Query = "call LayThongTinBenhNhands('" & temphoten & "','" & tempngaykham & "')"
            Command = New MySqlCommand(Query, conn)
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
