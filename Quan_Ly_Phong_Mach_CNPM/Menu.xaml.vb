Imports DTO
Imports BLL.BLLDanhSachKhamBenh
Imports BLL
Imports BLL.BLLHoaDon
Imports BLL.BLLKhamBenh
Imports BLL.BLLDanhSachBenhNhan
Imports BLL.BLLBaoCao
Imports BLL.BLLThuoc
Imports BLL.BLLThayDoiQuiDinh
Imports MySql.Data.MySqlClient
Imports System.Windows.Forms
Imports System.Text
Imports System.Collections
Imports System.Data
Imports MahApps.Metro.Controls.Dialogs
Imports MaterialDesignThemes.Wpf.DialogHost
Imports Microsoft.Office.Interop.Excel 'Before you add this refrence to your project you need to install Microsoft Office and find last version of this file.
Imports Microsoft.Office.Interop
Imports System.IO.Directory
Imports System.Windows.Controls
Public Class Menu
#Region "Khởi tạo chương trình, tải những dữ liệu cần thiết"
    Public Sub New()

        ' This call is required by the designer.
        Try

            InitializeComponent()
            ' Log error (including InnerExceptions!)
            ' Handle exception
        Catch ex As Exception
        End Try

        'Tab Qui Dinh
        QuiDinh_Load()

        ' Tab Quản lí
        ' Khởi tạo danh sách khám bệnh
        BLL_KhoiTaoDanhSachKhamBenh()
        ' Tải danh sách khám bệnh trong ngày 
        TaiDanhSachKhamBenhTrongNgay()

        ' Tab Khám bệnh
        'Tải dữ liệu danh sách bệnh nhân cho tab Khám Bênh
        KhamBenh_Load()
        'Tải danh sách thuốc
        TaiThuoc()

        'Tab Hóa đơn
        'Tải danh sách hóa đơn
        HoaDon_Load()
        Try
            tienkham = BLL_LayGiaTriTienKham()
        Catch ex As MySqlException
        Finally

        End Try
        'Tab Danh sách
        DanhSach_Load()

        'Tab Báo cáo
        BaoCao_Load()

        'Tab Thuốc 
        danhsachthuoc_load()

    End Sub
#End Region

#Region "CÁC TÁC VỤ DÀNH CHO TAB QUẢN LÍ BỆNH NHÂN"

#Region "Tải dữ liệu danh sách khám bệnh trong ngày từ database"

    ' Hàm thực hiện tác vụ tải dữ liệu từ database
    Public Sub TaiDanhSachKhamBenhTrongNgay()
        ' dbDataSet là data table chứa dữ liệu của bệnh nhân lấy từ database
        Dim dbDataSet As New System.Data.DataTable
        'Gọi tới lớp BLL thực hiện tác vụ tải dữ liệu từ database
        dbDataSet = BLL_TaiDanhSachKhamBenhTrongNgay()
        'Tải dữ liệu lên bảng danh sách bệnh nhân

        DanhSachKhamBenh.DataContext = dbDataSet
        sobenhnhandadangki.Text = dbDataSet.Rows.Count().ToString()
        quanli_soluongbenhnhankhamtoida.Text = "/ " + soluongbenhnhankham.Text

    End Sub
   
#End Region

#Region "Các hàm phụ khác"
    ' Tải label ngày khám bệnh hôm nay
    Private Sub Canvas_Loaded(sender As Object, e As RoutedEventArgs)
        txtbkNgayKham.Text = "Danh sách bệnh nhân ngày: " + DateTime.Now.ToString("dd/MM/yyyy")
    End Sub


#End Region

#Region "Thêm bệnh nhân mới và chỉnh sửa dữ liệu bệnh nhân"
    ' Lưu bệnh nhân mới hoặc đã chỉnh sửa xuống database
    Private Sub LuuBenhNhan(sender As Object, e As RoutedEventArgs)
        Dim bn As New BenhNhan()
        bn.hoten = Hoten.Text
        bn.mabenhnhan = MaBenhNhan.Text
        bn.diachi = Diachi.Text
        bn.namsinh = Namsinh.Text
        bn.CMND = Socmnd.Text
        bn.nghenghiep = Nghenghiep.Text
        bn.didong = Didong.Text
        If (gtNam.IsChecked = False And gtNu.IsChecked = False) Then
            MessageBox.Show("Chưa xác định giới tính")
            Exit Sub
        ElseIf (gtNam.IsChecked = True) Then
            bn.gioitinh = "Nam"
        Else
            bn.gioitinh = "Nữ"
        End If
        Dim ketqua As Integer
        If (chinhsuabenhnhan = False) Then
            ketqua = BLL_ThemBenhNhan(bn)
            BLL_KhoiTaoBenhNhanTrongChiTietDanhSachKhamBenh()
        Else
            ketqua = BLL_ChinhSuaBenhNhan(bn)
            chinhsuabenhnhan = False
        End If
        ' Kiểm tra tính hợp lệ của dữ liệu
        If (ketqua = 0) Then
            MessageBox.Show("Thao tác thành công")
            TaiDanhSachKhamBenhTrongNgay()
            frmthemvachinhsuaBenhNhan.IsOpen = False
            'Load lại danh sách khám bệnh
            KhamBenh_Load()
        ElseIf (ketqua = 1) Then
            Hoten.Focus()
            MessageBox.Show("Kiểm tra ô Họ và Tên")
        ElseIf (ketqua = 2) Then
            Namsinh.Focus()
            MessageBox.Show("Kiểm tra ô Năm Sinh")
        ElseIf (ketqua = 3) Then
            Didong.Focus()
            MessageBox.Show("Kiểm tra ô Di Động")
        ElseIf (ketqua = 4) Then
            Socmnd.Focus()
            MessageBox.Show("Kiểm tra ô Số CMND")
        ElseIf (ketqua = 5) Then
            Nghenghiep.Focus()
            MessageBox.Show("Kiểm tra ô Nghề Nghiệp")
        ElseIf (ketqua = 6) Then
            Diachi.Focus()
            MessageBox.Show("Kiểm tra ô Địa Chỉ")
        Else
            MessageBox.Show("Không thành công. Vui lòng kiểm tra lại")
        End If
    End Sub
    'Button thêm bệnh nhân để mở flyout thêm bệnh nhân
    Private Sub thembenhnhan_Click(sender As Object, e As RoutedEventArgs) Handles thembenhnhan.Click
        If (sobenhnhandadangki.Text = soluongbenhnhankham.Text) Then
            MessageBox.Show("Số lượng bệnh nhân được khám trong ngày đã vượt quá " + soluongbenhnhankham.Text + " cho phép")
        Else
            frmthemvachinhsuaBenhNhan.IsOpen = True
            MaBenhNhan.Text = BLL_LayMaBenhNhanMoiNhat()
            Hoten.Clear()
            Namsinh.Clear()
            Diachi.Clear()
            Didong.Clear()
            Nghenghiep.Clear()
            Socmnd.Clear()
            gtNam.IsChecked = False
            gtNu.IsChecked = False
        End If
    End Sub

    ' Thoát khỏi flyout thêm bệnh nhân
    Private Sub thoatfrmThemBenhNhan_Click(sender As Object, e As RoutedEventArgs) Handles thoatfrmThemBenhNhan.Click
        frmthemvachinhsuaBenhNhan.IsOpen = False
    End Sub
#End Region

#Region "Xuất file excel"
    Private Async Sub xuatExcel(sender As Object, e As RoutedEventArgs)
        'Initialize the objects before use

        Dim f As FolderBrowserDialog = New FolderBrowserDialog
        Try
            If f.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                Dim controller = Await Me.ShowProgressAsync("Please wait...", "Progress message")
                controller.SetIndeterminate()
                'This section help you if your language is not English.
                Await Task.Run(Sub() fun(f))

                MessageBox.Show("Export done successfully!")
                Await controller.CloseAsync()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK)
        End Try
    End Sub
    Private Sub fun(f As FolderBrowserDialog)
        Dim datatableMain As New System.Data.DataTable()
        System.Threading.Thread.CurrentThread.CurrentCulture = _
                System.Globalization.CultureInfo.CreateSpecificCulture("en-US")
        Dim oExcel As Excel.Application
        Dim oBook As Excel.Workbook
        Dim oSheet As Excel.Worksheet
        oExcel = CreateObject("Excel.Application")
        oBook = oExcel.Workbooks.Add(Type.Missing)
        oSheet = oBook.Worksheets(1)

        Dim dc As System.Data.DataColumn
        Dim dr As System.Data.DataRow
        Dim colIndex As Integer = 0
        Dim rowIndex As Integer = 0

        datatableMain = BLL_TaiDanhSachKhamBenhTrongNgay()

        'Export the Columns to excel file
        For Each dc In datatableMain.Columns
            colIndex = colIndex + 1
            oSheet.Cells(1, colIndex) = dc.ColumnName
        Next

        'Export the rows to excel file
        For Each dr In datatableMain.Rows
            rowIndex = rowIndex + 1
            colIndex = 0
            For Each dc In datatableMain.Columns
                colIndex = colIndex + 1
                oSheet.Cells(rowIndex + 1, colIndex) = dr(dc.ColumnName)
            Next
        Next

        'Set final path
        Dim fileName As String = "\DanhSachKhamBenh_" + DateTime.Now.ToString("dd-MM-yyyy") + ".xls"
        Dim finalPath = f.SelectedPath + fileName
        oSheet.Columns.AutoFit()
        'Save file in final path
        oBook.SaveAs(finalPath, XlFileFormat.xlWorkbookNormal, Type.Missing, _
        Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive, _
        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing)

        'Release the objects
        ReleaseObject(oSheet)
        oBook.Close(False, Type.Missing, Type.Missing)
        ReleaseObject(oBook)
        oExcel.Quit()
        ReleaseObject(oExcel)
        'Some time Office application does not quit after automation: 
        'so i am calling GC.Collect method.
        GC.Collect()
    End Sub

    Private Sub ReleaseObject(ByVal o As Object)
        Try
            While (System.Runtime.InteropServices.Marshal.ReleaseComObject(o) > 0)
            End While
        Catch
        Finally
            o = Nothing
        End Try
    End Sub
#End Region
#Region "Nhập từ file Excel"
    Private Sub nhapfileexcel(sender As Object, e As RoutedEventArgs)
        Dim ofd As OpenFileDialog = New OpenFileDialog()
        ofd.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*"
        If ofd.ShowDialog() = True Then
            Try
                Dim path As String = ofd.FileName
                Dim con As OleDb.OleDbConnection
                Dim dataset As DataSet
                Dim adt As OleDb.OleDbDataAdapter
                con = New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + path + ";Extended Properties=Excel 12.0")
                adt = New OleDb.OleDbDataAdapter("select * from [Sheet1$]", con)
                dataset = New DataSet()
                adt.Fill(dataset)
                DanhSachKhamBenh.ItemsSource = dataset.Tables(0).DefaultView
                con.Dispose()
                con.Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub
#End Region
#End Region
#Region "Tác vụ tìm kiếm"
    Private Sub timkiem_Click(sender As Object, e As RoutedEventArgs) Handles timkiemBenhNhan.Click
        Dim temphoten As String
        Dim tempSoCMND As String
        Dim tempMaBN As String
        Dim tempTT As String
        Dim tempGt As String
        Dim tempNamSinh As String
        If (tkgtNam.IsChecked = False And tkgtNu.IsChecked = False) Then
            tempGt = ""
        ElseIf (tkgtNam.IsChecked = True) Then
            tempGt = "Nam"
        Else
            tempGt = "Nữ"
        End If
        temphoten = tkHoten.Text
        tempMaBN = tkMaBN.Text
        tempSoCMND = tkCMND.Text
        tempNamSinh = tknamsinh.Text
        If (tkTT.SelectedIndex <> -1) Then
            tempTT = tkTT.SelectedItem.ToString().Substring(38)
        Else
            tempTT = ""
        End If
        Dim newTable As New System.Data.DataTable
        Dim ketqua As Integer = BLL_TimKiemBenhNhan(newTable, temphoten, tempMaBN, tempSoCMND, tempTT, tempGt, tempNamSinh)
        If (ketqua = 0) Then
            DanhSachKhamBenh.DataContext = newTable
        ElseIf (ketqua = 1) Then
            MessageBox.Show("Năm sinh không hợp lệ")
            tknamsinh.Focusable = True
        ElseIf (ketqua = 2) Then
            MessageBox.Show("CMND không hợp lệ")
            tkCMND.Focusable = True
        Else
            MessageBox.Show("Có lỗi xảy ra")
        End If
    End Sub

    ' Nút refresh để tải lại danh sách khám bệnh ban đầu
    Private Sub refresh_Click(sender As Object, e As RoutedEventArgs) Handles refreshDanhSach.Click
        tkCMND.Clear()
        tkHoten.Clear()
        tkMaBN.Clear()
        tknamsinh.Clear()
        tkTT.SelectedIndex = -1
        tkgtNam.IsChecked = False
        tkgtNu.IsChecked = False
        TaiDanhSachKhamBenhTrongNgay()
    End Sub
#End Region

#Region "Xử lí các button trong cột chức năng"
    'Button sửa đổi dữ liệu bệnh nhân
    Dim chinhsuabenhnhan As Boolean = False
    Dim bncanlay As New BenhNhan
    Private Sub chinhSua_Click(sender As Object, e As RoutedEventArgs)
        Dim row As DataRowView = DirectCast(DanhSachKhamBenh.SelectedItem, DataRowView)
        If (row(9).ToString = "Chưa Khám") Then
            Dim mbn As String = row(1).ToString()
            'Lấy dữ liệu từ database
            bncanlay = BLL_LayDuLieuBnMBN(mbn)
            chinhsuabenhnhan = True
            frmthemvachinhsuaBenhNhan.IsOpen = True
            MaBenhNhan.Text = bncanlay.mabenhnhan
            Hoten.Text = bncanlay.hoten
            Namsinh.Text = bncanlay.namsinh
            Diachi.Text = bncanlay.diachi
            Didong.Text = bncanlay.didong
            Nghenghiep.Text = bncanlay.nghenghiep
            Socmnd.Text = bncanlay.CMND
            If (bncanlay.gioitinh = "Nam") Then
                gtNam.IsChecked = True
            Else
                gtNu.IsChecked = True
            End If
        End If
    End Sub

    'Button Xóa bệnh nhân
    Private Sub xoa_click(sender As Object, e As RoutedEventArgs)
        Dim row As DataRowView = DirectCast(DanhSachKhamBenh.SelectedItem, DataRowView)
        If (row(9).ToString = "Chưa Khám") Then
            Dim mbn As String = row(1).ToString()
            If (BLL_XoaBenhNhanKhamTrongNgay(mbn) = 1) Then
                MessageBox.Show("Xóa thành công")
                TaiDanhSachKhamBenhTrongNgay()
            Else
                MessageBox.Show("Xóa không thành công")
            End If
        End If
    End Sub

    'Button Khám bệnh
    Private Sub KhamBenh_Click(sender As Object, e As RoutedEventArgs)
        Dim row As DataRowView = DirectCast(DanhSachKhamBenh.SelectedItem, DataRowView)
        If (row(9).ToString = "Chưa Khám") Then
            menutabcontrol.SelectedIndex = 1
            frmphieukhambenh.IsOpen = True
            Dim mbn As String = row(1).ToString()
            'Lấy dữ liệu từ database
            bncanlay = BLL_LayDuLieuBnMBN(mbn)
            LoadPhieuKhamBenh(True, bncanlay, ListTHUOC)
        End If
    End Sub
#End Region


#Region "CÁC TÁC VỤ DÀNH CHO TAB KHÁM BỆNH"
#Region "Tải danh sách bệnh nhân đã khám và chưa khám"
    'Tải dữ liệu bệnh nhân chưa khám
    Private Sub LoadTinhTrangKhamBenh_ChuaKham()
        'Bảng chứa danh sách bệnh nhân chưa khám
        danhsachchuakham.DataContext = BLL_LoadDataChuaKhamFromDatabase()
    End Sub

    'Tải dữ liệu bệnh nhân đã khám
    Private Sub LoadTinhTrangKhamBenh_DaKham()
        'Bảng chứa danh sách bệnh nhân đã khám
        danhsachdakham.DataContext = BLL_LoadDataDaKhamFromDatabase()
    End Sub

    'Load tab khám bệnh lần đầu và gọi hai hàm để tải dữ liệu bệnh nhân chưa khám và đã khám
    Private Sub KhamBenh_Load()
        LoadTinhTrangKhamBenh_ChuaKham()
        LoadTinhTrangKhamBenh_DaKham()
    End Sub

#End Region

#Region "Phím chức năng"
    Private Sub tabkhambenh_khambenh(sender As Object, e As RoutedEventArgs)
        frmphieukhambenh.IsOpen = True
        Dim row As DataRowView = DirectCast(danhsachchuakham.SelectedItem, DataRowView)
        Dim mbn As String = row(1).ToString()
        'Lấy dữ liệu từ database
        bncanlay = BLL_LayDuLieuBnMBN(mbn)
        LoadPhieuKhamBenh(True, bncanlay, ListTHUOC)
    End Sub
#End Region
    'Tải danh sách thuốc
    Private Sub TaiThuoc()
        tabkhambenh_danhsachthuoc.DataContext = BLL_TaiDanhSachThuoc()
    End Sub
    Dim ListTHUOC As New List(Of PhanTuThuoc)()
    Dim vitrimacdinh As Integer = 180
    'Dấu + trong phiếu khám bệnh để thêm thuốc
    Private Sub ThemThuoc_Click(sender As Object, e As RoutedEventArgs)
        If ListTHUOC(ListTHUOC.Count - 1).tenthuoc.Text.Length > 0 Then
            If (ListTHUOC.Count = 0) Then
                Dim Ptt As New PhanTuThuoc(vitrimacdinh, ListTHUOC.Count + 1)
                PhieuKhamBenh.Children.Add(Ptt)
                ThemThuoc.SetValue(Canvas.TopProperty, Canvas.GetTop(ThemThuoc) + 95.0)
                ListTHUOC.Add(Ptt)
                vitrimacdinh = vitrimacdinh + 95
            ElseIf (ListTHUOC.Count > 2) Then
                tabkhambenh_phieukhambenh.Height = tabkhambenh_phieukhambenh.Height + 95
                phieukhambenhnhan.Height = phieukhambenhnhan.Height + 95
                loidanbacsi.SetValue(Canvas.TopProperty, Canvas.GetTop(loidanbacsi) + 95.0)
                richtxbloidanbacsi.SetValue(Canvas.TopProperty, Canvas.GetTop(richtxbloidanbacsi) + 95.0)
                pkbNgayKham.SetValue(Canvas.TopProperty, Canvas.GetTop(pkbNgayKham) + 95.0)

                Dim Ptt As New PhanTuThuoc(vitrimacdinh, ListTHUOC.Count + 1)
                AddHandler Ptt.ButtonClick, AddressOf Button_Click
                PhieuKhamBenh.Children.Add(Ptt)
                ThemThuoc.SetValue(Canvas.TopProperty, Canvas.GetTop(ThemThuoc) + 95.0)
                ListTHUOC.Add(Ptt)

                vitrimacdinh = vitrimacdinh + 95
            Else
                Dim Ptt As New PhanTuThuoc(vitrimacdinh, ListTHUOC.Count + 1)
                AddHandler Ptt.ButtonClick, AddressOf Button_Click
                PhieuKhamBenh.Children.Add(Ptt)
                ThemThuoc.SetValue(Canvas.TopProperty, Canvas.GetTop(ThemThuoc) + 95.0)
                ListTHUOC.Add(Ptt)
                vitrimacdinh = vitrimacdinh + 95
            End If
        Else
            MessageBox.Show("Chưa nhập thuốc")
        End If
    End Sub
    'Dấu cộng trong frm phiếu khám bệnh
    Private Sub Button_Click(sender As Object)
        Dim temp As PhanTuThuoc = DirectCast(sender, PhanTuThuoc)
        ListTHUOC.RemoveAt(temp.stt - 1)
        For i As Integer = temp.stt To ListTHUOC.Count - 1
            ListTHUOC(i).SetValue(Canvas.TopProperty, Canvas.GetTop(ThemThuoc) - 95.0)
        Next
        If (ListTHUOC.Count > 2) Then
            tabkhambenh_phieukhambenh.Height = tabkhambenh_phieukhambenh.Height - 95
            phieukhambenhnhan.Height = phieukhambenhnhan.Height - 95
            loidanbacsi.SetValue(Canvas.TopProperty, Canvas.GetTop(loidanbacsi) - 95.0)
            richtxbloidanbacsi.SetValue(Canvas.TopProperty, Canvas.GetTop(richtxbloidanbacsi) - 95.0)
        End If
        ThemThuoc.SetValue(Canvas.TopProperty, Canvas.GetTop(ThemThuoc) - 95.0)
    End Sub
    Private Sub parenttimkiemthuoc_TextChanged(sender As Object, e As TextChangedEventArgs) Handles parenttimkiemthuoc.TextChanged
        tabkhambenh_danhsachthuoc.DataContext = BLL_TimKiemThuoc(parenttimkiemthuoc.Text)
    End Sub
    'Khởi tạo phiếu khám bệnh
    Private Sub LoadPhieuKhamBenh(phieumoi As Boolean, bncanlay As BenhNhan, thuoc As List(Of PhanTuThuoc))
        pkbHoTen.Text = String.Concat("Họ và tên: ", bncanlay.hoten.ToString())
        pkbNgayKham.Text = String.Concat("Ngày ", DateTime.Now.ToString("dd"), " Tháng ", DateTime.Now.ToString("MM"), " Năm ", DateTime.Now.ToString("yyyy"))
        pkbSDT.Text = String.Concat("Di động: ", bncanlay.didong.ToString())
        pkbGioiTinh.Text = String.Concat("Giới tính: ", bncanlay.gioitinh.ToString())
        pkbCMND.Text = String.Concat("Số CMND: ", bncanlay.CMND.ToString())
        For Each item As String In BLL_taidulieuvaoloaibenh()
            chandoanbenh.Items.Add(item.ToString())
        Next
        If (phieumoi = True) Then
            Dim Ptt As New PhanTuThuoc(vitrimacdinh, ListTHUOC.Count + 1)
            PhieuKhamBenh.Children.Add(Ptt)
            ThemThuoc.SetValue(Canvas.TopProperty, Canvas.GetTop(ThemThuoc) + 95.0)
            ListTHUOC.Add(Ptt)
            vitrimacdinh = vitrimacdinh + 95
        Else
            For Each a As PhanTuThuoc In thuoc

            Next
        End If

    End Sub
    'Thoát phiếu khám bệnh
    Private Sub thoatPhieukhambenh_Click(sender As Object, e As RoutedEventArgs) Handles thoatPhieukhambenh.Click
        frmphieukhambenh.IsOpen = False
        For Each item As PhanTuThuoc In ListTHUOC
            PhieuKhamBenh.Children.Remove(item)
        Next
        ListTHUOC.Clear()
        vitrimacdinh = 180
    End Sub
    'In phiếu khám bệnh
    Private Sub pkbin_Click(sender As Object, e As RoutedEventArgs) Handles pkbin.Click
        Dim printDlg As New System.Windows.Controls.PrintDialog()
        printDlg.PrintVisual(phieukhambenhin, "Phiếu khám bệnh")
    End Sub
    'Chọn thuốc từ danh sách thuốc trong phiếu khám bệnh
    Private Sub tabkhambenh_danhsachthuoc_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles tabkhambenh_danhsachthuoc.MouseDoubleClick
        Dim row As DataRowView = DirectCast(tabkhambenh_danhsachthuoc.SelectedItem, DataRowView)
        Dim tenthuoc As String = row(1).ToString()
        ListTHUOC(ListTHUOC.Count - 1).tenthuoc.Text = tenthuoc
    End Sub
    Private Sub laphoadon_click(sender As Object, e As RoutedEventArgs) Handles pkblaphoadon.Click
        BLL_khoitaophieukhambenh(bncanlay.mabenhnhan)
        pkbmaphieukhambenh.Text = BLL_LayMaPhieuKhamBenh(bncanlay.mabenhnhan)
        menutabcontrol.SelectedIndex = 2
        hdngaykham.Text = "Ngày lập : " + DateTime.Now.ToString("dd/MM/yyyy")
        Dim temp As Integer = 1
        If (String.IsNullOrEmpty(TrieuChung.Text)) Then
            MessageBox.Show("Triệu chứng còn trống")
            TrieuChung.Focus()
            temp = -1
        End If
        If (String.IsNullOrEmpty(chandoanbenh.Text)) Then
            MessageBox.Show("Chẩn đoán bệnh còn trống")
            chandoanbenh.Focus()
            temp = -1
        End If
        If (temp = 1) Then
            temp = LuuChiTietPhieuKhamBenh(ListTHUOC, pkbmaphieukhambenh.Text)
        End If
        If (temp = 1) Then
            BLL_LuuTrieuChungLoaiBenhVaoPhieuKhamBenh(TrieuChung.Text, chandoanbenh.Text, pkbmaphieukhambenh.Text.ToString())
            frmphieukhambenh.IsOpen = False
            frmhoadon.IsOpen = True
            LapHoaDon_Load()
        End If
    End Sub
    ' Lưu chi tiết phiếu khám bệnh
    Public Function LuuChiTietPhieuKhamBenh(dsthuoc As List(Of PhanTuThuoc), mpkb As String)
        For Each item As PhanTuThuoc In dsthuoc
            Dim thuoc As New Thuoc
            If (String.IsNullOrEmpty(item.tenthuoc.Text)) Then
                item.tenthuoc.Focus()
                Return -1
            Else
                thuoc.TenThuoc = item.tenthuoc.Text
            End If

            If (String.IsNullOrEmpty(item.SoLuong.Text)) Then
                item.SoLuong.Focus()
                Return -1
            Else
                thuoc.SoLuong = item.SoLuong.Text
            End If

            If (String.IsNullOrEmpty(item.PhanTuThuocDonVi.Text)) Then
                item.PhanTuThuocDonVi.Focus()
                Return -1
            Else
                thuoc.DonVi = item.PhanTuThuocDonVi.Text
            End If

            If (String.IsNullOrEmpty(item.PhanTuThuocCachDung.Text)) Then
                item.PhanTuThuocCachDung.Focus()
                Return -1
            Else
                thuoc.CachDung = item.PhanTuThuocCachDung.Text
            End If
            BLL_LuuChiTietPhieuKhamBenh(thuoc, mpkb)
        Next
        Return 1
    End Function
#End Region

#Region "CÁC TÁC VỤ DÀNH CHO TAB HÓA ĐƠN"
    Dim tienkham As Double
    'Tải hóa đơn
    Private Sub HoaDon_Load()
        danhsachhoadon.DataContext = BLL_TaiHoaDon()
        DenNgay.SelectedDate = DateTime.Today
    End Sub
    'Tìm kiếm hóa đơn
    Private Sub timkiemhoadon_Click(sender As Object, e As RoutedEventArgs)
        Dim mahoadon As String = tkhdmahoadon.Text
        Dim mabenhnhan As String = tkhdMaBN.Text
        Dim hoten As String = tkhdhoten.Text
        Dim strtungay As String = ""
        If (Not TuNgay.SelectedDate Is Nothing) Then
            strtungay = TuNgay.SelectedDate.Value.ToString("yyyy-MM-dd")
        End If
        Dim strdenngay As String = DenNgay.SelectedDate.Value.ToString("yyyy-MM-dd")
        danhsachhoadon.DataContext = BLL_TimKiemHoaDon(mahoadon, mabenhnhan, hoten, strtungay, strdenngay)
    End Sub
    'Lập hóa đơn
    Public Sub LapHoaDon_Load()
        Dim dt As New System.Data.DataTable
        dt = BLL_LayThongTinBenhNhan(pkbHoTen.Text.Substring(11), pkbCMND.Text.Substring(9), pkbGioiTinh.Text.Substring(11))
        hdHoTen.Text = "Họ Tên : " + dt.Rows(0)(0)
        hdCMND.Text = "Số CMND : " + dt.Rows(0)(1)
        hdGioiTinh.Text = "Giới Tính : " + dt.Rows(0)(2)
        hdSDT.Text = "Di Động : " + dt.Rows(0)(3)
        hdnghenghiep.Text = "Nghề Nghiệp : " + dt.Rows(0)(4)
        hdnamsinh.Text = "Năm Sinh : " + dt.Rows(0)(5)
        hddiachi.Text = "Địa chỉ : " + dt.Rows(0)(6)
        BLL_KhoiTaoHoaDon(pkbmaphieukhambenh.Text, tienkham)
        frmhoadon_hoadon.DataContext = BLL_TaiHoaDonChoPhieuHoaDon(pkbmaphieukhambenh.Text)
        txttienkham.Text = tienkham.ToString()
        hoadon_tongcong.Text = BLL_TinhTongCongHoaDon(pkbmaphieukhambenh.Text)
    End Sub
    'Xuất hóa đơn
    Private Sub xuathoadon_Click(sender As Object, e As RoutedEventArgs) Handles xuathoadon.Click
        BLL_LuuHoaDonXuongBoNho(pkbmaphieukhambenh.Text.ToString())
        TaiDanhSachKhamBenhTrongNgay()
        KhamBenh_Load()
        frmhoadon.IsOpen = False
        KhamBenh_Load()
        HoaDon_Load()
        DanhSach_Load()
    End Sub
#End Region

#Region "CÁC TÁC VỤ DÀNH CHO TAB THUỐC"
    'Tải danh sách thuốc
    Public Sub danhsachthuoc_load()
        tabthuoc_danhsachthuoc.DataContext = BLL_TaiDuLieuThuoc()
    End Sub
    'Tìm kiếm thuốc
    Private Sub dsthuoc_timkiemthuoc_TextChanged(sender As Object, e As TextChangedEventArgs) Handles dsthuoc_timkiemthuoc.TextChanged
        tabthuoc_danhsachthuoc.DataContext = BLL_TimKiemThuoctrongDanhSachThuoc(dsthuoc_timkiemthuoc.Text)
    End Sub
#End Region

#Region "CÁC TÁC VỤ DÀNH CHO TAB BỆNH NHÂN"
    'Tải danh sách bệnh nhân
    Public Sub DanhSach_Load()
        danhsachbenhnhan.DataContext = BLL_LayDanhSachBenhNhan()
        tkdsDenNgay.SelectedDate = DateTime.Today
        For Each item As String In BLL_taidulieuvaoloaibenh()
            tkdsloaibenh.Items.Add(item.ToString())
        Next
    End Sub
    'Tìm kiếm bệnh nhân trong danh sách bệnh nhân
    Private Sub timkiemdanhsach_click(sender As Object, e As RoutedEventArgs) Handles timkiembenhnhantrongdanhsach.Click
        Dim temphoten As String
        Dim temploaibenh As String
        temphoten = tkdsHoten.Text
        temploaibenh = tkdsloaibenh.Text
        Dim tempTuNgay As String = ""
        If (Not tkdsTuNgay.SelectedDate Is Nothing) Then
            tempTuNgay = tkdsTuNgay.SelectedDate.Value.ToString("yyyy-MM-dd")
        End If
        Dim tempDenNgay As String = tkdsDenNgay.SelectedDate.Value.ToString("yyyy-MM-dd")
        danhsachbenhnhan.DataContext = BLL_timkiembenhnhantrongdanhsach(temphoten, temploaibenh, tempTuNgay, tempDenNgay)
    End Sub
    'Tải lại danh sách bệnh nhân
    Private Sub refreshDanhSachBenhnhan_Click(sender As Object, e As RoutedEventArgs) Handles refreshDanhSachBenhnhan.Click
        danhsachbenhnhan.DataContext = BLL_LayDanhSachBenhNhan()
        tkdsHoten.Clear()
        tkdsDenNgay.SelectedDate = DateTime.Today
        tkdsloaibenh.SelectedIndex = -1
        tkdsTuNgay.SelectedDate = Nothing
    End Sub
    'Xem thông tin chi tiết bệnh nhân
    Private Sub ds_xemthongtinbenhnhan(sender As Object, e As RoutedEventArgs)
        dsthongtinbenhnhan.Visibility = System.Windows.Visibility.Visible
        Dim row As DataRowView = DirectCast(danhsachbenhnhan.SelectedItem, DataRowView)
        Dim dt As New System.Data.DataTable
        dt = BLL_LayThongTinBenhNhands(row(1).ToString(), row(2).ToString())
        dsHoTen.Text = "Họ Tên : " + dt.Rows(0)(0)
        dsCMND.Text = "Số CMND : " + dt.Rows(0)(1)
        dsGioiTinh.Text = "Giới Tính : " + dt.Rows(0)(2)
        dsSDT.Text = "Di Động : " + dt.Rows(0)(3)
        dsnghenghiep.Text = "Nghề Nghiệp : " + dt.Rows(0)(4)
        dsnamsinh.Text = "Năm Sinh : " + dt.Rows(0)(5)
        dsdiachi.Text = "Địa chỉ : " + dt.Rows(0)(6)
    End Sub
#End Region

#Region "CÁC TÁC VỤ DÀNH CHO TAB BÁO CÁO"
    'Tải báo cáo
    Public Sub BaoCao_Load()
        'dpkbaocaodoanhthutheongay.for()
        'DateTimePickerFormat.Custom()
        ketquadoanhthu.Text = "Kết quả : 0"
        ketquasudung.Text = "Kết quả : 0"
        doanhthu_nam.Text = Date.Today.Year.ToString()
        sudung_nam.Text = Date.Today.Year.ToString()

    End Sub
    'Tải báo cáo doanh thu theo ngày
    Private Sub loaddanhthutheongay(sender As Object, e As RoutedEventArgs) Handles loadbaocaodoanhthutheongay.Click
        Dim thang As String = doanhthu_thang.Text
        thang = thang.Substring(thang.Length - 1)
        Dim nam As String = doanhthu_nam.Text
        Dim dt As System.Data.DataTable
        dt = BLL_BaoCaoDoanhThu(thang, nam)
        baocaodoanhthutheongay.DataContext = dt
        ketquadoanhthu.Text = "Kết quả : " + dt.Rows.Count.ToString()

    End Sub
    'Tải báo cáo doanh sử dụng thuốc
    Private Sub loadbaocaosudungthuoc_Click(sender As Object, e As RoutedEventArgs) Handles loadbaocaosudungthuoc.Click
        Dim thang As String = sudung_thang.Text()
        thang = thang.Substring(thang.Length - 1)
        Dim nam As String = sudung_nam.Text
        Dim dt As System.Data.DataTable
        dt = BLL_BaoCaoSuDungThuoc(thang, nam)
        baocaosudungthuoc.DataContext = dt
        ketquasudung.Text = "Kết quả : " + dt.Rows.Count.ToString()

    End Sub
    'Xuất excel báo cáo sử dụng thuốc
    Private Async Sub xuatexcel_sudungthuoc_Click(sender As Object, e As RoutedEventArgs) Handles xuatexcel_sudungthuoc.Click

        Dim f As FolderBrowserDialog = New FolderBrowserDialog
        Try
            If f.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                Dim controller = Await Me.ShowProgressAsync("Please wait...", "Progress message")
                controller.SetIndeterminate()
                'This section help you if your language is not English.
                Await Task.Run(Sub() fun1(f))

                MessageBox.Show("Export done successfully!")
                Await controller.CloseAsync()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK)
        End Try
    End Sub
    Private Sub fun1(f As FolderBrowserDialog)
        Dim datatableMain As New System.Data.DataTable()
        System.Threading.Thread.CurrentThread.CurrentCulture = _
                System.Globalization.CultureInfo.CreateSpecificCulture("en-US")
        Dim oExcel As Excel.Application
        Dim oBook As Excel.Workbook
        Dim oSheet As Excel.Worksheet
        oExcel = CreateObject("Excel.Application")
        oBook = oExcel.Workbooks.Add(Type.Missing)
        oSheet = oBook.Worksheets(1)

        Dim dc As System.Data.DataColumn
        Dim dr As System.Data.DataRow
        Dim colIndex As Integer = 0
        Dim rowIndex As Integer = 0

        Me.Dispatcher.Invoke(Function()
                                 datatableMain = BLL_BaoCaoSuDungThuoc(doanhthu_thang.Text.Substring(sudung_thang.Text.Length - 1), doanhthu_nam.Text)

                             End Function)

        'Export the Columns to excel file
        For Each dc In datatableMain.Columns
            colIndex = colIndex + 1
            oSheet.Cells(1, colIndex) = dc.ColumnName
        Next

        'Export the rows to excel file
        For Each dr In datatableMain.Rows
            rowIndex = rowIndex + 1
            colIndex = 0
            For Each dc In datatableMain.Columns
                colIndex = colIndex + 1
                oSheet.Cells(rowIndex + 1, colIndex) = dr(dc.ColumnName)
            Next
        Next
        Dim fileName As String
        'Set final path
        Me.Dispatcher.Invoke(Function()
                                 fileName = "\BaoCaoSuDungThuoc_Thang" + doanhthu_thang.Text.Substring(sudung_thang.Text.Length - 1) + "-Nam" + doanhthu_nam.Text + ".xls"
                             End Function)
        Dim finalPath = f.SelectedPath + fileName
        oSheet.Columns.AutoFit()
        'Save file in final path
        oBook.SaveAs(finalPath, XlFileFormat.xlWorkbookNormal, Type.Missing, _
        Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive, _
        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing)

        'Release the objects
        ReleaseObject(oSheet)
        oBook.Close(False, Type.Missing, Type.Missing)
        ReleaseObject(oBook)
        oExcel.Quit()
        ReleaseObject(oExcel)
        'Some time Office application does not quit after automation: 
        'so i am calling GC.Collect method.
        GC.Collect()
    End Sub
    'XUất excel báo cáo doanh thu
    Private Async Sub xuatexcel_doanhthu_Click(sender As Object, e As RoutedEventArgs) Handles xuatexcel_doanhthu.Click
        Dim f As FolderBrowserDialog = New FolderBrowserDialog
        Try
            If f.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                Dim controller = Await Me.ShowProgressAsync("Please wait...", "Progress message")
                controller.SetIndeterminate()
                'This section help you if your language is not English.
                Await Task.Run(Sub() fun2(f))

                MessageBox.Show("Export done successfully!")
                Await controller.CloseAsync()
                'This section help you if your language is not English.
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub fun2(f As FolderBrowserDialog)
        Dim datatableMain As New System.Data.DataTable()
        'This section help you if your language is not English.
        System.Threading.Thread.CurrentThread.CurrentCulture = _
        System.Globalization.CultureInfo.CreateSpecificCulture("en-US")
        Dim oExcel As Excel.Application
        Dim oBook As Excel.Workbook
        Dim oSheet As Excel.Worksheet
        oExcel = CreateObject("Excel.Application")
        oBook = oExcel.Workbooks.Add(Type.Missing)
        oSheet = oBook.Worksheets(1)

        Dim dc As System.Data.DataColumn
        Dim dr As System.Data.DataRow
        Dim colIndex As Integer = 0
        Dim rowIndex As Integer = 0

        Me.Dispatcher.Invoke(Function()
                                 datatableMain = BLL_BaoCaoDoanhThu(doanhthu_thang.Text.Substring(sudung_thang.Text.Length - 1), doanhthu_nam.Text)

                             End Function)

        'Export the Columns to excel file
        For Each dc In datatableMain.Columns
            colIndex = colIndex + 1
            oSheet.Cells(1, colIndex) = dc.ColumnName
        Next

        'Export the rows to excel file
        For Each dr In datatableMain.Rows
            rowIndex = rowIndex + 1
            colIndex = 0
            For Each dc In datatableMain.Columns
                colIndex = colIndex + 1
                oSheet.Cells(rowIndex + 1, colIndex) = dr(dc.ColumnName)
            Next
        Next
        Dim fileName As String
        'Set final path
        Me.Dispatcher.Invoke(Function()
                                 fileName = "\BaoCaoDoanhThuTheoNgay_Thang" + doanhthu_thang.Text.Substring(sudung_thang.Text.Length - 1) + "-Nam" + doanhthu_nam.Text + ".xls"
                             End Function)
        Dim finalPath = f.SelectedPath + fileName
        oSheet.Columns.AutoFit()
        'Save file in final path
        oBook.SaveAs(finalPath, XlFileFormat.xlWorkbookNormal, Type.Missing, _
        Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive, _
        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing)

        'Release the objects
        ReleaseObject(oSheet)
        oBook.Close(False, Type.Missing, Type.Missing)
        ReleaseObject(oBook)
        oExcel.Quit()
        ReleaseObject(oExcel)
        'Some time Office application does not quit after automation: 
        'so i am calling GC.Collect method.
        GC.Collect()
    End Sub
#End Region
#Region "CÁC TÁC VỤ DÀNH CHO TAB QUI ĐỊNH"
    'Thay đổi qui định
    Private Sub thaydoiquidinh_Click(sender As Object, e As RoutedEventArgs) Handles thaydoiquidinh.Click
        Dim _soluongbenhnhankham As String = soluongbenhnhankham.Text
        Dim _soluongloaibenh As String = soluongloaibenh.Text
        Dim _soluongthuoc As String = soluongthuoc.Text
        Dim _soluongdonvi As String = soluongdonvi.Text
        Dim _soluongcachdung As String = soluongcachdung.Text
        If (BLL_chinhsuaquidinh(_soluongbenhnhankham, _soluongloaibenh, _soluongthuoc, _soluongdonvi, _soluongcachdung) = 1) Then
            MessageBox.Show("Thay đổi thành công")
            quanli_soluongbenhnhankhamtoida.Text = "/ " + _soluongbenhnhankham
            QuiDinh_Load()
        Else
            MessageBox.Show("Thay đổi thất bại")
        End If
    End Sub
    'Tải qui định
    Private Sub QuiDinh_Load()
        Dim dt As New System.Data.DataTable
        dt = BLL_laythamso()
        If (dt.Rows.Count > 0) Then
            soluongbenhnhankham.Text = dt.Rows(0)(0)
            soluongloaibenh.Text = dt.Rows(0)(1)
            soluongthuoc.Text = dt.Rows(0)(2)
            soluongdonvi.Text = dt.Rows(0)(3)
            soluongcachdung.Text = dt.Rows(0)(4)
        End If
    End Sub
    'Phục hồi qui định
    Private Sub refreshquidinh_Click(sender As Object, e As RoutedEventArgs) Handles refreshquidinh.Click
        QuiDinh_Load()
    End Sub
#End Region
End Class
