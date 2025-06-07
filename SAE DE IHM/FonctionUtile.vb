
Module FonctionUtile
    Public Sub RemplirComboBox(cmb As ComboBox)
        Dim joueursList As List(Of JoueurStat.StatJoueur) = JoueurStat.Recup
        cmb.Items.Clear()
        For Each j As StatJoueur In joueursList
            cmb.Items.Add(j.Tout.NomJ)
        Next
    End Sub
    Public ModeHard As Boolean = False
End Module
