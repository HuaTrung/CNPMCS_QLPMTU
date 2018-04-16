
Public Class BenhNhan
    Private strmabenhnhan As String
    Private strhoten As String
    Private strgioitinh As String
    Private strnamsinh As String
    Private strdiachi As String
    Private strdidong As String
    Private strCMND As String
    Private strnghenghiep As String
    Public Sub New()
        strmabenhnhan = ""
        strhoten = ""
        strgioitinh = ""
        strnamsinh = ""
        strdiachi = ""
        strdiachi = ""
        strCMND = ""
        strnghenghiep = ""
    End Sub
    Property mabenhnhan() As String
        Get
            Return strmabenhnhan
        End Get
        Set(ByVal value As String)
            strmabenhnhan = value
        End Set
    End Property
    Property hoten() As String
        Get
            Return strhoten
        End Get
        Set(ByVal value As String)
            strhoten = value
        End Set
    End Property
    Property gioitinh() As String
        Get
            Return strgioitinh
        End Get
        Set(ByVal value As String)
            strgioitinh = value
        End Set
    End Property
    Property namsinh() As String
        Get
            Return strnamsinh
        End Get
        Set(ByVal value As String)
            strnamsinh = value
        End Set
    End Property
    Property diachi() As String
        Get
            Return strdiachi
        End Get
        Set(ByVal value As String)
            strdiachi = value
        End Set
    End Property
    Property didong() As String
        Get
            Return strdidong
        End Get
        Set(ByVal value As String)
            strdidong = value
        End Set
    End Property
    Property CMND() As String
        Get
            Return strCMND
        End Get
        Set(ByVal value As String)
            strCMND = value
        End Set
    End Property
    Property nghenghiep() As String
        Get
            Return strnghenghiep
        End Get
        Set(ByVal value As String)
            strnghenghiep = value
        End Set
    End Property
End Class


