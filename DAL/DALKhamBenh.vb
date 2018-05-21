Imports DTO
Imports MySql.Data.MySqlClient
Imports System.Text
Imports System.Collections
Public Class DALKhamBenh
#Region "TÁC VỤ CHO TAB KHÁM BỆNH"
    'Khởi tạo các biến cần thiết để kết nối với database
    Shared conn As New MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("ketnoicosodulieu").ConnectionString)
    Shared Command As New MySqlCommand
    Public Shared Function DAL_LayDuLieuBnMBN(mbn As String) As BenhNhan
        Dim bncanlay As New BenhNhan
        ' Dim SDA As New MySqlDataAdapter
        Dim myReader As MySqlDataReader
        Try
            conn.Open()
            Command = New MySqlCommand("CALL LayDuLieuBenhNhanKhiBietMaBenhNhan('" & mbn & "')", conn)
            myReader = Command.ExecuteReader()
            While myReader.Read()
                bncanlay.mabenhnhan = myReader("MaBenhNhan").ToString()
                bncanlay.hoten = myReader("Hoten").ToString()
                bncanlay.CMND = myReader("cmnd").ToString()
                bncanlay.gioitinh = myReader("Gioitinh").ToString()
                bncanlay.diachi = myReader("DiaChi").ToString()
                bncanlay.didong = myReader("DiDong").ToString()
                bncanlay.namsinh = myReader("NamSinh").ToString()
                bncanlay.nghenghiep = myReader("NgheNghiep").ToString()
            End While
            myReader.Close()
            conn.Close()
        Catch ex As MySqlException
        Finally
            conn.Dispose()
        End Try
        Return bncanlay
    End Function

#Region " Được gọi từ lớp BLL.Kết nối tới databse tải dữ liệu vào dbDataSet (DataTable)"
    'Init necessary variables to connect to the database
    Public Shared Function DAL_LoadDataDaKhamFromDatabase() As DataTable
        Dim dt As New DataTable
        Dim SDA As New MySqlDataAdapter
        Try
            conn.Open()
            Command = New MySqlCommand("call LayDuLieuBenhNhanDaKhamTrongNgay(now())", conn)
            SDA.SelectCommand = Command
            SDA.Fill(dt)
            conn.Close()
        Catch ex As MySqlException
        Finally
            conn.Dispose()
        End Try
        Return dt
    End Function
    Public Shared Function DAL_LoadDataChuaKhamFromDatabase() As DataTable
        Dim dt As New DataTable
        Dim SDA As New MySqlDataAdapter
        Try
            conn.Open()
            Command = New MySqlCommand("call LayDuLieuBenhNhanChuaKhamTrongNgay(now())", conn)
            SDA.SelectCommand = Command
            SDA.Fill(dt)
            conn.Close()
        Catch ex As MySqlException
        Finally
            conn.Dispose()
        End Try
        Return dt
    End Function

    Public Shared Function DAL_TaiDanhSachThuoc() As DataTable
        Dim dt As New DataTable
        Dim SDA As New MySqlDataAdapter
        Try
            conn.Open()
            Command = New MySqlCommand("call LayDanhSachThuoc()", conn)
            SDA.SelectCommand = Command
            SDA.Fill(dt)
            conn.Close()
        Catch ex As MySqlException
        Finally
            conn.Dispose()
        End Try
        Return dt
    End Function

#End Region
    Public Shared Function DAL_TimKiemThuoc(tenthuoc As String) As DataTable
        Dim dt As New DataTable
        Dim coNhieuHon1DieuKien As Boolean = False
        Dim SDA As New MySqlDataAdapter
        Try
            conn.Open()
            Dim Query As String
            If (tenthuoc.Length = 0) Then
                Query = "call LayDuLieuThuoc()"
            Else
                Query = "call TimKiemThuoc('" & tenthuoc & "')"
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
    Public Shared Sub DAL_khoitaophieukhambenh(mbn As String)
        Dim READER As MySqlDataReader
        Try
            conn.Open()
            Dim Query As String
            Query = "call KhoiTaoPhieuKhamBenh(now(),'" & mbn & "')"
            Command = New MySqlCommand(Query, conn)
            READER = Command.ExecuteReader
            conn.Close()
        Catch ex As MySqlException
        Finally
            conn.Dispose()
        End Try
    End Sub
    Public Shared Sub DAL_LuuChiTietPhieuKhamBenh(thuoc As Thuoc, mpkb As String)
        Dim READER As MySqlDataReader
        Try
            conn.Open()
            Dim Query As String
            Query = "call ThemChiTietPhieuKhamBenh('" & mpkb & "','" & thuoc.TenThuoc & "','" & thuoc.DonVi & "','" & thuoc.CachDung & "','" & thuoc.SoLuong & "')"
            Command = New MySqlCommand(Query, conn)
            READER = Command.ExecuteReader
            conn.Close()
        Catch ex As MySqlException
        Finally
            conn.Dispose()
        End Try
    End Sub
    Public Shared Sub DAL_LuuTrieuChungLoaiBenhVaoPhieuKhamBenh(TrieuChung As String, chandoanbenh As String, maphieukhambenh As String)
        Dim READER As MySqlDataReader
        Try
            conn.Open()
            Dim Query As String
            Query = "call ThemTrieuChungVaLoaiBenhVaoPhieuKhamBenh('" & TrieuChung & "','" & chandoanbenh & "','" & maphieukhambenh & "')"
            Command = New MySqlCommand(Query, conn)
            READER = Command.ExecuteReader
            conn.Close()
        Catch ex As MySqlException
        Finally
            conn.Dispose()
        End Try
    End Sub
    Public Shared Function DAL_LayMaPhieuKhamBenh(mbn As String) As String
        Dim _maphieukhambenh As String = ""
        ' Dim SDA As New MySqlDataAdapter
        Dim myReader As MySqlDataReader
        Try
            conn.Open()
            Command = New MySqlCommand("CALL LayMaPhieuKhamBenh('" & mbn & "',now())", conn)
            myReader = Command.ExecuteReader()
            While myReader.Read()
                _maphieukhambenh = myReader("MaPhieuKhamBenh").ToString()
            End While
            'SDA.SelectCommand = Command
            ' SDA.Fill(dt)
            myReader.Close()
            conn.Close()
        Catch ex As MySqlException
        Finally
            conn.Dispose()
        End Try
        Return _maphieukhambenh
    End Function
    Public Shared Function DAL_taidulieuvaoloaibenh() As List(Of String)
        Dim list As New List(Of String)()
        ' Dim SDA As New MySqlDataAdapter
        Dim myReader As MySqlDataReader
        Try
            conn.Open()
            Command = New MySqlCommand("CALL LayDanhSachBenh()", conn)
            myReader = Command.ExecuteReader()
            While myReader.Read()
                list.Add(myReader.GetString("LoaiBenh"))
            End While
            myReader.Close()
            conn.Close()
        Catch ex As MySqlException
        Finally
            conn.Dispose()
        End Try
        Return list
    End Function
#End Region
End Class
