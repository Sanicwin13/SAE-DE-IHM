Imports System.Drawing.Imaging
Imports System.Reflection
Imports System.Windows.Forms.VisualStyles

Public Class Boutique
    Private joueursList As List(Of JoueurStat.StatJoueur)
    Dim joueurSelectionne As String = Nothing

    Private Sub Boutique_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RemplirComboBox(ComboBox1)
        joueursList = Recup
        Me.BackgroundImage = Image.FromFile("images/TestShop.png")
        Me.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim Joueurrecherche As String = ComboBox1.Text.Trim()
        Dim joueur = joueursList.Find(Function(j) j.Tout.NomJ.Equals(Joueurrecherche, StringComparison.OrdinalIgnoreCase))
        If joueur.Tout.NomJ IsNot Nothing Then
            joueurSelectionne = joueur.Tout.NomJ
            Label2.Text = joueur.Tout.Monnaie.ToString()
            Label2.Visible = True
            PictureBox2.Visible = True
        Else
            MsgBox("Ce joueur n'existe pas")
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If joueurSelectionne IsNot Nothing Then
            joueursList = JoueurStat.Recup.ToList()
            Dim joueur = joueursList.Find(Function(j) j.Tout.NomJ.Equals(joueurSelectionne, StringComparison.OrdinalIgnoreCase))
            If joueur.Tout.Hard = False Then
                JoueurStat.MAJMonnaie(joueurSelectionne, 1)
                MajHard(joueur.Tout.NomJ)
                MajBoutique(Label2, joueur)
            Else
                MsgBox("Vous possédez déjà le mode hard")
            End If
        Else
            MsgBox("Aucun joueur sélectionné")
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        frmAccueil.Show()
        Me.Close()
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub
    Private Sub MajBoutique(nomL As Label, Joueur As StatJoueur)
        nomL.Text = Joueur.Tout.Monnaie.ToString()
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub
End Class
