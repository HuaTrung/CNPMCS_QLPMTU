Imports DTO
Imports DAL
Imports DAL.DALDanhSachKhamBenh
Imports DAL.DALBaoCao
Imports System.Text.RegularExpressions
Public Class BLLBaoCao
    Public Shared Function BLL_BaoCaoDoanhThu(thang As String, nam As String) As DataTable
        Return (DAL_BaoCaoDoanhThu(thang, nam))
    End Function
    Public Shared Function BLL_BaoCaoSuDungThuoc(thang As String, nam As String) As DataTable
        Return (DAL_BaoCaoSuDungThuoc(thang, nam))
    End Function
End Class
