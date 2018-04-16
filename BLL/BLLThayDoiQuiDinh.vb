Imports DTO
Imports DAL
Imports DAL.DALThayDoiQuiDinh
Imports System.Text.RegularExpressions
Public Class BLLThayDoiQuiDinh
    Public Shared Function BLL_chinhsuaquidinh(_soluongbenhnhankham As String, _soluongloaibenh As String, _soluongthuoc As String, _soluongdonvi As String, _soluongcachdung As String) As Integer
        Return DAL_chinhsuaquidinh(_soluongbenhnhankham, _soluongloaibenh, _soluongthuoc, _soluongdonvi, _soluongcachdung)
    End Function
    Public Shared Function BLL_laythamso() As DataTable
        Return DAL_laythamso()
    End Function
End Class
