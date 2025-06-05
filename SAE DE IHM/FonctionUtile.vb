
Module FonctionUtile
    Public Sub RemplirComboBox(cmb As ComboBox)
        Dim joueursList As List(Of JoueurStat.StatJoueur) = JoueurStat.Recup
        cmb.Items.Clear()
        For Each j In joueursList
            cmb.Items.Add(j.Tout.NomJ)
        Next
    End Sub
End Module
