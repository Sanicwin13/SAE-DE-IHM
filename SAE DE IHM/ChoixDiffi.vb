Public Class ChoixDiffi

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        VerifTheme(Me)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ModeHard = True
        VerifTheme(Me)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        frmAccueil.Show()
        Me.Close()
    End Sub


    Private Sub ChoixDiffi_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Private Sub ChoixDiffi_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        VerifierFermeture(e)
    End Sub
End Class