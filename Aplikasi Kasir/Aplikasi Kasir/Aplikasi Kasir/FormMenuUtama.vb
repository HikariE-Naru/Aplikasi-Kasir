Imports System.Data.Odbc
Public Class FormMenuUtama

    Sub Terkunci()
        LoginToolStripMenuItem.Enabled = True
        LogoutToolStripMenuItem.Enabled = False
        MasterToolStripMenuItem.Enabled = False
        TransaksiToolStripMenuItem.Enabled = False
        LaporanToolStripMenuItem.Enabled = False
        UtilityToolStripMenuItem.Enabled = False

    End Sub



    Private Sub FormMenuUtama_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call Terkunci()
        STlabel10.Text = Today
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        STlabel8.Text = TimeOfDay
    End Sub

    Private Sub LoginToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoginToolStripMenuItem.Click
        FormLogin.ShowDialog()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        If MsgBox("Sure?", MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "Why?") = DialogResult.Yes Then
            End
        Else
        End If
    End Sub

    Private Sub LogoutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LogoutToolStripMenuItem.Click
        If MsgBox("Sure?", MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "Why?") = DialogResult.Yes Then
            Call Terkunci()
        Else
        End If
    End Sub

    Private Sub AdminToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AdminToolStripMenuItem.Click
        FormMasterAdmin.ShowDialog()
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        FormAbout.ShowDialog()
    End Sub

    Private Sub PelangganToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PelangganToolStripMenuItem.Click
        FormMasterPelanggan.ShowDialog()
    End Sub

    Private Sub BarangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BarangToolStripMenuItem.Click
        FormMasterBarang.ShowDialog()
    End Sub

    Private Sub TransaksiPenjualanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TransaksiPenjualanToolStripMenuItem.Click
        FormTransJual.ShowDialog()
    End Sub

    Private Sub ChangePasswordToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChangePasswordToolStripMenuItem.Click
        FormChangePassword.ShowDialog()
    End Sub
End Class
