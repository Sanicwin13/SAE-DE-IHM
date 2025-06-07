Imports System.IO
Imports System.Windows.Forms.AxHost

Public Class frmJeu
    Private secondeLeft As Integer = 60
    Private WithEvents timer As Timer = New Timer()
    Private IsActive As Boolean = False
    Dim cartes(19) As PictureBox
    Dim faces As New List(Of String)
    Dim dos As String = "images/ParisC.png"
    Private cartesRetournees As New List(Of PictureBox)
    Private clicsBloques As Boolean = False
    Private joueursList As List(Of JoueurStat.StatJoueur)
    Private Sub btnAbandonner_Click(sender As Object, e As EventArgs) Handles btnAbandonner.Click
        If MsgBox("Veux-tu abandonner ? ", vbYesNo) = vbYes Then
            Me.Close()
            timer.Stop()
            If ModeHard Then
                For i As Integer = 1 To 2
                    MAJStat(frmAccueil.ComboBox1.Text, carreTrv, 30 - secondeLeft, TempsDernCarreTrv)
                Next
            Else
                MAJStat(frmAccueil.ComboBox1.Text, carreTrv, 60 - secondeLeft, TempsDernCarreTrv)
            End If
            ModeHard = False
            frmAccueil.ActualiserComboBox()
            frmAccueil.Show()

        End If

    End Sub

    Private Sub btnPause_Click(sender As Object, e As EventArgs) Handles btnPause.Click
        If IsActive Then

            btnPause.Text = "Pause"
            clicsBloques = False
            IsActive = False
            timer.Start()
        Else
            btnPause.Text = "Reprendre"
            clicsBloques = True
            IsActive = True
            timer.Stop()
        End If
    End Sub

    Private Sub frmJeu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        carreTrv = 0
        TempsDernCarreTrv = 0
        timer.Interval = 1000
        timer.Start()
        lblNom.Text = frmAccueil.ComboBox1.Text
        ' Initialiser les faces
        faces = New List(Of String) From {
        "images/HassineMoungla.jpg", "images/HassineMoungla.jpg", "images/HassineMoungla.jpg", "images/HassineMoungla.jpg",
        "images/LaurentGiustignano.jpg", "images/LaurentGiustignano.jpg", "images/LaurentGiustignano.jpg", "images/LaurentGiustignano.jpg",
        "images/MariaPiaCantel.jpg", "images/MariaPiaCantel.jpg", "images/MariaPiaCantel.jpg", "images/MariaPiaCantel.jpg",
        "images/NinaFouilleul.jpg", "images/NinaFouilleul.jpg", "images/NinaFouilleul.jpg", "images/NinaFouilleul.jpg",
        "images/Ziane.jpg", "images/Ziane.jpg", "images/Ziane.jpg", "images/Ziane.jpg"
    }

        ' Mélanger les faces
        Dim rnd As New Random()
        faces = faces.OrderBy(Function() rnd.Next()).ToList()

        ' Associer les images aux PictureBox
        For i As Integer = 0 To 19
            cartes(i) = CType(Me.Controls("PictureBox" & (i + 1).ToString()), PictureBox)
            cartes(i).Image = Image.FromFile(dos)
            cartes(i).Tag = faces(i) ' on garde le chemin de la face cachée
            AddHandler cartes(i).Click, AddressOf Carte_Click
        Next
        If ModeHard = True Then
            secondeLeft = 30
        End If
    End Sub


    Private Sub lblTemps_Click(sender As Object, e As EventArgs) Handles timer.Tick
        If secondeLeft > 0 And carreTrv < 5 Then
            secondeLeft -= 1
            lblTemps.Text = secondeLeft.ToString
        Else
            lblTemps.Text = "Fin"
            timer.Stop()
            clicsBloques = True
            If ModeHard Then
                For i As Integer = 1 To 2
                    MAJStat(frmAccueil.ComboBox1.Text, carreTrv, 30 - secondeLeft, TempsDernCarreTrv)
                Next
            Else
                MAJStat(frmAccueil.ComboBox1.Text, carreTrv, 60 - secondeLeft, TempsDernCarreTrv)
            End If
            joueursList = JoueurStat.Recup()
            Dim nomJoueur As String = frmAccueil.ComboBox1.Text
            Dim stats As StatJoueur = joueursList.Find(Function(j) j.Tout.NomJ.Equals(nomJoueur, StringComparison.OrdinalIgnoreCase))
            Dim message As String = "Nom : " & nomJoueur & vbCrLf &
                            "Carrés trouvés (max) : " & carreTrv & vbCrLf &
                            "Temps total de jeu : " & TempsDernCarreTrv & " secondes"
            If MsgBox(message) = MsgBoxResult.Yes Then
                frmAccueil.ActualiserComboBox()
                frmAccueil.Show()
                Me.Close()
            End If
        End If
    End Sub

    Private Async Sub Carte_Click(sender As Object, e As EventArgs)
        If clicsBloques Then Exit Sub
        Dim pb As PictureBox = CType(sender, PictureBox)
        If cartesRetournees.Contains(pb) OrElse pb.ImageLocation = pb.Tag.ToString() Then Exit Sub
        pb.Image = Image.FromFile(pb.Tag.ToString())
        cartesRetournees.Add(pb)
        Dim nomFichierRef As String = Path.GetFileName(cartesRetournees(0).Tag.ToString()).ToLower()

        Dim toutesIdentiques As Boolean = cartesRetournees.All(Function(c) Path.GetFileName(c.Tag.ToString()).ToLower() = nomFichierRef)
        If Not toutesIdentiques Then
            clicsBloques = True
            Await Task.Delay(500)
            For Each carte As PictureBox In cartesRetournees
                carte.Image = Image.FromFile(dos)
            Next
            cartesRetournees.Clear()
            clicsBloques = False
        End If
        If cartesRetournees.Count = 4 Then
            For Each carte As PictureBox In cartesRetournees
                carte.Enabled = False
                carte.BackColor = Color.Gray
            Next
            carreTrv += 1
            If ModeHard Then
                TempsDernCarreTrv = 30 - secondeLeft
            End If
            TempsDernCarreTrv = 60 - secondeLeft
            cartesRetournees.Clear()
        End If
    End Sub

End Class





