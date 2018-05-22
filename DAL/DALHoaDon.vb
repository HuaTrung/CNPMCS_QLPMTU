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
        Dim SDA As New MySqlDataAdapter
        Try
            conn.Open()
            Dim Query As String
            Query = "call LayDanhSachHoaDon()"
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
    Public Shared Function DAL_TimKiemHoaDon(mahoadon As String, mabenhnhan As String, hoten As String, strtungay As String, strdenngay As String) As DataTable
        Dim dt As New DataTable
        Dim SDA As New MySqlDataAdapter
        If String.IsNullOrEmpty(strtungay) Then
            strtungay = "2017-04-01"
        End If
        Try
            conn.Open()
            Dim Query As String
            Query = "call TimKiemHoadon('" & mahoadon & "','" & mabenhnhan & "','" & hoten & "','" & strtungay & "','" & strdenngay & "')"
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

    Public Shared Function DAL_TaiHoaDonChoPhieuHoaDon(mpkb As String) As DataTable
        Dim dt As New DataTable
        Dim SDA As New MySqlDataAdapter
        Try
            conn.Open()
            Dim Query As String
            Query = "call TaiHoaDonChoPhieuHoaDon('" & mpkb & "')"
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
    Public Shared Function DAL_LayGiaTriTienKham() As String
        Dim tienkham As String = "0"
        Dim SDA As New MySqlDataAdapter
        Dim myReader As MySqlDataReader
        Try
            conn.Open()
            Command = New MySqlCommand("CALL LayGiaTriTienKham()", conn)
            myReader = Command.ExecuteReader()
            While myReader.Read()
                tienkham = myReader.GetString("TienKham")
            End While
            'SDA.SelectCommand = Command
            ' SDA.Fill(dt)
            myReader.Close()
            conn.Close()
        Catch ex As MySqlException
        Finally
            conn.Dispose()
        End Try
        Return tienkham
    End Function
    Public Shared Sub DAL_LuuHoaDonXuongBoNho(mpkb As String)
        Dim READER As MySqlDataReader
        Try
            conn.Open()
            Dim Query As String
            Query = "call LuuHoaDonXuongBoNho('" & mpkb & "')"
            Command = New MySqlCommand(Query, conn)
            READER = Command.ExecuteReader
            conn.Close()
        Catch ex As MySqlException

        Finally
            conn.Dispose()
        End Try

    End Sub
    Public Shared Sub DAL_KhoiTaoHoaDon(mpkb As String, tienkham As Double)
        Dim READER As MySqlDataReader
        Try
            conn.Open()
            Dim Query As String
            Query = "call KhoiTaoHoaDon('" & mpkb & "','" & tienkham & "')"
            Command = New MySqlCommand(Query, conn)
            READER = Command.ExecuteReader
            conn.Close()
        Catch ex As MySqlException

        Finally
            conn.Dispose()
        End Try
    End Sub
    Public Shared Function DAL_TinhTongCongHoaDon(mpkb As String) As String
        Dim tongcong As String = ""
        Dim SDA As New MySqlDataAdapter
        Dim myReader As MySqlDataReader
        Dim Command As New MySqlCommand
        Try
            conn.Open()
            Dim Query As String
            Query = "CALL TinhTongCongHoaDon('" & mpkb & "')"
            Command = New MySqlCommand(Query, conn)
            myReader = Command.ExecuteReader()
            While myReader.Read()
                tongcong = myReader.GetString("tongcong")
            End While
            myReader.Close()
            conn.Close()
        Catch ex As MySqlException
        Finally
            conn.Dispose()
        End Try
        Return tongcong
    End Function
    Public Shared Function DAL_LayThongTinBenhNhan(pkbHoTen As String, pkbCMND As String, pkbGioiTinh As String) As DataTable
        Dim dt As New DataTable
        Dim SDA As New MySqlDataAdapter
        Try
            conn.Open()
            Dim Query As String
            Query = "call LayThongTinBenhNhan('" & pkbHoTen & "','" & pkbCMND & "','" & pkbGioiTinh & "')"
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
