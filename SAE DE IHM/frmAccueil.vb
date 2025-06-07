Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class frmAccueil
    Private joueursList As List(Of JoueurStat.StatJoueur)
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles btnQuitter.Click

        If MsgBox("Veux tu quitter ? ", vbYesNo) = vbYes Then

            Me.Close()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnJouer.Click
        If ComboBox1.Text.Length < 3 Then
            MsgBox("Tu n'as pas assez de lettres")
        Else
            joueursList = Recup()
            Dim joueur As String = ComboBox1.Text
            Dim stats As StatJoueur = joueursList.Find(Function(j) j.Tout.NomJ.Equals(joueur, StringComparison.OrdinalIgnoreCase))
            If stats.Tout.Hard = False Then
                frmJeu.Show()
                Me.Hide()
            Else
                ChoixDiffi.Show()
                Me.Hide()
            End If
        End If
    End Sub

    Private Sub btnScores_Click(sender As Object, e As EventArgs) Handles btnScores.Click
        frmScores.Show()
        Me.Hide()
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Boutique.Show()
        Me.Hide()
    End Sub
    Private Sub frmAccueil_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ActualiserComboBox()
        Me.BackgroundImage = Image.FromFile("images/Poop-emoji-scaled.jpg")
        Me.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Public Sub ActualiserComboBox()
        RemplirComboBox(ComboBox1)
    End Sub

End Class