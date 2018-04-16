Public Class Thuoc
    Private strMaThuoc As String
    Private strTenThuoc As String
    Private strSoLuong As Integer
    Private strDonGia As Double
    Private strDonVi As String
    Private strCachDung As String
    Public Sub New()
        strMaThuoc = ""
        strTenThuoc = ""
        strSoLuong = 0
        strDonGia = 0
        strDonVi = 0
        strCachDung = 0
    End Sub
    Property MaThuoc() As String
        Get
            Return strMaThuoc
        End Get
        Set(ByVal value As String)
            strMaThuoc = value
        End Set
    End Property
    Property TenThuoc() As String
        Get
            Return strTenThuoc
        End Get
        Set(ByVal value As String)
            strTenThuoc = value
        End Set
    End Property
    Property SoLuong() As Integer
        Get
            Return strSoLuong
        End Get
        Set(ByVal value As Integer)
            strSoLuong = value
        End Set
    End Property
    Property DonGia() As Double
        Get
            Return strDonGia
        End Get
        Set(ByVal value As Double)
            strDonGia = value
        End Set
    End Property
    Property DonVi() As String
        Get
            Return strDonVi
        End Get
        Set(ByVal value As String)
            strDonVi = value
        End Set
    End Property
    Property CachDung() As String
        Get
            Return strCachDung
        End Get
        Set(ByVal value As String)
            strCachDung = value
        End Set
    End Property
End Class
