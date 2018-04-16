Imports DTO
Imports DAL
Imports DAL.DALKhamBenh
Imports System.Text.RegularExpressions
Public Class BLLKhamBenh
    Public Shared Sub BLL_khoitaophieukhambenh(mbn As String)
        DAL_khoitaophieukhambenh(mbn)
    End Sub
    Public Shared Function BLL_TaiDanhSachThuoc() As DataTable
        Return DAL_TaiDanhSachThuoc()
    End Function
    Public Shared Function BLL_TimKiemThuoc(temp As String) As DataTable
        Return DAL_TimKiemThuoc(temp)
    End Function
    Public Shared Sub BLL_LuuChiTietPhieuKhamBenh(thuoc As Thuoc, mpkb As String)
        DAL_LuuChiTietPhieuKhamBenh(thuoc, mpkb)
    End Sub
    Public Shared Sub BLL_LuuTrieuChungLoaiBenhVaoPhieuKhamBenh(TrieuChung As String, chandoanbenh As String, mpkb As String)
        DAL_LuuTrieuChungLoaiBenhVaoPhieuKhamBenh(TrieuChung, chandoanbenh, mpkb)
    End Sub
    Public Shared Function BLL_LayMaPhieuKhamBenh(mbn As String) As String
        Return DAL_LayMaPhieuKhamBenh(mbn)
    End Function
    Public Shared Function BLL_taidulieuvaoloaibenh() As List(Of String)
        Return DAL_taidulieuvaoloaibenh()
    End Function
End Class
