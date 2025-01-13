Imports System.Data.Odbc
Public Class FormTransJual
    Sub KondisiAwal()
        LBLNamaPlg.Text = ""
        LBLAlamat.Text = ""
        LBLTelepone.Text = ""
        LBLTanggal.Text = Today
        LBLAdmin.Text = FormMenuUtama.STlabel4.Text
        LBLKembali.Text = ""
        TextBox2.Text = ""
        LBLNamaBarang.Text = ""
        LBLHargaBarang.Text = ""
        TextBox3.Text = ""
        TextBox3.Enabled = False
        Call MunculKodePelanggan()
        Call NomorOtomatis()
        Call BuatKolom()
        LBLTotal.Text = ""
        ComboBox1.Text = ""
        TextBox1.Text = ""
        LBLItem.Text = ""
    End Sub

    Sub MunculKodePelanggan()
        ComboBox1.Items.Clear()
        Call Koneksi()
        Cmd = New OdbcCommand("Select * From tbl_pelanggan", Conn)
        Rd = Cmd.ExecuteReader
        Do While Rd.Read
            ComboBox1.Items.Add(Rd.Item(0))
        Loop
        Rd.Close()
    End Sub

    Sub NomorOtomatis()
        Call Koneksi()
        Cmd = New OdbcCommand("SELECT * FROM tbl_jual WHERE nojual IN (SELECT MAX(nojual) FROM tbl_jual)", Conn)
        Dim UrutanKode As String
        Dim Hitung As Long
        Rd = Cmd.ExecuteReader()
        Rd.Read()
        If Not Rd.HasRows Then
            UrutanKode = "J" & Format(Now, "yyMMdd") & "001"
        Else
            Hitung = Microsoft.VisualBasic.Right(Rd.GetString(0), 9) + 1
            UrutanKode = "J" & Format(Now, "yyMMdd") & Microsoft.VisualBasic.Right("000" & Hitung, 3)
        End If
        LBLNoJual.Text = UrutanKode
        Rd.Close()
        Conn.Close()
    End Sub


    Sub BuatKolom()
        DataGridView1.Columns.Clear()
        DataGridView1.Columns.Add("Kode", "Kode")
        DataGridView1.Columns.Add("Nama", "Nama Barang")
        DataGridView1.Columns.Add("Harga", "Harga")
        DataGridView1.Columns.Add("Jumlah", "Jumlah")
        DataGridView1.Columns.Add("Subtotal", "Subtotal")

    End Sub

    Sub RumusSubtotal()
        Dim Hitung As Integer = 0
        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            Hitung = Hitung + DataGridView1.Rows(i).Cells(4).Value
            LBLTotal.Text = Hitung
        Next
    End Sub

    Sub RumusItem()
        Dim HitungItem As Integer = 0
        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            HitungItem = HitungItem + DataGridView1.Rows(i).Cells(3).Value
            LBLItem.Text = HitungItem
        Next
    End Sub

    Private Sub FormTransJual_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call KondisiAwal()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        LBLJam.Text = TimeOfDay
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Call Koneksi()
        Cmd = New OdbcCommand("Select * From tbl_pelanggan where kodepelanggan = '" & ComboBox1.Text & "'", Conn)
        Rd = Cmd.ExecuteReader
        Rd.Read()
        If Rd.HasRows Then
            LBLNamaPlg.Text = Rd!namapelanggan
            LBLAlamat.Text = Rd!alamatpelanggan
            LBLTelepone.Text = Rd!telppelanggan
        End If
        Rd.Close()
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = Chr(13) Then
            Call Koneksi()
            Cmd = New OdbcCommand("Select * From tbl_barang where Kodebarang='" & TextBox2.Text & "'", Conn)
            Rd = Cmd.ExecuteReader
            Rd.Read()
            If Not Rd.HasRows Then
                MsgBox("Barang Kosong!", MsgBoxStyle.Information, "Information!")
            Else
                TextBox2.Text = Rd.Item("Kodebarang")
                LBLNamaBarang.Text = Rd.Item("Namabarang")
                LBLHargaBarang.Text = Rd.Item("Hargabarang")
                LBLJumlahbrg.Text = Rd.Item("Jumlahbarang")
                TextBox3.Enabled = True
            End If
        End If
        Rd.Close()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If LBLNamaBarang.Text = "" Or TextBox3.Text = "" Then
            MsgBox("Harap Masukkan Kode Barang Terlebih Dahulu", MsgBoxStyle.Critical, "Information")
        Else
            If Val(LBLJumlahbrg.Text) < Val(TextBox3.Text) Then
                MsgBox("Stock Kurang", MsgBoxStyle.Information, "Critic!")
            Else
                DataGridView1.Rows.Add(New String() {TextBox2.Text, LBLNamaBarang.Text, LBLHargaBarang.Text, TextBox3.Text, Val(LBLHargaBarang.Text) * Val(TextBox3.Text)})
                Call RumusSubtotal()
                TextBox2.Text = ""
                LBLNamaBarang.Text = ""
                LBLHargaBarang.Text = ""
                TextBox3.Text = ""
                TextBox3.Enabled = False
                Call RumusItem()
            End If
        End If
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            If Val(TextBox1.Text) < Val(LBLTotal.Text) Then
                MsgBox("Uang Anda kurang", MsgBoxStyle.Critical, "Warning!")
            ElseIf Val(TextBox1.Text) = Val(LBLTotal.Text) Then
                LBLKembali.Text = "0"
            ElseIf Val(TextBox1.Text) > Val(LBLTotal.Text) Then
                LBLKembali.Text = Val(TextBox1.Text) - Val(LBLTotal.Text)
                Button1.Focus()
            End If
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If LBLKembali.Text = "" Or LBLNamaPlg.Text = "" Or LBLTotal.Text = "" Then
            MsgBox("Harap Isi Semua Field", MsgBoxStyle.Critical, "Warning")
        Else
            Dim SimpanJual As String = "insert into tbl_jual values ('" & LBLNoJual.Text & "','" & LBLTanggal.Text & "','" & LBLJam.Text & "','" & LBLItem.Text & "','" & LBLTotal.Text & "','" & TextBox1.Text & "','" & LBLKembali.Text & "','" & ComboBox1.Text & "','" & FormMenuUtama.STlabel2.Text & "')"
            Cmd = New OdbcCommand(SimpanJual, Conn)
            Cmd.ExecuteNonQuery()

            For Baris As Integer = 0 To DataGridView1.Rows.Count - 2
                Dim SimpanDetail As String = "insert into tbl_detailjual values('" & LBLNoJual.Text & "','" & DataGridView1.Rows(Baris).Cells(0).Value & "','" & DataGridView1.Rows(Baris).Cells(1).Value & "','" & DataGridView1.Rows(Baris).Cells(2).Value & "','" & DataGridView1.Rows(Baris).Cells(3).Value & "','" & DataGridView1.Rows(Baris).Cells(4).Value & "')"
                Cmd = New OdbcCommand(SimpanDetail, Conn)
                Cmd.ExecuteNonQuery()

                Cmd = New OdbcCommand("select * from tbl_barang where kodebarang='" & DataGridView1.Rows(Baris).Cells(0).Value & "'", Conn)
                Rd = Cmd.ExecuteReader
                Rd.Read()
                Dim KurangiStok As String = "Update tbl_barang set jumlahbarang = '" & Rd.Item("jumlahbarang") - DataGridView1.Rows(Baris).Cells(3).Value & "' where kodebarang= '" & DataGridView1.Rows(Baris).Cells(0).Value & "'"
                Rd.Close()
                Cmd = New OdbcCommand(KurangiStok, Conn)
                Cmd.ExecuteNonQuery()
            Next

            Call KondisiAwal()
            MsgBox("Simpan Data Berhasil", MsgBoxStyle.Information, "Infomation")
        End If
    End Sub
End Class