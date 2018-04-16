Imports DTO
Imports DAL
Imports DAL.DALThuoc
Public Class BLLThuoc
    Public Shared Function BLL_TaiDuLieuThuoc() As DataTable
        Return DAL_TaiDuLieuThuoc()
    End Function
    Public Shared Function BLL_TimKiemThuoctrongDanhSachThuoc(text As String) As DataTable
        Return DAL_TimKiemThuoctrongDanhSachThuoc(text)
    End Function

End Class
