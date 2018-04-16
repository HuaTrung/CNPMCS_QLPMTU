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

        Return bncanlay
    End Function

#Region " Được gọi từ lớp BLL.Kết nối tới databse tải dữ liệu vào dbDataSet (DataTable)"
    'Init necessary variables to connect to the database
    Public Shared Function DAL_LoadDataDaKhamFromDatabase() As DataTable
        Dim dt As New DataTable

        Return dt
    End Function
    Public Shared Function DAL_LoadDataChuaKhamFromDatabase() As DataTable
        Dim dt As New DataTable

        Return dt
    End Function

    Public Shared Function DAL_TaiDanhSachThuoc() As DataTable
        Dim dt As New DataTable

        Return dt
    End Function

#End Region
    Public Shared Function DAL_TimKiemThuoc(tenthuoc As String) As DataTable
        Dim dt As New DataTable

        Return dt
    End Function
    Public Shared Sub DAL_khoitaophieukhambenh(mbn As String)

    End Sub
    Public Shared Sub DAL_LuuChiTietPhieuKhamBenh(thuoc As Thuoc, mpkb As String)

    End Sub
    Public Shared Sub DAL_LuuTrieuChungLoaiBenhVaoPhieuKhamBenh(TrieuChung As String, chandoanbenh As String, maphieukhambenh As String)

    End Sub
    Public Shared Function DAL_LayMaPhieuKhamBenh(mbn As String) As String
        Dim _maphieukhambenh As String = ""

        Return _maphieukhambenh
    End Function
    Public Shared Function DAL_taidulieuvaoloaibenh() As List(Of String)
        Dim list As New List(Of String)()

        Return list
    End Function
#End Region
End Class
