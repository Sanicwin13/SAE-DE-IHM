Public Class ChoixDiffi

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        frmJeu.Show()
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ModeHard = True
        frmJeu.Show()
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        frmAccueil.Show()
        Me.Close()
    End Sub


    Private Sub ChoixDiffi_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class