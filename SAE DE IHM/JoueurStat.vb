Imports System.Windows.Forms.AxHost
Imports System.IO
Public Module JoueurStat

    Public Structure StatJoueur
        Private _nomJ As String
        Private _NbrMaxCarre As Integer
        Private _TempsMinTrv As Integer
        Private _NbrPartieJouer As Integer
        Private _TempsTotal As Integer
        Private _Monnaie As Integer
        Private themeIsaac As Boolean
        Private _Hard As Boolean
        Public Sub New(nomJ As String, nbrMaxCarre As Integer, tempsMinTrv As Integer, nbrPartieJouer As Integer, tempsTotal As Integer, Monnaie As Integer, theme As Boolean, Hard As Boolean)
            _nomJ = nomJ
            _NbrMaxCarre = nbrMaxCarre
            _TempsMinTrv = tempsMinTrv
            _NbrPartieJouer = nbrPartieJouer
            _TempsTotal = tempsTotal
            _Monnaie = Monnaie
            themeIsaac = theme
            _Hard = Hard
        End Sub
        Public ReadOnly Property Tout As (NomJ As String, NbrMaxCarre As Integer, TempsMinTrv As Integer, NbrPartieJouer As Integer, TempsTotal As Integer, Monnaie As Integer, theme As Boolean, Hard As Boolean)
            Get
                Return (_nomJ, _NbrMaxCarre, _TempsMinTrv, _NbrPartieJouer, _TempsTotal, _Monnaie, themeIsaac, _Hard)
            End Get
        End Property


    End Structure
    Public carreTrv As Integer = 0
    Public TempsDernCarreTrv As Integer = Integer.MaxValue

    Private Joueur As New Dictionary(Of String, StatJoueur)

    Public Sub MAJStat(nom As String, carreTrv As Integer, tempsPartie As Integer, TempsDernCarreTrv As Integer)
        Dim stat As StatJoueur

        If Joueur.ContainsKey(nom) Then
            stat = Joueur(nom)
        Else
            stat = New StatJoueur(nom, 0, Integer.MaxValue, 0, 0, 0, False, False)
        End If
        Dim nbrMaxCarre As Integer = stat.Tout.NbrMaxCarre
        Dim tempsMinTrv As Integer = stat.Tout.TempsMinTrv
        Dim nbrPartieJouer As Integer = stat.Tout.NbrPartieJouer
        Dim tempsTotal As Integer = stat.Tout.TempsTotal
        Dim Monnaie As Integer = stat.Tout.Monnaie
        Dim theme As Boolean = stat.Tout.theme
        Dim Hard As Boolean = stat.Tout.Hard

        If carreTrv > nbrMaxCarre Then
            nbrMaxCarre = carreTrv
            tempsMinTrv = TempsDernCarreTrv
        ElseIf carreTrv = nbrMaxCarre And TempsDernCarreTrv < tempsMinTrv Then
            tempsMinTrv = TempsDernCarreTrv
        End If

        nbrPartieJouer += 1
        tempsTotal += tempsPartie
        Monnaie += carreTrv * 30
        If carreTrv = 5 Then
            If ModeHard Then
                Monnaie += (30 - TempsDernCarreTrv) * 2
            End If
            Monnaie += (60 - TempsDernCarreTrv) * 2
        End If

        stat = New StatJoueur(nom, nbrMaxCarre, tempsMinTrv, nbrPartieJouer, tempsTotal, Monnaie, theme, Hard)

        Joueur(nom) = stat
    End Sub
    Public Sub MAJMonnaie(nom As String, MonnaieDepense As Integer)
        Dim stat As StatJoueur
        If Joueur.ContainsKey(nom) Then
            stat = Joueur(nom)
        Else
            stat = New StatJoueur(nom, 0, Integer.MaxValue, 0, 0, 0, False, False)
        End If
        Dim Monnaie As Integer = stat.Tout.Monnaie
        If Monnaie < MonnaieDepense Then
            MsgBox("Pas assez de monnaie")
        Else
            Monnaie -= MonnaieDepense
        End If
        stat = New StatJoueur(nom, stat.Tout.NbrMaxCarre, stat.Tout.TempsMinTrv, stat.Tout.NbrPartieJouer, stat.Tout.TempsTotal, Monnaie, stat.Tout.theme, stat.Tout.Hard)

        Joueur(nom) = stat
    End Sub
    Public Sub MajHard(nom As String)
        Dim stat As StatJoueur
        stat = Joueur(nom)
        Dim Hard As Boolean = stat.Tout.Hard
        Hard = True
        stat = New StatJoueur(nom, stat.Tout.NbrMaxCarre, stat.Tout.TempsMinTrv, stat.Tout.NbrPartieJouer, stat.Tout.TempsTotal, stat.Tout.Monnaie, stat.Tout.theme, Hard)
        Joueur(nom) = stat

    End Sub
    Public Sub MajThemeIsaac(nom As String)
        Dim stat As StatJoueur
        stat = Joueur(nom)
        Dim Isaac As Boolean = stat.Tout.theme
        Isaac = True
        stat = New StatJoueur(nom, stat.Tout.NbrMaxCarre, stat.Tout.TempsMinTrv, stat.Tout.NbrPartieJouer, stat.Tout.TempsTotal, stat.Tout.Monnaie, Isaac, stat.Tout.Hard)
        Joueur(nom) = stat

    End Sub
    Sub main()
        Application.Run(frmAccueil)
    End Sub

    Public ReadOnly Property Recup() As List(Of StatJoueur)
        Get
            Return Joueur.Values.ToList()
        End Get
    End Property

    Private Const fichierScores As String = "scores.txt"

    Private Function StatToString(stat As StatJoueur) As String
        Return $"{stat.Tout.NomJ};{stat.Tout.NbrMaxCarre};{stat.Tout.TempsMinTrv};{stat.Tout.NbrPartieJouer};{stat.Tout.TempsTotal};{stat.Tout.Monnaie};{stat.Tout.theme};{stat.Tout.Hard}"
    End Function

    Private Function StringToStat(line As String) As StatJoueur
        Dim parts() As String = line.Split(";"c)
        If parts.Length <> 8 Then Throw New Exception("Format invalide")
        Return New StatJoueur(parts(0), Integer.Parse(parts(1)), Integer.Parse(parts(2)), Integer.Parse(parts(3)), Integer.Parse(parts(4)), Integer.Parse(parts(5)), Boolean.Parse(parts(6)), Boolean.Parse(parts(7)))
    End Function

    Public Sub ChargerScores()
        Joueur.Clear()
        If File.Exists(fichierScores) Then
            For Each line As String In File.ReadAllLines(fichierScores)
                Try
                    Dim stat As StatJoueur = StringToStat(line)
                    Joueur(stat.Tout.NomJ) = stat
                Catch ex As Exception
                End Try
            Next
        End If
    End Sub

    Public Sub SauvegarderScores()
        Dim lignes As New List(Of String)
        For Each stat As StatJoueur In Joueur.Values
            lignes.Add(StatToString(stat))
        Next
        File.WriteAllLines(fichierScores, lignes)
    End Sub

End Module
