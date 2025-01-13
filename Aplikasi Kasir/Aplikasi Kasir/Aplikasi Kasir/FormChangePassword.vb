Imports System.Data.Odbc
Public Class FormChangePassword

    Sub Awal()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox1.Enabled = True
        TextBox2.Enabled = False
        TextBox3.Enabled = False
        TextBox1.Focus()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If MsgBox("Sure?", MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "Why?") = DialogResult.Yes Then
            Me.Hide()
        ElseIf TextBox1.Text = "" Then
            TextBox1.Focus()
        End If
    End Sub

    Private Sub FormChangePassword_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Awal()
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            Call Koneksi()
            Cmd = New OdbcCommand("Select * from tbl_admin where kodeadmin='" & FormMenuUtama.STlabel2.Text & "' And passwordadmin='" & TextBox1.Text & "'", Conn)
            Rd = Cmd.ExecuteReader
            Rd.Read()
            If Not Rd.HasRows Then
                MsgBox("Harap Masukkan Password Yang Valid", MsgBoxStyle.Critical, "Error Password")
            Else
                If Rd("passwordadmin") = TextBox1.Text Then
                    TextBox1.Enabled = False
                    TextBox2.Enabled = True
                    TextBox3.Enabled = True
                    TextBox2.Focus()
                Else
                End If
            End If
            Rd.Close()
            Conn.Close()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox2.Text = "" Or TextBox3.Text = "" Then
            MsgBox("Harap Isi Semua Field Terlebih Dahulu", MsgBoxStyle.Critical, "Error Query")
        ElseIf TextBox2.Text <> TextBox3.Text Then
            MsgBox("Password Baru dan Konfirmasi Password Harus Sama!", MsgBoxStyle.Critical, "Error Query")
        ElseIf TextBox2.Text = TextBox1.Text Then
            MsgBox("Password Baru Tidak Boleh Sama dengan Password Lama!", MsgBoxStyle.Critical, "Error Query"
        Else
            Call Koneksi()
            Dim SimpanData As String = "Update tbl_admin set passwordadmin='" & TextBox2.Text & "' where kodeadmin='" & FormMenuUtama.STlabel2.Text & "' "
            Cmd = New OdbcCommand(SimpanData, Conn)
            Cmd.ExecuteNonQuery()
            MsgBox("Update Password Berhasil!", MsgBoxStyle.Information, "Query Success")
            Call Awal()
        End If
    End Sub
End Class