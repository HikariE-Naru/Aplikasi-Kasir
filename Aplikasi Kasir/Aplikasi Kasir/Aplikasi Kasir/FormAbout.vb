Public Class FormAbout
    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Process.Start("Chrome", "https://www.instagram.com/hikariyusuf28/")
    End Sub
End Class