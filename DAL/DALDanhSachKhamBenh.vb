Imports DTO
Imports MySql.Data.MySqlClient
Imports System.Text
Imports System.Collections
Public Class DALDanhSachKhamBenh
#Region " Được gọi từ lớp BLL.Kết nối tới databse tải dữ liệu vào dbDataSet (DataTable)"
    'Khởi tạo các biến cần thiết để kết nối với database
    Shared conn As New MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("ketnoicosodulieu").ConnectionString)
    Shared Command As New MySqlCommand
    'Khởi tạo danh sách khám bệnh
    Public Shared Sub DAL_KhoiTaoDanhSachKhamBenh()
        Dim READER As MySqlDataReader
        Try
            conn.Open()
            Dim Query As String
            Query = "call KhoiTaoDanhSachKhamBenh(now())"
            Command = New MySqlCommand(Query, conn)
            READER = Command.ExecuteReader
            conn.Close()
        Catch ex As MySqlException
        Finally
            conn.Dispose()
        End Try
    End Sub

    'Tải dữ liệu từ database vào danh sách khám bệnh trong ngày
    Public Shared Function DAL_TaiDanhSachKhamBenhTrongNgay() As DataTable
        Dim ds As New DataTable
        Dim SDA As New MySqlDataAdapter
        Try
            conn.Open()
            Command = New MySqlCommand("call LayDuLieuDanhSachKhamBenhTrongNgay(now())", conn)
            SDA = New MySqlDataAdapter(Command)
            SDA.Fill(ds)
            conn.Close()
        Catch ex As MySqlException
        Finally
            conn.Dispose()
        End Try
        Return ds
    End Function
#End Region
    Public Shared Function DAL_ThemBenhNhan(bn As BenhNhan) As Integer
        Dim Command As New MySqlCommand
        Dim READER As MySqlDataReader
        Try
            conn.Open()
            Dim Query As String
            Query = "call ThemBenhNhan('" & bn.mabenhnhan & "','" & bn.hoten & "','" & bn.gioitinh & "','" & bn.namsinh & "','" & bn.diachi & "','" & bn.didong & "','" & bn.CMND & "','" & bn.nghenghiep & "',now())"
            Command = New MySqlCommand(Query, conn)
            READER = Command.ExecuteReader
            conn.Close()
        Catch ex As MySqlException
            Return -1
        Finally
            conn.Dispose()
        End Try
        Return 0
    End Function
    Public Shared Sub DAL_KhoiTaoBenhNhanTrongChiTietDanhSachKhamBenh()
        Dim Command As New MySqlCommand
        Dim READER As MySqlDataReader
        Try
            conn.Open()
            Dim Query As String
            Query = "call KhoiTaoBenhNhanTrongChiTietDanhSachKhamBenh()"
            Command = New MySqlCommand(Query, conn)
            READER = Command.ExecuteReader
            conn.Close()
        Catch ex As MySqlException

        Finally
            conn.Dispose()
        End Try

    End Sub
    Public Shared Function DAL_ChinhSuaBenhNhan(bn As BenhNhan) As Integer
        Dim Command As New MySqlCommand
        Dim READER As MySqlDataReader
        Try
            conn.Open()
            Dim Query As String
            Query = "call ThayDoiThongTinBenhNhan('" & bn.mabenhnhan & "','" & bn.hoten & "','" & bn.CMND & "','" & bn.gioitinh & "','" & bn.namsinh & "','" & bn.diachi & "','" & bn.didong & "','" & bn.nghenghiep & "')"
            Command = New MySqlCommand(Query, conn)
            READER = Command.ExecuteReader
            conn.Close()
        Catch ex As MySqlException
            Return -1
        Finally
            conn.Dispose()
        End Try
        Return 0
    End Function
    Public Shared Function DAL_TimKiemBenhNhan(hoten As String, mabn As String, cmnd As String, tt As String, gioitinh As String, namsinh As String) As DataTable
        Dim dt As New DataTable
        Dim SDA As New MySqlDataAdapter
        Try
            conn.Open()
            Dim Query As String
            Query = "call TimKiemBenhNhanTrongDanhSachKhamBenhTrongNgay('" & mabn & "','" & hoten & "','" & cmnd & "','" & gioitinh & "','" & namsinh & "','" & tt & "',now())"
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
    Public Shared Function DAL_XoaBenhNhanKhamTrongNgay(mbn As String) As Integer
        Dim READER As MySqlDataReader
        Try
            conn.Open()
            Dim Query As String
            Query = "call XoaBenhNhanKhamTrongNgay('" & mbn & "',now())"
            Command = New MySqlCommand(Query, conn)
            READER = Command.ExecuteReader
            conn.Close()
        Catch ex As MySqlException
        Finally
            conn.Dispose()
        End Try
        Return 1
    End Function
    Public Shared Function DAL_LayMaBenhNhanMoiNhat() As String
        Dim mbn As String = ""
        Dim myReader As MySqlDataReader
        Try
            conn.Open()
            Command = New MySqlCommand("CALL LayMaBenhNhanMoiNhat()", conn)
            myReader = Command.ExecuteReader()
            While myReader.Read()
                mbn = myReader("MaBenhNhan").ToString()
            End While
            'SDA.SelectCommand = Command
            ' SDA.Fill(dt)
            myReader.Close()
            conn.Close()
        Catch ex As MySqlException
        Finally
            conn.Dispose()
        End Try
        Return mbn
    End Function

End Class
