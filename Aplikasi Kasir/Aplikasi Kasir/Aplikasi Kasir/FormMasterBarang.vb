Imports System.Data.Odbc
Public Class FormMasterBarang

    Sub Awal()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        ComboBox1.Items.Clear()
        ComboBox1.Text = ""
        TextBox1.Enabled = False
        TextBox2.Enabled = False
        TextBox3.Enabled = False
        TextBox4.Enabled = False
        ComboBox1.Enabled = False

        Button1.Text = "Input"
        Button2.Text = "Edit"
        Button3.Text = "Hapus"
        Button4.Text = "Tutup"
        Button1.Enabled = True
        Button2.Enabled = True
        Button3.Enabled = True
        Button4.Enabled = True

        Call Koneksi()
        Da = New OdbcDataAdapter("Select kodebarang, namabarang, hargabarang, jumlahbarang, satuanbarang from tbl_barang", Conn)
        Ds = New DataSet
        Da.Fill(Ds, "tbl_barang")
        DataGridView1.DataSource = Ds.Tables("tbl_barang")
        Conn.Close()
    End Sub

    Sub Siap()
        TextBox1.Enabled = True
        TextBox2.Enabled = True
        TextBox3.Enabled = True
        TextBox4.Enabled = True
        ComboBox1.Enabled = True
        Call MunculSatuan()
    End Sub

    Sub MunculSatuan()
        Call Koneksi()
        Cmd = New OdbcCommand("select distinct satuanbarang from tbl_barang", Conn)
        Rd = Cmd.ExecuteReader
        ComboBox1.Items.Clear()
        Do While Rd.Read
            ComboBox1.Items.Add(Rd.Item("satuanbarang"))
        Loop
    End Sub

    Sub NomorUrut()
        Call Koneksi()
        Cmd = New OdbcCommand("Select * from tbl_barang where kodebarang in(select max(kodebarang) from tbl_barang)", Conn)
        Dim UrutanKode As String
        Dim Hitung As Long
        Rd = Cmd.ExecuteReader
        Rd.Read()
        If Not Rd.HasRows Then
            UrutanKode = "BRG" + "001"
        Else
            Hitung = Microsoft.VisualBasic.Right(Rd.GetString(0), 3) + 1
            UrutanKode = "BRG" + Microsoft.VisualBasic.Right("000" & Hitung, 3)
        End If
        TextBox1.Text = UrutanKode
    End Sub
    Private Sub FormMasterBarang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call Awal()
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            Call Koneksi()
            Cmd = New OdbcCommand("Select * from tbl_barang where kodebarang='" & TextBox1.Text & "'", Conn)
            Rd = Cmd.ExecuteReader
            Rd.Read()
            If Rd.HasRows Then
                TextBox1.Text = Rd!kodebarang
                TextBox2.Text = Rd!namabarang
                TextBox3.Text = Rd!hargabarang
                TextBox4.Text = Rd!jumlahbarang
                ComboBox1.Text = Rd!satuanbarang
            Else
                MsgBox("Kode Tidak Ditemukan!", MsgBoxStyle.Information, "Information")
                TextBox1.Clear()
            End If
        End If
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Button1.Text = "Input" Then
            Button1.Text = "Simpan"
            Button2.Enabled = False
            Button3.Enabled = False
            Button4.Text = "Batal"
            Call Siap()
            Call NomorUrut()
            TextBox1.Enabled = False
            TextBox2.Focus()
        Else
            If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or ComboBox1.Text = "" Then
                MsgBox("Harap Isi Terlebih Dahulu!", MsgBoxStyle.Critical, "Warning")
            Else
                Call Koneksi()
                Dim InputData As String = "insert into tbl_barang values('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "','" & ComboBox1.Text & "')"
                Cmd = New OdbcCommand(InputData, Conn)
                Cmd.ExecuteNonQuery()
                MsgBox("Input Data Berhasil!", MsgBoxStyle.Information, "Information")
                Call Awal()
            End If
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Button2.Text = "Edit" Then
            Button2.Text = "Simpan"
            Button1.Enabled = False
            Button3.Enabled = False
            Button4.Text = "Batal"
            Call Siap()
        Else
            If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Then
                MsgBox("Harap Isi Terlebih Dahulu!", MsgBoxStyle.Critical, "Warning")
            Else
                Call Koneksi()
                Dim EditData As String = "Update tbl_barang set namabarang='" & TextBox2.Text & "',hargabarang='" & TextBox3.Text & "',jumlahbarang='" & TextBox4.Text & "',satuanbarang='" & ComboBox1.Text & "' where kodebarang='" & TextBox1.Text & "'"
                Cmd = New OdbcCommand(EditData, Conn)
                Cmd.ExecuteNonQuery()
                MsgBox("Update Data Berhasil!", MsgBoxStyle.Information, "Information")
                Call Awal()
            End If
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Button3.Text = "Hapus" Then
            Button3.Text = "Delete"
            Button1.Enabled = False
            Button2.Enabled = False
            Button4.Text = "Batal"
            Call Siap()
        Else
            If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Then
                MsgBox("Harap Isi Terlebih Dahulu!", MsgBoxStyle.Critical, "Warning")
            Else
                Call Koneksi()
                Dim HapusData As String = "Delete from tbl_barang where kodebarang='" & TextBox1.Text & "'"
                Cmd = New OdbcCommand(HapusData, Conn)
                Cmd.ExecuteNonQuery()
                MsgBox("Hapus Data Berhasil!", MsgBoxStyle.Information, "Information")
                Call Awal()
            End If
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If Button4.Text = "Tutup" Then
            Me.Hide()
        Else
            Call Awal()
        End If
    End Sub

End Class