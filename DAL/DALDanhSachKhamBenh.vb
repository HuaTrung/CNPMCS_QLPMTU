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

    End Sub

    'Tải dữ liệu từ database vào danh sách khám bệnh trong ngày
    Public Shared Function DAL_TaiDanhSachKhamBenhTrongNgay() As DataTable
        Dim ds As New DataTable

        Return ds
    End Function
#End Region
    Public Shared Function DAL_ThemBenhNhan(bn As BenhNhan) As Integer
        Dim Command As New MySqlCommand

        Return 0
    End Function
    Public Shared Sub DAL_KhoiTaoBenhNhanTrongChiTietDanhSachKhamBenh()


    End Sub
    Public Shared Function DAL_ChinhSuaBenhNhan(bn As BenhNhan) As Integer

    End Function
    Public Shared Function DAL_TimKiemBenhNhan(hoten As String, mabn As String, cmnd As String, tt As String, gioitinh As String, namsinh As String) As DataTable
        Dim dt As New DataTable

        Return dt
    End Function
    Public Shared Function DAL_XoaBenhNhanKhamTrongNgay(mbn As String) As Integer

        Return 1
    End Function
    Public Shared Function DAL_LayMaBenhNhanMoiNhat() As String
        Dim mbn As String = ""

        Return mbn
    End Function

End Class
