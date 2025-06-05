Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class frmScores
    Private triDecroissant As Boolean = True

    Private joueursList As List(Of JoueurStat.StatJoueur)
    Private Sub FormStats_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        joueursList = Recup()


        TrierEtAfficher()
    End Sub

    Private Sub TrierEtAfficher()
        If triDecroissant Then
            joueursList = joueursList.OrderByDescending(Function(j) j.Tout.NbrMaxCarre).
                ThenBy(Function(j) j.Tout.TempsMinTrv).ToList()
        Else
            joueursList = joueursList.OrderBy(Function(j) j.Tout.NbrMaxCarre).
                ThenBy(Function(j) j.Tout.TempsMinTrv).ToList()
        End If

        RemplirListBox()
        RemplirComboBox(ComboBox1)
    End Sub
    Private Sub RemplirListBox()
        ListBox1.Items.Clear()
        ListBox2.Items.Clear()
        ListBox3.Items.Clear()
        ListBox4.Items.Clear()
        ListBox5.Items.Clear()

        For Each j In joueursList
            ListBox1.Items.Add(j.Tout.NomJ)
            ListBox2.Items.Add(j.Tout.NbrMaxCarre)
            ListBox3.Items.Add(j.Tout.TempsMinTrv)
            ListBox4.Items.Add(j.Tout.NbrPartieJouer)
            ListBox5.Items.Add(j.Tout.TempsTotal)
        Next
    End Sub

    Private Sub ListBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged, ListBox2.SelectedIndexChanged, ListBox3.SelectedIndexChanged
        Dim lb As ListBox = CType(sender, ListBox)
        Dim index = lb.SelectedIndex
        If index < 0 Then Return

        RemoveHandler ListBox1.SelectedIndexChanged, AddressOf ListBox_SelectedIndexChanged
        RemoveHandler ListBox2.SelectedIndexChanged, AddressOf ListBox_SelectedIndexChanged
        RemoveHandler ListBox3.SelectedIndexChanged, AddressOf ListBox_SelectedIndexChanged
        RemoveHandler ListBox4.SelectedIndexChanged, AddressOf ListBox_SelectedIndexChanged
        RemoveHandler ListBox5.SelectedIndexChanged, AddressOf ListBox_SelectedIndexChanged

        ListBox1.SelectedIndex = index
        ListBox2.SelectedIndex = index
        ListBox3.SelectedIndex = index
        ListBox4.SelectedIndex = index
        ListBox5.SelectedIndex = index

        ComboBox1.Text = joueursList(index).Tout.NomJ

        AddHandler ListBox1.SelectedIndexChanged, AddressOf ListBox_SelectedIndexChanged
        AddHandler ListBox2.SelectedIndexChanged, AddressOf ListBox_SelectedIndexChanged
        AddHandler ListBox3.SelectedIndexChanged, AddressOf ListBox_SelectedIndexChanged
        AddHandler ListBox4.SelectedIndexChanged, AddressOf ListBox_SelectedIndexChanged
        AddHandler ListBox5.SelectedIndexChanged, AddressOf ListBox_SelectedIndexChanged
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        triDecroissant = Not triDecroissant
        TrierEtAfficher()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim nomRecherche = ComboBox1.Text.Trim()
        Dim joueur = joueursList.Find(Function(j) j.Tout.NomJ.Equals(nomRecherche, StringComparison.OrdinalIgnoreCase))
        If joueur.Tout.NomJ IsNot Nothing Then
            MessageBox.Show($"Nom: {joueur.Tout.NomJ}" & vbCrLf &
                            $"Meilleur score: {joueur.Tout.NbrMaxCarre}" & vbCrLf &
                            $"Temps min: {joueur.Tout.TempsMinTrv}" & vbCrLf &
                            $"Parties jouées: {joueur.Tout.NbrPartieJouer}" & vbCrLf &
                            $"Temps total: {joueur.Tout.TempsTotal}",
                            "Statistiques joueur",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information)
        Else
            MessageBox.Show("Joueur non trouvé.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        frmAccueil.Show()
        Me.Close()
    End Sub

    Private Sub ListBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox4.SelectedIndexChanged

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub
End Class