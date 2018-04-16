Imports DTO
Imports MySql.Data.MySqlClient
Imports System.Text
Imports System.Collections
Public Class PhanTuThuoc
    Inherits UserControl
    Implements IDisposable

    Public stt As Integer
    Public Sub New(top As Double, _stt As Integer)
        stt = _stt
        ' This call is required by the designer.
        InitializeComponent()
        Me.SetValue(Canvas.TopProperty, top)
        Me.SetValue(Canvas.LeftProperty, 20.0)
        If (_stt > 9) Then
            Me.STTThuoc.Text = String.Concat(_stt.ToString(), ".")
        Else
            Me.STTThuoc.Text = String.Concat("0", _stt.ToString(), ".")
        End If
        loaddataintocomboxdonvi()
        loaddataintocomboxcachdung()
    End Sub
    Public Sub loaddataintocomboxdonvi()
        ' Dim SDA As New MySqlDataAdapter
        Dim conn As New MySqlConnection
        Dim Command1 As New MySqlCommand

        conn = New MySqlConnection
        Dim myReader1 As MySqlDataReader

        conn.ConnectionString = "SERVER=localhost;DATABASE=quanlyphongmachtu;UID=root;PASSWORD=trung;"
        Try
            conn.Open()

            Command1 = New MySqlCommand("CALL LayDanhSachDonVi()", conn)
            myReader1 = Command1.ExecuteReader()
            While myReader1.Read()
                PhanTuThuocDonVi.Items.Add(myReader1.GetString("TenDonVi"))
            End While

            myReader1.Close()

            conn.Close()
        Catch ex As MySqlException
        Finally
            conn.Dispose()
        End Try
    End Sub
    Public Sub loaddataintocomboxcachdung()
        ' Dim SDA As New MySqlDataAdapter
        Dim conn As New MySqlConnection

        Dim Command2 As New MySqlCommand
        conn = New MySqlConnection

        Dim myReader2 As MySqlDataReader
        conn.ConnectionString = "SERVER=localhost;DATABASE=quanlyphongmachtu;UID=root;PASSWORD=trung;"
        Try
            conn.Open()


            Command2 = New MySqlCommand("CALL layDanhSachCachDung()", conn)
            myReader2 = Command2.ExecuteReader()
            While myReader2.Read()
                PhanTuThuocCachDung.Items.Add(myReader2.GetString("TenLoaiCachDung"))
            End While

            myReader2.Close()
            conn.Close()
        Catch ex As MySqlException
        Finally
            conn.Dispose()
        End Try
    End Sub
#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
    Public Event ButtonClick(sender As Object)
    Private Sub XoaThuoc_Click(sender As Object, e As RoutedEventArgs) Handles XoaThuoc.Click
        RaiseEvent ButtonClick(Me)
        DirectCast(Me.Parent, Panel).Children.Remove(Me)
    End Sub
End Class
