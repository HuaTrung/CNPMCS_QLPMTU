Imports DTO
Imports DAL
Imports DAL.DALDanhSachKhamBenh
Imports DAL.DALHoaDon
Imports DAL.DALKhamBenh
Imports System.Text.RegularExpressions
Public Class BLLDanhSachKhamBenh
#Region "Tác vụ tải dữ liệu từ database lên lớp GUI cho tab Bệnh Nhân"
    'Khởi tạo danh sách khám bệnh trong ngày
    Public Shared Sub BLL_KhoiTaoDanhSachKhamBenh()
        DAL_KhoiTaoDanhSachKhamBenh()
    End Sub

    'Gọi tới lớp DAL thực hiện trực tiếp tác vụ tải dữ liệu từ database
    Public Shared Function BLL_TaiDanhSachKhamBenhTrongNgay() As DataTable       
        'Gọi lớp DAL để thao tác trực tiếp tới database
        Return (DAL_TaiDanhSachKhamBenhTrongNgay())
    End Function
#End Region

#Region "Tác vụ thêm dữ liệu mới"
    Public Shared Function checkHoTen(hoten As String) As Boolean
        Dim pattern As String = "^(?![\d\W]+$).+"
        If (hoten.Length = 0) Then
            Return False
        End If
        Return Regex.IsMatch(hoten, pattern)

    End Function
    Public Shared Function checkNamSinh(namsinh As String) As Boolean
        If (Not Regex.IsMatch(namsinh, "\d")) Then
            Return False
        End If
        Dim intnamsinh As Integer
        intnamsinh = Convert.ToInt32(namsinh)
        If (namsinh > 2018 Or namsinh < 1000) Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Shared Function BLL_ThemBenhNhan(bn As BenhNhan) As Integer
        If (checkHoTen(bn.hoten) = False) Then
            Return 1
        End If
        If (checkNamSinh(bn.namsinh) = False) Then
            Return 2
        End If
        If (Not Regex.IsMatch(bn.didong, "\d")) Then
            Return 3
        End If
        If (Not Regex.IsMatch(bn.CMND, "\d")) Then
            Return 4
        End If
        If (bn.nghenghiep.Length = 0) Then
            Return 5
        End If
        If (bn.diachi.Length = 0) Then
            Return 6
        End If
        Return DAL_ThemBenhNhan(bn)
    End Function

    Public Shared Sub BLL_KhoiTaoBenhNhanTrongChiTietDanhSachKhamBenh()

    End Sub
    Public Shared Function BLL_ChinhSuaBenhNhan(bn As BenhNhan) As Integer
        If (checkHoTen(bn.hoten) = False) Then
            Return 1
        End If
        If (checkNamSinh(bn.namsinh) = False) Then
            Return 2
        End If
        If (Not Regex.IsMatch(bn.didong, "\d")) Then
            Return 3
        End If
        If (Not Regex.IsMatch(bn.CMND, "\d")) Then
            Return 4
        End If
        If (bn.nghenghiep.Length = 0) Then
            Return 5
        End If
        If (bn.diachi.Length = 0) Then
            Return 6
        End If
        Return DAL_ChinhSuaBenhNhan(bn)
    End Function
#End Region

#Region "Tìm kiếm bệnh nhân"
    Public Shared Function BLL_TimKiemBenhNhan(ByRef newTable As DataTable, hoten As String, mabn As String, cmnd As String, tt As String, tempGt As String, tempNamSinh As String)
        If (tempNamSinh.Length > 0 And (Not Regex.IsMatch(tempNamSinh, "\d"))) Then
            Return 1
        ElseIf (cmnd.Length > 0 And (Not Regex.IsMatch(cmnd, "\d"))) Then
            Return 2
        Else
            newTable = DAL_TimKiemBenhNhan(hoten, mabn, cmnd, tt, tempGt, tempNamSinh)
            Dim a As Integer = newTable.Rows.Count
            Return 0
        End If
    End Function
#End Region

#Region "Tác vụ tải dữ liệu từ database lên lớp GUI cho tab Khám Bệnh"

    'Gọi tới lớp DAL thực hiện trực tiếp tác vụ tải dữ liệu từ database
    Public Shared Function BLL_LoadDataDaKhamFromDatabase() As DataTable
        'Gọi lớp DAL để thao tác trực tiếp tới database
        Return DAL_LoadDataDaKhamFromDatabase()
    End Function
    Public Shared Function BLL_LoadDataChuaKhamFromDatabase() As DataTable
        'Gọi lớp DAL để thao tác trực tiếp tới database
        Return DAL_LoadDataChuaKhamFromDatabase()
    End Function

#End Region

#Region "Lấy dữ liệu bệnh nhân có mã bệnh nhân"
    Public Shared Function BLL_LayDuLieuBnMBN(mbn As String) As BenhNhan
        Return DAL_LayDuLieuBnMBN(mbn)
    End Function
#End Region

#Region "Xóa dữ liệu bệnh nhân khám trong ngày"
    Public Shared Function BLL_XoaBenhNhanKhamTrongNgay(mbn As String) As Integer
        Return DAL_XoaBenhNhanKhamTrongNgay(mbn)
    End Function
#End Region
    Public Shared Function BLL_LayMaBenhNhanMoiNhat() As String
        Return DAL_LayMaBenhNhanMoiNhat()
    End Function
  
End Class
