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
        Dim SDA As New MySqlDataAdapter
        Try
            conn.Open()
            Command = New MySqlCommand("call LayDuLieuThuoc()", conn)
            SDA.SelectCommand = Command
            SDA.Fill(dt)
            conn.Close()
        Catch ex As MySqlException
        Finally
            conn.Dispose()
        End Try
        Return dt
    End Function
    Public Shared Function DAL_TimKiemThuoctrongDanhSachThuoc(text As String) As DataTable
        Dim dt As New DataTable
        Dim coNhieuHon1DieuKien As Boolean = False
        Dim SDA As New MySqlDataAdapter
        Try
            conn.Open()
            Dim Query As String
            If (text.Length = 0) Then
                Query = "call LayDuLieuThuoc()"
            Else
                Query = "call TimKiemThuoctrongDanhSachThuoc('" & text & "')"
            End If
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
