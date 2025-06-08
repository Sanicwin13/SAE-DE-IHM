
Module FonctionUtile
    Public Sub RemplirComboBox(cmb As ComboBox)
        Dim joueursList As List(Of JoueurStat.StatJoueur) = JoueurStat.Recup
        cmb.Items.Clear()
        For Each j As StatJoueur In joueursList
            cmb.Items.Add(j.Tout.NomJ)
        Next
    End Sub
    Public ModeHard As Boolean = False
    Public ThemeIsaac As Boolean = False
    Public Sub VerifTheme(nom As Form)
        Dim joueursList As List(Of JoueurStat.StatJoueur) = JoueurStat.Recup
        joueursList = Recup()
        Dim joueur As String = frmAccueil.ComboBox1.Text
        Dim stats As StatJoueur = joueursList.Find(Function(j) j.Tout.NomJ.Equals(joueur, StringComparison.OrdinalIgnoreCase))
        If stats.Tout.theme = False Then
            frmJeu.Show()
            nom.Hide()
        Else
            ChoixTheme.Show()
            nom.Hide()
        End If
    End Sub


    Public Sub VerifierFermeture(e As FormClosingEventArgs)
        If e.CloseReason = CloseReason.UserClosing Then
            frmAccueil.Show()
        End If
    End Sub

End Module
