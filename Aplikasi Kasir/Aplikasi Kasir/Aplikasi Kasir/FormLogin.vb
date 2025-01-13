Imports System.Data.Odbc
Public Class FormLogin

    Sub Terbuka()
        FormMenuUtama.LoginToolStripMenuItem.Enabled = False
        FormMenuUtama.LogoutToolStripMenuItem.Enabled = True
        FormMenuUtama.MasterToolStripMenuItem.Enabled = True
        FormMenuUtama.TransaksiToolStripMenuItem.Enabled = True
        FormMenuUtama.LaporanToolStripMenuItem.Enabled = True
        FormMenuUtama.UtilityToolStripMenuItem.Enabled = True
        FormMenuUtama.STlabel2.Text = Rd!kodeadmin
        FormMenuUtama.STlabel4.Text = Rd!namaadmin
        FormMenuUtama.STlabel6.Text = Rd!leveladmin

    End Sub

    Sub Awal()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox1.Focus()
    End Sub

    Sub HakAkses()
        If FormMenuUtama.STlabel6.Text = "Admin" Then
            FormMenuUtama.AdminToolStripMenuItem.Enabled = True
        Else
            FormMenuUtama.AdminToolStripMenuItem.Enabled = False
        End If
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Down Then
            TextBox2.Focus()
        ElseIf e.KeyCode = Keys.Up Then
            Button2.Focus()
        End If
    End Sub
    Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyDown
        TextBox2.UseSystemPasswordChar = True
        If e.KeyCode = Keys.Down Then
            Button1.Focus()
        ElseIf e.KeyCode = Keys.Up Then
            TextBox1.Focus()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Then
            MsgBox("Harap Isi Semua Field!", MsgBoxStyle.Critical, "Warning!")
        Else
            Call Koneksi()
            Cmd = New OdbcCommand("SELECT * FROM tbl_admin WHERE kodeadmin = '" & TextBox1.Text & "'", Conn)
            Rd = Cmd.ExecuteReader()

            If Rd.HasRows Then
                Rd.Read()
                If Rd("passwordadmin").ToString() = TextBox2.Text Then
                    TextBox1.Clear()
                    TextBox2.Clear()
                    Call Terbuka()
                    Call HakAkses()
                    Me.Hide()
                Else
                    MsgBox("Password Salah!", MsgBoxStyle.Critical, "Warning!")
                    TextBox2.Clear()
                    TextBox2.Focus()
                End If
            Else
                MsgBox("Kode Admin Tidak Ditemukan!", MsgBoxStyle.Critical, "Warning!")
                TextBox1.Clear()
                TextBox2.Clear()
                TextBox1.Focus()
            End If

            Rd.Close()
            Conn.Close()
        End If
    End Sub

    Private Sub FormLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Clear()
        TextBox2.Clear()
    End Sub
End Class